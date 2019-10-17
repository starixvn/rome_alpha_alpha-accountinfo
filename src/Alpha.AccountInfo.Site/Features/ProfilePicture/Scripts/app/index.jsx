import ReactDOM from "react-dom";
import React, { PureComponent } from "react";

class App extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            croppedImageUrl: null
        };

        this.profilePictureClosing = this.profilePictureClosing.bind(this);
        this.handleOpenPopup = this.handleOpenPopup.bind(this);
    }

    profilePictureClosing(src) {
        window.top.createProfilePicturePopup.close();
        this.setState({ croppedImageUrl: src })
    }

    handleOpenPopup() {
        if (window.top.createProfilePicturePopup === null || window.top.createProfilePicturePopup === undefined) {
            window.top.createProfilePicturePopup = window.top.ageWnd.createInstance();
        }

        var url = `${window.top.alpha.urls.accountInfoRootPath}/ProfilePicture/CreateProfilePicture`;

        window.top.createProfilePicturePopup.onClosing = this.profilePictureClosing;
        window.top.popupManager.openWithHanlder(top.createProfilePicturePopup, url, 'Create avatar', 800, 800);
    }

    render() {
        const { crop, croppedImageUrl, src } = this.state;
        const style1  = {
            display: 'block',
            margin: '20px'
        };

        const style2 = {
            display: 'block',
        };

        const style3 = {
            width: '240px',
            height: '240px'
        };

        return (
            <div className="App">
                <div style={style1}>
                    <button onClick={this.handleOpenPopup}>Create Profile Picture</button>
                </div>
                <div style={style2}>
                    <img style={style3} alt="Crop" src={this.state.croppedImageUrl} />
                </div>
            </div>
        );
    }
}

export default App;

export const renderApp = () => {
    ReactDOM.render(
        <App />,
        document.getElementById('root')
    );
};

window.renderApp = renderApp;