import React, { PureComponent } from "react";
import Modal from 'react-modal';
import { handleError, requests } from '../../../Shared/Scripts/api-client';
import { urls } from './profile-picture-helper';
import { isEmpty } from 'lodash';

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
        height: '710px'
    }
};

let tapped = false;

export default class PictureLibraryModal extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            croppedImage: this.props.pictureLibrary[0]
        };

        this.handleOnClick = this.handleOnClick.bind(this);
        this.handleOnSelect = this.handleOnSelect.bind(this);
    }

    handleOnClick(image) {
        if (!tapped) {
            this.setState({ croppedImage: image });
            tapped = setTimeout(function () {
                tapped = null;
            }, 300);
        } else {
            clearTimeout(tapped);
            tapped = null;
            this.handleOnSelect();
        }
    }

    handleOnSelect() {
        const { croppedImage } = this.state;
        this.props.setCroppedImage(croppedImage);
        this.props.closeModal();
    }

    render() {
        const { croppedImage } = this.state, { resources, closeModal, modalIsOpen, pictureLibrary } = this.props;
        return (
            <Modal
                isOpen={modalIsOpen}
                onRequestClose={closeModal}
                style={customStyles}
                contentLabel=""
            >
                <div className="picture-library select-modal">
                    <div className="header">
                        <span className="title">Profile picture gallery</span>
                        <span className="icon-close" onClick={closeModal}></span>
                    </div>
                    <div className="btn-group">
                        <button className='btn-select' onClick={this.handleOnSelect}>{resources.Selected}</button>
                        <button className='btn-cancel' onClick={closeModal}>Cancel</button> 
                    </div>
                    <div className="library">
                        {
                           pictureLibrary.map((profilePicture, index) =>
                                <img className={`image-item ${croppedImage.ImageId === profilePicture.ImageId ? 'selected-image' : ''}`}
                                    alt="Crop"
                                    src={profilePicture.Image}
                                    onClick={() => this.handleOnClick(profilePicture)}
                                    onTouchStart={() => this.handleOnClick(profilePicture)}
                                    key={index} />)
                        }
                    </div>
                </div>
            </Modal>
        );
    }
}