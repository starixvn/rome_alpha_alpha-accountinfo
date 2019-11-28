import React, { PureComponent } from "react";
import ReactCrop from "react-image-crop";
import Modal from 'react-modal';
import { croppedImageId } from './profile-picture-helper';
import { cloneDeep } from 'lodash';

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
            height: '700px',
            overflow: 'auto',
        }
};

let style = {
    marginRight: '0px',
    marginLeft: '0px',
    marginTop: '0px',
    marginBottom: '0px',
    position: 'absolute'
};

let cropDefault = {
    x: 180,
    y: 50,
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
        const { srcHeight, srcWidth } = this.props;
        if (this.props.imageSrc !== null && nextProps.imageSrc !== this.props.imageSrc) {
            const aa = $('body');
            const height1 = window.top.alpha.isScreenLower768 ? aa.height() : 700;
            const width1 = window.top.alpha.isScreenLower768 ? aa.width() : 600;

            if (srcHeight > srcWidth) {
                const scale = srcWidth / srcHeight;
                style.height = height1 - 108;
                style.marginTop = 10;
                style.width = width1 * scale;
                style.left = (width1 * (1 - scale)) / 2;
                customStyles.content.height = height1;

                cropDefault.width = 240 * (style.width / srcWidth);
                cropDefault.height = 240 * (style.width / srcWidth);
                this.setState({ crop: cropDefault });
            }
            else {
                const scale01 = srcHeight / srcWidth;
                style.width = width1;
                style.height = width1 * scale01;
                style.marginTop = 10;
                style.left = 0;
                customStyles.content.height = (width1 * scale01) + 100;

                cropDefault.height = 240 * (style.height / srcHeight);
                cropDefault.width = 240 * (style.height / srcHeight);
                this.setState({ crop: cropDefault });
            }

            this.setState({
                iframeHeight: height1,
                iframeWidth: width1,
                imageWidth: srcWidth,
                imageHeight: srcHeight,
                imageWidthScale: style.width,
                imageHeightScale: style.height
            });
        }
    }

    onImageLoaded(image) {
        this.imageRef = image;
    };

    onCropComplete(crop) {
        this.makeClientCrop(crop);
    };

    onCropChange(crop) {
        this.setState({ crop: crop.width < cropDefault.width || crop.height < cropDefault.height || crop.x < 0 || crop.y < 0 ? cropDefault : crop });
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
                            style={style}
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