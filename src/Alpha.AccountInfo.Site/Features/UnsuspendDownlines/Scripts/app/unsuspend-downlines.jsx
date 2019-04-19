import * as React from 'react';
import ReactDOM from 'react-dom';

window.renderUnsuspendDownlines = function (unsuspendModel) {
	ReactDOM.render(
		<UnsuspendDownlines 
			unsuspendModel={unsuspendModel}
		/>,
		document.getElementById('unsuspend-downlines')
	);
};

export default class UnsuspendDownlines extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			hideMessages: true,
			updateSuccess: false,
			message: '',
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
		const { unsuspendModel } = this.props;

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
							unsuspendModel.UnsuspendByLevel.map((level, index) => <UnsuspendRow {...{ level, key: index }} />)
						}
					</tbody>
					<tfoot>
						<tr>
							<th className="text-center">Total</th>
							<th className="text-center"><a href='#'>{unsuspendModel.AffectedDownlinesTotal}</a></th>
							<th className="text-center"><a href='#'>{unsuspendModel.UnchangedDownlinesTotal}</a></th>
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

export const UnsuspendRow = ({ level}) => {
	return (
		<tr>
			<td className="text-center">
				{level.RoleId}
			</td>
			<td className="text-center">
				<a href='#'>
					{level.AffectedDownlines}
				</a>
			</td>
			<td className="text-center">
				<a href='#'>
					{level.UnchangedDownlines}
				</a>
			</td>
		</tr>
	);
}