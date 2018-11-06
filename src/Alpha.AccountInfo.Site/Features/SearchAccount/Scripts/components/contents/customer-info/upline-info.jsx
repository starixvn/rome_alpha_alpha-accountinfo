import * as React from 'react';

const UplineInfo = ({ username, fetchCustomerInfoFromServer }) =>
    <span
        className="enabled"
        onClick={() => { fetchCustomerInfoFromServer(username)}}
    >
        {username}
    </span>;

export default UplineInfo; 