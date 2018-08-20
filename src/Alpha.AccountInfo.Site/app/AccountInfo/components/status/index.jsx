import React from 'react';
import StatusCheckbox from './status-checkbox';
import StatusOnProduct from './status-on-product';
import CustomizedAccordionItem from '../common/customized-accordion-item';

const StatusBlock = ({ resources, username, getCustomerAllStatus, fetchCustomerInfoFromServer, blockId, expanded, statusData }) =>
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
                    (status.Visible && <StatusOnProduct resources={resources}
                        title={status.Text}
                        url={status.Url}
                        confirmMessage={status.ConfirmMessage}
                        disabled={status.Disabled}
                        key={index}
                        statusId={status.DisplayOrder} />) :
                    (status.Visible && <StatusCheckbox resources={resources}
                        fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                        username={username}
                        classNames='col checkbox'
                        title={status.Text}
                        url={status.Url}
                        param={status.Param}
                        confirmMessage={status.ConfirmMessage}
                        disabled={status.Disabled}
                        checked={status.Checked}
                        key={index}
                        statusId={status.DisplayOrder} />)

            })}
    </CustomizedAccordionItem>

export default StatusBlock;
