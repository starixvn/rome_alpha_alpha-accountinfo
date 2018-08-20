import React from 'react';

export default class StatusOnProduct extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpen = this.handleOpen.bind(this);
    }

    handleOpen(e) {
        window.top.ageWnd.open(this.props.url);
    };

    render() {
        const { resources, disabled, statusId, title, confirmMessage, url } = this.props;

        return (
            <div className="col">
                <a onClick={this.handleOpen}>{title}</a>
                <span className="icon-ic_list"></span>
            </div>
        );
    }
}