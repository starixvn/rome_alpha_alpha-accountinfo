import React, { PureComponent } from "react";
import ReactCrop from "react-image-crop";
import Modal from 'react-modal';
import { croppedImageId } from './profile-picture-helper';
import { cloneDeep } from 'lodash';

const isDesktop = !window.top.alpha.isScreenLower768,
    totalHeightOfTitleAndButtonOnPopup = 108,
    profilePictureWidthDefault=240,
    popupStyleMobile = {
        top: '0',
        left: '0',
        right: '0',
        bottom: '0',
        border: 'none',
        padding: '0',
        overflow: 'hidden'
    },
    popupStyleDesktop = {
        top: '50%',
        left: '50%',
        right: 'auto',
        bottom: 'auto',
        padding: '0',
        marginRight: '-50%',
        transform: 'translate(-50%, -50%)',
        width: '600px',
        height: '710px',
        overflow: 'hidden',
    },
    cropDefault = {
        x: 0,
        y: 0,
        aspect: 1,
        width: 240,
        height: 240
    },
    popupStyles = { content: isDesktop ? popupStyleDesktop : popupStyleMobile };

export default class CreateProfilePictureModal extends PureComponent {
    constructor(props) {
        super(props);
        $.blockUI();
        this.state = {
            croppedImageUrl: null,
            crop: cropDefault,
            imageWrapperStyle: {},
            popupStyles: popupStyles
        };

        this.onImageLoaded = this.onImageLoaded.bind(this);
        this.onCropComplete = this.onCropComplete.bind(this);
        this.onCropChange = this.onCropChange.bind(this);
        this.handleOnSelect = this.handleOnSelect.bind(this);
    }

    componentWillUpdate(nextProps) {
        if (this.props.imageSrc !== nextProps.imageSrc) {
            this.setState({ crop: cropDefault, imageWrapperStyle: {}, popupStyles: popupStyles });
        }
    }

    onImageLoaded(image) {
        this.imageRef = image;
        var imgDimension = this.getContainedSize(image);

        if (isDesktop && imgDimension.width > imgDimension.height) {
            let customStyles = cloneDeep(popupStyles);
            customStyles.content.height = imgDimension.height + totalHeightOfTitleAndButtonOnPopup;
            this.setState({ popupStyles: customStyles });
        }

        this.setState({
            imageWrapperStyle: {
                width: imgDimension.width,
                height: imgDimension.height
            }
        });
    };

    onCropComplete(crop) {
        this.makeClientCrop(crop);
    };

    onCropChange(crop) {
        const invalidCrop = crop.width < profilePictureWidthDefault || crop.height < profilePictureWidthDefault || crop.x < 0 || crop.y < 0;
        this.setState({
            crop: invalidCrop? cropDefault : crop
        });
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
        const ratio = img.naturalWidth / img.naturalHeight;

        let height = img.height < img.naturalHeight ? img.height : img.naturalHeight ;
        let width = height * ratio;

        let widthTemp = img.width < img.naturalWidth ? img.width : img.naturalWidth;
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
        const { crop, imageWrapperStyle, popupStyles } = this.state, { imageSrc, closeModal, resources, modalIsOpen } = this.props;

        return (
            <Modal
                isOpen={modalIsOpen}
                onRequestClose={closeModal}
                style={popupStyles}
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
                        <div className="img-container">
                            <ReactCrop
                                minWidth={profilePictureWidthDefault}
                                src={imageSrc}
                                crop={crop}
                                style={imageWrapperStyle}
                                onImageLoaded={this.onImageLoaded}
                                onComplete={this.onCropComplete}
                                onChange={this.onCropChange}
                                />
                        </div>
                    </div>
                )}
            </Modal>
        );
    }
}