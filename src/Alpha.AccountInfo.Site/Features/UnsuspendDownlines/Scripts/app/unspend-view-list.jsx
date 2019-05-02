import * as React from 'react';
import Pagination from "react-js-pagination";
import { unsuspendAPI } from './unsuspend-api';
import { getUserLevelName } from './unsuspend-helper';

export default class UnsuspendViewList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            viewModel: null,
            activePage: null,
            resources: this.props.resources
        };

        this.fetchCustomerListFromServer = this.fetchCustomerListFromServer.bind(this);
    }

    componentWillMount() {
        this.fetchCustomerListFromServer(1);
    }

    fetchCustomerListFromServer(pageNumber) {
        $.blockUI();
        const { isAffected, downlineRoleId, searchAccountId } = this.props;
        this.setState({ activePage: pageNumber });

        //var params = {
        //    SearchAccountId: searchAccountId,
        //    PageSize: 20,
        //    PageIndex: pageNumber - 1,
        //    IsAffected: isAffected,
        //    DownlineRoleId: downlineRoleId
        //};

        var params = {
            SearchAccountId: 223322,
            PageSize: 20,
            PageIndex: pageNumber - 1,
            IsAffected: true,
            DownlineRoleId: 2
        };

        unsuspendAPI.fetchCustomerList(params).then(
            (responeData) => {
                this.setState({
                    viewModel: responeData
                });

                $.unblockUI();
            });

    }

    render() {
        const { viewModel, activePage, resources } = this.state;
       

        return viewModel === null ? null : (
            < div className="" >
                <div>
                    <a className="btn-cancel" href='#' onClick={() => this.props.redirectMainPage(false)}>
                        Back
                    </a>
                </div>
                <p className="note">
                    List of downlines whose status are changed/unchanged after going to unsuspend 'selected customer'
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
                <Pagination
                    activePage={activePage}
                    itemsCountPerPage={20}
                    totalItemsCount={400}
                    pageRangeDisplayed={5}
                    onChange={this.fetchCustomerListFromServer}
                />
               
            </div >
        );
    }
}