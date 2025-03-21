import React from 'react';
import ReactDOM from 'react-dom';
import { cloneDeep, forEach } from 'lodash';
import OpenViewList from '../open-view-list';
import OpenMultiple from './open-multiple';
import { decodeResources } from '../../../../SearchAccount/Scripts/app/helpers/accountinfo-helper';

class OpenMultipleContainer extends React.Component {
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
            originalViewModel: cloneDeep(viewModel),
            viewModel: viewModel
        };

        this.handleReset = this.handleReset.bind(this);
        this.toggleCloseAll = this.toggleCloseAll.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.redirectViewListDetail = this.redirectViewListDetail.bind(this);
        this.redirectMainPage = this.redirectMainPage.bind(this);
    }

    correctData() {
        const { viewModel, searchAccountRoleId } = this.props;

        forEach(viewModel, dataRow => {
            let currentLevel = dataRow.Downlines.length;
            while (currentLevel < searchAccountRoleId - 1) {
                if (currentLevel === 0 || dataRow.Downlines[0].DownlineRoleId !== 1) {
                    dataRow.Downlines.push({
                        CustId: 0,
                        DownlineRoleId: 0,
                        AffectedDownlines: 0,
                        UnchangedDownlines: 0
                    });
                } else {
                    dataRow.Downlines.unshift({
                        CustId: 0,
                        DownlineRoleId: 0,
                        AffectedDownlines: 0,
                        UnchangedDownlines: 0
                    });
                }

                currentLevel++;
            }
        });
    }

    handleReset() {
        this.setState(prevState => ({
            viewModel: prevState.originalViewModel
        }));
    }

    toggleCloseAll(closeAll) {
        const viewModelCopy = cloneDeep(this.state.viewModel);

        viewModelCopy.forEach(item => { item.IsClosed = closeAll; });

        this.setState({ viewModel: viewModelCopy });
    }

    handleChange(index, value) {
        const { viewModel } = this.state;
        const viewModelCopy = cloneDeep(viewModel);

        viewModelCopy[index].IsClosed = value;

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
        const { searchAccountId, searchAccountRoleId, searchAccountName, isAffected, downlineRoleId, originalViewModel, viewModel, resources } = this.state;

        return this.state.showViewList ?
            <OpenViewList {...{
                isAffected,
                downlineRoleId,
                searchAccountId,
                searchAccountName,
                resources,
                redirectMainPage: this.redirectMainPage
            }} /> :
            <OpenMultiple
                {...{
                    viewModel,
                    originalViewModel,
                    searchAccountRoleId,
                    resources,
                    handleReset: this.handleReset,
                    toggleCloseAll: this.toggleCloseAll,
                    handleChange: this.handleChange,
                    redirectViewListDetail: this.redirectViewListDetail
                }} />;
    }
}

export default OpenMultipleContainer;

export const renderOpenMultiple = (viewModel, resources) => {
    ReactDOM.render(
        <OpenMultipleContainer {...{
            viewModel: viewModel.Data,
            searchAccountRoleId: viewModel.SearchAccountRoleId,
            resources: decodeResources(resources)
        }} />,
        document.getElementById('container')
    );
};

window.renderOpenMultiple = renderOpenMultiple;