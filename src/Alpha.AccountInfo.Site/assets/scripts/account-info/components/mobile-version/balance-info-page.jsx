import React from 'react';
import { NoInformFeature } from '../../components/common/no-inform-feature';
import { HeaderFeature } from '../../components/common/header-feature';
import BalanceInfoContent from '../contents/balance-info-content';

const BalanceInfoPage = ({ customerInfo, resources, fetchCustomerInfoFromServer }) => customerInfo === null ?
    <NoInformFeature url="/" iconClass="icon-balance" featureText={resources.BalanceInfo} noInform={resources.NoInfo} /> :
    <div className="feature-detail">
        <HeaderFeature url="/" iconClass="icon-balance" featureText={resources.BalanceInfo} />
        <BalanceInfoContent resources={resources} customerInfo={customerInfo} fetchCustomerInfoFromServer={fetchCustomerInfoFromServer} />
    </div>;

export default BalanceInfoPage;
