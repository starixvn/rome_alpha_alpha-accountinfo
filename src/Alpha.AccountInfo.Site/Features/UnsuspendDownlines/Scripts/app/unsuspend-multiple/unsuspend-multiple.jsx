import * as React from 'react';
import * as _ from 'lodash';
import { unsuspendAPI } from '../unsuspend-api';
import { RowContent } from './unsuspend-multiple-components';

export default class UnsuspendMultiple extends React.Component {
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
            unsuspendList.push({
                CustId: item.CustId,
                Username: item.Username,
                Suspended: item.Suspended
            });
        });

        unsuspendAPI.updateSuspend("UnsuspendDownlines/UnsuspendMultiple", { UnsuspendList: unsuspendList })
            .then((data) => {
                let isSuccess = true;
                let message = <div className='col-xs-12 msg-container'>
                    {
                        data.map((result, index) => {
                            if (!result.IsSuccess && isSuccess) {
                                isSuccess = false;
                            }

                            return <div key={index} className={result.IsSuccess ? 'msg msg-success' : 'msg msg-error'}>
                                {result.Message}
                            </div>;
                        })}
                </div>;

                self.setState({
                    hideMessages: false,
                    message: message,
                    updateSuccess: isSuccess
                });

                if (isSuccess) {
                    setTimeout(() => {
                        window.top.realoadLeftPanel = true;
                        window.top.closedPopup = true;
                        window.top.ageWnd.close();
                    }, 3000);
                }

                $.unblockUI();
            });
    }

    buildErrorMessage(errors) {
        return (
            <div>
                {
                    errors.map((eror, index) => {
                    if (!eror.IsSuccess && this.state.updateSuccess) {
                        this.setState({
                            updateSuccess: false
                        });
                    }

                    return <div key={index}>{eror.Message}</div>
                })}
            </div>
        );
    }

    render() {
        const { viewModel, originalViewModel, toggleSuspendAll, handleChange, redirectViewListDetail, handleReset, resources } = this.props
        const searchAccountRoleId = viewModel.length > 0 ? viewModel[0].SearchAccountRoleId : 1;
        const headerLevel = searchAccountRoleId == 2 ?
            `${resources.Master}/${resources.Agent}/${resources.Member}` :
            `${resources.Agent}/${resources.Member}`

        return (
            <div className="">
                {this.state.message}
                <table className="tblRpt tblRpt-bordered width-100per">
                    <thead>
                        <tr>
                            <th rowSpan='2'>{resources.Username}</th>
                            <th>
                                {resources.SuspendStatus}
                            </th>
                            {searchAccountRoleId > 1 && <th rowSpan='2'>{resources.AffectedDownlines} <br /> ({headerLevel})</th>}
                            {searchAccountRoleId > 1 && <th rowSpan='2'>{resources.RemainUnchangedDownlines} <br /> ({headerLevel})</th>}
                        </tr>
                        <tr>
                            <th>
                                <button className="btn-suspend-all" onClick={() => toggleSuspendAll(true)}>{resources.Suspend}</button>
                                <button className="btn-unsuspend-all" onClick={() => toggleSuspendAll(false)}>{resources.Unsuspend}</button>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            viewModel.map((dataRow, index) =>
                                <RowContent {...{
                                    originaldataRow: originalViewModel[index],
                                    dataRow,
                                    searchAccountRoleId,
                                    index,
                                    handleChange,
                                    redirectViewListDetail,
                                    resources,
                                    key: index
                                    }}
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