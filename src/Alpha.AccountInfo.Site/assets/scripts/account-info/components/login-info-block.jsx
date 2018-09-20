import React from 'react';
import { Link } from 'react-router-dom';
import { NoInfo } from '../components/common/no-info';

const LoginInfoBlock = ({ resources, customerInfo, handleOpenIPInfo }) => {
    const header = <Link to="/" className="feature-item">
        <span className="icon-login" />
        {resources.LoginInfo}
    </Link>;

    if (customerInfo !== null) {
        const loginInfo = customerInfo.LoginInfo;
        return (
            <div className="feature-detail login-info">
                {header}
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
                    <div className="row-info">
                        <span className="link" onClick={handleOpenIPInfo} data-ip={loginInfo.LoginIP}>
                            {loginInfo.LoginIP}
                        </span>
                    </div>
                </div>
            </div>
        );
    }
    else {
        return (
            <div>
                {header}
                <NoInfo text={resources.NoInfo} />
            </div>
        );
    }
};

export default LoginInfoBlock;
