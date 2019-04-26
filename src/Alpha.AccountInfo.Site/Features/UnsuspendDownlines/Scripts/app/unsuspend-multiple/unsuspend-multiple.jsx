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
            unsuspendList.push({ CustId: item.CustId, Suspended: item.Suspended });
        });

        unsuspendAPI.updateSuspend(
            "UnsuspendDownlines/UnsuspendMultiple",
            { UnsuspendList: unsuspendList })
            .then((data) => {
                const isSuccess = data.length === 0;
                self.setState({
                    hideMessages: false,
                    message: isSuccess ? resources.UpdateSuccessfully : this.buildErrorMessage(data),
                    updateSuccess: isSuccess
                });

                if (isSuccess) {
                    setTimeout(() => {
                        window.top.realoadLeftPanel = true;
                        window.top.closedPopup = true;
                        window.top.ageWnd.close();
                    }, 1500);
                }

                $.unblockUI();
            });
    }

    buildErrorMessage(errors) {
        return (
            <div>
                {errors.map((eror, index) => <div key={index}>{eror.Message}</div>)}
            </div>
        );
    }

    render() {
        const { updateSuccess, message, hideMessages } = this.state;
        const { viewModel, originalViewModel, toggleSuspendAll, handleChange, redirectViewListDetail, handleReset, resources } = this.props
        const downlineRoleId = viewModel.length > 0 ? viewModel[0].RoleId : 1;
        const headerLevel = downlineRoleId == 2 ? `${resources.Master}/${resources.Agent}/${resources.Member}` : `${resources.Agent}/${resources.Member}`

        return (
            <div className="">
                <div className={hideMessages ? `hidden` : ``}>
                    <span className={updateSuccess ? 'col-xs-12 msg msg-success' : 'col-xs-12 msg msg-error'}>  {message} </span>
                </div>

                <table className="tblRpt tblRpt-bordered width-100per">
                    <thead>
                        <tr>
                            <th rowSpan='2'>{resources.Username}</th>
                            <th>
                                {resources.SuspendStatus}
                            </th>
                            {downlineRoleId > 1 && <th rowSpan='2'>{resources.AffectedDownlines} <br /> ({headerLevel})</th>}
                            {downlineRoleId > 1 && <th rowSpan='2'>{resources.RemainUnchangedDownlines} <br /> ({headerLevel})</th>}
                        </tr>
                        <tr>
                            <th>
                                <button className="suspend-all-button" onClick={() => toggleSuspendAll(true)}>{resources.Suspend}</button>
                                <button className="btn-default" onClick={() => toggleSuspendAll(false)}>{resources.Unsuspend}</button>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            viewModel.map((item, index) =>
                                <RowContent
                                    originalViewModel={originalViewModel}
                                    item={item}
                                    downlineRoleId={downlineRoleId}
                                    key={index}
                                    index={index}
                                    handleChange={handleChange}
                                    redirectViewListDetail={redirectViewListDetail}
                                    resources={resources}
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