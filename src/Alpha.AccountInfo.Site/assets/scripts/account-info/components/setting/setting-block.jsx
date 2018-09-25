import React from 'react';
import { NoInformFeature } from '../../components/common/no-inform-feature';
import { HeaderFeature } from '../../components/common/header-feature';
import SettingContent from './setting-content';

const SettingBlock = ({ resources, settingData }) => settingData === null ?
    <NoInformFeature url="/" iconClass="icon-settings" featureText={resources.Setting} noInform={resources.NoInfo} /> :
    <div className="feature-detail block-settings">
        <HeaderFeature url="/" iconClass="icon-status" featureText={resources.Setting} />
        <SettingContent resources={resources} settingData={settingData} />
    </div>;

export default SettingBlock;