import * as React from 'react';
import { NoInformFeature } from '../../components/common/no-inform-feature';
import { HeaderFeature } from '../../components/common/header-feature';
import LoginInfoContent from '../contents/login-info-content';

const LoginInfoPage = ({ resources, customerInfo, handleOpenIPInfo }) => customerInfo === null ?
    <NoInformFeature url="/" iconClass="icon-login" featureText={resources.LoginInfo} noInform={resources.NoInfo} /> :
    <div className="feature-detail login-info with-push-menu">
        <HeaderFeature url="/" iconClass="icon-login" featureText={resources.LoginInfo} />
        <LoginInfoContent resources={resources} loginInfo={customerInfo.LoginInfo} handleOpenIPInfo={handleOpenIPInfo} />
    </div>;

export default LoginInfoPage;
