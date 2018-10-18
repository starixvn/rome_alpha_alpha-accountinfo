import React from 'react';
import ConfirmStatus from './confirm-status';
import BettypeLevelStatus from './bettype-level-status';
import ProductLevelStatus from './product-level-status';

const Status = ({ resources, username, statusData, fetchCustomerInfoFromServer }) => {
    return (
        <div className="status-setting">
            {
                statusData.map((status, index) => {
                    return status.ConfirmMessage ?
                        <ConfirmStatus
                            username={username}
                            resources={resources}
                            status={status}
                            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                            key={index}
                        /> : 
                        status.IsEditOnProductLevel ?
                            <ProductLevelStatus resources={resources} status={status} key={index} /> :
                            <BettypeLevelStatus resources={resources} status={status} key={index} />;

                })
            }
        </div>
    );
}

export default Status;