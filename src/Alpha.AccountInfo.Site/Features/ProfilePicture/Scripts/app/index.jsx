import ReactDOM from "react-dom";
import React, { PureComponent } from "react";
import { profilePictureAPI } from './profile-picture-api';
import { setTimeout } from "core-js";
import { handleError } from '../../../Shared/Scripts/api-client';
import Raven from 'raven-js';

class App extends PureComponent {
    constructor(props) {
        super(props);

        const { avatar, imagesList } = this.props;
        this.state = {
            imagesList: imagesList,
            originalAvatar: avatar,
            croppedImage: avatar,
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
                this.handleCreateProfilePicture(reader.result);
            });
            reader.readAsDataURL(e.target.files[0]);
        }
    };

    render() {
        const { croppedImage, imagesList, originalAvatar } = this.state;

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
                        <button onClick={this.uploadFromDevice} className="btn-from-device" type='button'>
                            Upload from your device
                            <input type="file" ref="fileUploader" onChange={this.onSelectFile}/>
                        </button>
                    </div>
                </div>
                <div className="choose-picture">
                    <div className="extend-section">
                        <span className="txt">or choose from our gallery</span>
                        <a className='view-all' onClick={this.handleViewLibrary}>View All</a>
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
                <div className="btn-group">
                    <button className="btn-submit" onClick={this.handleOnSubmit}>Submit</button>
                    <button className="btn-reset" onClick={this.handleOnReset}>Reset</button>
                </div>
            </div>
        );
    }
}

export default App;

export const renderApp = (avatar, imagesList) => {
    ReactDOM.render(
        <App avatar={avatar} imagesList={imagesList}/>,
        document.getElementById('root')
    );
};

window.renderApp = renderApp;