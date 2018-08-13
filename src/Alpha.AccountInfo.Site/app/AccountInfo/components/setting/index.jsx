import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import SettingContent from './setting-list-content';

export default class SettingBlock extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { resources, blockId, expanded, settingData, getCustomerAllStatus } = this.props;

        return (
            <CustomizedAccordionItem
                title={resources.Setting}
                iconClassname="icon-settings"
                expanded={expanded}
                handleClick={getCustomerAllStatus}
                blockId={blockId}>
                {settingData != null && <SettingContent resources={resources} setting={settingData} oldMainPath={settingData.OldMainPath}/>}
            </CustomizedAccordionItem>
        );
    }
}