import React from 'react';
import NumberFormat from 'react-number-format';
import CellContainNumberFormat from '../../common/cell-contain-number-format';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';
import { UserLevel } from '../../../constants';
import { mobileScale, resizePopupOnMobile } from '../../../helpers/accountinfo-helper';
import BalanceInfoRowContent from './balance-info-row-content';

export default class BalanceInfoContent extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenGivenCreditPopup = this.handleOpenGivenCreditPopup.bind(this);
    }

    handleOpenGivenCreditPopup(e) {
        let self = this;
        let resources = this.props.resources;
        let { mobileView, customerInfo } = this.props;
        let width = 470, height = 300;

        let givenCreditUrl = `${accountInfoSiteAppPath}SearchAccount/GivenCredit` +
            `?SearchAccountId=${customerInfo.CustId}&SearchAccountName=${customerInfo.UserName}` +
            `&SearchAccountRoleId=${customerInfo.RoleId}&CurrentGivenCredit=${customerInfo.BalanceInfo.GivenCredit}` +
            `&GivenCreditMin=${customerInfo.BalanceInfo.GivenCreditMin}&GivenCreditMax=${customerInfo.BalanceInfo.GivenCreditMax}`;

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
                window.top.realoadLeftPanel && self.props.fetchCustomerInfoFromServer(customerInfo.UserName);
            }
        }
    }

    buildMemberMaxCredit() {
        let { resources, customerInfo } = this.props;

        let balanceInfo = customerInfo.BalanceInfo;

        const content = <CellContainNumberFormat value={balanceInfo.MemberMaxCredit} />;
        const contentWithTooltip =
            <Tooltip
                title={balanceInfo.MemberMaxCreditLimit}
                position="bottom"
                trigger="mouseenter"
                arrow="true"
            >
                {content}
            </Tooltip>;

        return (
            <div className="row-info">
                <div className="col">{resources.MemberMaxCredit}</div>
                <div className="col col-right">
                    {
                        balanceInfo.ShowMemberMaxCreditLimit ? contentWithTooltip : content
                    }
                </div>
            </div>
        );
    }

    buildSharedProps() {
        let { resources, customerInfo, mobileView } = this.props;
        let balanceInfo = customerInfo.BalanceInfo;

        let sharedProps =
            {
                totalBalanceContent:
                    <BalanceInfoRowContent
                        title={resources.TotalBalance}
                        value={balanceInfo.TotalBalance}
                        hrefLink={balanceInfo.ShowTotalBalanceTransferLink ? balanceInfo.TotalBalanceTransferLink : ``}
                        mobileView={mobileView}
                    />,
                yesterdayBalanceContent:
                    <BalanceInfoRowContent
                        title={resources.YesterdayBalance}
                        value={balanceInfo.YesterdayBalance}
                        hrefLink={balanceInfo.ShowYesterdayBalanceTransferLink ? balanceInfo.YesterdayBalanceTransferLink : ``}
                        mobileView={mobileView}
                    />,
                reportContent:
                    <BalanceInfoRowContent
                        title={balanceInfo.ReportLink.Text}
                        value={balanceInfo.Outstanding}
                        hrefLink={balanceInfo.ShowOutstandingLink ? balanceInfo.ReportLink.Url : ``}
                        mobileView={mobileView}
                    />,
                givenCreditContent:
                    <div className="row-info">
                        <div className="col">{resources.GivenCredit}</div>
                        <div className="col col-right" onClick={this.handleOpenGivenCreditPopup} >
                            <div className="given-credit">
                                <span className="icon-border-color icon-edited" />
                                <NumberFormat
                                    className="number-edited"
                                    value={balanceInfo.GivenCredit}
                                    displayType={'text'}
                                    decimalScale={2}
                                    fixedDecimalScale={true}
                                    thousandSeparator={true}
                                />
                            </div>
                        </div>
                    </div>
            };

        return sharedProps;
    }

    getBalanceInfoContentsByLevel() {
        let { resources, customerInfo } = this.props;
        let balanceInfo = customerInfo.BalanceInfo;

        let sharedProps = this.buildSharedProps();

        const balanceInfoByLevel = [
            {
                Id: UserLevel.Master,
                Contents: [
                    <BalanceInfoRowContent
                        title={resources.TotalCash}
                        value={balanceInfo.TotalCash}
                    />,
                    <BalanceInfoRowContent
                        title={resources.YesterdayCash}
                        value={balanceInfo.YesterdayCash}
                    />,
                    sharedProps.totalBalanceContent,
                    sharedProps.yesterdayBalanceContent,
                    sharedProps.reportContent,
                    <BalanceInfoRowContent
                        title={resources.UsedCredit}
                        value={balanceInfo.UsedCredit}
                    />,
                    sharedProps.givenCreditContent
                ]
            },
            {
                Id: UserLevel.Agent,
                Contents: [
                    <BalanceInfoRowContent
                        title={resources.TotalCash}
                        value={balanceInfo.TotalCash}
                    />,
                    <BalanceInfoRowContent
                        title={resources.YesterdayCash}
                        value={balanceInfo.YesterdayCash}
                    />,
                    sharedProps.totalBalanceContent,
                    sharedProps.yesterdayBalanceContent,
                    sharedProps.reportContent,
                    <BalanceInfoRowContent
                        title={resources.UsedCredit}
                        value={balanceInfo.UsedCredit}
                    />,
                    sharedProps.givenCreditContent,
                    this.buildMemberMaxCredit()
                ]
            },
            {
                Id: UserLevel.Member,
                Contents: [
                    sharedProps.totalBalanceContent,
                    sharedProps.yesterdayBalanceContent,
                    sharedProps.reportContent,
                    sharedProps.givenCreditContent,
                    <BalanceInfoRowContent
                        title={resources.NetPosition}
                        value={balanceInfo.NetPosition}
                    />,
                    <BalanceInfoRowContent
                        title={resources.BetCredit}
                        value={balanceInfo.BetCredit}
                    />
                ]
            }
        ];

        return balanceInfoByLevel.find(level => customerInfo.RoleId === level.Id).Contents;
    }

    render() {
        return (
            <div className="balance-info">
                {
                    this.getBalanceInfoContentsByLevel()
                        .map((content, index) => {
                            return <div key={index}>{content} </div>;
                        })
                }
            </div>
        );
            
    }
}