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
    fetchCustomerAllStatusFromServer
}) => {
    return (
        <div className="status-setting">
            {
                statusData.map((status, index) => {
                    let props = {
                        status: status,
                        username: username,
                        mobileView: mobileView,
                        resources: resources,
                        fetchCustomerAllStatusFromServer: fetchCustomerAllStatusFromServer,
                        fetchCustomerInfoFromServer: fetchCustomerInfoFromServer
                    };

                    if (Object.keys(status).length === 0) {
                        return null;
                    }

                    return status.ConfirmMessage ?
                        <ConfirmStatus {...props} key={index} /> :
                        status.IsEditOnProductLevel ?
                            <ProductLevelStatus {...props} key={index} /> :
                            <BettypeLevelStatus {...props} key={index} />;

                })
            }
        </div>
    );
}

export default Status;