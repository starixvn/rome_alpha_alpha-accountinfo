import React from 'react';

export default class LoginInfoContent extends React.Component {
    builRow(title, value) {
        return (
            <div className="group">
                <div className="row-info bold">{title}:</div>
                <div className="row-info">{value}</div>
            </div>
        );
    }

    render() {
        let { resources, loginInfo, handleOpenIPInfo } = this.props;
        return (
            <div className="login-info">
                {this.builRow(resources.CreatedDate, loginInfo.CreatedDate)}
                {this.builRow(resources.LastLogin, loginInfo.LastLogin)}
                {this.builRow(resources.LastTxn, loginInfo.LastTxn)}
                <div className="group">
                    <div className="row-info bold">{resources.LoginIP}:</div>
                    <div className="row-info">
                        <span className="link" onClick={handleOpenIPInfo} data-ip={loginInfo.LoginIP}>
                            {loginInfo.LoginIP}
                        </span>
                    </div>
                </div>
            </div>
        );
    }
}

