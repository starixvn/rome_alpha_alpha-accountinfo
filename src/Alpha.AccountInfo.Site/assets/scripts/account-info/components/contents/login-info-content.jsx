import React from 'react';

export default class LoginInfoContent extends React.Component {
    render() {
        let { resources, loginInfo, handleOpenIPInfo } = this.props;

        let row = (title, value) =>
            <div className="group">
                <div className="row-info bold">{title}:</div>
                <div className="row-info">{value}</div>
            </div>;

        return (
            <div>
                {row(resources.CreatedDate, loginInfo.CreatedDate)}
                {row(resources.LastLogin, loginInfo.LastLogin)}
                {row(resources.LastTxn, loginInfo.LastTxn)}
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

