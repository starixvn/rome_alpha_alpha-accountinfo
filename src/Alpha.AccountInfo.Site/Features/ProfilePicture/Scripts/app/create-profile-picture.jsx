import ReactDOM from "react-dom";
import React, { PureComponent } from "react";
import ReactCrop from "react-image-crop";

class App extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            src: window.top.createProfilePicturePopup.imageBase64,
            crop: {
                unit: 'px',
                x: 130,
                y: 50,
                aspect: 1,
                width: 240,
                height: 240
            }
        };

        this.onImageLoaded = this.onImageLoaded.bind(this);
        this.onCropComplete = this.onCropComplete.bind(this);
        this.onDragStart = this.onDragStart.bind(this);
        this.onCropChange = this.onCropChange.bind(this);
        this.handleOnClosing = this.handleOnClosing.bind(this);
    }

    handleOnClosing() {
        window.top.createProfilePicturePopup.onClosing(
            {
                Image: this.state.croppedImageUrl,
                ImageId: -1
            }
        );
    }

    onImageLoaded(image) {
        //this.imageRef = image;
    };

    onCropComplete(crop) {
        //this.makeClientCrop(crop);
    };

    onDragStart(crop) {
        //alert('drag start');
    };

    onCropChange(crop) {
        this.setState({ crop });
    };

    makeClientCrop(crop) {
        //if (this.imageRef && crop.width && crop.height) {
        //    const croppedImageUrl = this.getCroppedImg(
        //        this.imageRef,
        //        crop,
        //        "newFile.Jpeg"
        //    );

        //    this.setState({ croppedImageUrl });
        //}
    }

    getCroppedImg(image, crop) {
        const canvas = document.createElement("canvas");
        const scaleX = image.naturalWidth / image.width;
        const scaleY = image.naturalHeight / image.height;
        canvas.width = crop.width;
        canvas.height = crop.height;
        const ctx = canvas.getContext("2d");

        ctx.drawImage(
            image,
            crop.x * scaleX,
            crop.y * scaleY,
            crop.width * scaleX,
            crop.height * scaleY,
            0,
            0,
            crop.width,
            crop.height
        );

        return canvas.toDataURL();
    }

    render() {
        const { crop, src } = this.state;

        return (
            <div className="crop-image">
                {src && (
                    <div>
                        <ReactCrop
                            minWidth={240}
                            src={src}
                            crop={crop}
                            onImageLoaded={this.onImageLoaded}
                            onComplete={this.onCropComplete}
                            onChange={this.onCropChange}
                            onDragStart={this.onDragStart}
                    />
                        <button className="btn-submit" onClick={this.handleOnClosing}>Select</button>
                    </div>
                )}

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