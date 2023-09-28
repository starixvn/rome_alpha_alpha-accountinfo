import React from 'react';

export const LoginInfoRow = ({ title, value }) => {
    return <div className='group'>
        <div className='row-info bold'>{title}:</div>
        <div className='row-info'>{value}</div>
    </div>;
};

export const LoginInfoWithLinkRow = ({ title, value, handleOpenIPInfo }) => {
    return <div className='group'>
        <div className='row-info bold'>{title}:</div>
        <div className='row-info'>
            <span className='link' onClick={handleOpenIPInfo} data-ip={value}>
                {value}
            </span>
        </div>
    </div>;
};

const LoginInfo = ({ resources, loginInfo, lastTxn, handleOpenIPInfo }) => {
    if (loginInfo === null) {
        return <div className='no-information'>{resources.NoInfo}</div>;
    }

    const loginInfoPropsArray = [
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
            value: lastTxn
        }
    ];

    if (loginInfo.AllowOpenInfoPopup) {
        loginInfoPropsArray.push({
            title: resources.LoginIP,
            value: loginInfo.LoginIP
        });
    }

    return <div className='login-info'>
        {
            loginInfoPropsArray.map((loginInfoProps, index) => <LoginInfoRow {...loginInfoProps} key={index} />)
        }
        {
            loginInfo.AllowOpenInfoPopup ?
                <LoginInfoRow {...{ title: resources.LoginIP, value: loginInfo.LoginIP }} /> :
                <LoginInfoWithLinkRow
                    {...{
                        title: resources.LoginIP,
                        value: loginInfo.LoginIP,
                        handleOpenIPInfo
                    }}
                />
        }
    </div>;
};

export default LoginInfo;
