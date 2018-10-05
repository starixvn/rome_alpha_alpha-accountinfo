import * as React from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';
import UserLevelInfo from './user-level-info';
import { UserLevel } from '../constants/user-level';
import CustomAnchorTag from './common/custom-anchor-tag';

const CustomerInfoBlock = ({
    customerInfo,
    resources,
    handleSearchFromUserLevel,
    mobileView,
    rowClasses = "col-xs-12 row-info",
    firstCellClass = "col-xs-5",
    secondCellClass = "col-xs-7 col-right" }) => {
    const content = customerInfo === null ? <div className="no-information">{resources.NoInfo}</div> :
        <div id="blockInfo" className="block-info">
            {
                customerInfo.ShowMaster &&
                <UserLevelInfo customerInfo={customerInfo}
                    userLevelName={resources.Master}
                    userLevelId={UserLevel.Master}
                    username={customerInfo.MasterName}
                    resources={resources}
                    mobileView={mobileView}
                    handleSearchFromUserLevel={handleSearchFromUserLevel}
                />
            }

            {
                customerInfo.ShowAgent &&
                <UserLevelInfo customerInfo={customerInfo}
                    userLevelName={resources.Agent}
                    userLevelId={UserLevel.Agent}
                    username={customerInfo.AgentName}
                    resources={resources}
                    mobileView={mobileView}
                    handleSearchFromUserLevel={handleSearchFromUserLevel}
                />
            }

            {
                customerInfo.ShowMember &&
                <UserLevelInfo customerInfo={customerInfo}
                    userLevelName={resources.Customer}
                    userLevelId={UserLevel.Member}
                    username={customerInfo.MemberName}
                    resources={resources}
                    mobileView={mobileView}
                    handleSearchFromUserLevel={handleSearchFromUserLevel}
                />
            }

            <div className={rowClasses + " text-center list-button"}>
                <CustomAnchorTag
                    classNames="btn-default with-level"
                    href={customerInfo.CustomerListOrBetListLink.Url}
                    mobileView={mobileView}
                >
                    {customerInfo.CustomerListOrBetListLink.Text} </CustomAnchorTag>
                <CustomAnchorTag
                    classNames="btn-default with-level"
                    href={customerInfo.StatemenLink}
                    mobileView={mobileView}
                >
                    {resources.Statement}
                </CustomAnchorTag>
            </div>
        </div>;

    return content;
};

export default CustomerInfoBlock;