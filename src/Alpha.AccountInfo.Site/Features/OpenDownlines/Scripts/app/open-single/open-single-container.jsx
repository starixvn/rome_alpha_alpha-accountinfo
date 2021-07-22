import React from 'react';
import ReactDOM from 'react-dom';
import OpenViewList from '../open-view-list';
import OpenSingle from './open-single';
import { decodeResources } from '../../../../SearchAccount/Scripts/app/helpers/accountinfo-helper';

class OpenSingleContainer extends React.Component {
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
            <OpenViewList {...{
                isAffected,
                downlineRoleId,
                resources,
                searchAccountId: viewModel.SearchAccountId,
                searchAccountName: viewModel.SearchAccountName,
                redirectMainPage: this.redirectMainPage
            }} /> :
            <OpenSingle {...{
                viewModel,
                resources,
                redirectViewListDetail: this.redirectViewListDetail
            }} />;
    }
}

export default OpenSingleContainer;

export const renderOpenSingle = (viewModel, resources) => {
    ReactDOM.render(
        <OpenSingleContainer {...{ viewModel, resources: decodeResources(resources) }} />,
        document.getElementById('container')
    );
};

window.renderOpenSingle = renderOpenSingle;