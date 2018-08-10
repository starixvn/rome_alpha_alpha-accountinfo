import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import BalanceInfoContent from './balance-info-content';

export default class BalanceInfoBlock extends React.Component{
    constructor(props) {
        super(props);
    }

    render() {
        const {
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
            balanceInfo
        } = this.props;

        return (
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
      );
    }
}