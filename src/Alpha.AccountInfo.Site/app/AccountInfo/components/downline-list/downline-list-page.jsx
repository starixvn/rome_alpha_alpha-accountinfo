import React from 'react';
import DownlineName from './downline-name';
export default class DownlineListPage extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { usernames, fetchCustomerInfoFromServer } = this.props;

        return (
            <div className="swiper-slide" >
                {usernames.map((username, index) => {
                    return <DownlineName username={username.UserName} key={index} fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}/>
                })}
            </div>
        )
    }
}
