import * as React from 'react';
import { Fragment } from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';
import CustomAnchorTag from '../../common/custom-anchor-tag';

const ClosedReason = {
    ByOtherReason: 0,
    ByAdmin: 1,
    ByUpline: 2,
    ByWrongOTPSC: 3
};

export default class UserLevelInfo extends React.Component {
    getStatusInfo(customerInfo, userLevelId, resources) {
        const statusInfo = {
            Closed: [
                {
                    Id: ClosedReason.ByOtherReason,
                    Css: "closed-wrongPwdOthers",
                    Tootltip: resources.ClosedByFailedLoginOrAccountExpired
                },
                {
                    Id: ClosedReason.ByAdmin,
                    Css: "closed-byAdmin",
                    Tootltip: resources.ClosedByAdmin
                },
                {
                    Id: ClosedReason.ByUpline,
                    Css: "closed-byUpline",
                    Tootltip: resources.ClosedByUpline
                },
                {
                    Id: ClosedReason.ByWrongOTPSC,
                    Css: "closed-wrongOTPSC",
                    Tootltip: resources.ClosedByFailedOTPOrSC
                }
            ],
            Suspended: {
                Css: "suspended",
                Tootltip: resources.ThisAccountWasSuspended
            },
            Disabled: {
                Css: "disabled",
                Tootltip: resources.ThisAccountWasDisabled
            },
            Enabled: {
                Css: 'enabled',
                Tootltip: ''
            }
        };

        if (customerInfo.RoleId === userLevelId) {
            if (customerInfo.Closed) {
                return statusInfo.Closed.find(closedReason => customerInfo.ClosedReason === closedReason.Id);
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

    buildSearchAccountInfoContent() {
        let {
            customerInfo,
            userLevelId,
            username,
            resources,
            mobileView
        } = this.props;

        let statusInfo = this.getStatusInfo(customerInfo, userLevelId, resources);

        const content =
            <CustomAnchorTag
                mobileView={mobileView}
                title={resources.Viewlog}
                classNames="pointer"
                href={customerInfo.ViewLogLink}
            >
                <span className={statusInfo.Css} data-username={username}> {username} </span>
                <span className="icon-viewlog" />
            </CustomAnchorTag>;

        const contentWithTooltip =
            <Tooltip
                title={statusInfo.Tootltip}
                position="bottom"
                trigger="mouseenter"
                arrow="true"
            >
                {content}
            </Tooltip>;

        return statusInfo.Tootltip === '' ? content : contentWithTooltip;
    }

    buildUplineLevelInfoContent() {
        let { username, handleSearchFromUserLevel} = this.props;
        return (
            <span 
                className="enabled"
                onClick={handleSearchFromUserLevel}
                data-username={username}
            >
                {username}
            </span>
        );
    }

    buildUserLevelInfoContent() {
        let { customerInfo, userLevelId } = this.props;

        return customerInfo.RoleId === userLevelId ?
            this.buildSearchAccountInfoContent() :
            this.buildUplineLevelInfoContent();
    }

    render() {
        let { userLevelName } = this.props;
        return (
            <div className="col-xs-12 row-info" >
                <div className="col-xs-5">
                    {userLevelName}
                </div>
                <div className="col-xs-7 col-right">
                    {
                        this.buildUserLevelInfoContent()
                    }
                </div>
            </div>
        );
    }
}