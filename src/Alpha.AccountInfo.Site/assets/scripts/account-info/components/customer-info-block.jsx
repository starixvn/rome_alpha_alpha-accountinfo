import * as React from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';
import UserLevelInfo from './user-level-info';
import { NoInfo } from '../components/common/no-info';

const CustomerInfoBlock = ({
    data,
    resources,
    handleSearchFromUserLevel,
    rowClasses = "col-xs-12 row-info",
    firstCellClass = "col-xs-5",
    secondCellClass = "col-xs-7 col-right" }) => {
    const memberLevel = 1, agentLevel = 2, masterLevel = 3;
    return (
        <div id="blockInfo" className="block-info">
            {
                data.ShowMaster &&
                <UserLevelInfo data={data}
                    userLevelName={resources.Master}
                    userLevelId={masterLevel}
                    username={data.MasterName}
                    resources={resources}
                    handleSearchFromUserLevel={handleSearchFromUserLevel}
                />
            }

            {
                data.ShowAgent &&
                <UserLevelInfo data={data}
                    userLevelName={resources.Agent}
                    userLevelId={agentLevel}
                    username={data.AgentName}
                    resources={resources}
                    handleSearchFromUserLevel={handleSearchFromUserLevel}
                />
            }

            {
                data.ShowMember &&
                <UserLevelInfo data={data}
                    userLevelName={resources.Customer}
                    userLevelId={memberLevel}
                    username={data.MemberName}
                    resources={resources}
                    handleSearchFromUserLevel={handleSearchFromUserLevel}
                />
            }

            <div className={rowClasses + " text-center list-button"}>
                <a className="btn-default with-level" href={data.CustomerListOrBetListLink.Url} target="main">{data.CustomerListOrBetListLink.Text}</a>
                <a className="btn-default with-level" href={data.StatemenLink} target="main">{resources.Statement}</a>
            </div>
        </div>
    );
};

export default CustomerInfoBlock;