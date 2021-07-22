import React from 'react';
import { handleError } from '../../../Shared/Scripts/api-client';
import { openAPI } from './open-api';
import { getUserLevelName } from './open-helper';
import ReactPagination from '../../../Shared/Scripts/react-paginations/react-pagination';
import { usernameRegExp } from '../../../SearchAccount/Scripts/app/helpers/accountinfo-helper';

export default class OpenViewList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            viewModel: null,
            resources: this.props.resources,
            activePage: 1,
            defaultPageLength: 50,
            pageLength: 50
        };

        this.fetchCustomerListFromServer = this.fetchCustomerListFromServer.bind(this);
    }

    componentWillMount() {
        const { activePage, pageLength } = this.state;
        this.fetchCustomerListFromServer({ page: activePage, pageLength: pageLength });
    }

    fetchCustomerListFromServer({ page, pageLength }) {
        $.blockUI();
        const { isAffected, downlineRoleId, searchAccountId, searchAccountName } = this.props;
        this.setState({
            activePage: page,
            pageLength: pageLength
        });

        const params = {
            SearchAccountId: searchAccountId,
            SearchAccountName: searchAccountName,
            PageSize: pageLength,
            PageIndex: page - 1,
            IsAffected: isAffected,
            DownlineRoleId: downlineRoleId
        };

        const that = this;
        openAPI.fetchCustomerList(params).then(
            responeData => {
                that.setState({
                    viewModel: responeData
                });

                $.unblockUI();
            }).catch(handleError);
    }

    render() {
        const { viewModel, activePage, defaultPageLength, pageLength, resources } = this.state;
        const { isAffected } = this.props;
        if (viewModel === null) {
            return null;
        }

        const totalRecords = viewModel.Data.TotalRows, showPaging = totalRecords > defaultPageLength;
        const viewListNote = isAffected ? resources.AffectedViewListNote : resources.UnchangedViewListNote;
        let counter = pageLength * (activePage - 1);

        const back = <div>
            <a className="btn-cancel" href='#' onClick={() => this.props.redirectMainPage(false)}>
                {resources.Back}
            </a>
        </div>;

        return (
            <div>
                {showPaging && back}
                <p className="note">
                    {usernameRegExp(viewListNote, viewModel.SearchAccountName)}
                </p>

                <table className="tblRpt tblRpt-bordered width-100per">
                    <thead>
                        <tr>
                            <th>{resources.NumberColumn}</th>
                            <th>{resources.Level}</th>
                            <th>{resources.Username}</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            viewModel.Data.Levels.map(level => {
                                const length = level.DownlineUsernames.length;
                                return (
                                    level.DownlineUsernames.map((username, index) => {
                                        counter++;
                                        return (
                                            <tr key={counter}>
                                                <td>
                                                    {counter}
                                                </td>
                                                {
                                                    index === 0 && <td rowSpan={length}>
                                                        {
                                                            getUserLevelName(level.RoleId, resources)
                                                        }
                                                    </td>
                                                }
                                                <td>
                                                    {username}
                                                </td>
                                            </tr>
                                        );
                                    })
                                );
                            })
                        }
                    </tbody>
                </table>
                {
                    showPaging && <ReactPagination
                        pageResource={resources.Page}
                        ofResource={resources.Of}
                        page={activePage}
                        pageLength={pageLength}
                        totalRecords={totalRecords}
                        onPageChange={this.fetchCustomerListFromServer}
                        prevPageRenderer={() => <span className="icon-chevron-left" />}
                        nextPageRenderer={() => <span className="icon-chevron-right" />}
                        firstPageRenderer={() => <span className="icon-skip-previous" />}
                        lastPageRenderer={() => <span className="icon-skip-next" />}
                        pageRecordsRenderer={({ totalItems = totalRecords, from, to }) =>
                            `${resources.Displaying} ${from} ${resources.To} ${to} ${resources.Of} ${totalItems}`}
                    />
                }
                {back}
            </div >
        );
    }
}