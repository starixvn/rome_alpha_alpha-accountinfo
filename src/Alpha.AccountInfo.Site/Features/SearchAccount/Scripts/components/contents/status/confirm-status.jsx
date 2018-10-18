import React from 'react';
import { accountInfoAPI } from '../../../api-client';
import { resizeBootboxOnMobile } from '../../../helpers/accountinfo-helper';

export default class ConfirmStatus extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenPopup = this.handleOpenPopup.bind(this);
    }

    updateCustomerStatus(url, param) {
        $.blockUI();
        let { fetchCustomerInfoFromServer, username } = this.props;

        accountInfoAPI.updateCustomerStatus(url, JSON.parse(param)).then((data) => {
            if (data.IsSuccess) {
                setTimeout(() => {
                    $.unblockUI();
                    fetchCustomerInfoFromServer(username, false);
                }, 3000);
            }
            else {
                $.unblockUI();
            }
        });
    }

    handleOpenPopup(e) {
        let { mobileView, status} = this.props;

        if (!status.Disabled) {
            window.top.bootboxConfirm(status.ConfirmMessage, e.target, () => this.updateCustomerStatus(status.Url, status.JsonParams));
            if (mobileView) {
                resizeBootboxOnMobile();
            }
        }
    }

    render() {
        const { status} = this.props;
        const disabled = status.Disabled ? "disabled" : "";

        const inputProps = {
            id: status.displayOrder,
            type: "checkbox",
            disabled: status.Disabled,
            checked: status.Checked,
            onChange: () => { }
        };

        return (
            <div className={`col-xs-6 checkbox ${disabled}`} onClick={this.handleOpenPopup} >
                <input {...inputProps} />
                <label className="label-text" htmlFor={status.displayOrder}>{status.Text}</label>
            </div>
        );
    }
}
