import React from 'react';
import CellContainNumberFormat from '../common/cell-contain-number-format';
import NumberFormat from 'react-number-format';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';
import { UserLevel } from '../../constants/user-level';
import { mobileScale, resizePopupOnMobile } from '../../../helpers/accountinfo-helper';

export default class BalanceInfoContent extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenGivenCreditPopup = this.handleOpenGivenCreditPopup.bind(this);
    }

    handleOpenGivenCreditPopup(e) {
        let self = this;
        let resources = this.props.resources;
        let { mobileView, customerInfo } = this.props;

        let givenCreditUrl = `${accountInfoSiteAppPath}AccountInfo/GivenCredit?SearchAccountId=${customerInfo.CustId}&SearchAccountName=${customerInfo.UserName}&SearchAccountRoleId=${customerInfo.RoleId}&CurrentGivenCredit=${customerInfo.BalanceInfo.GivenCredit}`;
        let width = 370, height = 270;

        if (mobileView) {
            width = mobileScale(width);
            height = mobileScale(height);
        }

        window.top.ageWnd.open(givenCreditUrl, resources.GivenCreditPopupTitle, undefined, undefined, width, height);

        if (mobileView) {
            resizePopupOnMobile();
        }

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
        let { resources, customerInfo, rowClasses = "row-info" } = this.props;
        let roleId = this.props.customerInfo.RoleId;
        let balanceInfo = this.props.customerInfo.BalanceInfo;

        let searchAccountIsMember = roleId === UserLevel.Member;
        let searchAccountIsNotMember = !searchAccountIsMember;

        let row = (title, value, href = null) =>
            <div className={rowClasses}>
                <div className="col">{title}</div>
                <CellContainNumberFormat value={value} classNames="col col-right" hrefLink={href} target={href !== null ? `main` : null} />
            </div>;

        return (
            <div className="balance-info">
                {searchAccountIsNotMember && row(resources.TotalCash, balanceInfo.TotalCash)}
                {searchAccountIsNotMember && row(resources.YesterdayCash, balanceInfo.YesterdayCash)}

                {row(resources.TotalBalance, balanceInfo.TotalBalance, balanceInfo.ShowTotalBalanceTransferLink ? balanceInfo.TotalBalanceTransferLink : ``)}

                {row(resources.YesterdayBalance, balanceInfo.YesterdayBalance,balanceInfo.ShowYesterdayBalanceTransferLink ? balanceInfo.YesterdayBalanceTransferLink :``)}

                {row(balanceInfo.ReportLink.Text,balanceInfo.Outstanding,balanceInfo.ShowOutstandingLink ? balanceInfo.ReportLink.Url : ``)}

                {searchAccountIsNotMember && row(resources.UsedCredit, balanceInfo.UsedCredit)}

                <div className={rowClasses}>
                    <div className="col">{resources.GivenCredit}</div>
                    <div className="col col-right" onClick={this.handleOpenGivenCreditPopup} >
                        <div className="given-credit">
                            <span className="icon-border-color icon-edited" />
                            <Tooltip
                                title={balanceInfo.GivenCreditLimit}
                                position="bottom"
                                trigger="mouseenter"
                                arrow="true"
                            >
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

                {searchAccountIsMember && row(resources.NetPosition, balanceInfo.NetPosition)}
                {searchAccountIsMember && row(resources.BetCredit, balanceInfo.BetCredit)}
            </div>
        );

    }
}