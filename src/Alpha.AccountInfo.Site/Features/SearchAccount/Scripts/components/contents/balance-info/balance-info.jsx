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
            title={resources.TotalBalance}
            value={balanceInfo.TotalBalance}
            hrefLink={balanceInfo.ShowTotalBalanceTransferLink ? balanceInfo.TotalBalanceTransferLink : ``}
            mobileView={mobileView}
        />,
        <BalanceInfoRow
            title={resources.YesterdayBalance}
            value={balanceInfo.YesterdayBalance}
            hrefLink={balanceInfo.ShowYesterdayBalanceTransferLink ? balanceInfo.YesterdayBalanceTransferLink : ``}
            mobileView={mobileView}
        />,
        <BalanceInfoRow
            title={balanceInfo.ReportLink.Text}
            value={balanceInfo.Outstanding}
            hrefLink={balanceInfo.ShowOutstandingLink ? balanceInfo.ReportLink.Url : ``}
            mobileView={mobileView}
        />,

    ];

    let givenCreditContent =
        <GivenCreditRow
            customerInfo={customerInfo}
            resources={resources}
            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
        />;

    let masterContents = [
        <BalanceInfoRow title={resources.TotalCash} value={balanceInfo.TotalCash} />,
        <BalanceInfoRow title={resources.YesterdayCash} value={balanceInfo.YesterdayCash} />,
        ...sharedContents,
        <BalanceInfoRow title={resources.UsedCredit} value={balanceInfo.UsedCredit} />,
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
                <MemberCreditMaxRow resources={resources} balanceInfo={balanceInfo} />
            ]
        },
        {
            Id: UserLevel.Member,
            Contents: [
                ...sharedContents,
                givenCreditContent,
                <BalanceInfoRow title={resources.NetPosition} value={balanceInfo.NetPosition} />,
                <BalanceInfoRow title={resources.BetCredit} value={balanceInfo.BetCredit} />
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