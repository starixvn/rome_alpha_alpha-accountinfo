import React from 'react';

const LoginInfoContent = ({ resources, loginInfo, handleOpenIPInfo }) => <div>
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
</div>;

export default LoginInfoContent;
