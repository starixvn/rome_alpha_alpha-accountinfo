import React from 'react';
import SettingContent from '../contents/setting-content';
import MobilePage from './mobile-page';

const SettingPage = ({
    resources,
    settingData,
    mobileView,
    redirectToBalanceInfoPage }) =>
    <MobilePage iconClass="icon-settings"
        featureText={resources.Setting}
        noInfoText={resources.NoInfo}
        redirectToBalanceInfoPage={redirectToBalanceInfoPage}
        data={settingData}
    >
        <SettingContent
            resources={resources}
            mobileView={mobileView}
            settingData={settingData}
        />
    </MobilePage>;

export default SettingPage;