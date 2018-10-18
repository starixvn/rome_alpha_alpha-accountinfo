import * as React from 'react';
import UplineInfo from './upline-info'
import SearchAccountInfo from './search-account-info';

const UserLevelInfo = ({
    userLevelName,
    customerInfo,
    userLevelId,
    username,
    resources,
    mobileView,
    handleSearchFromUserLevel
}) => {
    let uplineInfoProps = {
        username: username,
        handleSearchFromUserLevel
    };

    let searchAccountInfoProps = {
        customerInfo: customerInfo,
        userLevelId: userLevelId,
        username: username,
        resources: resources,
        mobileView: mobileView
    };

    return (
        <div className="col-xs-12 row-info" >
            <div className="col-xs-5">
                {userLevelName}
            </div>
            <div className="col-xs-7 col-right">
                {
                    customerInfo.RoleId === userLevelId ?
                        <SearchAccountInfo {...searchAccountInfoProps} /> :
                        <UplineInfo {...uplineInfoProps} />
                }
            </div>
        </div>
    );
}

export default UserLevelInfo; 