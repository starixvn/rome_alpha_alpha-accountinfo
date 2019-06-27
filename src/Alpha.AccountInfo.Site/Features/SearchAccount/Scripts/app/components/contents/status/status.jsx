import React from 'react';
import ConfirmStatus from './confirm-status';
import BettypeLevelStatus from './bettype-level-status';
import ProductLevelStatus from './product-level-status';

const Status = ({
    resources,
    username,
    statusData,
    mobileView,
    fetchCustomerInfoFromServer,
    fetchCustomerAllStatusFromServer }) => {
    if (statusData === null || statusData.length === 0) {
        return mobileView ? <div className="no-information">{resources.NoInfo}</div> : null;
    }

    return <div className="status-setting">
        {
            statusData.map((status, index) => {
                const props = {
                    status,
                    username,
                    mobileView,
                    resources,
                    fetchCustomerAllStatusFromServer,
                    fetchCustomerInfoFromServer,
                    key: index
                };

                if (status.ConfirmMessage) {
                    return <ConfirmStatus {...props} />;
                }

                return status.IsEditOnProductLevel ?
                    <ProductLevelStatus {...props} /> :
                    <BettypeLevelStatus {...props} />;
            })
        }
    </div>;
};

export default Status;