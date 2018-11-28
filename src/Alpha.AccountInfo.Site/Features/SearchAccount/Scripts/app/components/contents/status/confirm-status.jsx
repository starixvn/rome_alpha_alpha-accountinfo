import React from 'react';
import { accountInfoAPI } from '../../../api-client';
import { resizeBootboxOnMobile } from '../../../helpers/accountinfo-helper';

export default class ConfirmStatus extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenPopup = this.handleOpenPopup.bind(this);
    }

    updateCustomerStatus() {
        $.blockUI();
        let { fetchCustomerAllStatusFromServer, fetchCustomerInfoFromServer, username, status } = this.props;

        accountInfoAPI
            .updateCustomerStatus(status.Url, JSON.parse(status.JsonParams))
            .then((data) => {
                if (data.IsSuccess) {
                    setTimeout(() => {
                        $.unblockUI();
                        if (status.ReloadCustomerInfo) {
                            fetchCustomerInfoFromServer(username, false, false);
                        }

                        fetchCustomerAllStatusFromServer(true);
                    }, 3000);
                }
                else {
                    $.unblockUI();
                }
            });
    }

    handleOpenPopup(e) {
        let { mobileView, status } = this.props;

        if (!status.Disabled) {
            window.top.bootboxConfirm(
                status.ConfirmMessage,
                e.target,
                () => this.updateCustomerStatus()
            );

            if (mobileView) {
                resizeBootboxOnMobile();
            }
        }
    }

    render() {
        const { status } = this.props;

        const inputProps = {
            type: "checkbox",
            checked: status.Checked,
            onChange: () => { }
        };

        return (
            <div
                id={`confirm-${status.Text}`}
                className={`col-xs-6 checkbox ${status.Disabled ? 'disabled' : ''}`}
                onClick={this.handleOpenPopup}
            >
                <input {...inputProps} />
                <label
                    className="label-text"
                    htmlFor={status.displayOrder}
                >
                    {status.Text}
                </label>
            </div>
        );
    }
}
