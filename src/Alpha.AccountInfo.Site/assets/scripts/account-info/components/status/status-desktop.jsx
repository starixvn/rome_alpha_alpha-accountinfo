import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import StatusContent from './status-content';

const StatusDesktop = ({
    statusData,
    resources,
    username,
    fetchCustomerInfoFromServer,
    expanded,
    blockId,
    handleCollapExpandBlock }) => statusData === null ?
        <div className="no-information">{resources.NoInfo}</div> :
        <CustomizedAccordionItem
            title={resources.Status}
            iconClassname="icon-status"
            accordionItemBodyClassName="accordion__body status"
            expanded={expanded}
            handleClick={handleCollapExpandBlock}
            blockId={blockId}
        >
            <StatusContent resources={resources} statusData={statusData} username={username} fetchCustomerInfoFromServer={fetchCustomerInfoFromServer} />
        </CustomizedAccordionItem>;

export default StatusDesktop;