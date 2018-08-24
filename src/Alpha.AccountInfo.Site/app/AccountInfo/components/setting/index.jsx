import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import Paging from '../common/paging';

export default class SettingBlock extends React.Component {
    constructor(props) {
        super(props);
    }

    renderCallback(value) {
        return <a href={value.Url} target="main">{value.Text}</a>;
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
                {
                    settingData != null &&
                    <Paging resourcePage={this.props.resources.Page} values={settingData.Settings} renderCallback={this.renderCallback} />
                }

            </CustomizedAccordionItem>
           
        )
    }
}
