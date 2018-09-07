import * as React from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';

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
                    handleSearchFromUserLevel={handleSearchFromUserLevel} />
            }

            {
                data.ShowAgent &&
                <UserLevelLevelInfo data={data}
                    userLevelName={resources.Agent}
                    userLevelId={agentLevel}
                    username={data.AgentName}
                    resources={resources}
                    handleSearchFromUserLevel={handleSearchFromUserLevel} />
            }

            {
                data.ShowMember &&
                <UserLevelLevelInfo data={data}
                    userLevelName={resources.Customer}
                    userLevelId={memberLevel}
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
                    data.RoleId == memberLevel &&
                    <a className="btn-default with-level" href={data.BetListLink} target="main">{resources.BetList}</a>
                }

                {
                    data.RoleId == agentLevel &&
                    <a className="btn-default with-level" href={data.CustomerListLink} target="main">{resources.MemberList}</a>
                }

                {
                    data.RoleId == masterLevel &&
                    <a className="btn-default with-level" href={data.CustomerListLink} target="main">{resources.AgentList}</a>
                }

                <a className="btn-default with-level" href={data.StatemenLink} target="main">{resources.Statement}</a>
            </div>
        </div>);
}
export default CustomerInfoBlock;

const UserLevelLevelInfo = ({
    data,
    userLevelName,
    userLevelId,
    username,
    resources,
    handleSearchFromUserLevel,
    rowClasses = "col-xs-12 row-info",
    firstCellClass = "col-xs-5",
    secondCellClass = "col-xs-7 col-right" }) => {

    let statusCss = 'enabled';
    let statusMessage = '';

    if (data.RoleId == userLevelId) {
        if (data.Closed) {
            switch (data.ClosedReason) {
                case 0:
                    statusCss = "closed-wrongPwdOthers";
                    statusMessage = resources.ClosedByFailedLoginOrAccountExpired;
                    break;
                case 1:
                    statusCss = "closed-byAdmin";
                    statusMessage = resources.ClosedByAdmin;
                    break;
                case 2:
                    statusCss = "closed-byUpline";
                    statusMessage = resources.ClosedByUpline;
                    break;
                default:
                    statusCss = "closed-wrongOTPSC";
                    statusMessage = resources.ClosedByFailedOTPOrSC;
                    break;
            }
        }
        else if (data.Suspended) {
            statusCss = "suspended";
            statusMessage = resources.ThisAccountWasSuspended;
        }
        else if (!data.Enabled) {
            statusCss = "disabled";
            statusMessage = resources.ThisAccountWasDisabled;
        }
    }

    const contentWithViewlogLink = <a title={resources.Viewlog} className="pointer" href={data.ViewLogLink} target="main">
        <span className={statusCss} data-username={username}> {username} </span>
        <span className="icon-viewlog"></span>
    </a>;

    return (

        <div className={rowClasses} >
            <div className={firstCellClass}>{userLevelName}</div>
            <div className={secondCellClass}>
                {
                    data.RoleId < userLevelId &&
                    <span className="enabled" onClick={handleSearchFromUserLevel} data-username={username}>
                        {username}
                    </span>
                }

                {
                    data.RoleId == userLevelId && statusMessage == '' && contentWithViewlogLink
                }

                {
                    data.RoleId == userLevelId && statusMessage != '' &&
                    <Tooltip
                        title={statusMessage}
                        position="bottom"
                        trigger="mouseenter"
                        arrow="true">
                        {contentWithViewlogLink}
                    </Tooltip>
                }
            </div>
        </div>
    );
}