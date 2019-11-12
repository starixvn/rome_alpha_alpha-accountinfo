import ReactDOM from "react-dom";
import React, { PureComponent } from "react";
import { handleError, requests } from '../../../Shared/Scripts/api-client';
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
const accountinfoUrl = window.top.alpha.urls.accountInfoRootPath;

export default class PictureLibraryModal extends PureComponent {
    constructor(props) {
        super(props);

        //const { imagesList } = this.props;
        this.state = {
            imagesList: []
        };

        this.handleOnClick = this.handleOnClick.bind(this);
        this.hanldeOnSelect = this.hanldeOnSelect.bind(this);
        this.getPictureLibary = this.getPictureLibary.bind(this);
        this.afterOpenModal = this.afterOpenModal.bind(this);
    }

    componentDidMount() {
        const { imagesList } = this.state;
        if (imagesList.length === 0) {
            this.getPictureLibary();
        }
    }

    getPictureLibary() {
        requests.post(accountinfoUrl + '/ProfilePicture/GetPictureLibrary').then(
            data => {
                this.setState({
                    imagesList: data,
                    croppedImage: data.length > 0 ? data[0] : undefined
                });
            }
        ).catch(handleError);
    }

    handleOnClick(image) {
        this.setState({ croppedImage: image })
    }

    hanldeOnSelect() {
        const { croppedImage } = this.state;
        this.props.setCroppedImage(croppedImage);
        this.props.closeModal();
    }
    afterOpenModal() {
        // references are now sync'd and can be accessed.
        //this.subtitle.style.color = '#f00';
    }
    render() {
        const { croppedImage, imagesList } = this.state;
        return (
            <Modal
                isOpen={this.props.pictureLibraryModalIsOpen}
                onAfterOpen={this.afterOpenModal}
                onRequestClose={this.closeModal}
                style={customStyles}
                contentLabel="Example Modal"
            >
                <div className="picture-library">
                    <div className="btn-group">
                        <button className='btn-select' onClick={this.hanldeOnSelect}>Select</button>
                        <button className="btn-cancel" onClick={this.props.closeModal}>Cancel</button>
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
                </div>
            </Modal>
        );
    }
}