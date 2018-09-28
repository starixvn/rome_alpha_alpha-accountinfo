import React from 'react';
import { NoInformFeature } from './no-inform-feature';
import { HeaderFeature } from './header-feature';
import StatusContent from '../contents/status-content';

const StatusPage = ({ resources, username, statusData, fetchCustomerInfoFromServer, redirectToBalanceInfoPage }) =>
    statusData === null ?
        <NoInformFeature url="/featureList" iconClass="icon-status" featureText={resources.Status} noInform={resources.NoInfo} /> :
        <div className="feature-detail with-push-menu">
            <HeaderFeature url="/featureList" iconClass="icon-status" featureText={resources.Status} redirectToBalanceInfoPage={redirectToBalanceInfoPage} />
            <StatusContent resources={resources} statusData={statusData} username={username} fetchCustomerInfoFromServer={fetchCustomerInfoFromServer} />
        </div>;

export default StatusPage;