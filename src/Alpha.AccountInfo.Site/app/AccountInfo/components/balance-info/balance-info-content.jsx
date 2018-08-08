import React from 'react';
import NumberFormat from 'react-number-format';
import CellContainNumberFormat from '../common/cell-contain-number-format';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import DialogExampleModal from './dialog'
import GivenCreditModal from './given-credit-dialog'

export default class BalanceInfoContent extends React.Component{
    constructor(props) {
        super(props);
    }

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
                    roleId > 1 &&
                    <div className={rowClasses}>
                        <div className="col-xs-6">Yesterday Cash</div>
                        <CellContainNumberFormat title={balanceInfo.YesterdayCash} classNames="col-xs-6 col-right" />
                    </div>
                }

                {
                    roleId > 1 &&
                    <div className={rowClasses}>
                        <div className="col-xs-6">Yesterday Cash</div>
                        <CellContainNumberFormat title={balanceInfo.YesterdayCash} classNames="col-xs-6 col-right" />
                    </div>
                }

                <div className={rowClasses}>
                    <div className="col-xs-6">Total Balance</div>
                    <CellContainNumberFormat
                        title={balanceInfo.TotalBalance}
                        classNames="col-xs-6 col-right"
                        hrefLink={balanceInfo.ShowTransferLink && balanceInfo.AllowedTotalBalanceTransfer ? balanceInfo.TransferLink : null}
                        target="main"/>
                </div>

                <div className={rowClasses}>
                    <div className="col-xs-6">Yesterday Balance</div>
                    <CellContainNumberFormat
                        title={balanceInfo.YesterdayBalance}
                        classNames="col-xs-6 col-right"
                        hrefLink={balanceInfo.ShowTransferLink && !balanceInfo.AllowedTotalBalanceTransfer ? balanceInfo.TransferLink : null}
                        target="main" />
                </div>

                <div className={rowClasses}>
                    <div className="col-xs-6">
                    {
                            (roleId == 3 && "Master Outstanding") ||
                            (roleId == 2 && "Agent Outstanding") ||
                            (roleId == 1 && "Outstanding")
                    }
                    </div>
                    <CellContainNumberFormat
                        title={balanceInfo.Outstanding}
                        classNames="col-xs-6 col-right"
                        hrefLink={balanceInfo.ShowOutstandingLink ? balanceInfo.OutstandingLink : null}
                        target="main" />
                    
                </div>

                {
                    roleId > 1 &&
                    <div className={rowClasses}>
                        <div className="col-xs-6">Used Credit</div>
                        <CellContainNumberFormat title={balanceInfo.UsedCredit} classNames="col-xs-6 col-right" />
                    </div>
                }

                {
                    roleId == 1 &&
                    <div className={rowClasses}>
                        <div className="col-xs-6">Net Position</div>
                        <CellContainNumberFormat title={balanceInfo.NetPosition} classNames="col-xs-6 col-right" />
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