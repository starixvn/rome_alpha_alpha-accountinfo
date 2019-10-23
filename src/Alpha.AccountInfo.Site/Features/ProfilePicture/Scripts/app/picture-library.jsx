import ReactDOM from "react-dom";
import React, { PureComponent } from "react";

class App extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            croppedImageUrl: null
        };

        this.handleOnClick = this.handleOnClick.bind(this);
        this.hanldeOnSelect = this.hanldeOnSelect.bind(this);
    }

    handleOnClick(src) {
        this.setState({ croppedImageUrl: src })
    }

    hanldeOnSelect() {
        const { croppedImageUrl } = this.state;
        window.top.createProfilePicturePopup.onClosing(croppedImageUrl);
    }

    render() {
        const style2 = {
            display: 'block'
        };

        const style3 = {
            height: '700px',
            overflow: 'auto',
            marginBottom: '10px'
        };

        const style4 = {
            width: '120px',
            height: '120px',
            padding: '4px'
        };
        const { imagesList } = this.props;
        return (
            <div className="App">
                <div style={style2}>
                    <div style={style3}>
                        {

                            imagesList.map(
                                (image, index) =>
                                    <img style={style4} alt="Crop" onClick={() => this.handleOnClick(image)} src={image} key={index}/>
                            )
                        }
                    </div>
                </div>

                <div>
                    <button onClick={this.hanldeOnSelect}>Select</button>
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