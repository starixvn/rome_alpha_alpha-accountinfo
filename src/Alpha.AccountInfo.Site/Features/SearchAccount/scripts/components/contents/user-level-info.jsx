import * as React from 'react';
import { Fragment } from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';
import CustomAnchorTag from '../common/custom-anchor-tag';

const UserLevelInfo = ({
    customerInfo,
    userLevelName,
    userLevelId,
    username,
    resources,
    handleSearchFromUserLevel,
    mobileView,
    rowClasses = "col-xs-12 row-info",
    firstCellClass = "col-xs-5",
    secondCellClass = "col-xs-7 col-right" }) => {

    let statusCss = 'enabled';
    let statusMessage = '';

    if (customerInfo.RoleId === userLevelId) {
        if (customerInfo.Closed) {
            switch (customerInfo.ClosedReason) {
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
        else if (customerInfo.Suspended) {
            statusCss = "suspended";
            statusMessage = resources.ThisAccountWasSuspended;
        }
        else if (!customerInfo.Enabled) {
            statusCss = "disabled";
            statusMessage = resources.ThisAccountWasDisabled;
        }
    }

    const contentWithViewlogLink =
        <CustomAnchorTag
            mobileView={mobileView}
            title={resources.Viewlog}
            classNames="pointer"
            href={customerInfo.ViewLogLink}>
            <span className={statusCss} data-username={username}> {username} </span>
            <span className="icon-viewlog" />
        </CustomAnchorTag>;

    return (

        <div className={rowClasses} >
            <div className={firstCellClass}>{userLevelName}</div>
            <div className={secondCellClass}>
                {
                    customerInfo.RoleId < userLevelId &&
                    <span className="enabled" onClick={handleSearchFromUserLevel} data-username={username}>
                        {username}
                    </span>
                }

                {
                    customerInfo.RoleId === userLevelId && statusMessage === '' && contentWithViewlogLink
                }

                {
                    customerInfo.RoleId === userLevelId && statusMessage !== '' &&
                    <Tooltip
                        title={statusMessage}
                        position="bottom"
                        trigger="mouseenter"
                        arrow="true"
                    >
                        {contentWithViewlogLink}
                    </Tooltip>
                }
            </div>
        </div>
    );
};

export default UserLevelInfo;