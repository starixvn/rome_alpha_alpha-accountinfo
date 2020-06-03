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
    if (customerInfo === null) {
        return <div className='no-information'>{resources.NoInfo}</div>;
    }

    const balanceInfo = customerInfo.BalanceInfo;

	const sharedContents = [
        <BalanceInfoRow
            id='total-balance'
            title={resources.TotalBalance}
            value={balanceInfo.TotalBalance}
            hrefLink={balanceInfo.ShowTotalBalanceTransferLink ? balanceInfo.TotalBalanceTransferLink : ``}
            mobileView={mobileView}
        />,
        <BalanceInfoRow
            id='yesterday-balance'
            title={resources.YesterdayBalance}
            value={balanceInfo.YesterdayBalance}
            hrefLink={balanceInfo.ShowYesterdayBalanceTransferLink ? balanceInfo.YesterdayBalanceTransferLink : ``}
            mobileView={mobileView}
        />,
        <BalanceInfoRow
            id='report-link'
            title={balanceInfo.ReportLink.Text}
            value={balanceInfo.Outstanding}
            hrefLink={balanceInfo.ShowOutstandingLink ? balanceInfo.ReportLink.Url : ``}
            mobileView={mobileView}
		/>,
		<BalanceInfoRow
			id='pending-transfer-3rd'
			title={resources.PendingTransfer3rd}
			value={balanceInfo.PendingTransfer3rd}
			mobileView={mobileView}
		/>
    ];

    const givenCreditContent = <GivenCreditRow {...{ customerInfo, mobileView, resources, fetchCustomerInfoFromServer }} />;

    const masterContents = [
        <BalanceInfoRow id='total-cash' title={resources.TotalCash} value={balanceInfo.TotalCash} />,
        <BalanceInfoRow id='yesterday-cash' title={resources.YesterdayCash} value={balanceInfo.YesterdayCash} />,
        ...sharedContents,
        <BalanceInfoRow id='used-credit' title={resources.UsedCredit} value={balanceInfo.UsedCredit} />,
        givenCreditContent
    ];

    const balanceInfoByLevel = [
        {
            Id: UserLevel.Master,
            Contents: masterContents
        },
        {
            Id: UserLevel.Agent,
            Contents: [
                ...masterContents,
                <MemberCreditMaxRow {...{ resources, mobileView, balanceInfo }} />
            ]
        },
        {
            Id: UserLevel.Member,
            Contents: [
                ...sharedContents,
                givenCreditContent,
                <BalanceInfoRow id='net-position' title={resources.NetPosition} value={balanceInfo.NetPosition} />,
                <BalanceInfoRow id='bet-credit' title={resources.BetCredit} value={balanceInfo.BetCredit} />
            ]
        }
    ];

    const contents = balanceInfoByLevel.find(level => customerInfo.RoleId === level.Id).Contents;

    return <div className='balance-info'>
        {
            contents.map((content, index) => <div key={index}>{content} </div>)
        }
    </div>;
};

export default BalanceInfo;