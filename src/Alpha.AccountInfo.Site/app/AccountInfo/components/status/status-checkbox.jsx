import React from 'react';
import { accountInfoAPI } from '../../api-client';

export default class StatusCheckbox extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpen = this.handleOpen.bind(this);
    }

    handleOpen(e) {
        var self = this;
        var url = this.props.status.Url;
        var param = self.props.status.Param;
        var confirmMessage = this.props.status.ConfirmMessage;

        confirmMessage == undefined ? window.top.ageWnd.open(url) :
            window.top.bootboxConfirm(confirmMessage, e,
                function () {
                    accountInfoAPI.updateStatus(url, JSON.parse(param)).then((data) => {
                        if (data.IsSuccess) {
                            setTimeout(() => {
                                self.props.fetchCustomerAllStatusFromServer();
                                self.props.fetchCustomerInfoFromServer(e, self.props.username);
                            }, 2000);
                        }
                    });
                });
    };

    render() {
        const { status, displayOrder } = this.props;

        return (
            <div>
                <div className={`${status.Disabled && "disabled"} col checkbox`} >
                    <input id={displayOrder} type="checkbox" disabled={status.Disabled} checked={status.Checked} onChange={this.handleOpen} />
                    <label className="label-text" htmlFor={displayOrder}>{status.Text}</label>
                </div>
            </div>
        );
    }
}