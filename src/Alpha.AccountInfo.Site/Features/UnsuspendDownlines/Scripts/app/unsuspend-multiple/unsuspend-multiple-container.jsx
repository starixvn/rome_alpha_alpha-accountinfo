import * as React from 'react';
import ReactDOM from 'react-dom';
import * as _ from 'lodash';
import UnsuspendViewList from '../unspend-view-list';
import UnsuspendMultiple from './unsuspend-multiple';
import { decodeResources } from '../../../../SearchAccount/Scripts/app/helpers/accountinfo-helper';

window.renderUnsuspendMultipe = function (viewModel, resources) {
    ReactDOM.render(
        <UnsuspendMultipleContainer {...{ viewModel, resources: decodeResources(resources)}}/>,
        document.getElementById('unsuspend-multiple')
    );
};

export class UnsuspendMultipleContainer extends React.Component {
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
            <UnsuspendMultiple
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