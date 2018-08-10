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
            resources,
            custId,
            username,
            roleId,
            balanceInfo,
            fetchCustomerInfoFromServer
        } = this.props;


        // TO DO : lblMemberMaxCredit
        return (
            <div>
                {
                    roleId > 1 &&
                    <div className={rowClasses}>
                        <div className="col-xs-6">{resources.TotalCash}</div>
                        <CellContainNumberFormat title={balanceInfo.TotalCash} classNames="col-xs-6 col-right" />
                    </div>
                }

                {
                    roleId > 1 &&
                    <div className={rowClasses}>
                        <div className="col-xs-6">{resources.YesterdayCash}</div>
                        <CellContainNumberFormat title={balanceInfo.YesterdayCash} classNames="col-xs-6 col-right" />
                    </div>
                }

                <div className={rowClasses}>
                    <div className="col-xs-6">{resources.TotalBalance}</div>
                    <CellContainNumberFormat
                        title={balanceInfo.TotalBalance}
                        classNames="col-xs-6 col-right"
                        hrefLink={balanceInfo.ShowTransferLink && balanceInfo.AllowedTotalBalanceTransfer ? balanceInfo.TransferLink : null}
                        target="main"/>
                </div>

                <div className={rowClasses}>
                    <div className="col-xs-6">{resources.YesterdayBalance}</div>
                    <CellContainNumberFormat
                        title={balanceInfo.YesterdayBalance}
                        classNames="col-xs-6 col-right"
                        hrefLink={balanceInfo.ShowTransferLink && !balanceInfo.AllowedTotalBalanceTransfer ? balanceInfo.TransferLink : null}
                        target="main" />
                </div>

                <div className={rowClasses}>
                    <div className="col-xs-6">
                    {
                            (roleId == 3 && resources.MasterOutstanding) ||
                            (roleId == 2 && resources.AgentOutstanding) ||
                            (roleId == 1 && resources.Outstanding)
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
                        <div className="col-xs-6">{resources.UsedCredit}</div>
                        <CellContainNumberFormat title={balanceInfo.UsedCredit} classNames="col-xs-6 col-right" />
                    </div>
                }
                
                <GivenCreditModal
                    resources={resources}
                    fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                    balanceInfo={balanceInfo}
                    custId={custId}
                    roleId={roleId}
                    username={username} />

                {
                    roleId == 1 &&
                    <div className={rowClasses}>
                        <div className="col-xs-6">{resources.NetPosition}</div>
                        <CellContainNumberFormat title={balanceInfo.NetPosition} classNames="col-xs-6 col-right" />
                    </div>
                }

                {
                    roleId == 1 &&
                    <div className={rowClasses}>
                        <div className="col-xs-6">{resources.BetCredit}</div>
                        <CellContainNumberFormat title={balanceInfo.BetCredit} classNames="col-xs-6 col-right" />
                    </div>
                }

            </div>
      );
    }
}