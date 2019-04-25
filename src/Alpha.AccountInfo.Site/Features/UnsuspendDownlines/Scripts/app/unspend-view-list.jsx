import * as React from 'react';
import ReactDOM from 'react-dom';
import Pagination from "react-js-pagination";
import { unsuspendAPI } from './unsuspend-api';

window.renderUnsuspendViewList = function (viewModel) {
	ReactDOM.render(
        <UnsuspendViewList viewListModel={viewModel}/>,
		document.getElementById('unsuspend-list')
	);
};

export default class UnsuspendViewList extends React.Component {
	constructor(props) {
        super(props);
        this.state = {
            viewModel: null,
            activePage: 1
        };

        this.handlePageChange = this.handlePageChange.bind(this);
        this.fetchCustomerListFromServer = this.fetchCustomerListFromServer.bind(this);

        
	}

    componentWillMount() {
        this.fetchCustomerListFromServer(1);
    }

    handlePageChange(pageNumber) {
        this.setState({ activePage: pageNumber });
    }

	buildNote() {
		return (
			<div className="note">
				List of downlines whose status are changed/unchanged after going to unsuspend 'selected customer'
            </div>
		);
	}

    fetchCustomerListFromServer(pageNumber) {

        $.blockUI();
        this.setState({ activePage: pageNumber });

            var params = {
                SearchAccountId: 223322,
                PageSize: 20,
                PageIndex: pageNumber - 1,
                CustIds: '223322,223323',
                IsAffected: true,
                SearchAccountRoleId: 3,
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
        const { viewModel } = this.state;

        return this.state.viewModel === null ? null : (
			< div className="" >
                <div>
                    <a href='#' onClick={() => this.props.hrefBack()}>
                      Back
                    </a>
                </div>
				{this.buildNote()}

				<table className="tblRpt tblRpt-bordered width-100per">
					<thead>
						<tr>
							<th>Level</th>
							<th>Username</th>
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
													<td className="text-center" rowSpan={length}>
                                                        {level.RoleId == 3 ? `Master` : level.RoleId == 2 ? `Agent` : `Member`}
													</td>
													<td className="text-center">
                                                        {username.Username}
													</td>
												</tr>
											);
										}
										else {
											return (
												<tr key={index1}>
													<td className="text-center">
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
                <div className="text-center">
                    <Pagination
                        activePage={this.state.activePage}
                        itemsCountPerPage={20}
                        totalItemsCount={100}
                        pageRangeDisplayed={5}
                        onChange={this.fetchCustomerListFromServer}
                  />
      </div>
			</div >
		);
	}
}

export const UnsuspendRow = ({ level}) => {
	return (
		<tr>
			<td className="text-center" rowSpan={level.UnsuspendDownlines.length}>
				{level.RoleId}
			</td>
			<td className="text-center">
				<a href='#'>
					{level.AffectedDownlines}
				</a>
			</td>
		</tr>
	);
}