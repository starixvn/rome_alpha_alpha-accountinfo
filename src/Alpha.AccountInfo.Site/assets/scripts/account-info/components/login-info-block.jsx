import React from 'react';
import { NoInformFeature } from '../components/common/no-inform-feature';
import { HeaderFeature } from '../components/common/header-feature';

const LoginInfoBlock = ({ resources, customerInfo, handleOpenIPInfo }) => {

    if (customerInfo !== null) {
        const loginInfo = customerInfo.LoginInfo;
        return (
            <div className="feature-detail login-info">
                <HeaderFeature url="/" iconClass="icon-login" featureText={resources.LoginInfo} />
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
        return <NoInformFeature url="/" iconClass="icon-login" featureText={resources.LoginInfo} noInform={resources.NoInfo} />
    }
};

export default LoginInfoBlock;
