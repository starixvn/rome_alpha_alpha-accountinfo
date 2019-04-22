import * as React from 'react';
import ReactDOM from 'react-dom';

window.renderUnsuspendMultipe = function (viewModel) {
	ReactDOM.render(
		<UnsuspendDownlinesMultiple viewModel={viewModel}/>,
		document.getElementById('unsuspend-multiple')
	);
};

export default class UnsuspendDownlinesMultiple extends React.Component {
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


	render() {
		const { updateSuccess, message, hideMessages, resources } = this.state;
        const { viewModel } = this.props;

		return (
			< div className="" >
				<div className={hideMessages ? `hidden` : ``}>
					<span className={updateSuccess ? 'col-xs-12 msg msg-success' : 'col-xs-12 msg msg-error'}>  {message} </span>
				</div>

				<table className="tblRpt tblRpt-bordered width-100per">
					<thead>
						<tr>
							<th rowSpan='2'>Username</th>
							<th>Suspend Status</th>
							<th rowSpan='2'>Affected Downlines <br/> (Master/Agent/Member)</th>
								<th rowSpan='2'>Remain Unchanged Downlines <br/> (Master/Agent/Member)</th>
						</tr>
						<tr>
							<th>Suspended button</th>
						</tr>
					</thead>
					<tbody>
						{
                            viewModel.map((item, index) => <UnsuspendRow {...{ item, key: index }} />)
						}
					</tbody>
				</table>
				<div className="col-xs-12 row-info popup-bottom list-button">
					<button className="btn-reset">Reset</button>
					<button className="btn-submit">Unsuspend</button>
				</div>
			</div >
		);
	}
}

export const UnsuspendRow = ({ item }) => {
	//const checked = item.Suspended ? 'checked' : '';
	const inputProps = {
		type: "checkbox",
		checked: item.Suspended,
		onChange: () => { }
	};

	return (
		<tr>
			<td className="text-center">
				{item.Username}
			</td>
			<td className="text-center">
				<input {...inputProps }/>
			</td>
			<td className="text-center">
				{
					item.UnsuspendByLevel.map((level, index) => {
						return (
							<a href='#' key={index}>
								{level.AffectedDownlines} /
							</a> 
						);
					})
				}
			</td>

			<td className="text-center">
				{
					item.UnsuspendByLevel.map((level, index) => {
						return (
							<a href='#' key={index}>
								{level.UnchangedDownlines} /
							</a>
						);
					})
				}
			</td>
		</tr>
	);
}