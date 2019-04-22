import * as React from 'react';
import ReactDOM from 'react-dom';
import { unsuspendAPI } from './unsuspend-api';

window.renderUnsuspendSingle = function (viewModel) {
	ReactDOM.render(
        <UnsuspendSingle viewModel={viewModel} />,
        document.getElementById('unsuspend-single')
	);
};

export default class UnsuspendSingle extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			hideMessages: true,
			updateSuccess: false,
			message: ''
		};

        this.handleSubmit = this.handleSubmit.bind(this);
		this.handleKeyPress = this.handleKeyPress.bind(this);
	}

	handleKeyPress(e) {
		if (e.key === 'Enter') {
			this.handleSubmit(e);
		}
	}

	showErrorMessage(message) {
		this.setState((prevState) => ({
			hideMessages: false,
			message: message,
			updateSuccess: false
		}));
	}

	buildNote() {
		return (
			<div className="note">
				List of downlines whose status are changed/unchanged after going to unsuspend 'selected customer'
            </div>
		);
	}

    handleSubmit(e) {
        let self = this;

        $.blockUI();

        unsuspendAPI.updateSuspend(
            "UnsuspendDownlines/UnsuspendSingle",
            { SearchAccountId: this.props.viewModel.SearchAccountId })
            .then((data) => {
            self.setState({
                hideMessages: false,
                message: data.IsSuccess ? 'Update Successfully' : data.Message.trim(),
                updateSuccess: data.IsSuccess
            });

            if (data.IsSuccess) {
                setTimeout(() => {
                    window.top.realoadLeftPanel = true;
                    window.top.closedPopup = true;
                    window.top.ageWnd.close();
                }, 1500);
            }

            $.unblockUI();
        });
    }

    buildBodyRow(level, searchAccountId, index) {
        const accountinfoUrl = top.alpha.urls.accountInfoRootPath;

        return (
            <tr key={index}>
                <td className="text-center">
                    {level.RoleId}
                </td>
                <td className="text-center">
                    <a href={`${accountinfoUrl}/UnsuspendDownlines/ViewUnsuspendList?SearchAccountId=${searchAccountId}`}>
                        {level.AffectedDownlines}
                    </a>
                </td>
                <td className="text-center">
                    <a href={`${accountinfoUrl}/UnsuspendDownlines/ViewUnsuspendList?SearchAccountId=${searchAccountId}`}>
                        {level.UnchangedDownlines}
                    </a>
                </td>
            </tr>
        );
    }

	render() {
		const { updateSuccess, message, hideMessages, resources } = this.state;
        const { viewModel } = this.props;

		return (
			< div className="col-xs-12 " >
				<div className={hideMessages ? `hidden` : ``}>
					<span className={updateSuccess ? 'col-xs-12 msg msg-success' : 'col-xs-12 msg msg-error'}>  {message} </span>
				</div>

				{this.buildNote()}
				< div className="col-xs-12 " >
				<table className="tblRpt tblRpt-bordered width-100per">
					<thead>
						<tr>
							<th>Level</th>
							<th>Affected Downlines</th>
							<th>Remain Unchanged Downlines</th>
						</tr>
					</thead>
					<tbody>
						{
                                viewModel.UnsuspendByLevel.map((level, index) =>
                                    this.buildBodyRow(level, viewModel.SearchAccountId, index))
						}
					</tbody>
					<tfoot>
						<tr>
							<th className="text-center">Total</th>
                                <th className="text-center">
                                    <a href='#'>
                                        {viewModel.AffectedDownlinesTotal}
                                    </a>
                                </th>
                                <th className="text-center">
                                    <a href='#'>
                                        {viewModel.UnchangedDownlinesTotal}
                                    </a>
                                </th>
						</tr>
					</tfoot>
					</table>
				</ div>
				<div className="col-xs-12 row-info popup-bottom list-button text-right">
                    <button className="btn-submit" onClick={this.handleSubmit}>Unsuspend</button>
				</div>
			</div >
		);
	}
}