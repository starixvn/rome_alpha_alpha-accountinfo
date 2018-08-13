import React from 'react';
import Status from './status';
import StatusOnProduct from './status-on-product';
import CustomizedAccordionItem from '../common/customized-accordion-item';

export default class StatusBlock extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { resources, getCustomerAllStatus, blockId, expanded, statusData } = this.props;

        return (
            <CustomizedAccordionItem
                title={resources.Status}
                iconClassname="icon-status"
                accordionItemBodyClassName="accordion__body block-status"
                expanded={expanded}
                handleClick={getCustomerAllStatus}
                blockId={blockId}>
                {statusData != null && statusData.Settings.map((status, index) => {
                    return status.IsEditOnProductLevel != undefined && !status.IsEditOnProductLevel ?
                        <StatusOnProduct resources={resources}
                            title={status.Text}
                            url={status.Url}
                            confirmMessage={status.ConfirmMessage}
                            disabled={status.Disabled}
                            key={index}
                            statusId={status.DisplayOrder}/> :
                        <Status resources={resources}
                            classNames='col checkbox'
                            title={status.Text}
                            url={status.Url}
                            confirmMessage={status.ConfirmMessage}
                            disabled={status.Disabled}
                            checked={status.Checked}
                            key={index}
                            statusId={status.DisplayOrder} /> 
                      
                })}
            </CustomizedAccordionItem>
      );
    }
}