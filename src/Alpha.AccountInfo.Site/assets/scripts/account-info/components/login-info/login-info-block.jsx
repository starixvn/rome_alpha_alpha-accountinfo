import * as React from 'react';
import { NoInformFeature } from '../../components/common/no-inform-feature';
import { HeaderFeature } from '../../components/common/header-feature';
import LoginInfoContent from './login-info-content';

const LoginInfoBlock = ({ resources, customerInfo, handleOpenIPInfo }) => customerInfo === null ?
    <NoInformFeature url="/" iconClass="icon-login" featureText={resources.LoginInfo} noInform={resources.NoInfo} /> :
    <div className="feature-detail">
        <HeaderFeature url="/" iconClass="icon-login" featureText={resources.LoginInfo} />
        <LoginInfoContent resources={resources} loginInfo={customerInfo.LoginInfo} handleOpenIPInfo={handleOpenIPInfo} />
    </div>;

export default LoginInfoBlock;
