import React from 'react';
import NumberFormat from 'react-number-format';
import CellContainNumberFormat from '../common/cell-contain-number-format';
import CustomizedAccordionItem from '../common/customized-accordion-item';

export default class BalanceInfoContent extends React.Component{
    constructor(props) {
        super(props);
        this.state = {
            resources: this.props.resources
        }
        this.handleOpen = this.handleOpen.bind(this);
    }

    handleOpen(e) {
        let givenCreditUrl = `${accountInfoSiteAppPath}AccountInfo/GivenCredit?SearchAccountId=${this.props.custId}&SearchAccountName=${this.props.username}&SearchAccountRoleId=${this.props.roleId}&CurrentGivenCredit=${this.props.balanceInfo.GivenCredit}`;
        window.top.ageWnd.open(givenCreditUrl, this.state.resources.GivenCreditPopupTitle, undefined, undefined, 330, 248);

        window.top.closeGivenCreditPopup = false;
        let timer = setInterval(reloadBalanceInfo, 1000);
        let self = this;

        function reloadBalanceInfo(e) {
            if (window.top.closeGivenCreditPopup) {
                self.props.fetchCustomerInfoFromServer(e, self.props.username);
                clearInterval(timer);
            }
        }
    };

    render() {
        const rowClasses = "col-xs-12 row-info";

        const {
            resources,
            custId,
            username,
            roleId,
            balanceInfo
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

                <div className={rowClasses}>
                    <div className="col-xs-6">{resources.GivenCredit}</div>
                    <div className="col-xs-6 col-right given-credit" title={balanceInfo.GivenCreditLimit} onClick={this.handleOpen} >
                        <NumberFormat
                            className="number-edited"
                            value={balanceInfo.GivenCredit}
                            displayType={'text'}
                            decimalScale="2"
                            fixedDecimalScale={true}
                            thousandSeparator={true} />

                        <span className="icon-border-color icon-edited" />
                    </div>
                </div>

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