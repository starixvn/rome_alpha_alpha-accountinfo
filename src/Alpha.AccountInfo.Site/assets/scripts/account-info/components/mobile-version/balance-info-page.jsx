import React from 'react';
import { NoInformFeature } from './no-inform-feature';
import { HeaderFeature } from './header-feature';
import BalanceInfoContent from '../contents/balance-info-content';

const BalanceInfoPage = ({ customerInfo, resources, fetchCustomerInfoFromServer }) =>
    customerInfo === null ?
        <NoInformFeature url="/featureList" iconClass="icon-balance" featureText={resources.BalanceInfo} noInform={resources.NoInfo} /> :
        <div className="feature-detail translate-from-left">
            <HeaderFeature url="/featureList" iconClass="icon-balance" featureText={resources.BalanceInfo} />
            <BalanceInfoContent resources={resources} customerInfo={customerInfo} fetchCustomerInfoFromServer={fetchCustomerInfoFromServer} />
        </div>;

export default BalanceInfoPage;
