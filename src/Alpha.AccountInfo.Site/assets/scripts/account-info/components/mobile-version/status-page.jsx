import React from 'react';
import { NoInformFeature } from './no-inform-feature';
import { HeaderFeature } from './header-feature';
import StatusContent from '../contents/status-content';

const StatusPage = ({ resources, mobileView, username, statusData, fetchCustomerInfoFromServer, redirectToBalanceInfoPage }) =>
    statusData === null ?
        <NoInformFeature url="/featureList" iconClass="icon-status" featureText={resources.Status} noInform={resources.NoInfo} /> :
        <div className="feature-detail translate-from-left">
            <HeaderFeature url="/featureList" iconClass="icon-status" featureText={resources.Status} redirectToBalanceInfoPage={redirectToBalanceInfoPage} />
            <StatusContent resources={resources} mobileView={mobileView} statusData={statusData} username={username} fetchCustomerInfoFromServer={fetchCustomerInfoFromServer} />
        </div>;

export default StatusPage;