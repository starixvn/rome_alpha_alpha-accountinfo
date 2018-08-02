import React from 'react';

export default class DownlineName extends React.Component {
    constructor(props) {
        super(props);
        this.handleClick = this.handleClick.bind(this);
    }

    handleClick(e) {
        this.props.fetchCustomerInfoFromServer(e, this.props.username);
    }
    render() {
        const { username } = this.props;

        return (
            <div className="two-col" onClick={this.handleClick}>
                <a href="#">{username}</a>
            </div>
        )
    }
}
