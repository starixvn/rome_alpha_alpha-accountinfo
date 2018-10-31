import React from 'react';
import ConfirmStatus from './confirm-status';
import BettypeLevelStatus from './bettype-level-status';
import ProductLevelStatus from './product-level-status';

const Status = ({ resources, username, statusData, mobileView, fetchCustomerInfoFromServer }) => {
    return (
        <div className="status-setting">
            {
                statusData.map((status, index) => {
                    let props = {
                        status: status,
                        mobileView: mobileView,
                        resources: resources
                    };

                    return status.ConfirmMessage ?
                        <ConfirmStatus
                            {...props}
                            username={username}
                            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                            key={index}
                        /> :
                        status.IsEditOnProductLevel ?
                            <ProductLevelStatus {...props} key={index} /> :
                            <BettypeLevelStatus {...props} key={index} />;

                })
            }
        </div>
    );
}

export default Status;