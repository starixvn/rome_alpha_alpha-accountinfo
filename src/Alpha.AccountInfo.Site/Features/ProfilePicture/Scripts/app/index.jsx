import { setTimeout } from "core-js";
import { cloneDeep } from 'lodash';
import React, { PureComponent } from "react";
import ReactDOM from "react-dom";
import CreateProfilePictureModal, { croppedImageId } from './create-profile-picture-modal';
import PictureLibraryModal from './picture-library-modal';
import { handleError, requests } from '../../../Shared/Scripts/api-client';
import Raven from 'raven-js';
import Modal from 'react-modal';

const defaultImageId = -100, accountinfoUrl = window.top.alpha.urls.accountInfoRootPath;
Modal.setAppElement('#root');

class App extends PureComponent {
    constructor(props) {
        super(props);

        const { imagesList, defaultAvatarUrl } = this.props,
            originalAvatar = imagesList.length == 0 ? {
                ImageId: defaultImageId,
                Image: defaultAvatarUrl
            } : imagesList.pop();

        this.state = {
            src: null,
            modalIsOpen: false,
            pictureLibraryModalIsOpen: false,
            imagesList: cloneDeep(imagesList).slice(1, 5),
            originalAvatar: originalAvatar,
            croppedImage: originalAvatar,
            hideMessages: true,
            message: ''
        };

        this.handleOnClick = this.handleOnClick.bind(this);
        this.handleOnReset = this.handleOnReset.bind(this);
        this.handleOnSubmit = this.handleOnSubmit.bind(this);
        this.onSelectFile = this.onSelectFile.bind(this);

        this.handleViewLibrary = this.handleViewLibrary.bind(this);

        this.handleActionsAfterUpdate = this.handleActionsAfterUpdate.bind(this);
        this.handleActionsAfterUpdateSuccessfully = this.handleActionsAfterUpdateSuccessfully.bind(this);
        this.showMessage = this.showMessage.bind(this);

        this.setCroppedImage = this.setCroppedImage.bind(this);
        this.openModal = this.openModal.bind(this);
        this.closeModal = this.closeModal.bind(this);
    }

    setCroppedImage(profilePicture) {
        this.setState({
            croppedImage: profilePicture
        });
    }

    openModal() {
        this.setState({ modalIsOpen: true, pictureLibraryModalIsOpen: true });
    }

    closeModal() {
        this.setState({ modalIsOpen: false, pictureLibraryModalIsOpen: false });
    }

    componentDidCatch(error, errorInfo) {
        this.setState({ error });
        Raven.captureException(error, { extra: errorInfo });
    }

    handleOnClick(image) {
        this.setState({ croppedImage: image });
    }

    handleOnSubmit() {
        $.blockUI();
        const { croppedImage } = this.state;

        croppedImage.ImageId !== defaultImageId && requests.post(
            accountinfoUrl + '/ProfilePicture/UpdateActorProfilePicture',
            {
                ImageId: croppedImage.ImageId,
                Image: croppedImage.ImageId === croppedImageId ? croppedImage.Image.split(';base64,')[1] : null
            }).then(data => this.handleActionsAfterUpdate(data)).catch(handleError);
    }

    showMessage(message) {
        this.setState({
            hideMessages: false,
            message: message
        });
    }

    handleActionsAfterUpdate(data) {
        const message = <div className='msg-container'>
            <div className={`msg ${data.IsSuccess ? 'msg-success' : 'msg msg-error'}`}>
                {data.IsSuccess ? 'Update successfully' : data.Message.trim()}
            </div>
        </div>;

        this.showMessage(message);

        if (data.IsSuccess) {
            setTimeout(() => {
                this.handleActionsAfterUpdateSuccessfully();
            }, 1500);
        }

        $.unblockUI();
    }

    handleActionsAfterUpdateSuccessfully() {
        const { croppedImage } = this.state, { imagesList } = this.props;

        window.top.alpha.isScreenLower768 ?
            window.top.headerNav.setProfilePictureOnNav(croppedImage.Image) :
            window.top.indexManager.loadPartialViews();

        this.setState({
            imagesList: cloneDeep(imagesList).filter(item => item.ImageId != croppedImage.ImageId).slice(1, 5),
            originalAvatar: croppedImage,
            hideMessages: true,
            message: ''
        });

    }

    handleOnReset() {
        const { originalAvatar } = this.state;
        this.setState({ croppedImage: originalAvatar });
    }

    handleViewLibrary() {
        this.setState({ pictureLibraryModalIsOpen: true });
    }

    onSelectFile(e) {
        if (e.target.files && e.target.files.length > 0) {
            const reader = new FileReader();
            reader.addEventListener("load", () => {
                this.setState({ src: reader.result, modalIsOpen: true });
            });
            reader.readAsDataURL(e.target.files[0]);
            $(e.target).val('');
        }
    }

    render() {
        const { croppedImage, imagesList, originalAvatar, modalIsOpen, src, pictureLibraryModalIsOpen } = this.state,
            showButton = imagesList.length > 0;

        return (
            <div className="profile-picture">
                <div className="crop-img-info">
                    <img alt="Crop" src={croppedImage.Image} className='cropped-image' />
                    <div className="wrapper-right">
                        <div className="noted">
                            <span>- You can Upload JPG, GIF or PNG files.</span>
                            <span>- Maximum file size 1024KB.</span>
                            <span>- Your picture should be square.</span>
                        </div>

                        {showButton && <label htmlFor="files" className="btn-from-device">Upload from your device </label>}
                        <input id="files" accept="image/png, image/jpeg, image/gif" type="file" onChange={this.onSelectFile} />
                    </div>
                </div>
                <div className="choose-picture">
                    <div className="extend-section">
                        <span className="txt">or choose from our gallery</span>
                        {showButton && <a className='view-all' onClick={this.handleViewLibrary}>View All</a>}
                    </div>
                    <div className="list-picture">
                        <img className={`image-item ${croppedImage.ImageId === originalAvatar.ImageId ? 'selected-image' : ''}`}
                            alt="Crop"
                            onClick={() => this.handleOnClick(originalAvatar)} src={originalAvatar.Image} />
                        {
                            imagesList.map((profilePicture, index) =>
                                <img className={`image-item ${croppedImage.ImageId === profilePicture.ImageId ? 'selected-image' : ''}`}
                                    alt="Crop"
                                    onClick={() => this.handleOnClick(profilePicture)} src={profilePicture.Image}
                                    key={index} />
                            )
                        }
                    </div>
                </div>
                {this.state.message}
                {showButton &&
                    <div className="btn-group">
                        <button className="btn-submit" onClick={this.handleOnSubmit}>Submit</button>
                        <button className="btn-reset" onClick={this.handleOnReset}>Reset</button>
                    </div>
                }

                <CreateProfilePictureModal
                    modalIsOpen={modalIsOpen}
                    imageSrc={src}
                    setCroppedImage={this.setCroppedImage}
                    openModal={this.openModal}
                    closeModal={this.closeModal}
                />

                <PictureLibraryModal
                    pictureLibraryModalIsOpen={pictureLibraryModalIsOpen}
                    setCroppedImage={this.setCroppedImage}
                    openModal={this.openModal}
                    closeModal={this.closeModal}
                />
            </div>
        );
    }
}

export default App;

export const renderApp = (imagesList, defaultAvatarUrl) => {
    ReactDOM.render(
        <App {...{ imagesList, defaultAvatarUrl}}/>,
        document.getElementById('root')
    );
};

window.renderApp = renderApp;