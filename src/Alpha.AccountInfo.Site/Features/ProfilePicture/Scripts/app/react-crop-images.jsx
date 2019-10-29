import ReactDOM from "react-dom";
import React, { PureComponent } from "react";
import ReactCrop from "react-image-crop";

class App extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            src: null,
            crop: {
                unit: 'px',
                x: 130,
                y: 50,
                aspect: 1,
                width: 240,
                height: 240
            }
        };

        this.onSelectFile = this.onSelectFile.bind(this);
        this.onImageLoaded = this.onImageLoaded.bind(this);
        this.onCropComplete = this.onCropComplete.bind(this);
        this.onCropChange = this.onCropChange.bind(this);

        this.handleOnClosing = this.handleOnClosing.bind(this);
    }

    componentDidMount() {
        //setTimeout(function () {
        //    //$("input").trigger("click");
        //    $('#select-file').click();
        //}, 1000);

        //$('#select-file').click();
        //$('#select-file').hide();
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
        window.top.createProfilePicturePopup.onClosing(
            {
                Image: this.state.croppedImageUrl,
                ImageId: -1
            }
        );
    }

    onImageLoaded(image) {
        this.imageRef = image;
    };

    onCropComplete(crop) {
        this.makeClientCrop(crop);
    };

    onCropChange (crop, percentCrop) {
        this.setState({ crop });
    };

    makeClientCrop(crop) {
        if (this.imageRef && crop.width && crop.height) {
            const croppedImageUrl = this.getCroppedImg(
                this.imageRef,
                crop,
                "newFile.Jpeg"
            );

            var stringLength = croppedImageUrl.length - 'data:image/png;base64,'.length;

            var sizeInBytes = 4 * Math.ceil((stringLength / 3)) * 0.5624896334383812;
            var sizeInKb = sizeInBytes / 1000;
            console.log(sizeInKb);

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
    }

    render() {
        const { crop, src } = this.state;

        return (
            <div className="crop-image">
                <div className='select-image'>
                    <input id='select-file' type='file' onChange={this.onSelectFile} />
                </div>
                {src && (
                    <div>
                    <ReactCrop
                        src={src}
                        crop={crop}
                        onImageLoaded={this.onImageLoaded}
                        onComplete={this.onCropComplete}
                        onChange={this.onCropChange}
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