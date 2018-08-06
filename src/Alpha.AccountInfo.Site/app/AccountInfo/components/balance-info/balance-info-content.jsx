import React from 'react';
import NumberFormat from 'react-number-format';
import CellContainNumberFormat from '../common/cell-contain-number-format';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import Cash from './cash';
import DialogExampleModal from './dialog'
import GivenCreditModal from './given-credit-dialog'

export default class BalanceInfoContent extends React.Component{
    constructor(props) {
        super(props);
    }

    handleCancelClick () { }

    handleSubmitClick ()  { }

    handleOutStandingChange () { }

    handleNewGivenCredit () { }

    handleGivenCredit () { }

    render() {
        const rowClasses = "col-xs-12 row-info";

        const {
            custId,
            username,
            roleId,
            balanceInfo,
            fetchCustomerInfoFromServer
        } = this.props;

        return (
            <div>
                {
                    this.props.roleId > 1
                    && <Cash TotalCash={this.props.balanceInfo.TotalCash} YesterdayCash={this.props.balanceInfo.YesterdayCash} />
                }

                <div className={rowClasses}>
                    <div className="col-xs-6">Total Balance</div>
                    <CellContainNumberFormat title={this.props.balanceInfo.TotalBalance} classNames="col-xs-6 col-right" />
                </div>

                <div className={rowClasses}>
                    <div className="col-xs-6">Yesterday Balance</div>
                    <CellContainNumberFormat title={this.props.balanceInfo.YesterdayBalance} classNames="col-xs-6 col-right" />
                </div>

                <div className={rowClasses}>
                    <div className="col-xs-6">Outstanding</div>
                    <div className="col-xs-6 col-right">
                        <a href="#" className="link">{this.props.balanceInfo.Outstanding}</a>
                    </div>
                    
                </div>

                {
                    this.props.roleId > 1 &&
                    <div className={rowClasses}>
                        <div className="col-xs-6">Used Credit</div>
                        <CellContainNumberFormat title={this.props.balanceInfo.UsedCredit} classNames="col-xs-6 col-right" />
                    </div>
                }

                {
                    this.props.roleId == 1 &&
                    <div className={rowClasses}>
                        <div className="col-xs-6">Net Position</div>
                        <CellContainNumberFormat title={this.props.balanceInfo.NetPosition} classNames="col-xs-6 col-right" />
                    </div>
                }

                <GivenCreditModal
                    fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                    balanceInfo={balanceInfo}
                    custId={custId}
                    roleId={roleId}
                    username={username} />
            </div>
      );
    }
}