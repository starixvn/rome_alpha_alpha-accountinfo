import React, { PureComponent } from "react";
import ReactCrop from "react-image-crop";
import Modal from 'react-modal';
import { croppedImageId } from './profile-picture-helper';

let customStyles = {
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
            height: '710px',
            overflow: 'auto',
        }
};

let style = {};

const cropDefault = {
    x: 0,
    y: 0,
    aspect: 1,
    width: 240,
    height: 240
};

export default class CreateProfilePictureModal extends PureComponent {
    constructor(props) {
        super(props);
        $.blockUI();
        this.state = {
            croppedImageUrl: null,
            crop: cropDefault,
            iframeHeight: 0,
            iframeWidth: 0,
            imageHeight: 0,
            imageWidth: 0,
            imageWidthScale: 0,
            imageHeightScale: 0
        };

        this.onImageLoaded = this.onImageLoaded.bind(this);
        this.onCropComplete = this.onCropComplete.bind(this);
        this.onCropChange = this.onCropChange.bind(this);
        this.handleOnSelect = this.handleOnSelect.bind(this);
    }

    componentWillUpdate(nextProps) {
        if (this.props.imageSrc !== nextProps.imageSrc) {
            this.setState({ crop: cropDefault });
            style = {};

            if (!window.top.alpha.isScreenLower768) {
                customStyles.content.height = 710;
            }
        }
    }

    onImageLoaded(image) {
        this.imageRef = image;
        var imgDimension = this.getContainedSize(image);

        style.width = imgDimension.width;
        style.height = imgDimension.height;

        if (!window.top.alpha.isScreenLower768 && style.width > style.height) {
            customStyles.content.height = style.height + 108;
        }

        this.setState({
            imageWidthScale: imgDimension.width,
            imageHeightScale: imgDimension.height
        });
    };

    onCropComplete(crop) {
        this.makeClientCrop(crop);
    };

    onCropChange(crop) {
        this.setState({ crop: crop.width < 240 || crop.height < 240 || crop.x < 0 || crop.y < 0 ? cropDefault : crop });
    };

    handleOnSelect() {
        const { setCroppedImage, closeModal, resetMessageContainer } = this.props;
        setCroppedImage({
            Image: this.state.croppedImageUrl,
            ImageId: croppedImageId
        });

        closeModal();
        resetMessageContainer();
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

    getContainedSize(img) {
        var ratio = img.naturalWidth / img.naturalHeight;
        var heightTemp = img.height < img.naturalHeight ? img.height : img.naturalHeight ;
        var width = heightTemp * ratio;
        var height = heightTemp;

        var widthTemp = img.width < img.naturalWidth ? img.width : img.naturalWidth;
        if (width > widthTemp) {
            width = widthTemp;
            height = widthTemp / ratio;
        }  

        return { width, height };
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
        const { crop } = this.state, { imageSrc, closeModal, resources, modalIsOpen } = this.props;

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
                            <span className='icon-close' onClick={closeModal}></span>
                        </div>
                        <div className="btn-group">
                            <button className="btn-select" onClick={this.handleOnSelect}>{resources.Selected}</button>
                            <button className='btn-cancel' onClick={closeModal}>Cancel</button>
                        </div>
                        <ReactCrop
                            minWidth={240}
                            src={imageSrc}
                            crop={crop}
                            style={style}
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