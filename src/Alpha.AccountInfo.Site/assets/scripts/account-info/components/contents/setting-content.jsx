import React from 'react';

export default class SettingContent extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenPopup = this.handleOpenPopup.bind(this);
    }

    handleOpenPopup(e) {
        let url = e.currentTarget.dataset.url;
        let productName = e.currentTarget.dataset.name;
        let resources = this.props.resources;
        if (JSON.parse(e.currentTarget.dataset.enabled)) {
            e.currentTarget.dataset.message === undefined ?
                window.top.ageWnd.open(url, `${resources.Update} ${productName} ${resources.Status}`) :
                this.handleOpenConfirmPopup(e);
        }
    }

    render() {
        const { resources, settingData  } = this.props;
        return (
            <div className="setting">
                {
                    settingData.map((value, index) => {
                        return <div className="col-xs-6" key={index}>
                            <a href={value.Url} target="main">{value.Text}</a>
                        </div>;
                    })}
            </div>
        );
    }
}
