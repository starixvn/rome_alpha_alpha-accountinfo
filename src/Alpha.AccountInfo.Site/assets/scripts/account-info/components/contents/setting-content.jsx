import React from 'react';

export default class SettingContent extends React.Component {
    constructor(props) {
        super(props);
        this.handleOnclick = this.handleOnclick.bind(this);
    }

    handleOnclick(e) {
        if (this.props.mobileView) {
            let closeButton = window.parent.document.getElementById('closeIconOnMobile');
            closeButton.click();
        }
    }

    render() {
        const { resources, settingData } = this.props;
        return (
            <div className="setting">
                {
                    settingData.map((value, index) => {
                        return <div className="col-xs-6" key={index}>
                            <a href={value.Url} target="main" onClick={this.handleOnclick}>{value.Text}</a>
                        </div>;
                    })}
            </div>
        );
    }
}
