import * as React from 'react';
import ReactDOM from 'react-dom';
import UnsuspendViewList from '../unspend-view-list';
import UnsuspendSingle from './unsuspend-single';
import { decodeResources } from '../../../../SearchAccount/Scripts/app/helpers/accountinfo-helper';

window.renderUnsuspendSingle = function (viewModel, resources) {
    ReactDOM.render(
        <UnsuspendSingleContainer {...{ viewModel, resources: decodeResources(resources)}}/>,
        document.getElementById('container')
    );
};

export class UnsuspendSingleContainer extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showViewList: false,
            isAffected: false,
            downlineRoleId: 1
        };

        this.redirectViewListDetail = this.redirectViewListDetail.bind(this);
        this.redirectMainPage = this.redirectMainPage.bind(this);
    }

    redirectViewListDetail(isAffected, downlineRoleId) {
        this.setState({
            showViewList: true,
            isAffected: isAffected,
            downlineRoleId: downlineRoleId
        });
    }

    redirectMainPage() {
        this.setState({ showViewList: false });
    }

    render() {
        const { isAffected, downlineRoleId } = this.state;
        const { viewModel, resources } = this.props;

        return this.state.showViewList ?
            <UnsuspendViewList {...{
                isAffected,
                downlineRoleId,
                resources,
                searchAccountId: viewModel.SearchAccountId,
                searchAccountName: viewModel.SearchAccountName,
                redirectMainPage: this.redirectMainPage
            }} /> :
            <UnsuspendSingle {...{
                viewModel,
                resources,
                redirectViewListDetail: this.redirectViewListDetail
            }} />;
    }
}
