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
        height: 'auto',
        width: window.top.alpha.isScreenLower768 ? '100%' : '900px'
    }
};

export const croppedImageId = -1;

export default class CreateProfilePictureModal extends PureComponent {
    constructor(props) {
        super(props);
        $.blockUI();
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
        this.props.setCroppedImage({
            Image: this.state.croppedImageUrl,
            ImageId: croppedImageId
        });
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
        $.unblockUI();
        const { crop } = this.state, { imageSrc, closeModal, resources} = this.props;

        return (
            <Modal
                isOpen={this.props.modalIsOpen}
                onAfterOpen={this.afterOpenModal}
                onRequestClose={this.closeModal}
                style={customStyles}
                contentLabel=""
            >
                {imageSrc && (
                    <div className='crop-image'>
                        <div className='header'>
                            <span className='title'>{resources.UploadFromYourDevice}</span>
                            <span className='closed' onClick={this.props.closeModal}>X</span>
                        </div>
                        <ReactCrop
                            minWidth={240}
                            src={imageSrc}
                            crop={crop}
                            onImageLoaded={this.onImageLoaded}
                            onComplete={this.onCropComplete}
                            onChange={this.onCropChange}
                        />
                        <div className="btn-group">
                            <button className="btn-submit" onClick={this.handleOnSelect}>{resources.Selected}</button>
                            <button className="btn-cancel" onClick={closeModal}>{resources.Cancel}</button>
                        </div>
                    </div>
                )}
            </Modal>
        );
    }
}