import ReactDOM from "react-dom";
import React, { PureComponent } from "react";

class App extends PureComponent {
    constructor(props) {
        super(props);

        const { imagesList } = this.props;
        this.state = {
            croppedImage: imagesList.length > 0 ? imagesList[0] : undefined
        };

        this.handleOnClick = this.handleOnClick.bind(this);
        this.hanldeOnSelect = this.hanldeOnSelect.bind(this);
    }

    handleOnClick(image) {
        this.setState({ croppedImage: image })
    }

    hanldeOnSelect() {
        const { croppedImage } = this.state;
        window.top.createProfilePicturePopup.onClosing(croppedImage);
    }

    render() {
        const { imagesList } = this.props, { croppedImage } = this.state;
        return (
            <div className="picture-library">
                <div>
                    <button className='btn-select' onClick={this.hanldeOnSelect}>Select</button>
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