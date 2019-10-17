import ReactDOM from "react-dom";
import React, { PureComponent } from "react";
import ReactCrop from "react-image-crop";
//import "react-image-crop/dist/ReactCrop.css";

class App extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            src: null,
            crop: {
                unit: 'px', // default, can be 'px' or '%'
                x: 130,
                y: 50,
                aspect: 1,
                width: 200,
                maxWidth :200,
                height: 200
            }
        };

        this.onSelectFile = this.onSelectFile.bind(this);
        this.onImageLoaded = this.onImageLoaded.bind(this);
        this.onCropComplete = this.onCropComplete.bind(this);
        this.onCropChange = this.onCropChange.bind(this);

        this.handleOnClosing = this.handleOnClosing.bind(this);
    }

    onSelectFile(e) {
        if (e.target.files && e.target.files.length > 0) {
            const reader = new FileReader();
            reader.addEventListener("load", () =>
                this.setState({ src: reader.result })
            );
            reader.readAsDataURL(e.target.files[0]);
        }
    };

    handleOnClosing() {
        window.top.createProfilePicturePopup.onClosing(this.state.croppedImageUrl);
    }

    // If you setState the crop in here you should return false.
    onImageLoaded(image) {
        this.imageRef = image;
    };

    onCropComplete(crop) {
        this.makeClientCrop(crop);
    };

    onCropChange (crop, percentCrop) {
        // You could also use percentCrop:
        // this.setState({ crop: percentCrop });
        this.setState({ crop });
    };

    makeClientCrop(crop) {
        if (this.imageRef && crop.width && crop.height) {
            const croppedImageUrl = this.getCroppedImg(
                this.imageRef,
                crop,
                "newFile.jpeg"
            );
            this.setState({ croppedImageUrl });
        }
    }

    getCroppedImg(image, crop, fileName) {
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
        //return new Promise((resolve, reject) => {
        //    canvas.toBlob(blob => {
        //        if (!blob) {
        //            //reject(new Error('Canvas is empty'));
        //            console.error("Canvas is empty");
        //            return;
        //        }
        //        blob.name = fileName;
        //        window.URL.revokeObjectURL(this.fileUrl);
        //        this.fileUrl = window.URL.createObjectURL(blob);
        //        resolve(this.fileUrl);
        //    }, "image/jpeg");
        //});
    }

    render() {
        const { crop, croppedImageUrl, src } = this.state;
        const style = {
            marginBottom: '20px'
        };

        return (
            <div className="App">
                <div>
                    <input type="file" onChange={this.onSelectFile} style={style} />
                </div>
                {src && (
                    <ReactCrop
                        src={src}
                        crop={crop}
                        onImageLoaded={this.onImageLoaded}
                        onComplete={this.onCropComplete}
                        onChange={this.onCropChange}
                    />
                )}
                <button onClick={this.handleOnClosing}>OK</button>
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