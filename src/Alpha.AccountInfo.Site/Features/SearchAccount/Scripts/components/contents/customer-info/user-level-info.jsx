import * as React from 'react';
import { Fragment } from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';
import CustomAnchorTag from '../../common/custom-anchor-tag';

export default class UserLevelInfo extends React.Component {
    getStatusInfo(customerInfo, userLevelId, resources) {
        const statusInfo = {
            Closed: [
                {
                    ByOtherReason: 0,
                    Css: "closed-wrongPwdOthers",
                    Message: resources.ClosedByFailedLoginOrAccountExpired
                },
                {
                    ByAdmin: 1,
                    Css: "closed-byAdmin",
                    Message: resources.ClosedByAdmin
                },
                {
                    ByUpline: 2,
                    Css: "closed-byUpline",
                    Message: resources.ClosedByUpline
                },
                {
                    ByWrongOTPSC: 3,
                    Css: "closed-wrongOTPSC",
                    Message: resources.ClosedByFailedOTPOrSC
                }
            ],
            Suspended: {
                Css: "suspended",
                Message: resources.ThisAccountWasSuspended
            },
            Disabled: {
                Css: "disabled",
                Message: resources.ThisAccountWasDisabled
            },
            Enabled: {
                Css: 'enabled',
                Message: ''
            }
        };

        if (customerInfo.RoleId === userLevelId) {
            if (customerInfo.Closed) {
                return statusInfo.Closed[customerInfo.ClosedReason];
            }
            else if (customerInfo.Suspended) {
                return statusInfo.Suspended;
            }
            else if (!customerInfo.Enabled) {
                return statusInfo.Disabled;
            }

        }

        return statusInfo.Enabled;
    }

    render() {
        let {
            customerInfo,
            userLevelName,
            userLevelId,
            username,
            resources,
            handleSearchFromUserLevel,
            mobileView
        } = this.props;

        let statusInfo = this.getStatusInfo(customerInfo, userLevelId, resources);

        const contentWithViewlogLink =
            <CustomAnchorTag
                mobileView={mobileView}
                title={resources.Viewlog}
                classNames="pointer"
                href={customerInfo.ViewLogLink}
            >
                <span className={statusInfo.Css} data-username={username}> {username} </span>
                <span className="icon-viewlog" />
            </CustomAnchorTag>;

        return (
            <div className="col-xs-12 row-info" >
                <div className="col-xs-5">
                    {userLevelName}
                </div>
                <div className="col-xs-7 col-right">
                    {
                        customerInfo.RoleId < userLevelId &&
                        <span className="enabled" onClick={handleSearchFromUserLevel} data-username={username}>
                            {username}
                        </span>
                    }

                    {
                        customerInfo.RoleId === userLevelId && statusInfo.Message === '' && contentWithViewlogLink
                    }

                    {
                        customerInfo.RoleId === userLevelId && statusInfo.Message !== '' &&
                        <Tooltip
                            title={statusInfo.Message}
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
    }
};