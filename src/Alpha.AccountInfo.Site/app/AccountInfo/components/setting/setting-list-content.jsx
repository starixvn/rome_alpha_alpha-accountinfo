import React from 'react';
import SettingInfo from './setting-info';

const SettingContent = ({ resources, setting, oldMainPath }) => 
    <SettingInfo resources={resources} settings={setting.Settings} oldMainPath={oldMainPath} />

export default SettingContent;