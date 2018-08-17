import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import SettingContent from './setting-list-content';

const SettingBlock = ({ resources, blockId, expanded, settingData, getCustomerAllStatus }) =>
    <CustomizedAccordionItem
        title={resources.Setting}
        iconClassname="icon-settings"
        expanded={expanded}
        handleClick={getCustomerAllStatus}
        blockId={blockId}>
        {settingData != null && <SettingContent resources={resources} settings={settingData.Settings} />}
    </CustomizedAccordionItem>

export default SettingBlock;
