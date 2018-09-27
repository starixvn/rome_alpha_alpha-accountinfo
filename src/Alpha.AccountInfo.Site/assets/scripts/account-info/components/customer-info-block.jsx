import * as React from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';
import UserLevelInfo from './user-level-info';

const CustomerInfoBlock = ({
    customerInfo,
    resources,
    handleSearchFromUserLevel,
    rowClasses = "col-xs-12 row-info",
    firstCellClass = "col-xs-5",
    secondCellClass = "col-xs-7 col-right" }) => {
    const memberLevel = 1, agentLevel = 2, masterLevel = 3;

    const content = customerInfo === null ? <div className="no-information">{resources.NoInfo}</div> :
        <div id="blockInfo" className="block-info">
            {
                customerInfo.ShowMaster &&
                <UserLevelInfo customerInfo={customerInfo}
                    userLevelName={resources.Master}
                    userLevelId={masterLevel}
                    username={customerInfo.MasterName}
                    resources={resources}
                    handleSearchFromUserLevel={handleSearchFromUserLevel}
                />
            }

            {
                customerInfo.ShowAgent &&
                <UserLevelInfo customerInfo={customerInfo}
                    userLevelName={resources.Agent}
                    userLevelId={agentLevel}
                    username={customerInfo.AgentName}
                    resources={resources}
                    handleSearchFromUserLevel={handleSearchFromUserLevel}
                />
            }

            {
                customerInfo.ShowMember &&
                <UserLevelInfo customerInfo={customerInfo}
                    userLevelName={resources.Customer}
                    userLevelId={memberLevel}
                    username={customerInfo.MemberName}
                    resources={resources}
                    handleSearchFromUserLevel={handleSearchFromUserLevel}
                />
            }

            <div className={rowClasses + " text-center list-button"}>
                <a className="btn-default with-level" href={customerInfo.CustomerListOrBetListLink.Url} target="main">{customerInfo.CustomerListOrBetListLink.Text}</a>
                <a className="btn-default with-level" href={customerInfo.StatemenLink} target="main">{resources.Statement}</a>
            </div>
        </div>;

    return content;
};

export default CustomerInfoBlock;