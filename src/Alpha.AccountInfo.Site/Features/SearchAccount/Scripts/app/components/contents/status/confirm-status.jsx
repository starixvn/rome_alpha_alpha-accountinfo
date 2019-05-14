import React from 'react';
import { accountInfoAPI } from '../../../accountinfo-api';
import { resizeBootboxOnMobile, resizePopupOnMobile, buildTooltipProps, formatMessage, usernameRegExp } from '../../../helpers/accountinfo-helper';
import { Tooltip } from 'react-tippy';

export default class ConfirmStatus extends React.Component {
	constructor(props) {
		super(props);
        this.handleOpenPopup = this.handleOpenPopup.bind(this);
        this.openBootboxConfirm = this.openBootboxConfirm.bind(this);
        this.openPopup = this.openPopup.bind(this);
		this.updateCustomerStatus = this.updateCustomerStatus.bind(this);
	}

	updateCustomerStatus(e) {
		$.blockUI();
		let { fetchCustomerAllStatusFromServer, fetchCustomerInfoFromServer, username, status, mobileView } = this.props;

		accountInfoAPI
			.updateCustomerStatus(status.Url, JSON.parse(status.JsonParams))
			.then((data) => {
				if (data.IsSuccess) {
					setTimeout(() => {
						if (status.ReloadCustomerInfo) {
							fetchCustomerInfoFromServer(username, false, false);
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
        let { status } = this.props;
		
        if (!status.Disabled) {
            status.IsPopup ? this.openPopup(e) : this.openBootboxConfirm(e);
		}
    }

    openPopup(e) {
        let { fetchCustomerAllStatusFromServer, fetchCustomerInfoFromServer, username, status, mobileView, resources } = this.props;
        window.top.closedPopup = false;
        window.top.realoadLeftPanel = false;
        let timer = setInterval(reloadCustomerStatus, 1000);

        function reloadCustomerStatus() {
            if (window.top.closedPopup) {
                clearInterval(timer);
                if (window.top.realoadLeftPanel) {
                    fetchCustomerInfoFromServer(username, false, false);
                    fetchCustomerAllStatusFromServer(true);
                }
            }
        }

        const headerPopupText = usernameRegExp(resources.UnuspendStatusPopupHeader, username);
        window.top.ageWnd.open(status.Url, headerPopupText, undefined, undefined, 700, 500);
        if (mobileView) {
            resizePopupOnMobile();
        }
    }

    openBootboxConfirm(e) {
        let { status, mobileView } = this.props;
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
			type: "checkbox",
			checked: status.Checked,
			onChange: () => { }
		};

        let content = <div>
            <input {...inputProps} />
			<label className="label-text" htmlFor={status.displayOrder}>
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
