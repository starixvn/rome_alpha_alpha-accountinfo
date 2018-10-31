import React from 'react';

export const LoginInfoRow = ({ title, value }) => {
    return (
        <div className="group">
            <div className="row-info bold">{title}:</div>
            <div className="row-info">{value}</div>
        </div>
    );
} 

export const LoginInfoWithLinkRow = ({ title, value, handleOpenIPInfo }) => {
    return (
        <div className="group">
            <div className="row-info bold">{title}:</div>
            <div className="row-info">
                <span className="link" onClick={handleOpenIPInfo} data-ip={value}>
                    {value}
                </span>
            </div>
        </div>
    );
} 

const LoginInfo = ({ resources, loginInfo, handleOpenIPInfo}) => {
    let loginInfoPropsArray = [
        {
            title: resources.CreatedDate,
            value: loginInfo.CreatedDate
        },
        {
            title: resources.LastLogin,
            value: loginInfo.LastLogin
        },
        {
            title: resources.LastTxn,
            value: loginInfo.LastTxn
        },
    ];

    return (
        <div className="login-info">
            {
                loginInfoPropsArray.map((loginInfoProps, index) => {
                    return <LoginInfoRow {...loginInfoProps} key={index} />;
                })
            }
            <LoginInfoWithLinkRow
                title={resources.LoginIP}
                value={loginInfo.LoginIP}
                handleOpenIPInfo={handleOpenIPInfo}
            />
        </div>
    );
} 

export default LoginInfo;
