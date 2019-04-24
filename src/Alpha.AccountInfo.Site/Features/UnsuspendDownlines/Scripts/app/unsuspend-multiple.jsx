import * as React from 'react';
import ReactDOM from 'react-dom';
import * as _ from 'lodash';
import { unsuspendAPI } from './unsuspend-api';

window.renderUnsuspendMultipe = function (viewModel) {
    ReactDOM.render(
        <UnsuspendMultiple viewModel={viewModel} />,
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
        this.setState({ viewModel: originalViewModel });
    }

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
                { errors.map((eror, index) => <div key={index}>{eror.Message}</div>) }
            </div>
        );
    }

    handleChange(index, value) {
        const { viewModel } = this.state;
        let viewModelCopy = _.cloneDeep(viewModel);

        viewModelCopy[index].Suspended = value;

        this.setState({ viewModel: viewModelCopy });
    }

    buildViewListUrl(searchAccountId, searchAccountRoleId, downlines, IsAffected, downlineRoleId, showSlash) {
        const accountinfoUrl = top.alpha.urls.accountInfoRootPath;
        let custIds = '';
        const { viewModel } = this.state;

        viewModel.forEach(function (item) {
            custIds = custIds + item.CustId + ',';
        })

        const href = `${accountinfoUrl}/UnsuspendDownlines/ViewUnsuspendList` +
            `?SearchAccountId=${searchAccountId}&SearchAccountRoleId=${searchAccountRoleId}&CustIds=${custIds}&DownlineRoleId=${downlineRoleId}` +
            `&IsAffected=${IsAffected}`;

        const text = `${downlines} ${showSlash ? '/': ''}`;
        return downlines === 0 ? text : <a href={href}> {text}</a>
    }

    buildRow(item, downlineRoleId, index) {
        const accountinfoUrl = top.alpha.urls.accountInfoRootPath;

        const length = item.UnsuspendByLevel.length;
        const { originalViewModel } = this.state;
        const showValue = originalViewModel[index].Suspended !== item.Suspended;
        return (
            <tr key={index}>
                <td className="text-center">
                    {item.Username}
                </td>
                <td className="text-center">
                    <a onClick={() => this.handleChange(index, !item.Suspended)}>
                        <img alt="test" className="suspend-button" src={`${accountinfoUrl}/Features/UnsuspendDownlines/Styles/Images/${item.Suspended ? 'suspened.png' : 'Unsuspend.png'}`} />
                        {item.Suspended ? `Suspended` : `Unsuspended`}
                    </a>
                </td>
                {
                    downlineRoleId > 1 && <td className="text-center">
                        {
                            showValue && (originalViewModel[index].Suspended ? item.UnsuspendByLevel.map((level, index) =>
                                        this.buildViewListUrl(level.CustId, item.UplineRoleId, level.AffectedDownlines, true, level.RoleId, index < length - 1)) : 'All')
                        }
                    </td>
                }

                {
                    downlineRoleId > 1 && <td className="text-center">
                        {
                            showValue && (originalViewModel[index].Suspended ?  item.UnsuspendByLevel.map((level, index) =>
                                this.buildViewListUrl(level.CustId, item.UplineRoleId, level.UnchangedDownlines, true, level.RoleId, index < length - 1)) : 'All')
                        }
                    </td>
                }
            </tr>);
    }

    render() {
        const { updateSuccess, message, hideMessages, resources, viewModel } = this.state;
        const downlineRoleId = viewModel.length > 0 ? viewModel[0].RoleId : 1;
        const headerLevel = downlineRoleId == 2 ? 'Master/Agent/Member' : 'Agent/Member';

        return (
            <div className="">
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
                            {downlineRoleId > 1 && <th rowSpan='2'>Affected Downlines <br /> ({headerLevel})</th>}
                            {downlineRoleId > 1 && <th rowSpan='2'>Remain Unchanged Downlines <br /> ({headerLevel})</th>}
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
                            viewModel.map((item, index) => this.buildRow(item, downlineRoleId, index))
                        }
                    </tbody>
                </table>
                <div className="col-xs-12 row-info popup-bottom list-button text-right">
                    <button className="btn-reset" onClick={this.handleReset}>Reset</button>
                    <button className="btn-submit" onClick={this.handleSubmit}>Submit</button>
                </div>
            </div >
        );
    }
}