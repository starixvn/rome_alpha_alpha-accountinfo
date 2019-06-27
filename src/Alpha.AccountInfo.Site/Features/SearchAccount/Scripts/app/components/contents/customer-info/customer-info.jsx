import React from 'react';
import UplineInfo from './upline-info';
import SearchAccountInfo from './search-account-info';
import { UserLevel } from '../../../constants';
import CustomAnchorTag from '../../common/custom-anchor-tag';

const CustomerInfo = ({
    customerInfo,
    resources,
    fetchCustomerInfoFromServer,
    searchCounter,
    mobileView
}) => {
    if (searchCounter === 0 && ((mobileView && customerInfo === null) || !mobileView)) {
        return null;
    }

    if (searchCounter > 0 && customerInfo === null) {
        return <div className='no-information'>{resources.AccountNotFound}</div>;
    }

    const userLevelInfoArray = [
        {
            Name: customerInfo.MasterName,
            LevelName: resources.Master,
            LevelId: UserLevel.Master,
            Visible: customerInfo.ShowMaster
        },
        {
            Name: customerInfo.AgentName,
            LevelName: resources.Agent,
            LevelId: UserLevel.Agent,
            Visible: customerInfo.ShowAgent
        },
        {
            Name: customerInfo.MemberName,
            LevelName: resources.Customer,
            LevelId: UserLevel.Member,
            Visible: customerInfo.ShowMember
        }
    ];

    return (
        <div id='customer-info' className='block-info'>
            {
                userLevelInfoArray.map((userLevelInfo, index) => {
                    const uplineInfoProps = {
                        username: userLevelInfo.Name,
                        fetchCustomerInfoFromServer
                    };

                    const searchAccountInfoProps = {
                        customerInfo,
                        resources,
                        mobileView
                    };

                    return userLevelInfo.Visible &&
                        <div className='col-xs-12 row-info' key={index}>
                            <div className='col-xs-5'>
                                {userLevelInfo.LevelName}
                            </div>
                            <div className='col-xs-7 col-right'>
                                {
                                    customerInfo.RoleId === userLevelInfo.LevelId ?
                                        <SearchAccountInfo {...searchAccountInfoProps} /> :
                                        <UplineInfo {...uplineInfoProps} />
                                }
                            </div>
                        </div>;
                })
            }

            <div className={`col-xs-12 row-info text-center list-button ${customerInfo.Buttons.length === 2 ? 'two-button' : 'one-button'}`}>
                {
                    customerInfo.Buttons.map((buton, index) => {
                        const customAnchorTagProps = {
                            mobileView,
                            classNames: 'btn-default with-level',
                            href: buton.Url,
                            key: index
                        };

                        return (
                            <CustomAnchorTag {...customAnchorTagProps}>
                                {buton.Text}
                            </CustomAnchorTag>
                        );
                    })
                }
            </div>
        </div>
    );
};

export default CustomerInfo;