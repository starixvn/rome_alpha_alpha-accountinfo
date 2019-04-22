import * as React from 'react';
import ReactDOM from 'react-dom';
import * as _ from 'lodash'; 
import { unsuspendAPI } from './unsuspend-api';

window.renderUnsuspendMultipe = function (viewModel) {
	ReactDOM.render(
        <UnsuspendMultiple viewModel={viewModel}/>,
		document.getElementById('unsuspend-multiple')
	);
};

export default class UnsuspendMultiple extends React.Component {
	constructor(props) {
        super(props);
        const { viewModel } = this.props
		this.state = {
			hideMessages: true,
			updateSuccess: false,
            message: '',
            originalViewModel: _.cloneDeep(viewModel),
            viewModel: viewModel
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleKeyPress = this.handleKeyPress.bind(this);
        this.handleToggleSuspendAll = this.handleToggleSuspendAll.bind(this);
        this.handleSuspendAll = this.handleSuspendAll.bind(this);
        this.handleUnsuspendAll = this.handleUnsuspendAll.bind(this);
        this.handleReset = this.handleReset.bind(this);
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

    handleReset() {
        const { originalViewModel } = this.state;
        this.setState({ viewModel: originalViewModel });    }

    handleSuspendAll() {
        this.handleToggleSuspendAll(true);
    }

    handleUnsuspendAll() {
        this.handleToggleSuspendAll(false);
    }

    handleToggleSuspendAll(suspendAll) {
        const { viewModel } = this.state;
        let viewModelCopy = _.cloneDeep(viewModel);

        viewModelCopy.forEach(function (item) {
            item.Suspended = suspendAll;
        })

        this.setState({ viewModel: viewModelCopy });
    }

    handleSubmit(e) {
        let self = this;
        const { viewModel } = this.state;
        $.blockUI();

        let unsuspendList = [];

        viewModel.map(function (item, index) {
            unsuspendList.push({ CustId: item.CustId, Suspended: item.Suspended });
        });

        unsuspendAPI.updateSuspend(
            "UnsuspendDownlines/UnsuspendMultiple",
            { UnsuspendList: unsuspendList })
            .then((data) => {
                const isSuccess = data.length === 0;
            self.setState({
                hideMessages: false,
                message: isSuccess ? 'Update Successfully' : this.buildErrorMessage(data),
                updateSuccess: isSuccess
            });

                if (isSuccess) {
                setTimeout(() => {
                    window.top.realoadLeftPanel = true;
                    window.top.closedPopup = true;
                    window.top.ageWnd.close();
                }, 1500);
            }

            $.unblockUI();
        });
    }

    buildErrorMessage(errors) {
        return (
            <div>
                {
                    errors.map((eror, index) => <div key={index}>{eror.Message}</div>)
                }
            </div>);
    }

	render() {
        const { updateSuccess, message, hideMessages, resources, viewModel } = this.state;

		return (
			< div className="" >
				<div className={hideMessages ? `hidden` : ``}>
					<span className={updateSuccess ? 'col-xs-12 msg msg-success' : 'col-xs-12 msg msg-error'}>  {message} </span>
				</div>

				<table className="tblRpt tblRpt-bordered width-100per">
					<thead>
						<tr>
							<th rowSpan='2'>Username</th>
                            <th>
                              Suspend Status
                            </th>
							<th rowSpan='2'>Affected Downlines <br/> (Master/Agent/Member)</th>
								<th rowSpan='2'>Remain Unchanged Downlines <br/> (Master/Agent/Member)</th>
						</tr>
                        <tr>
                            <th>
                                <button className="btn-refresh" onClick={this.handleSuspendAll}>Suspend</button>
                                <button className="btn-default" onClick={this.handleUnsuspendAll}>Unsuspend</button>
                            </th>
						</tr>
					</thead>
					<tbody>
						{
                            viewModel.map((item, index) => <UnsuspendRow {...{ item, key: index }} />)
						}
					</tbody>
				</table>
                <div className="col-xs-12 row-info popup-bottom list-button">
                    <button className="btn-reset" onClick={this.handleReset}>Reset</button>
                    <button className="btn-submit" onClick={this.handleSubmit}>Unsuspend</button>
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

    const length = item.UnsuspendByLevel.length;
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
                                {level.AffectedDownlines} {index === length - 1 ? `` : `/ `}
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
                                {level.UnchangedDownlines} {index === length - 1 ? `` : `/ `}
							</a>
						);
					})
				}
			</td>
		</tr>
	);
}