import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';

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

    const contentWithViewlogLink = <a title={resources.Viewlog} className="link-viewlog" href={data.ViewLogLink} target="main">
        <span className={statusCss} data-username={username}> {username} </span>
        <span className="icon-viewlog"></span>
    </a>;

    return (
        <div className={rowClasses}>
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
        </div>);
}

export default UserLevelLevelInfo;