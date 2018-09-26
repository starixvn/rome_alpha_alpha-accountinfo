import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import SettingContent from './setting-content';

const SettingDesktop = ({
    settingData,
    resources,
    expanded,
    blockId,
    handleCollapExpandBlock }) => settingData === null ?
        <div className="no-information">{resources.NoInfo}</div> :
        <CustomizedAccordionItem
            title={resources.Setting}
            iconClassname="icon-settings"
            accordionItemBodyClassName="accordion__body settings"
            expanded={expanded}
            handleClick={handleCollapExpandBlock}
            blockId={blockId}
        >
            <SettingContent resources={resources} settingData={settingData} />
        </CustomizedAccordionItem>;

export default SettingDesktop;