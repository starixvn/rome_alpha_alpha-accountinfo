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
        const { title } = this.props;

        return (
            <div className="col pointer" onClick={this.handleOpen}>
                <span className="link">{title}</span>
                <span className="icon-ic_list"></span>
            </div>
        );
    }
}