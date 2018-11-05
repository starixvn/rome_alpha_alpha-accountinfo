import React from 'react';
import { UserLevel } from '../../../constants';
import BalanceInfoRow from './balance-info-row';
import GivenCreditRow from './given-credit-row';
import MemberCreditMaxRow from './member-credit-max-row';

const BalanceInfo = ({
    resources,
    customerInfo,
    mobileView,
    fetchCustomerInfoFromServer
}) => {
    let balanceInfo = customerInfo.BalanceInfo;

    let sharedContents = [
        <BalanceInfoRow
            id="total-balance"
            title={resources.TotalBalance}
            value={balanceInfo.TotalBalance}
            hrefLink={balanceInfo.ShowTotalBalanceTransferLink ? balanceInfo.TotalBalanceTransferLink : ``}
            mobileView={mobileView}
        />,
        <BalanceInfoRow
            id="yesterday-balance"
            title={resources.YesterdayBalance}
            value={balanceInfo.YesterdayBalance}
            hrefLink={balanceInfo.ShowYesterdayBalanceTransferLink ? balanceInfo.YesterdayBalanceTransferLink : ``}
            mobileView={mobileView}
        />,
        <BalanceInfoRow
            id="report-link"
            title={balanceInfo.ReportLink.Text}
            value={balanceInfo.Outstanding}
            hrefLink={balanceInfo.ShowOutstandingLink ? balanceInfo.ReportLink.Url : ``}
            mobileView={mobileView}
        />,

    ];

    let givenCreditContent = <GivenCreditRow {...{ customerInfo, mobileView, resources, fetchCustomerInfoFromServer }} />;

    let masterContents = [
        <BalanceInfoRow id="total-cash" title={resources.TotalCash} value={balanceInfo.TotalCash} />,
        <BalanceInfoRow id="yesterday-cash" title={resources.YesterdayCash} value={balanceInfo.YesterdayCash} />,
        ...sharedContents,
        <BalanceInfoRow id="used-credit" title={resources.UsedCredit} value={balanceInfo.UsedCredit} />,
        givenCreditContent
    ];

    let balanceInfoByLevel = [
        {
            Id: UserLevel.Master,
            Contents: masterContents
        },
        {
            Id: UserLevel.Agent,
            Contents: [
                ...masterContents,
                <MemberCreditMaxRow {...{ resources, mobileView, balanceInfo}} />
            ]
        },
        {
            Id: UserLevel.Member,
            Contents: [
                ...sharedContents,
                givenCreditContent,
                <BalanceInfoRow id="net-position" title={resources.NetPosition} value={balanceInfo.NetPosition} />,
                <BalanceInfoRow id="bet-credit" title={resources.BetCredit} value={balanceInfo.BetCredit} />
            ]
        }
    ];

    let contents = balanceInfoByLevel.find(level => customerInfo.RoleId === level.Id).Contents;

    return (
        <div className="balance-info">
            {
                contents.map((content, index) => {
                    return <div key={index}>{content} </div>;
                })
            }
        </div>
    );
}


export default BalanceInfo;