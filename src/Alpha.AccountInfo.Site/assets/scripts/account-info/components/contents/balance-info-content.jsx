import React from 'react';
import CellContainNumberFormat from '../common/cell-contain-number-format';
import NumberFormat from 'react-number-format';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';
import { UserLevel } from '../../constants/user-level';

export default class BalanceInfoContent extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenGivenCreditPopup = this.handleOpenGivenCreditPopup.bind(this);
    }

    handleOpenGivenCreditPopup(e) {
        let self = this;
        let resources = this.props.resources;
        let customerInfo = this.props.customerInfo;

        let givenCreditUrl = `${accountInfoSiteAppPath}AccountInfo/GivenCredit?SearchAccountId=${customerInfo.CustId}&SearchAccountName=${customerInfo.UserName}&SearchAccountRoleId=${customerInfo.RoleId}&CurrentGivenCredit=${customerInfo.BalanceInfo.GivenCredit}`;
        window.top.ageWnd.open(givenCreditUrl, resources.GivenCreditPopupTitle, undefined, undefined, 350, 270);

        window.top.closedPopup = false;
        window.top.realoadLeftPanel = false;
        let timer = setInterval(reloadBalanceInfo, 1000);

        function reloadBalanceInfo(e) {
            if (window.top.closedPopup) {
                clearInterval(timer);
                window.top.realoadLeftPanel && self.props.fetchCustomerInfoFromServer(e, customerInfo.UserName);
            }
        }
    }

    render() {
        const { resources, customerInfo } = this.props;
        const rowClasses = "row-info";
        const roleId = this.props.customerInfo.RoleId;
        const balanceInfo = this.props.customerInfo.BalanceInfo;

        const totalBalanceTransferLink = balanceInfo.ShowTotalBalanceTransferLink ? balanceInfo.TotalBalanceTransferLink : "";
        const yesterdayBalanceTransferLink = balanceInfo.ShowYesterdayBalanceTransferLink ? balanceInfo.YesterdayBalanceTransferLink : "";
        const outstandingLink = balanceInfo.ShowOutstandingLink ? balanceInfo.ReportLink.Url : "";

        return (
            <div className="balance-info">
                {
                    roleId > UserLevel.Member &&
                    <div className={rowClasses}>
                        <div className="col">{resources.TotalCash}</div>
                        <CellContainNumberFormat value={balanceInfo.TotalCash} classNames="col col-right" />
                    </div>
                }

                {
                    roleId > UserLevel.Member &&
                    <div className={rowClasses}>
                        <div className="col">{resources.YesterdayCash}</div>
                        <CellContainNumberFormat value={balanceInfo.YesterdayCash} classNames="col col-right" />
                    </div>
                }

                <div className={rowClasses}>
                    <div className="col">{resources.TotalBalance}</div>
                    <CellContainNumberFormat
                        value={balanceInfo.TotalBalance}
                        classNames="col col-right"
                        hrefLink={totalBalanceTransferLink}
                        target="main"
                    />
                </div>

                <div className={rowClasses}>
                    <div className="col">{resources.YesterdayBalance}</div>
                    <CellContainNumberFormat
                        value={balanceInfo.YesterdayBalance}
                        classNames="col col-right"
                        hrefLink={yesterdayBalanceTransferLink}
                        target="main"
                    />
                </div>

                <div className={rowClasses}>
                    <div className="col">
                        {balanceInfo.ReportLink.Text}
                    </div>
                    <CellContainNumberFormat
                        value={balanceInfo.Outstanding}
                        classNames="col col-right"
                        hrefLink={outstandingLink}
                        target="main"
                    />
                </div>

                {
                    roleId > UserLevel.Member &&
                    <div className={rowClasses}>
                        <div className="col">{resources.UsedCredit}</div>
                        <CellContainNumberFormat value={balanceInfo.UsedCredit} classNames="col col-right" />
                    </div>
                }

                <div className={rowClasses}>
                    <div className="col">{resources.GivenCredit}</div>
                    <div className="col col-right" onClick={this.handleOpenGivenCreditPopup} >
                        <div className="given-credit">
                            <span className="icon-border-color icon-edited" />
                            <Tooltip
                                title={balanceInfo.GivenCreditLimit}
                                position="bottom"
                                trigger="mouseenter"
                                arrow="true">
                                <NumberFormat
                                    className="number-edited"
                                    value={balanceInfo.GivenCredit}
                                    displayType={'text'}
                                    decimalScale={2}
                                    fixedDecimalScale={true}
                                    thousandSeparator={true}
                                />
                            </Tooltip>
                        </div>
                    </div>
                </div>

                {
                    roleId === UserLevel.Agent &&
                    <div className={rowClasses}>
                        <div className="col">{resources.MemberMaxCredit}</div>
                        <div className="col col-right">
                            {
                                balanceInfo.ShowMemberMaxCreditLimit &&
                                <Tooltip
                                    title={balanceInfo.MemberMaxCreditLimit}
                                    position="bottom"
                                    trigger="mouseenter"
                                    arrow="true"
                                >
                                    <CellContainNumberFormat value={balanceInfo.MemberMaxCredit} />
                                </Tooltip>
                            }
                            {
                                !balanceInfo.ShowMemberMaxCreditLimit &&
                                <CellContainNumberFormat value={balanceInfo.MemberMaxCredit} />
                            }
                        </div>
                    </div>
                }

                {
                    roleId === UserLevel.Member &&
                    <div className={rowClasses}>
                        <div className="col">{resources.NetPosition}</div>
                        <CellContainNumberFormat value={balanceInfo.NetPosition} classNames="col col-right" />
                    </div>
                }

                {
                    roleId === UserLevel.Member  &&
                    <div className={rowClasses}>
                        <div className="col">{resources.BetCredit}</div>
                        <CellContainNumberFormat value={balanceInfo.BetCredit} classNames="col col-right" />
                    </div>
                }
            </div>
        );
           
    }
}