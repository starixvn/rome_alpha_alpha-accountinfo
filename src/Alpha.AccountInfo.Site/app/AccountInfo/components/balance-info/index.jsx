import React from 'react';
import Button from '../common/button';

import CellContainNumberFormat from '../common/cell-contain-number-format';
import CellContainInput from '../common/cell-contain-input';
import Cell from '../common/cell';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import BalanceInfoContent from './balance-info-content';

export default class BalanceInfoBlock extends React.Component{
    constructor(props) {
        super(props);
        this.state = {
            username: '',
            url: window.accountInfoSiteAppPath + "accountinfo/getcustomerinfo",
            data: this.props.balanceInfo
        }
    }

    render() {
        const {
            headingText,
            iconClassname,
            blockId,
            expanded,
            roleId,
            handleCollapExpandBlock
        } = this.props;

        const { data } = this.state;
 
        return (
            <CustomizedAccordionItem
                title="Balance Info"
                iconClassname="icon-balance"
                accordionItemBodyClassName="accordion__body balance-info"
                expanded={expanded}
                handleClick={handleCollapExpandBlock}
                hanldeGetData={this.hanldeGetData}
                blockId={blockId}>

                {data != null && <BalanceInfoContent balanceInfo={data} roleId={roleId} />}
            </CustomizedAccordionItem>
      );
    }
}