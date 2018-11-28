import * as React from 'react';

const UplineInfo = ({ username, fetchCustomerInfoFromServer }) =>
    <span 
        id="upline-info"
        className="enabled"
        onClick={() => { fetchCustomerInfoFromServer(username)}}
    >
        {username}
    </span>;

export default UplineInfo; 