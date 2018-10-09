import * as React from 'react';
import LoginInfoContent from '../contents/login-info-content';
import MobilePage from './mobile-page';

const LoginInfoPage = ({
    resources,
    loginInfo,
    handleOpenIPInfo,
    redirectToBalanceInfoPage }) =>
    <MobilePage iconClass="icon-login"
        featureText={resources.LoginInfo}
        noInfoText={resources.NoInfo}
        redirectToBalanceInfoPage={redirectToBalanceInfoPage}
        data={loginInfo}
    >
        <LoginInfoContent
            resources={resources}
            loginInfo={loginInfo}
            handleOpenIPInfo={handleOpenIPInfo}
        />
    </MobilePage>;

export default LoginInfoPage;