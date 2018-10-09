import React from 'react';
import BalanceInfoContent from '../contents/balance-info-content';
import MobilePage from './mobile-page';

const BalanceInfoPage = ({
    customerInfo,
    resources,
    mobileView,
    fetchCustomerInfoFromServer }) =>
    <MobilePage
        iconClass="icon-balance"
        featureText={resources.BalanceInfo}
        noInfoText={resources.NoInfo}
        data={customerInfo}
    >
        <BalanceInfoContent
            resources={resources}
            mobileView={mobileView}
            customerInfo={customerInfo}
            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
        />
    </MobilePage>;

export default BalanceInfoPage;