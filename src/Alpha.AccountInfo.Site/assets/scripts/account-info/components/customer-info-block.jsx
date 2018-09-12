import * as React from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';
import UserLevelLevelInfo from './user-level-info';

const CustomerInfoBlock = ({
    data,
    resources,
    handleSearchFromUserLevel,
    rowClasses = "col-xs-12 row-info",
    firstCellClass = "col-xs-5",
    secondCellClass = "col-xs-7 col-right" }) => {

    const memberLevel = 1, agentLevel = 2, masterLevel = 3;

    return (
        <div>
            {
                data.ShowMaster &&
                <UserLevelLevelInfo data={data}
                    userLevelName={resources.Master}
                    userLevelId={masterLevel}
                    username={data.MasterName}
                    resources={resources}
                    handleSearchFromUserLevel={handleSearchFromUserLevel}
                />
            }

            {
                data.ShowAgent &&
                <UserLevelLevelInfo data={data}
                    userLevelName={resources.Agent}
                    userLevelId={agentLevel}
                    username={data.AgentName}
                    resources={resources}
                    handleSearchFromUserLevel={handleSearchFromUserLevel}
                />
            }

            {
                data.ShowMember &&
                <UserLevelLevelInfo data={data}
                    userLevelName={resources.Customer}
                    userLevelId={memberLevel}
                    username={data.MemberName}
                    resources={resources}
                    handleSearchFromUserLevel={handleSearchFromUserLevel}
                />
            }

            <div className={rowClasses}>
                <div className={firstCellClass}>{resources.Name}</div>
                <div className={secondCellClass}>{data.FullName}</div>
            </div>

            <div className={rowClasses}>
                <div className={firstCellClass}>{resources.Phone}</div>
                <div className={secondCellClass}>{data.Phone}</div>
            </div>

            <div className={rowClasses}>
                <div className={firstCellClass}>{resources.Fax}</div>
                <div className={secondCellClass}>{data.Fax}</div>
            </div>

            <div className={rowClasses + " text-center list-button"}>
                <a className="btn-default with-level" href={data.CustomerListOrBetListLink.Url} target="main">{data.CustomerListOrBetListLink.Text}</a>
                <a className="btn-default with-level" href={data.StatemenLink} target="main">{resources.Statement}</a>
            </div>
        </div>);
};

export default CustomerInfoBlock;