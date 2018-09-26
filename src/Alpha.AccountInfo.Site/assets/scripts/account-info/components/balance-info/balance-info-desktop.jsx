import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import BalanceInfoContent from './balance-info-content';

const BalanceInfoDesktop = ({
    customerInfo,
    resources,
    fetchCustomerInfoFromServer,
    expanded,
    blockId,
    handleCollapExpandBlock }) => customerInfo === null ?
    <div className="no-information">{resources.NoInfo}</div> :
    <CustomizedAccordionItem
        title={resources.BalanceInfo}
        iconClassname="icon-balance"
        accordionItemBodyClassName="accordion__body balance-info"
        expanded={expanded}
        handleClick={handleCollapExpandBlock}
        blockId={blockId}
    >
        <BalanceInfoContent resources={resources} customerInfo={customerInfo} fetchCustomerInfoFromServer={fetchCustomerInfoFromServer} />
    </CustomizedAccordionItem>;

export default BalanceInfoDesktop;
