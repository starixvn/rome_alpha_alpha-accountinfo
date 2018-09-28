import * as React from 'react';
import { NoInformFeature } from './no-inform-feature';
import { HeaderFeature } from './header-feature';
import LoginInfoContent from '../contents/login-info-content';

const LoginInfoPage = ({ resources, customerInfo, handleOpenIPInfo, redirectToBalanceInfoPage }) =>
    customerInfo === null ?
        <NoInformFeature url="/featureList" iconClass="icon-login" featureText={resources.LoginInfo} noInform={resources.NoInfo} /> :
        <div className="feature-detail login-info with-push-menu">
            <HeaderFeature url="/featureList" iconClass="icon-login" featureText={resources.LoginInfo} redirectToBalanceInfoPage={redirectToBalanceInfoPage} />
            <LoginInfoContent resources={resources} loginInfo={customerInfo.LoginInfo} handleOpenIPInfo={handleOpenIPInfo} />
        </div>;

export default LoginInfoPage;
