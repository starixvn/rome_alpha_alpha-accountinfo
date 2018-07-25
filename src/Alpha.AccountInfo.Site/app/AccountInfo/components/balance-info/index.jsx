import React from 'react';
import Button from '../common/button';

import CellContainNumberFormat from '../common/cell-contain-number-format';
import CellContainInput from '../common/cell-contain-input';
import Cell from '../common/cell';
import CustomizedAccordionItem from '../common/customized-accordion-item';

import BalanceInfoContent from './balance-info-content'

export default class BalanceInfoBlock extends React.Component{
    constructor(props) {
        super(props);
        this.state = {
            username: '',
            url: window.accountInfoSiteAppPath + "accountinfo/getcustomerinfoasync01",
            data: null
        }

        this.handleClick = this.handleClick.bind(this);
    }

    handleCancelClick () { }

    handleSubmitClick ()  { }

    handleOutStandingChange () { }

    handleNewGivenCredit () { }

    handleGivenCredit () { }

    handleClick(e) {
        if (this.state.data === null) {
            this.setState({
                data: this.loadBalanceInfoFromServer()
            });
        }
    }

    loadBalanceInfoFromServer() {
        console.log("load balance info");

        return {
            TotalCash: 45,
            YesterdayCash: 56,
            TotalBalance: 4656,
            YesterdayBalance: 87,
            Outstanding: 1234,
            NetPostion: 749877,
            GivenCredit: 7897,
            BetCredit: 1234,
            MemMaxCredit_Max: 4156476,
            UsedCredit: 456,
            CurrentGivenCredit: 45674,
            Difference: 7897,
            NewGivenCredit: 45674

        }
    }

    render() {
        const rowClasses = "col-xs-12 row-info";
        const {
            headingText,
            iconClassname,
            handleClick,
            dataId,
            expanded
        } = this.props; 
 
        return (
            <CustomizedAccordionItem title="Balance Info" iconClassname="icon-balance"
                accordionItemBodyClassName="accordion__body balance-info" handleClick={this.handleClick}>
                {this.state.data != null && <BalanceInfoContent balanceInfo={this.state.data} /> }
            </CustomizedAccordionItem>
      );
    }
}