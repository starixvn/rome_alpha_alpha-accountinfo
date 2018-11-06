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
    fetchCustomerAllStatusFromServer }) =>
    <div className="status-setting">
        {
            statusData.map((status, index) => {
                let props = {
                    status,
                    username,
                    mobileView,
                    resources,
                    fetchCustomerAllStatusFromServer,
                    fetchCustomerInfoFromServer,
                    key: index
                };

                return status.ConfirmMessage ?
                    <ConfirmStatus {...props} /> :
                    status.IsEditOnProductLevel ?
                        <ProductLevelStatus {...props} /> :
                        <BettypeLevelStatus {...props} />;
            })
        }
    </div>;

export default Status;