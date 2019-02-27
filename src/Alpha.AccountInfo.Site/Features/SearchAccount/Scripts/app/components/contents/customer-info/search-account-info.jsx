import * as React from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';
import CustomAnchorTag from '../../common/custom-anchor-tag';

const ClosedReason = {
    ByOtherReason: 0,
    ByAdmin: 1,
    ByUpline: 2,
    ByWrongOTPSC: 3
};

export default class SearchAccountInfo extends React.Component {
    constructor(props) {
        super(props);
        let statusInfo = this.getStatusInfo(this.props.customerInfo);
        this.state = {
            Css: statusInfo.Css,
            Tootltip: statusInfo.Tootltip
        };
    }

    componentWillReceiveProps(nextProps) {
        let { customerInfo } = this.props;
        if (nextProps.customerInfo !== customerInfo) {
            let statusInfo = this.getStatusInfo(nextProps.customerInfo);
            this.state = {
                Css: statusInfo.Css,
                Tootltip: statusInfo.Tootltip
            };
        }
    }

    getStatusInfo(customerInfo) {
        let { resources } = this.props;
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

        if (customerInfo.Closed) {
            return statusInfo.Closed.find(closedReason => customerInfo.ClosedReason === closedReason.Id);
        }
        else if (customerInfo.Suspended) {
            return statusInfo.Suspended;
        }
        else if (!customerInfo.Enabled) {
            return statusInfo.Disabled;
        }

        return statusInfo.Enabled;
    };

    render() {
        let { customerInfo, resources, mobileView } = this.props;
		let { Css, Tootltip } = this.state;

		let username = customerInfo.UserName;
		let maxLength = 15;
		let isTruncate = username.length > maxLength;

		if (Tootltip === '' && isTruncate) {
			Tootltip = username;
		}
		
        let customAnchorTagProps = {
            title: resources.Viewlog,
            classNames: "pointer",
            href: customerInfo.ViewLogLink,
            mobileView
        };

        let tooltipProps = {
            title: Tootltip,
            position: "bottom",
            trigger: "mouseenter",
            arrow: "true"
        };

        const content =
			<CustomAnchorTag {...customAnchorTagProps}>
				<span className={Css} data-username={customerInfo.UserName}> {isTruncate ? `${username.substring(0, maxLength)}...` : username} </span>
                <span className="icon-viewlog" />
            </CustomAnchorTag>;

        const contentWithTooltip =
            <Tooltip {...tooltipProps}>
                {content}
            </Tooltip>;

        return Tootltip === '' ? content : contentWithTooltip;
    }
}