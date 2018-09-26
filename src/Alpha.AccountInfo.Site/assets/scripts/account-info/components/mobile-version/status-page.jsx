import React from 'react';
import { NoInformFeature } from '../../components/common/no-inform-feature';
import { HeaderFeature } from '../../components/common/header-feature';
import StatusContent from '../contents/status-content';

const StatusPage = ({ resources, username, statusData, fetchCustomerInfoFromServer }) => statusData === null ?
    <NoInformFeature url="/" iconClass="icon-status" featureText={resources.Status} noInform={resources.NoInfo} /> :
    <div className="feature-detail with-push-menu">
        <HeaderFeature url="/" iconClass="icon-status" featureText={resources.Status} />
        <StatusContent resources={resources} statusData={statusData} username={username} fetchCustomerInfoFromServer={fetchCustomerInfoFromServer} />
    </div>;

export default StatusPage;