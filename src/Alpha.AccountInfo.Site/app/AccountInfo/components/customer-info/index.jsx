import * as React from 'react';
import UserLevelLevelInfo from './user-level-info';

const CustomerInfoBlock = ({
    data,
    resources,
    handleSearchFromUserLevel,
    rowClasses = "col-xs-12 row-info",
    firstCellClass = "col-xs-5",
    secondCellClass = "col-xs-7 col-right" }) =>
    <div>
        {
            data.ShowMasterName &&
            <UserLevelLevelInfo data={data}
                userLevelName={resources.Master}
                userLevelId="3"
                username={data.MasterName}
                resources={resources}
                handleSearchFromUserLevel={handleSearchFromUserLevel} />
        }

        {
            data.ShowAgentName &&
            <UserLevelLevelInfo data={data}
                userLevelName={resources.Agent}
                userLevelId="2"
                username={data.AgentName}
                resources={resources}
                handleSearchFromUserLevel={handleSearchFromUserLevel} />
        }

        {
            data.ShowMemberName &&
            <UserLevelLevelInfo data={data}
                userLevelName={resources.Customer}
                userLevelId="1"
                username={data.MemberName}
                resources={resources}
                handleSearchFromUserLevel={handleSearchFromUserLevel} />
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
            {
                data.RoleId == 1 &&
                <a className="btn-default with-level" href={data.BetListLink} target="main">{resources.BetList}</a>
            }

            {
                data.RoleId == 2 &&
                <a className="btn-default with-level" href={data.CustomerListLink} target="main">{resources.MemberList}</a>
            }

            {
                data.RoleId == 3 &&
                <a className="btn-default with-level" href={data.CustomerListLink} target="main">{resources.AgentList}</a>
            }

            <a className="btn-default with-level" href={data.StatemenLink} target="main">{resources.Statement}</a>
        </div>
    </div>

export default CustomerInfoBlock;