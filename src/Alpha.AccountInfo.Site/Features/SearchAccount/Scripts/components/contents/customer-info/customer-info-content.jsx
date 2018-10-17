import * as React from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';
import UserLevelInfo from './user-level-info';
import { UserLevel } from '../../../constants';
import CustomAnchorTag from '../../common/custom-anchor-tag';

const CustomerInfoContent = ({
    customerInfo,
    resources,
    handleSearchFromUserLevel,
    mobileView
}) => {
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
                    return userLevelInfo.Visible &&
                        <UserLevelInfo
                            customerInfo={customerInfo}
                            username={userLevelInfo.Name}
                            userLevelName={userLevelInfo.LevelName}
                            userLevelId={userLevelInfo.LevelId}
                            resources={resources}
                            mobileView={mobileView}
                            handleSearchFromUserLevel={handleSearchFromUserLevel}
                            key={index}
                        />;
                })
            }

            <div className="col-xs-12 row-info text-center list-button">
                {
                    buttons.map((buton, index) => {
                        return (
                            <CustomAnchorTag
                                classNames="btn-default with-level"
                                href={buton.Url}
                                mobileView={mobileView}
                                key={index}
                            >
                                {buton.Text}
                            </CustomAnchorTag>
                        );
                    })
                }
            </div>
        </div>;

    return content;
};

export default CustomerInfoContent;