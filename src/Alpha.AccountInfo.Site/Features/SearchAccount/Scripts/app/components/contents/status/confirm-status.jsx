import React from 'react';
import { accountInfoAPI } from '../../../accountinfo-api';
import { LoadCustomerInfo } from '../../../constants';
import { resizeBootboxOnMobile, resizePopupOnMobile, buildTooltipProps, formatMessage, usernameRegExp } from '../../../helpers/accountinfo-helper';
import { Tooltip } from 'react-tippy';

export default class ConfirmStatus extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenPopup = this.handleOpenPopup.bind(this);
        this.openBootboxConfirm = this.openBootboxConfirm.bind(this);
        this.openPopup = this.openPopup.bind(this);
        this.updateCustomerStatus = this.updateCustomerStatus.bind(this);
        this.reloadCustomerConfirmStatus = this.reloadCustomerConfirmStatus.bind(this);

        this.reloadCustomerConfirmStatusTimer = null;
    }

    componentDidUpdate() {
        clearInterval(this.reloadCustomerConfirmStatusTimer);
    }

    componentWillUnmount() {
        clearInterval(this.reloadCustomerConfirmStatusTimer);
    }

    updateCustomerStatus() {
        $.blockUI();
        const { fetchCustomerAllStatusFromServer, fetchCustomerInfoFromServer, username, status, mobileView } = this.props;

        accountInfoAPI
            .updateCustomerStatus(status.Url, JSON.parse(status.JsonParams))
            .then(data => {
                if (data.IsSuccess) {
                    setTimeout(() => {
                        if (status.ReloadCustomerInfo) {
                            fetchCustomerInfoFromServer(username, LoadCustomerInfo.ByUpdateStatus, false, false);
                        }

                        fetchCustomerAllStatusFromServer(true);
                    }, 1500);
                }
                else {
                    window.top.bootboxAlert(data.Message, mobileView ? null : $(`#confirm-${status.DisplayOrder}`)[0]);
                    $.unblockUI();
                }
            });
    }

    handleOpenPopup(e) {
        const { status } = this.props;

        if (!status.Disabled) {
            status.IsPopup ? this.openPopup(e) : this.openBootboxConfirm(e);
        }
    }

    reloadCustomerConfirmStatus() {
        const { fetchCustomerAllStatusFromServer, fetchCustomerInfoFromServer, username } = this.props;
        if (window.top.isClearInterval) {
            clearInterval(this.reloadCustomerConfirmStatusTimer);
            if (window.top.reloadLeftPanel) {
                fetchCustomerInfoFromServer(username, LoadCustomerInfo.ByUpdateStatus, false, false);
                fetchCustomerAllStatusFromServer(true);
            }
        }
    }

    openPopup() {
        const { username, status, mobileView, resources } = this.props;
        window.top.isClearInterval = false;
        window.top.reloadLeftPanel = false;
        this.reloadCustomerConfirmStatusTimer = setInterval(this.reloadCustomerConfirmStatus, 1000);

        const headerPopupText = usernameRegExp(resources.UnuspendStatusPopupHeader, username);
        window.top.ageWnd.open(status.Url, headerPopupText, undefined, undefined, 700, 500);
        if (mobileView) {
            resizePopupOnMobile();
        }
    }

    openBootboxConfirm(e) {
        const { status, mobileView } = this.props;
        window.top.bootboxConfirm(
            formatMessage(status.ConfirmMessage),
            e.target,
            this.updateCustomerStatus
        );

        if (mobileView) {
            resizeBootboxOnMobile();
        }
    }

    render() {
        const { status } = this.props;

        const inputProps = {
            type: 'checkbox',
            checked: status.Checked,
            onChange: () => {
                // onChange on parent elememt
            }
        };

        const content = <div>
            <input {...inputProps} />
            <label className='label-text' htmlFor={status.displayOrder}>
                {status.Text}
            </label>
        </div>;

        return (<div
            id={`confirm-${status.DisplayOrder}`}
            className={`col-xs-6 checkbox ${status.Disabled ? 'disabled' : ''}`}
            onClick={this.handleOpenPopup}
        >
            {(typeof status.Tooltip === 'undefined') || status.Tooltip === '' ?
                content : <Tooltip {...buildTooltipProps(status.Tooltip)}>{content}</Tooltip>}

        </div>);
    }
}
