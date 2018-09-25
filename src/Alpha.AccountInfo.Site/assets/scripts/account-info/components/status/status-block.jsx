import React from 'react';
import { NoInformFeature } from '../../components/common/no-inform-feature';
import { HeaderFeature } from '../../components/common/header-feature';
import StatusContent from './status-content';

const StatusBlock = ({ resources, username, statusData, fetchCustomerInfoFromServer }) => statusData === null ?
    <NoInformFeature url="/" iconClass="icon-status" featureText={resources.Status} noInform={resources.NoInfo} /> :
    <div className="feature-detail">
        <HeaderFeature url="/" iconClass="icon-status" featureText={resources.Status} />
        <StatusContent resources={resources} statusData={statusData} username={username} fetchCustomerInfoFromServer={fetchCustomerInfoFromServer} />
    </div>;

export default StatusBlock;