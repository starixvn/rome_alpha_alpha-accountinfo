import React from 'react';
import DownlineName from './downline-name';

const DownlineListPage = ({ usernames, fetchCustomerInfoFromServer  }) =>
    <div className="swiper-slide" >
        {usernames.map((username, index) => {
            return <DownlineName username={username.UserName} key={index} fetchCustomerInfoFromServer={fetchCustomerInfoFromServer} />
        })}
    </div>

export default DownlineListPage;