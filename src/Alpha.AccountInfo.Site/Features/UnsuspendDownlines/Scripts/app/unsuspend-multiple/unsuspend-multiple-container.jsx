import * as React from 'react';
import ReactDOM from 'react-dom';
import * as _ from 'lodash';
import UnsuspendViewList from '../unspend-view-list';
import UnsuspendMultiple from './unsuspend-multiple';
import { decodeResources } from '../../../../SearchAccount/Scripts/app/helpers/accountinfo-helper';

export const renderUnsuspendMultipe = (viewModel, resources) => {
    ReactDOM.render(
        <UnsuspendMultipleContainer {...{
            viewModel : viewModel.Data,
            searchAccountRoleId: viewModel.SearchAccountRoleId,
            resources: decodeResources(resources)}}/>,
        document.getElementById('container')
    );
};

window.renderUnsuspendMultipe = renderUnsuspendMultipe;

export default class UnsuspendMultipleContainer extends React.Component {
    constructor(props) {
        super(props);
        this.correctData();
        const { viewModel, searchAccountRoleId, resources } = this.props;

        this.state = {
            showViewList: false,
            isAffected: false,
            searchAccountId: 0,
            searchAccountRoleId: searchAccountRoleId,
            searchAccountName: '',
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

    correctData() {
        const { viewModel, searchAccountRoleId } = this.props;
        _.forEach(viewModel, function (dataRow, index, arr) {
            let currentLevel = dataRow.Downlines.length;
            while (currentLevel < searchAccountRoleId - 1) {
                dataRow.Downlines.push({
                    CustId: 0,
                    DownlineRoleId: 0,
                    AffectedDownlines: 0,
                    UnchangedDownlines: 0
                });

                currentLevel++;
            }
        });
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

    redirectViewListDetail(searchAccountId, searchAccountName, downlineRoleId, isAffected) {
        this.setState({
            searchAccountId: searchAccountId,
            searchAccountName: searchAccountName,
            showViewList: true,
            isAffected: isAffected,
            downlineRoleId: downlineRoleId
        });
    }

    redirectMainPage() {
        this.setState({ showViewList: false });
    }

    render() {
        const { searchAccountId, searchAccountRoleId, searchAccountName, isAffected, downlineRoleId, originalViewModel, viewModel, resources} = this.state;

        return this.state.showViewList ?
            <UnsuspendViewList {...{
                isAffected,
                downlineRoleId,
                searchAccountId,
                searchAccountName,
                resources,
                redirectMainPage: this.redirectMainPage
            }} /> :
            <UnsuspendMultiple
                {...{
                    viewModel,
                    originalViewModel,
                    searchAccountRoleId,
                    resources,
                    handleReset: this.handleReset,
                    toggleSuspendAll: this.toggleSuspendAll,
                    handleChange: this.handleChange,
                    redirectViewListDetail: this.redirectViewListDetail
                }}/>;
    }
}