import React from 'react';
import CustomizedAccordionItem from './common/customized-accordion-item';
import CellContainNumberFormat from './common/cell-contain-number-format';
import NumberFormat from 'react-number-format';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';

export default class BalanceInfoBlock extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenGivenCreditPopup = this.handleOpenGivenCreditPopup.bind(this);
    }

    handleOpenGivenCreditPopup(e) {
        let self = this;
        let givenCreditUrl = `${accountInfoSiteAppPath}AccountInfo/GivenCredit?SearchAccountId=${this.props.custId}&SearchAccountName=${this.props.username}&SearchAccountRoleId=${this.props.roleId}&CurrentGivenCredit=${this.props.balanceInfo.GivenCredit}`;
        window.top.ageWnd.open(givenCreditUrl, this.props.resources.GivenCreditPopupTitle, undefined, undefined, 330, 248);

        window.top.closedPopup = false;
        window.top.realoadLeftPanel = false;
        let timer = setInterval(reloadBalanceInfo, 1000);

        function reloadBalanceInfo(e) {
            if (window.top.closedPopup) {
                window.top.realoadLeftPanel && self.props.fetchCustomerInfoFromServer(e, self.props.username);
                clearInterval(timer);
            }
        }
    };

    render() {
        const rowClasses = "col-xs-12 row-info";
        const { resources,
            iconClassname,
            blockId,
            expanded,
            roleId,
            handleCollapExpandBlock,
            balanceInfo } = this.props;

        const memberLevel = 1, agentLevel = 2, masterLevel = 3;

        return (
            <CustomizedAccordionItem
                title={resources.BalanceInfo}
                iconClassname="icon-balance"
                accordionItemBodyClassName="accordion__body balance-info"
                expanded={expanded}
                handleClick={handleCollapExpandBlock}
                blockId={blockId}>
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
                        hrefLink={balanceInfo.ShowTotalBalanceTransferLink && balanceInfo.TotalBalanceTransferLink}
                        target="main" />
                </div>

                <div className={rowClasses}>
                    <div className="col-xs-6">{resources.YesterdayBalance}</div>
                    <CellContainNumberFormat
                        title={balanceInfo.YesterdayBalance}
                        classNames="col-xs-6 col-right"
                        hrefLink={balanceInfo.ShowYesterdayBalanceTransferLink && balanceInfo.YesterdayBalanceTransferLink}
                        target="main" />
                </div>

                <div className={rowClasses}>
                    <div className="col-xs-6">
                        {balanceInfo.ReportLink.Text}
                    </div>
                    <CellContainNumberFormat
                        title={balanceInfo.Outstanding}
                        classNames="col-xs-6 col-right"
                        hrefLink={balanceInfo.ShowOutstandingLink && balanceInfo.ReportLink.Url}
                        target="main" />
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
                                thousandSeparator={true} />
                        </Tooltip>

                        <span className="icon-border-color icon-edited" />
                    </div>
                </div>

                {
                    roleId === agentLevel &&
                    <div className={rowClasses}>
                        <div className="col-xs-6">{resources.MemberMaxCredit}</div>
                        <div className="col-xs-6 col-right">
                            {
                                balanceInfo.ShowMemberMaxCreditLimit && <Tooltip
                                    title={balanceInfo.MemberMaxCreditLimit}
                                    position="bottom"
                                    trigger="mouseenter"
                                    arrow="true">
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
            </CustomizedAccordionItem>
        );
    }
}