import React from 'react';
import { accountInfoAPI } from '../../../api-client';
import { resizeBootboxOnMobile, buildTooltipProps, formatMessage } from '../../../helpers/accountinfo-helper';
import { Tooltip } from 'react-tippy';

export default class ConfirmStatus extends React.Component {
	constructor(props) {
		super(props);
		this.handleOpenPopup = this.handleOpenPopup.bind(this);
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
					}, 1000);
				}
				else {
					window.top.bootboxAlert(data.Message, mobileView ? null : $(`#confirm-${status.DisplayOrder}`)[0]);
					$.unblockUI();
				}
			});
	}

	handleOpenPopup(e) {
		let { mobileView, status } = this.props;

		if (!status.Disabled) {
			window.top.bootboxConfirm(
				formatMessage(status.ConfirmMessage),
				e.target,
				this.updateCustomerStatus
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

		let content = <div><input {...inputProps} />
			<label
				className="label-text"
				htmlFor={status.displayOrder}
			>
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
