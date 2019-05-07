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
                let message = <div className='msg-container'>
                    {
                        data.Results.map((result, index) => {
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
        const { viewModel, originalViewModel, toggleSuspendAll, handleChange, redirectViewListDetail, handleReset, resources } = this.props,
            searchAccountRoleId = viewModel.length > 0 ? viewModel[0].SearchAccountRoleId : 1,
            isNotMember = searchAccountRoleId > 1,
            headerLevel = searchAccountRoleId == 2 ?
                `${resources.Master}/${resources.Agent}/${resources.Member}` :
                `${resources.Agent}/${resources.Member}`;

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
                            {
                                isNotMember && [resources.AffectedDownlines, resources.RemainUnchangedDownlines].map((text, index) =>
                                    <th rowSpan='2' key={index}>{text} <br /> ({headerLevel})</th>)
                            }
                        </tr>
                        <tr>
                            <th>
                                {
                                    [{
                                        text: resources.Suspend,
                                        class: `btn-suspend-all`,
                                        suspended: true
                                    },
                                    {
                                        text: resources.Unsuspend,
                                        class: `btn-unsuspend-all`,
                                        suspended: false
                                    }].map((button, index) =>
                                        <button className={button.class} key={index}
                                            onClick={() => toggleSuspendAll(button.suspended)}>
                                            {button.text}
                                        </button>)
                                }
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
                                }} />)
                        }
                    </tbody>
                </table>
                <div className="col-xs-12 row-info popup-bottom list-button text-right">
                    {
                        [{
                            text: resources.Reset,
                            class: `btn-reset`,
                            onclick: handleReset
                        },
                        {
                            text: resources.Submit,
                            class: `btn-submit`,
                            onclick: this.handleSubmit
                        }].map((button, index) =>
                            <button className={button.class} key={index} onClick={button.onclick}>
                                {button.text}
                            </button>
                        )
                    }

                </div>
            </div >
        );
    }
}