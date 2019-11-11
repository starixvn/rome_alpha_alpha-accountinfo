import React, { PureComponent } from "react";
import ReactCrop from "react-image-crop";
import Modal from 'react-modal';

const customStyles = {
    content: {
        top: '50%',
        left: '50%',
        right: 'auto',
        bottom: 'auto',
        marginRight: '-50%',
        transform: 'translate(-50%, -50%)',
        height: '80%',
        width: '70%'
    }
};

Modal.setAppElement('#root');
class CreateProfilePictureModal extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            croppedImageUrl: null,
            crop: {
                unit: 'px',
                x: 130,
                y: 50,
                aspect: 1,
                width: 240,
                height: 240
            }
        };
        
        this.afterOpenModal = this.afterOpenModal.bind(this);

        this.onImageLoaded = this.onImageLoaded.bind(this);
        this.onCropComplete = this.onCropComplete.bind(this);
        this.onCropChange = this.onCropChange.bind(this);

        this.handleOnSelect = this.handleOnSelect.bind(this);
    }

    onImageLoaded(image) {
        this.imageRef = image;
    };

    onCropComplete(crop) {
        this.makeClientCrop(crop);
    };

    onCropChange(crop) {
        this.setState({ crop });
    };

    handleOnSelect() {
        this.props.setCroppedImage(this.state.croppedImageUrl);
        this.props.closeModal();
    }

    makeClientCrop(crop) {
        if (this.imageRef && crop.width && crop.height) {
            const croppedImageUrl = this.getCroppedImg(
                this.imageRef,
                crop,
                "newFile.Jpeg"
            );

            this.setState({ croppedImageUrl });
        }
    }

    getCroppedImg(image, crop) {
        const canvas = document.createElement("canvas");
        const scaleX = image.naturalWidth / image.width;
        const scaleY = image.naturalHeight / image.height;
        canvas.width = crop.width;
        canvas.height = crop.height;
        const ctx = canvas.getContext("2d");

        ctx.drawImage(
            image,
            crop.x * scaleX,
            crop.y * scaleY,
            crop.width * scaleX,
            crop.height * scaleY,
            0,
            0,
            crop.width,
            crop.height
        );

        return canvas.toDataURL();
    }

    afterOpenModal() {
        // references are now sync'd and can be accessed.
        //this.subtitle.style.color = '#f00';
    }

    render() {
        const { crop } = this.state, { imageSrc, closeModal} = this.props;

        return (
            <Modal
                isOpen={this.props.modalIsOpen}
                onAfterOpen={this.afterOpenModal}
                onRequestClose={this.closeModal}
                style={customStyles}
                contentLabel="Example Modal"
            >
                {imageSrc && (
                    <div className='crop-image'>
                        <ReactCrop
                            minWidth={240}
                            src={imageSrc}
                            crop={crop}
                            onImageLoaded={this.onImageLoaded}
                            onComplete={this.onCropComplete}
                            onChange={this.onCropChange}
                        />
                        <div className="btn-group">
                            <button className="btn-submit" onClick={this.handleOnSelect}>OK</button>
                            <button className="btn-reset" onClick={closeModal}>Cancel</button>
                        </div>
                    </div>
                )}
            </Modal>
        );
    }
}

export default CreateProfilePictureModal;