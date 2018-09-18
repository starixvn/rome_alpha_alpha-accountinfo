import React from 'react';
import { Link } from 'react-router-dom';

export default class LoginInfoBlock extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenIPInfo = this.handleOpenIPInfo.bind(this);
    }

    handleOpenIPInfo(e) {
        e.preventDefault();
        window.top.ageWnd.open(`${this.props.viewIpInfoLink}?ip=${this.props.loginInfo.LoginIP}`);
    }

    render() {
        const { resources, handleCollapExpandBlock, blockId, expanded, loginInfo } = this.props;

        return (
            <div className="feature-detail login-info">
                <Link to="/" className="feature-item">
                    <span className="icon-login" />
                    {resources.LoginInfo}</Link>
                <div className="group">
                    <div className="row-info bold">{resources.CreatedDate}:</div>
                    <div className="row-info">{loginInfo.CreatedDate}</div>
                </div>
                <div className="group">
                    <div className="row-info bold">{resources.LastLogin}:</div>
                    <div className="row-info">{loginInfo.LastLogin}</div>
                </div>
                <div className="group">
                    <div className="row-info bold">{resources.LastTxn}:</div>
                    <div className="row-info">{loginInfo.LastTxn}</div>
                </div>
                <div className="group">
                    <div className="row-info bold">{resources.LoginIP}:</div>
                    <div className="row-info"><span className="link" onClick={this.handleOpenIPInfo}>{loginInfo.LoginIP}</span></div>
                </div>
            </div>
        );
    }
}
