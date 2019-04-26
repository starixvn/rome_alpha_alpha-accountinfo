import * as React from 'react';
import ReactDOM from 'react-dom';
import * as _ from 'lodash';
import { unsuspendAPI, accountinfoUrl } from './unsuspend-api';
import UnsuspendViewList from './unspend-view-list';
import { decodeResources } from '../../../SearchAccount/Scripts/app/helpers/accountinfo-helper';

window.renderUnsuspendMultipe = function (viewModel, resources) {
    ReactDOM.render(
        <UnsuspendMultiple {...{ viewModel, resources: decodeResources(resources)}}/>,
        document.getElementById('unsuspend-multiple')
    );
};

export default class UnsuspendMultiple extends React.Component {
    constructor(props) {
        super(props);
        const { viewModel, resources } = this.props
        this.state = {
            showViewList: false,
            isAffected: false,
            searchAccountId: 0,
            downlineRoleId: 1,
            resources: resources,
            originalViewModel: _.cloneDeep(viewModel),
            viewModel: viewModel
        };

        this.handleReset = this.handleReset.bind(this);
        this.toggleSuspendAll = this.toggleSuspendAll.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.redirectViewListDetail = this.redirectViewListDetail.bind(this);
        this.redirectMainPage = this.redirectMainPage.bind(this);
    }

    handleReset() {
        this.setState((prevState) => ({
            viewModel: prevState.originalViewModel,
        }));
    }

    toggleSuspendAll(suspendAll) {
        let viewModelCopy = _.cloneDeep(this.state.viewModel);

        viewModelCopy.forEach(function (item) {
            item.Suspended = suspendAll;
        })

        this.setState({ viewModel: viewModelCopy });
    }

    handleChange(index, value) {
        const { viewModel } = this.state;
        let viewModelCopy = _.cloneDeep(viewModel);

        viewModelCopy[index].Suspended = value;

        this.setState({ viewModel: viewModelCopy });
    }

    redirectViewListDetail(searchAccountId, downlineRoleId, isAffected) {
        this.setState({
            searchAccountId: searchAccountId,
            showViewList: true,
            isAffected: isAffected,
            downlineRoleId: downlineRoleId
        });
    }

    redirectMainPage() {
        this.setState({ showViewList: false });
    }

    render() {
        const { searchAccountId, isAffected, downlineRoleId, originalViewModel, viewModel, resources} = this.state;

        return this.state.showViewList ?
            <UnsuspendViewList {...{
                isAffected,
                downlineRoleId,
                searchAccountId,
                resources,
                redirectMainPage: this.redirectMainPage
            }} /> :
            <MultipleView
                {...{
                    viewModel,
                    originalViewModel,
                    resources,
                    handleReset: this.handleReset,
                    toggleSuspendAll: this.toggleSuspendAll,
                    handleChange: this.handleChange,
                    redirectViewListDetail: this.redirectViewListDetail
                }}/>;
    }
}

export class MultipleView extends React.Component {
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

    handleSubmit(e) {
        $.blockUI();
        let self = this;
        const { viewModel } = this.props;
        let unsuspendList = [];

        viewModel.map(function (item) {
            unsuspendList.push({ CustId: item.CustId, Suspended: item.Suspended });
        });

        unsuspendAPI.updateSuspend(
            "UnsuspendDownlines/UnsuspendMultiple",
            { UnsuspendList: unsuspendList })
            .then((data) => {
                const isSuccess = data.length === 0;
                self.setState({
                    hideMessages: false,
                    message: isSuccess ? resources.UpdateSuccessfully : this.buildErrorMessage(data),
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
                {errors.map((eror, index) => <div key={index}>{eror.Message}</div>)}
            </div>
        );
    }

    render() {
        const { updateSuccess, message, hideMessages } = this.state;
        const { viewModel, originalViewModel, toggleSuspendAll, handleChange, redirectViewListDetail, handleReset, resources } = this.props
        const downlineRoleId = viewModel.length > 0 ? viewModel[0].RoleId : 1;
        const headerLevel = downlineRoleId == 2 ? `${resources.Master}/${resources.Agent}/${resources.Member}` : `${resources.Agent}/${resources.Member}`

        return (
            <div className="">
                <div className={hideMessages ? `hidden` : ``}>
                    <span className={updateSuccess ? 'col-xs-12 msg msg-success' : 'col-xs-12 msg msg-error'}>  {message} </span>
                </div>

                <table className="tblRpt tblRpt-bordered width-100per">
                    <thead>
                        <tr>
                            <th rowSpan='2'>{resources.Username}</th>
                            <th>
                                {resources.SuspendStatus}
                            </th>
                            {downlineRoleId > 1 && <th rowSpan='2'>{resources.AffectedDownlines} <br /> ({headerLevel})</th>}
                            {downlineRoleId > 1 && <th rowSpan='2'>{resources.RemainUnchangedDownlines} <br /> ({headerLevel})</th>}
                        </tr>
                        <tr>
                            <th>
                                <button className="suspend-all-button" onClick={() => toggleSuspendAll(true)}>{resources.Suspend}</button>
                                <button className="btn-default" onClick={() => toggleSuspendAll(false)}>{resources.Unsuspend}</button>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            viewModel.map((item, index) =>
                                <RowContent
                                    originalViewModel={originalViewModel}
                                    item={item}
                                    downlineRoleId={downlineRoleId}
                                    key={index}
                                    index={index}
                                    handleChange={handleChange}
                                    redirectViewListDetail={redirectViewListDetail}
                                    resources={resources}
                                />)
                        }
                    </tbody>
                </table>
                <div className="col-xs-12 row-info popup-bottom list-button text-right">
                    <button className="btn-reset" onClick={handleReset}>{resources.Reset}</button>
                    <button className="btn-submit" onClick={this.handleSubmit}>{resources.Submit}</button>
                </div>
            </div >
        );
    }
}

export const RowContent = ({ originalViewModel, item, downlineRoleId, index, handleChange, redirectViewListDetail, resources }) => {
    const length = item.UnsuspendByLevel.length;
    const showValue = originalViewModel[index].Suspended !== item.Suspended;

    return (
        <tr key={index}>
            <td className="text-center">
                {item.Username}
            </td>
            <td className="text-center">
                <div onClick={() => handleChange(index, !item.Suspended)} className={item.Suspended ? `suspend-button` : `unsuspend-button`}>
                    {item.Suspended ? resources.Suspended : resources.Unsuspended}
                </div>
            </td>
            {
                downlineRoleId > 1 && <td className="text-center">
                    {
                        showValue && (originalViewModel[index].Suspended ? item.UnsuspendByLevel.map((level, index) => {
                            return <ColumnContentByLevel
                                key={index}
                                downlines={level.AffectedDownlines}
                                showSlash={index < length - 1}
                                isAffected={true}
                                downlineRoleId={level.RoleId}
                                searchAccountId={item.CustId}
                                redirectViewListDetail={redirectViewListDetail}
                            />;
                        }) : resources.All)
                    }
                </td>
            }

            {
                downlineRoleId > 1 && <td className="text-center">
                    {
                        showValue && (originalViewModel[index].Suspended ? item.UnsuspendByLevel.map((level, index) => {
                            return <ColumnContentByLevel
                                key={index}
                                downlines={level.UnchangedDownlines}
                                showSlash={index < length - 1}
                                isAffected={false}
                                downlineRoleId={level.RoleId}
                                searchAccountId={item.CustId}
                                redirectViewListDetail={redirectViewListDetail}
                            />;
                        }) : resources.All)
                    }
                </td>
            }
        </tr>);
}

export const ColumnContentByLevel = ({ downlines, showSlash, isAffected, downlineRoleId, searchAccountId, redirectViewListDetail }) => {
    const text = `${downlines} ${showSlash ? '/' : ''}`;
    return downlines === 0 ? <span>{text}</span> :
        <a href='#' onClick={() => redirectViewListDetail(searchAccountId, downlineRoleId, isAffected)}> {text}</a>;
}