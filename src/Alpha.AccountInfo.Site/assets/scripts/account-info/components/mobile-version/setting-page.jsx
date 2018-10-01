import React from 'react';
import { NoInformFeature } from './no-inform-feature';
import { HeaderFeature } from './header-feature';
import SettingContent from '../contents/setting-content';

const SettingPage = ({ resources, settingData, mobileView, redirectToBalanceInfoPage }) =>
    settingData === null ?
        <NoInformFeature url="/featureList" iconClass="icon-settings" featureText={resources.Setting} noInform={resources.NoInfo} /> :
        <div className="feature-detail translate-from-left">
            <HeaderFeature url="/featureList" iconClass="icon-status" featureText={resources.Setting} redirectToBalanceInfoPage={redirectToBalanceInfoPage} />
            <SettingContent resources={resources} mobileView={mobileView} settingData={settingData} />
        </div>;

export default SettingPage;