import React from 'react';
import CellContainNumberFormat from './common/cell-contain-number-format';
import NumberFormat from 'react-number-format';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';
import { Link } from 'react-router-dom';
import { NoInfo } from '../components/common/no-info';

export default class BalanceInfoBlock extends React.Component {
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
                window.top.realoadLeftPanel && self.props.fetchCustomerInfoFromServer(e, customerInfo.UserName);
                clearInterval(timer);
            }
        }
    }

    render() {
        const { resources, customerInfo } = this.props;
        const header = <Link to="/" className="feature-item">
            <span className="icon-balance" />
            {resources.BalanceInfo}
        </Link>;

        if (customerInfo !== null) {
            const rowClasses = "col-xs-12 row-info";
            const { resources } = this.props;
            const roleId = this.props.customerInfo.RoleId;
            const balanceInfo = this.props.customerInfo.BalanceInfo;

            const memberLevel = 1, agentLevel = 2, masterLevel = 3;
            const totalBalanceTransferLink = balanceInfo.ShowTotalBalanceTransferLink ? balanceInfo.TotalBalanceTransferLink : "";
            const yesterdayBalanceTransferLink = balanceInfo.ShowYesterdayBalanceTransferLink ? balanceInfo.YesterdayBalanceTransferLink : "";
            const outstandingLink = balanceInfo.ShowOutstandingLink ? balanceInfo.ReportLink.Url : "";

            return (
                <div className="feature-detail balance-info">
                    {header}
                    {
                        roleId > memberLevel &&
                        <div className={rowClasses}>
                            <div className="col-xs-6">{resources.TotalCash}</div>
                            <CellContainNumberFormat title={balanceInfo.TotalCash} classNames="col-xs-6 col-right" />
                        </div>
                    }

                    {
                        roleId > memberLevel &&
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
                            hrefLink={totalBalanceTransferLink}
                            target="main"
                        />
                    </div>

                    <div className={rowClasses}>
                        <div className="col-xs-6">{resources.YesterdayBalance}</div>
                        <CellContainNumberFormat
                            title={balanceInfo.YesterdayBalance}
                            classNames="col-xs-6 col-right"
                            hrefLink={yesterdayBalanceTransferLink}
                            target="main"
                        />
                    </div>

                    <div className={rowClasses}>
                        <div className="col-xs-6">
                            {balanceInfo.ReportLink.Text}
                        </div>
                        <CellContainNumberFormat
                            title={balanceInfo.Outstanding}
                            classNames="col-xs-6 col-right"
                            hrefLink={outstandingLink}
                            target="main"
                        />
                    </div>

                    {
                        roleId > memberLevel &&
                        <div className={rowClasses}>
                            <div className="col-xs-6">{resources.UsedCredit}</div>
                            <CellContainNumberFormat title={balanceInfo.UsedCredit} classNames="col-xs-6 col-right" />
                        </div>
                    }

                    <div className={rowClasses}>
                        <div className="col-xs-6">{resources.GivenCredit}</div>
                        <div className="col-xs-6 col-right given-credit" onClick={this.handleOpenGivenCreditPopup} >
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
                                    decimalScale="2"
                                    fixedDecimalScale={true}
                                    thousandSeparator={true}
                                />
                            </Tooltip>
                        </div>
                    </div>

                    {
                        roleId === agentLevel &&
                        <div className={rowClasses}>
                            <div className="col-xs-6">{resources.MemberMaxCredit}</div>
                            <div className="col-xs-6 col-right">
                                {
                                    balanceInfo.ShowMemberMaxCreditLimit &&
                                    <Tooltip
                                        title={balanceInfo.MemberMaxCreditLimit}
                                        position="bottom"
                                        trigger="mouseenter"
                                        arrow="true"
                                    >
                                        <CellContainNumberFormat title={balanceInfo.MemberMaxCredit} />
                                    </Tooltip>
                                }
                                {
                                    !balanceInfo.ShowMemberMaxCreditLimit &&
                                    <CellContainNumberFormat title={balanceInfo.MemberMaxCredit} />
                                }
                            </div>
                        </div>
                    }

                    {
                        roleId === memberLevel &&
                        <div className={rowClasses}>
                            <div className="col-xs-6">{resources.NetPosition}</div>
                            <CellContainNumberFormat title={balanceInfo.NetPosition} classNames="col-xs-6 col-right" />
                        </div>
                    }

                    {
                        roleId === memberLevel &&
                        <div className={rowClasses}>
                            <div className="col-xs-6">{resources.BetCredit}</div>
                            <CellContainNumberFormat title={balanceInfo.BetCredit} classNames="col-xs-6 col-right" />
                        </div>
                    }
                </div>
            );
        }
        else {
            return (
                <div>
                    {header}
                    <NoInfo text={resources.NoInfo} />
                </div>
            );
        }
    }
}