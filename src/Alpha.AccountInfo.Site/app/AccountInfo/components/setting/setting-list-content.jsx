import React from 'react';
import SettingInfo from './setting-info';

const SettingContent = ({ setting, oldMainPath }) => 
    <SettingInfo settings={setting.Settings} oldMainPath={oldMainPath} />

export default SettingContent;