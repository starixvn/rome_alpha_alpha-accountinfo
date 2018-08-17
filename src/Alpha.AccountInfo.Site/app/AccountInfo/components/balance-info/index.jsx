import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import BalanceInfoContent from './balance-info-content';

const BalanceInfoBlock = ({
    resources,
    headingText,
    iconClassname,
    blockId,
    expanded,
    username,
    custId,
    roleId,
    handleCollapExpandBlock,
    fetchCustomerInfoFromServer,
    balanceInfo }) =>
    <CustomizedAccordionItem
        title={resources.BalanceInfo}
        iconClassname="icon-balance"
        accordionItemBodyClassName="accordion__body balance-info"
        expanded={expanded}
        handleClick={handleCollapExpandBlock}
        blockId={blockId}>

        {balanceInfo != null &&
            <BalanceInfoContent
                resources={resources}
                fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                balanceInfo={balanceInfo}
                custId={custId}
                roleId={roleId}
                username={username} />}
    </CustomizedAccordionItem>

export default BalanceInfoBlock;