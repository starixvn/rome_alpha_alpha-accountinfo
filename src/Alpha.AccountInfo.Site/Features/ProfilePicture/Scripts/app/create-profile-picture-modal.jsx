import React, { PureComponent } from "react";
import ReactCrop from "react-image-crop";
import Modal from 'react-modal';
import { croppedImageId } from './profile-picture-helper';

const customStyles = {
    content: window.top.alpha.isScreenLower768 ? {
        top: '0',
        left: '0',
        right: '0',
        bottom: '0',
        border: 'none',
        padding: '0',
        overflow: 'hidden'
    } : {
        top: '50%',
        left: '50%',
        right: 'auto',
        bottom: 'auto',
        padding: '0',
        marginRight: '-50%',
        transform: 'translate(-50%, -50%)',
        width: '600px',
        height: 'auto'
    }
};

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

    render() {
        $.unblockUI();
        const { crop } = this.state, { imageSrc, closeModal, resources, modalIsOpen} = this.props;

        return (
            <Modal
                isOpen={modalIsOpen}
                onRequestClose={closeModal}
                style={customStyles}
                contentLabel=""
            >
                {imageSrc && (
                    <div className='crop-image select-modal'>
                        <div className='header'>
                            <span className='title'>{resources.UploadFromYourDevice}</span>
                            <span className='icon-close' onClick={this.props.closeModal}></span>
                        </div>
                        <div className="btn-group">
                            <button className="btn-select" onClick={this.handleOnSelect}>{resources.Selected}</button>
                            <button className='btn-cancel' onClick={this.handleOnSelect}>Cancel</button> 
                        </div>
                        <ReactCrop
                            minWidth={240}
                            src={imageSrc}
                            crop={crop}
                            onImageLoaded={this.onImageLoaded}
                            onComplete={this.onCropComplete}
                            onChange={this.onCropChange}
                        />
                    </div>
                )}
            </Modal>
        );
    }
}