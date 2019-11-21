import React, { PureComponent } from "react";
import ReactDOM from "react-dom";
import { setTimeout } from "core-js";
import { cloneDeep } from 'lodash';
import Raven from 'raven-js';
import Modal from 'react-modal';
import { handleError, requests } from '../../../Shared/Scripts/api-client';
import { decodeResources } from '../../../SearchAccount/Scripts/app/helpers/accountinfo-helper';
import CreateProfilePictureModal from './create-profile-picture-modal';
import PictureLibraryModal from './picture-library-modal';
import { defaultImageId, croppedImageId, urls } from './profile-picture-helper';

Modal.setAppElement('#root');

class App extends PureComponent {
    constructor(props) {
        super(props);

        const { imagesList, defaultAvatarUrl } = this.props,
            originalAvatar = imagesList.length == 0 ? {
                ImageId: defaultImageId,
                Image: defaultAvatarUrl
            } : imagesList.pop();

        if (originalAvatar.Image === '') {
            originalAvatar.Image = defaultAvatarUrl;
        };

        this.state = {
            src: null,
            uploadFromDeviceModalIsOpen: false,
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
        this.handleActionsAfterUpdateSuccessfully = this.handleActionsAfterUpdateSuccessfully.bind(this);
        this.showMessage = this.showMessage.bind(this);

        this.setCroppedImage = this.setCroppedImage.bind(this);
        this.openModal = this.openModal.bind(this);
        this.closeModal = this.closeModal.bind(this);
    }

    setCroppedImage(profilePicture) {
        this.setState({ croppedImage: profilePicture });
    }

    openModal() {
        this.setState({ uploadFromDeviceModalIsOpen: true, pictureLibraryModalIsOpen: true });
    }

    closeModal() {
        this.setState({ uploadFromDeviceModalIsOpen: false, pictureLibraryModalIsOpen: false });
    }

    componentDidCatch(error, errorInfo) {
        this.setState({ error });
        Raven.captureException(error, { extra: errorInfo });
    }

    handleOnClick(image) {
        this.setState({ croppedImage: image });
    }

    handleOnSubmit() {
        const { croppedImage, originalAvatar } = this.state;

        if (croppedImage.ImageId > 0 && croppedImage.ImageId === originalAvatar.ImageId) {
            this.showMessage({
                IsSuccess: false,
                Message: this.props.resources.ThereIsNoChange
            });
        }
        else {
            $.blockUI();
            croppedImage.ImageId !== defaultImageId && requests.post(urls.UpdateActorProfilePicture,
                {
                    ImageId: croppedImage.ImageId,
                    Image: croppedImage.ImageId === croppedImageId ? croppedImage.Image.split(';base64,')[1] : null
                }).then(data => this.handleActionsAfterUpdate(data)).catch(handleError);
        }
    }

    showMessage(data) {
        const message = <div className='msg-container'>
            <div className={`msg ${data.IsSuccess ? 'msg-success' : 'msg msg-error'}`}>
                {data.IsSuccess ? this.props.resources.ProfilePictureUpdatedSuccessfully : data.Message.trim()}
            </div>
        </div>;

        this.setState({ hideMessages: false, message: message });
    }

    handleActionsAfterUpdate(data) {
        this.showMessage(data);

        if (data.IsSuccess) {
            setTimeout(() => {
                this.handleActionsAfterUpdateSuccessfully();
            }, 1500);
        }

        $.unblockUI();
    }

    handleActionsAfterUpdateSuccessfully() {
        const { croppedImage } = this.state, { imagesList } = this.props;
        window.top.headerNav.setProfilePictureOnNav(croppedImage.Image);
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
                this.setState({ src: reader.result, uploadFromDeviceModalIsOpen: true });
            });
            reader.readAsDataURL(e.target.files[0]);
            $(e.target).val('');
        }
    }

    // TODO: ProfilePictureResource
    render() {
        const { croppedImage, imagesList, originalAvatar, uploadFromDeviceModalIsOpen, src, pictureLibraryModalIsOpen } = this.state,
            { resources, setUpOTPLink, allowUpdate } = this.props, disabledButton = !allowUpdate || imagesList.length === 0,
            disabled = disabledButton ? { 'disabled': 'disabled' } : {};

        return (
            <div>
                <ul className="nav nav-tabs">
                    <li className="tab-item active">
                        <a>
                            <span>Profile Picture</span>
                        </a>
                    </li>
                    <li className="tab-item">
                        <a href={setUpOTPLink}>
                            <span>OTP</span>
                        </a>
                    </li>
                </ul>
                <div className="profile-picture">                    
                    <div className="crop-img-info">
                        <img alt="Crop" src={croppedImage.Image} className='cropped-image' />
                        <div className="wrapper-right">
                            <label htmlFor="files" {...disabled} className="btn-from-device"> {resources.UploadFromYourDevice} </label>
                            <input id="files" {...disabled} accept="image/png, image/jpeg, image/gif" type="file" onChange={this.onSelectFile} />
                        </div>
                    </div>
                    <div className="choose-picture">
                        <div className="extend-section">
                            <span className="txt">{resources.OrChooseFromOurGallery}</span>
                            <a className='view-all'
                                onClick={this.handleViewLibrary}
                                onTouchStart={this.handleViewLibrary} 
                            >{resources.ViewAll}</a>
                        </div>
                        <div className="list-picture">
                            <img className={`image-item ${croppedImage.ImageId === originalAvatar.ImageId ? 'selected-image' : ''}`}
                                alt="Crop"
                                onClick={() => this.handleOnClick(originalAvatar)}
                                onTouchStart={() => this.handleOnClick(originalAvatar)}
                                src={originalAvatar.Image}
                            />
                            {
                                imagesList.map((profilePicture, index) =>
                                    <img className={`image-item ${croppedImage.ImageId === profilePicture.ImageId ? 'selected-image' : ''}`}
                                        alt="Crop"
                                        onClick={() => this.handleOnClick(profilePicture)}
                                        onTouchStart={() => this.handleOnClick(profilePicture)}
                                        src={profilePicture.Image}
                                        key={index}
                                    />
                                )
                            }
                        </div>
                    </div>

                    {this.state.message}

                    <div className="btn-group">
                        <button className="btn-submit" {...disabled} onClick={this.handleOnSubmit} onTouchStart={this.handleOnSubmit}>{resources.Submit}</button>
                        <button className="btn-reset" {...disabled} onClick={this.handleOnReset} onTouchStart={this.handleOnReset}>{resources.Reset}</button>
                    </div>

                    <CreateProfilePictureModal
                        modalIsOpen={uploadFromDeviceModalIsOpen}
                        imageSrc={src}
                        resources={resources}
                        setCroppedImage={this.setCroppedImage}
                        openModal={this.openModal}
                        closeModal={this.closeModal}
                    />

                    <PictureLibraryModal
                        modalIsOpen={pictureLibraryModalIsOpen}
                        resources={resources}
                        setCroppedImage={this.setCroppedImage}
                        openModal={this.openModal}
                        closeModal={this.closeModal}
                    />
                </div >
            </div>
        );
    }
}

export default App;

export const renderApp = (imagesList, defaultAvatarUrl, resources, setUpOTPLink, allowUpdate) => {
    ReactDOM.render(
        <App {...{ imagesList, defaultAvatarUrl, resources: decodeResources(resources), setUpOTPLink, allowUpdate }} />,
        document.getElementById('root')
    );
};

window.renderApp = renderApp;