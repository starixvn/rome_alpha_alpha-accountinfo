import ReactDOM from "react-dom";
import React, { PureComponent } from "react";
import { profilePictureAPI } from './profile-picture-api';

class App extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            croppedImage: this.props.avatar
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
        window.top.popupManager.openWithHanlder(top.createProfilePicturePopup, url, 'Picture Library', 800, 800);
    }

    render() {
        const style1  = {
            display: 'block',
            margin: '20px'
        };

        const style2 = {
            display: 'block'
        };

        const style3 = {
            width: '240px',
            height: '240px'
        };

        const style4 = {
            width: '120px',
            height: '120px',
            padding: '4px'
        };
        const { avatar, imagesList } = this.props;

        return (
            <div className="App">
                <div style={style2}>
                    <img style={style3} alt="Crop" src={this.state.croppedImage.Image} />
                </div>

                <div style={style2}>
                    <span>Choose your picture</span>
                    <div>
                        <img style={style4} alt="Crop" onClick={() => this.handleOnClick(avatar)} src={avatar.Image} />
                        {

                            imagesList.map(
                                (profilePicture, index) =>
                                    <img style={style4} alt="Crop"
                                        onClick={() => this.handleOnClick(profilePicture)}
                                        src={profilePicture.Image}
                                        key={index} />
                            )
                        }

                    </div>
                    <div>
                        <a onClick={() => { this.handleOpenPopup(false); }} >View All</a>
                    </div>
                </div>

                <div style={style1}>
                    <button onClick={this.handleOpenPopup}>Create Profile Picture</button>
                </div>

                <div style={style2}>
                    <div style={style2}>- You can Upload JPG, GIF or PNG files.</div>
                    <div style={style2}>- Maximum file size 1024KB.</div>
                    <div style={style2}>- Your picture should be square.</div>
                </div>

                <div>
                    <button onClick={this.handleOnSubmit}>Submit</button>
                    <button onClick={this.handleOnReset}>Reset</button>
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