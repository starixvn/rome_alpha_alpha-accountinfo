import * as React from 'react';
import ReactDOM from 'react-dom';

window.renderUnsuspendSingle = function (viewModel) {
	ReactDOM.render(
		<UnsuspendDownlines viewModel={viewModel} />,
        document.getElementById('unsuspend-single')
	);
};

export default class UnsuspendDownlines extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			hideMessages: true,
			updateSuccess: false,
			message: ''
		};

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
                                    <UnsuspendRow {...{ level, searchAccountId: viewModel.SearchAccountId, key: index }} />)
						}
					</tbody>
					<tfoot>
						<tr>
							<th className="text-center">Total</th>
                                <th className="text-center"><a href='#'>{viewModel.AffectedDownlinesTotal}</a></th>
                                <th className="text-center"><a href='#'>{viewModel.UnchangedDownlinesTotal}</a></th>
						</tr>
					</tfoot>
					</table>
				</ div>
				<div className="col-xs-12 row-info popup-bottom list-button text-right">
					<button className="btn-submit">Unsuspend</button>
				</div>
			</div >
		);
	}
}

export const UnsuspendRow = ({ level, searchAccountId }) => {
    const accountinfoUrl = top.alpha.urls.accountInfoRootPath;

	return (
		<tr>
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