import ReactDOM from "react-dom";
import React, { PureComponent } from "react";
import { profilePictureAPI } from './profile-picture-api';
import { setTimeout } from "core-js";

class App extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            croppedImage: this.props.avatar,
            hideMessages: true,
            message: ''
        };

        this.profilePictureClosing = this.profilePictureClosing.bind(this);
        this.handleOpenPopup = this.handleOpenPopup.bind(this);
        this.handleOnClick = this.handleOnClick.bind(this);
        this.handleOnReset = this.handleOnReset.bind(this);
        this.handleOnSubmit = this.handleOnSubmit.bind(this);
    }

    handleOnClick(image) {
        this.setState({ croppedImage: image });
    }

    handleOnSubmit() {
        $.blockUI();
        const { croppedImage } = this.state;
        const data = {
            ImageId: croppedImage.ImageId,
            Image: croppedImage.ImageId < 0 ? croppedImage.Image.split(';base64,')[1] : null
        };

        profilePictureAPI.updateActorProfilePicture(data)
            .then(data => {
                const message = <div className='msg-container'>
                    <div className={`msg ${data.IsSuccess ? 'msg-success' : 'msg msg-error'}`}>
                        {data.IsSuccess ? 'Update successfully' : data.Message.trim()}
                    </div>
                </div>;

                this.setState({
                    hideMessages: false,
                    message: message
                });

                if (data.IsSuccess) {
                    setTimeout(function() {
                        window.top.location.reload(true);
                    }, 1500);
                }

                $.unblockUI();
            });
    }

    handleOnReset() {
        this.setState({ croppedImage: this.props.avatar });
    }

    profilePictureClosing(src) {
        window.top.createProfilePicturePopup.close();
        if (src !== undefined) {
            this.setState({ croppedImage: src });
        }
    }

    handleOpenPopup(createAvatar = true) {
        if (window.top.createProfilePicturePopup === null || window.top.createProfilePicturePopup === undefined) {
            window.top.createProfilePicturePopup = window.top.ageWnd.createInstance();
        }

        var url = `${window.top.alpha.urls.accountInfoRootPath}/ProfilePicture/${createAvatar ? 'CreateProfilePicture' : 'ViewPictureLibrary'}`;

        window.top.createProfilePicturePopup.onClosing = this.profilePictureClosing;
        window.top.popupManager.openWithHanlder(top.createProfilePicturePopup, url, 'Picture Library', 750, 750);
    }

    render() {
        const { avatar, imagesList } = this.props, { croppedImage } = this.state;

        return (
            <div className="profile-picture">
                <div className='cropped-image'>
                    <img alt="Crop" src={croppedImage.Image} />
                </div>

                <div className="choose-picture">
                    <span>Choose your picture</span>
                    <div>
                        <img className={`image-item ${croppedImage.ImageId === avatar.ImageId ? 'selected-image' : ''}`}
                            alt="Crop"
                            onClick={() => this.handleOnClick(avatar)} src={avatar.Image} />
                        
                        {
                            imagesList.map(
                                (profilePicture, index) =>
                                    <img className={`image-item ${croppedImage.ImageId === profilePicture.ImageId ? 'selected-image' : ''}`}
                                        alt="Crop"
                                        onClick={() => this.handleOnClick(profilePicture)} src={profilePicture.Image}
                                        key={index}/>
                            )
                        }

                    </div>
                    <div>
                        <a className='view-all' onClick={() => { this.handleOpenPopup(false); }} >View All</a>
                    </div>
                </div>

                <div className='create-profile-picture'>
                    <button onClick={this.handleOpenPopup}>Create Profile Picture</button>
                </div>

                <div className="noted">
                    <span>- You can Upload JPG, GIF or PNG files.</span>
                    <span>- Maximum file size 1024KB.</span>
                    <span>- Your picture should be square.</span>
                </div>
                {this.state.message}
                <div>
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