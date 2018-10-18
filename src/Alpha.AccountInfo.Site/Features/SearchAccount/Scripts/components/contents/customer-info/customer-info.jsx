import * as React from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';
import UserLevelInfo from './user-level-info';
import { UserLevel } from '../../../constants';
import CustomAnchorTag from '../../common/custom-anchor-tag';

const CustomerInfo = ({
    customerInfo,
    resources,
    handleSearchFromUserLevel,
    mobileView
}) => {
    if (customerInfo === null) {
        return <div className="no-information">{resources.NoInfo}</div>;
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

    const buttons = [customerInfo.CustomerListOrBetListLink, customerInfo.StatemenLink];

    const content =
        <div id="blockInfo" className="block-info">
            {
                userLevelInfoArray.map((userLevelInfo, index) => {
                    let userlevelInfoProps = {
                        customerInfo: customerInfo,
                        username: userLevelInfo.Name,
                        userLevelName: userLevelInfo.LevelName,
                        userLevelId: userLevelInfo.LevelId,
                        resources: resources,
                        mobileView: mobileView,
                        handleSearchFromUserLevel: handleSearchFromUserLevel,
                        key: index
                    };

                    return userLevelInfo.Visible && <UserLevelInfo {...userlevelInfoProps} />;
                })
            }

            <div className="col-xs-12 row-info text-center list-button">
                {
                    buttons.map((buton, index) => {
                        let customAnchorTagProps = {
                            classNames: "btn-default with-level",
                            href: buton.Url,
                            mobileView: mobileView,
                            key: index
                        }

                        return (
                            <CustomAnchorTag {...customAnchorTagProps}>
                                {buton.Text}
                            </CustomAnchorTag>
                        );
                    })
                }
            </div>
        </div>;

    return content;
};

export default CustomerInfo;