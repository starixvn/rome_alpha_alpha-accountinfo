import React from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';
import CustomAnchorTag from '../../common/custom-anchor-tag';
import { buildTooltipProps } from '../../../helpers/accountinfo-helper';

const ClosedReason = {
    ByOtherReason: 0,
    ByAdmin: 1,
    ByUpline: 2,
    ByWrongOTPSC: 3
};

export default class SearchAccountInfo extends React.Component {
    constructor(props) {
        super(props);
        const statusInfo = this.getStatusInfo(this.props.customerInfo);
        this.state = {
            Css: statusInfo.Css,
            Tootltip: statusInfo.Tootltip
        };
    }

    componentWillReceiveProps(nextProps) {
        const { customerInfo } = this.props;
        if (nextProps.customerInfo !== customerInfo) {
            const statusInfo = this.getStatusInfo(nextProps.customerInfo);
            this.state = {
                Css: statusInfo.Css,
                Tootltip: statusInfo.Tootltip
            };
        }
    }

    getStatusInfo(customerInfo) {
        const { resources } = this.props;
        const statusInfo = {
            Closed: [
                {
                    Id: ClosedReason.ByOtherReason,
                    Css: 'closed-wrongPwdOthers',
                    Tootltip: resources.ClosedByFailedLoginOrAccountExpired
                },
                {
                    Id: ClosedReason.ByAdmin,
                    Css: 'closed-byAdmin',
                    Tootltip: resources.ClosedByAdmin
                },
                {
                    Id: ClosedReason.ByUpline,
                    Css: 'closed-byUpline',
                    Tootltip: resources.ClosedByUpline
                },
                {
                    Id: ClosedReason.ByWrongOTPSC,
                    Css: 'closed-wrongOTPSC',
                    Tootltip: resources.ClosedByFailedOTPOrSC
                }
            ],
            Suspended: {
                Css: 'suspended',
                Tootltip: resources.ThisAccountWasSuspended
            },
            Disabled: {
                Css: 'disabled',
                Tootltip: resources.ThisAccountWasDisabled
            },
            Enabled: {
                Css: 'enabled',
                Tootltip: ''
            }
        };

        if (!customerInfo.Enabled) {
            return statusInfo.Disabled;
        }
        else if (customerInfo.Closed) {
            return statusInfo.Closed.find(closedReason => customerInfo.ClosedReason === closedReason.Id);
        }
        else if (customerInfo.Suspended) {
            return statusInfo.Suspended;
        }
        return statusInfo.Enabled;
    };

    render() {
        const { customerInfo, resources, mobileView } = this.props;
        let { Css, Tootltip } = this.state;

        const username = customerInfo.UserName;
        const maxLength = 15;
        const isTruncate = username.length > maxLength;

        if (Tootltip === '' && isTruncate) {
            Tootltip = username;
        }

        const customAnchorTagProps = {
            title: resources.Viewlog,
            classNames: 'pointer',
            href: customerInfo.ViewLogLink,
            mobileView
        };

        const spanContent = <span className={Css} data-username={customerInfo.UserName}> {isTruncate ? `${username.substring(0, maxLength)}...` : username} </span>;

        const spanContentViewLog =
            <CustomAnchorTag {...customAnchorTagProps}>
                {spanContent}
                <span className='icon-viewlog' />
            </CustomAnchorTag>;

        const content = customerInfo.CanViewLog ? spanContentViewLog : spanContent;

        const contentWithTooltip =
            <Tooltip {...buildTooltipProps(Tootltip)}>
                {content}
            </Tooltip>;

        return Tootltip === '' ? content : contentWithTooltip;
    }
}