import { setTimeout } from "core-js";
import { cloneDeep } from 'lodash';
import Raven from 'raven-js';
import React, { PureComponent } from "react";
import ReactDOM from "react-dom";
import { handleError } from '../../../Shared/Scripts/api-client';
import CreateProfilePictureModal from './create-profile-picture-modal';
import { profilePictureAPI } from './profile-picture-api';

const defaultAvatar = {
    ImageId: -100,
    Image: 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAIAAACRXR/mAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAkVJREFUeNrsmFlrAkEMgN1xvRFBRK34ouj//0Pqk3ghgme97VdDl+KZ7rZbHyYPwzqTzXxOMknUWSwWkdcTE3lJsVgWy2JZLIv1K+IGN+E4jjGGkefT6XQ8Hhn/E0uA1uv1fD5nZCaZTGazWcaAcG4QJjbudrv9fn+328HxGRPGxGKxt7OIgk/jvjsIdu10OoPBIBqNigdFQDkcDuVyuV6v+8byGfKgjEaj4XB4wSS4TLKEAg/hYbHxfr9nY+csNxUQFFC7qfBXWO9nIZLu2jVGdELFInokxh8ICqiFh0Ug33PftSv9Rb1PrEQi4brugy1ZQgG1sLFyuRw+uqfDEgqhYglZtVpl15sRxiRLKISdt9gvnU43m022JwtIqZGCyEcmWULhH7K8JFVKIflpOp1ut1tm4vE4viuVSpTFBy7+WyyvWsuBSboi0oM3EUEbG6mA5ixesNs28FaqlGyJy743NjhRusLTl4SBJVsSTKvViu5vuVxS+LxaxCr3IJVKZTIZ+kEuo2Tdp5XKf8hLdEMzHo8nkwl3UMLoohB5JwQf9zGfzxcKBfh+dA+0WADhqV6vRxfFgyA+fUtQ6FeLxWKlUuFBeWwqLOlSWq0WXvN+TfzotkKDTxuNBv7VkBmN74ikdrsN03UvqvQ+L/I6RpSNodEcFZE0m818d8BeScAIpjTeV53WZrMJ/tNPvIkpzWm5SnMcfnAsjCi/nqu5TVTfWq2mOXyNKU3Ia29icCaPTIPl/qIt+0eSxbJYFstiWSyVfAgwANaLooANmykXAAAAAElFTkSuQmCC'
};

class App extends PureComponent {
    constructor(props) {
        super(props);

        const { imagesList } = this.props, originalAvatar = imagesList.length == 0 ?  defaultAvatar : imagesList.pop();

        this.state = {
            src: null,
            modalIsOpen: false,
            imagesList: cloneDeep(imagesList).slice(1, 5),
            originalAvatar: originalAvatar,
            croppedImage: originalAvatar,
            hideMessages: true,
            message: ''
        };

        this.uploadFromDevice = this.uploadFromDevice.bind(this);
        this.profilePictureClosing = this.profilePictureClosing.bind(this);
        this.handleOnClick = this.handleOnClick.bind(this);
        this.handleOnReset = this.handleOnReset.bind(this);
        this.handleOnSubmit = this.handleOnSubmit.bind(this);
        this.onSelectFile = this.onSelectFile.bind(this);

        this.handleOpenPopup = this.handleOpenPopup.bind(this);
        this.handleViewLibrary = this.handleViewLibrary.bind(this);

        this.handleActionsAfterUpdate = this.handleActionsAfterUpdate.bind(this);
        this.handleActionsAfterUpdateSuccessfully = this.handleActionsAfterUpdateSuccessfully.bind(this);
        this.showMessage = this.showMessage.bind(this);

        this.setCroppedImage = this.setCroppedImage.bind(this);
        this.openModal = this.openModal.bind(this);
        this.closeModal = this.closeModal.bind(this);
    }

    setCroppedImage(src) {
        this.setState({
            croppedImage: {
                Image: src,
                ImageId: -1
            }
        });
    }

    openModal() {
        this.setState({ modalIsOpen: true });
    }

    closeModal() {
        this.setState({modalIsOpen: false});
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

        profilePictureAPI.updateActorProfilePicture({
            ImageId: croppedImage.ImageId,
            Image: croppedImage.ImageId < 0 ? croppedImage.Image.split(';base64,')[1] : null
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
            this.handleActionsAfterUpdateSuccessfully();
        }

        $.unblockUI();
    }

    handleActionsAfterUpdateSuccessfully() {
        setTimeout(() => {
            window.top.indexManager.loadPartialViews();
            const { croppedImage } = this.state;
            this.setState({
                imagesList: cloneDeep(this.props.imagesList).filter(item => item.ImageId != croppedImage.ImageId).slice(1, 5),
                originalAvatar: croppedImage,
                hideMessages: true,
                message: ''
            });
        }, 3000);
    }

    handleOnReset() {
        const { originalAvatar } = this.state;
        this.setState({ croppedImage: originalAvatar });
    }

    profilePictureClosing(src) {
        window.top.createProfilePicturePopup.close();
        if (src !== undefined) {
            this.setState({ croppedImage: src });
        }
    }

    uploadFromDevice() {
        this.refs.fileUploader.click();
    }

    handleViewLibrary() {
        this.handleOpenPopup('ViewPictureLibrary', 'Picture Library', 750, 750);
    }

    handleCreateProfilePicture(imageBase64) {
        this.handleOpenPopup('CreateProfilePicture', 'Create Profile Picture', 750, 550, imageBase64);
    }

    handleOpenPopup(actionName, popupTitle, width, height, imageBase64 = null ) {
        if (window.top.createProfilePicturePopup === null || window.top.createProfilePicturePopup === undefined) {
            window.top.createProfilePicturePopup = window.top.ageWnd.createInstance();
        }

        var url = `${window.top.alpha.urls.accountInfoRootPath}/ProfilePicture/${actionName}`;

        window.top.createProfilePicturePopup.onClosing = this.profilePictureClosing;
        window.top.createProfilePicturePopup.imageBase64 = imageBase64;
        window.top.popupManager.openWithHanlder(top.createProfilePicturePopup, url, popupTitle, width, height);
    }

    onSelectFile(e) {
        if (e.target.files && e.target.files.length > 0) {
            const reader = new FileReader();
            reader.addEventListener("load", () => {
                this.setState({ src: reader.result, modalIsOpen: true});
            });
            reader.readAsDataURL(e.target.files[0]);
            $(e.target).val('');
        }
    }

    render() {
        const { croppedImage, imagesList, originalAvatar, modalIsOpen, src } = this.state, showButton = imagesList.length > 0;

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
                        <input id="files" accept="image/png, image/jpeg" type="file" onChange={this.onSelectFile}/>
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
                                        key={index}/>
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

                <CreateProfilePictureModal modalIsOpen={modalIsOpen} imageSrc={src} setCroppedImage={this.setCroppedImage}
                    openModal={this.openModal} closeModal={this.closeModal} />
            </div>
        );
    }
}

export default App;

export const renderApp = (imagesList) => {
    ReactDOM.render(
        <App imagesList={imagesList}/>,
        document.getElementById('root')
    );
};

window.renderApp = renderApp;