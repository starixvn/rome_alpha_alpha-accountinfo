import React from 'react';
import StatusContent from '../contents/status-content';
import MobilePage from './mobile-page';

const StatusPage = ({
    resources,
    mobileView,
    username,
    statusData,
    fetchCustomerInfoFromServer,
    redirectToBalanceInfoPage }) =>
    <MobilePage iconClass="icon-status"
        featureText={resources.Status}
        noInfoText={resources.NoInfo}
        redirectToBalanceInfoPage={redirectToBalanceInfoPage}
        data={statusData}
    >
        <StatusContent
            resources={resources}
            mobileView={mobileView}
            statusData={statusData}
            username={username}
            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
        />
    </MobilePage>;

export default StatusPage;