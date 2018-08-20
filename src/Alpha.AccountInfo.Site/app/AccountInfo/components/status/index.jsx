import React from 'react';
import StatusCheckbox from './status-checkbox';
import StatusOnProduct from './status-on-product';
import CustomizedAccordionItem from '../common/customized-accordion-item';

const StatusBlock = ({
    resources,
    username,
    getCustomerAllStatus,
    fetchCustomerAllStatusFromServer,
    fetchCustomerInfoFromServer,
    blockId,
    expanded,
    statusData }) =>
    <CustomizedAccordionItem
        title={resources.Status}
        iconClassname="icon-status"
        accordionItemBodyClassName="accordion__body block-status"
        expanded={expanded}
        handleClick={getCustomerAllStatus}
        blockId={blockId}>
        {
            statusData != null && statusData.Settings.map((status, index) => {
                return status.IsEditOnProductLevel != undefined && !status.IsEditOnProductLevel ?
                    (status.Visible &&
                        <StatusOnProduct resources={resources} title={status.Text} url={status.Url} key={index} />) :
                    (status.Visible &&
                        <StatusCheckbox resources={resources}
                        fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                        fetchCustomerAllStatusFromServer={fetchCustomerAllStatusFromServer}
                        username={username}
                        status={status}
                        displayOrder={status.DisplayOrder}
                        key={index}/>)

            })
        }
    </CustomizedAccordionItem>

export default StatusBlock;
