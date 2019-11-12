import React, { PureComponent } from "react";
import Modal from 'react-modal';
import { handleError, requests } from '../../../Shared/Scripts/api-client';
import { urls } from './profile-picture-helper';

const customStyles = {
    content: {
        top: '50%',
        left: '50%',
        right: 'auto',
        bottom: 'auto',
        marginRight: '-50%',
        transform: 'translate(-50%, -50%)',
        height: window.top.alpha.isScreenLower768 ? '100%' : '85%',
        width: window.top.alpha.isScreenLower768 ? '100%' :'90%',
        overflow: 'hidden'
    }
};

export default class PictureLibraryModal extends PureComponent {
    constructor(props) {
        super(props);
        $.blockUI();
        this.state = {
            croppedImage: undefined,
            imagesList: []
        };

        this.handleOnClick = this.handleOnClick.bind(this);
        this.handleOnSelect = this.handleOnSelect.bind(this);
        this.getPictureLibary = this.getPictureLibary.bind(this);
    }

    componentDidMount() {
        const { imagesList } = this.state;
        if (imagesList.length === 0) {
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
        const { croppedImage, imagesList } = this.state, { resources, closeModal } = this.props;
        return (
            <Modal
                isOpen={this.props.pictureLibraryModalIsOpen}
                onRequestClose={closeModal}
                style={customStyles}
                contentLabel=""
            >
                <div className="picture-library">
                    <div className='header'>
                        <span className='title'>{resources.PictureLibrary}</span>
                        <span className='closed' onClick={this.props.closeModal}>X</span>
                    </div>
                    <div className="library">
                        {
                            imagesList.map((profilePicture, index) =>
                                <img className={`image-item ${croppedImage.ImageId === profilePicture.ImageId ? 'selected-image' : ''}`}
                                    alt="Crop"
                                    onClick={() => this.handleOnClick(profilePicture)} src={profilePicture.Image}
                                    key={index} />)
                        }
                    </div>
                    <div className="btn-group">
                        <button className="btn-submit" onClick={this.handleOnSelect}>{resources.Selected}</button>
                        <button className="btn-cancel" onClick={closeModal}>{resources.Cancel}</button>
                    </div>
                </div>
            </Modal>
        );
    }
}