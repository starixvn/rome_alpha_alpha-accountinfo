import React, { PureComponent } from "react";
import Modal from 'react-modal';
import { handleError, requests } from '../../../Shared/Scripts/api-client';
import { urls } from './profile-picture-helper';

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

export default class PictureLibraryModal extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            croppedImage: undefined,
            imagesList: []
        };

        this.handleOnClick = this.handleOnClick.bind(this);
        this.handleOnSelect = this.handleOnSelect.bind(this);
        this.getPictureLibary = this.getPictureLibary.bind(this);
    }

    componentDidUpdate() {
        const { imagesList } = this.state, { modalIsOpen } = this.props;
        if (imagesList.length === 0 && modalIsOpen) {
            $.blockUI();
            this.getPictureLibary();
        }
    }

    getPictureLibary() {
        requests.post(urls.GetPictureLibrary).then(
            data => {
                this.setState({
                    imagesList: data,
                    croppedImage: data.length > 0 ? data[0] : undefined
                });
                $.unblockUI();
            }
        ).catch(handleError);
    }

    handleOnClick(image) {
        this.setState({ croppedImage: image })
    }

    handleOnSelect() {
        const { croppedImage } = this.state;
        this.props.setCroppedImage(croppedImage);
        this.props.closeModal();
    }

    render() {
        const { croppedImage, imagesList } = this.state, { resources, closeModal, modalIsOpen } = this.props;
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
                        <span className="icon-close" onClick={this.props.closeModal}></span>
                    </div>
                    <button className='btn-select' onClick={this.handleOnSelect}>{resources.Selected}</button>
                    <button className='btn-cancel' onClick={this.handleOnSelect}>Cancel</button> 
                    <div className="library">
                        {
                            imagesList.map((profilePicture, index) =>
                                <img className={`image-item ${croppedImage.ImageId === profilePicture.ImageId ? 'selected-image' : ''}`}
                                    alt="Crop"
                                    onClick={() => this.handleOnClick(profilePicture)} src={profilePicture.Image}
                                    key={index} />)
                        }
                    </div>
                </div>
            </Modal>
        );
    }
}