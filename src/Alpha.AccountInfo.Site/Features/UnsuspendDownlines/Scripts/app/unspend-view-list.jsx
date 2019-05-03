import * as React from 'react';
import { unsuspendAPI } from './unsuspend-api';
import { getUserLevelName } from './unsuspend-helper';
import ReactPagination from '../../../Shared/Scripts/react-paginations/react-pagination';

export default class UnsuspendViewList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            viewModel: null,
            resources: this.props.resources,
            activePage: 1,
            pageLength: 10,
            totalRecords: 100 * 100
        };

        this.fetchCustomerListFromServer = this.fetchCustomerListFromServer.bind(this);
    }

    componentWillMount() {
        this.fetchCustomerListFromServer({ page: 1, pageLength: 10 });
    }

    fetchCustomerListFromServer({ page, pageLength }) {
        $.blockUI();
        const { isAffected, downlineRoleId, searchAccountId } = this.props;
        this.setState({
            activePage: page,
            pageLength: pageLength
        });

        //var params = {
        //    SearchAccountId: searchAccountId,
        //    PageSize: 20,
        //    PageIndex: pageNumber - 1,
        //    IsAffected: isAffected,
        //    DownlineRoleId: downlineRoleId
        //};

        var params = {
            SearchAccountId: 223322,
            PageSize: pageLength,
            PageIndex: page - 1,
            IsAffected: true,
            DownlineRoleId: 2
        };

        const that = this;
        unsuspendAPI.fetchCustomerList(params).then(
            (responeData) => {
                that.setState({
                    viewModel: responeData,
                });

                $.unblockUI();
            });

    }

    render() {
        const { viewModel, activePage, resources } = this.state;
        const back = <div>
            <a className="btn-cancel" href='#' onClick={() => this.props.redirectMainPage(false)}>
                {resources.Back}
            </a>
        </div>;

        return viewModel === null ? null : (
            < div className="" >
                {back}
                <p className="note">
                    {resources.ViewListNote}
                </p>

                <table className="tblRpt tblRpt-bordered width-100per">
                    <thead>
                        <tr>
                            <th>{resources.Level}</th>
                            <th>{resources.Username}</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            viewModel.UnsuspendDownlinesViewList.UnsuspendDownlinesByLevel.map((level, index) => {
                                const length = level.UnsuspendDownlines.length;
                                return (
                                    level.UnsuspendDownlines.map((username, index1, arr) => {
                                        if (index1 === 0) {
                                            return (
                                                <tr key={index1}>
                                                    <td rowSpan={length}>
                                                        {
                                                            getUserLevelName(level, resources)
                                                        }
                                                    </td>
                                                    <td>
                                                        {username.Username}
                                                    </td>
                                                </tr>
                                            );
                                        }
                                        else {
                                            return (
                                                <tr key={index1}>
                                                    <td>
                                                        {username.Username}
                                                    </td>
                                                </tr>
                                            );
                                        }
                                    })
                                );

                            })
                        }
                    </tbody>
                </table>
                <ReactPagination
                    type="full"
                    page={this.state.activePage}
                    pageLength={this.state.pageLength}
                    totalRecords={this.state.totalRecords}
                    onPageChange={this.fetchCustomerListFromServer}
                    prevPageRenderer={() => <span className="icon-chevron-left" />}
                    nextPageRenderer={() => <span className="icon-chevron-right" />}
                    firstPageRenderer={() => <span className="icon-skip-previous" />}
                    lastPageRenderer={() => <span className="icon-skip-next" />}
                />
                {back}
            </div >
        );
    }
}