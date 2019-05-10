import * as React from 'react';
import * as _ from 'lodash';
import { unsuspendAPI } from '../unsuspend-api';
import { RowContent } from './unsuspend-multiple-components';
import { Markup } from 'interweave';

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
        let params = [];

        viewModel.map(function (item) {
            params.push({
                CustId: item.CustId,
                Username: item.Username,
                Suspended: item.Suspended
            });
        });

        unsuspendAPI.updateSuspend("UnsuspendDownlines/UnsuspendMultiple", { UnsuspendList: params })
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
                    window.top.main.DelayReloadPage();
                    setTimeout(() => {
                        window.top.ageWnd.close();
                    }, 3000);
                }

                $.unblockUI();
            });
    }

    render() {
        const { viewModel, originalViewModel, toggleSuspendAll, handleChange, redirectViewListDetail, handleReset, resources } = this.props,
            searchAccountRoleId = viewModel.length > 0 ? viewModel[0].SearchAccountRoleId : 1,
            isNotMember = searchAccountRoleId > 1,
            isMaster = searchAccountRoleId == 3;

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
                                isNotMember && [
                                    isMaster ? resources.AffectedDownlinesAgentMember : resources.AffectedDownlinesMember,
                                    isMaster ? resources.RemainUnchangedDownlinesAgentMember : resources.RemainUnchangedDownlinesMember]
                                    .map((text, index) =>
                                    <th rowSpan='2' key={index}><Markup content={text} /> </th>)
                            }
                        </tr>
                        <tr>
                            <th>
                                {
                                    [{
                                        text: resources.Suspended,
                                        class: `btn-suspend-all`,
                                        suspended: true
                                    },
                                    {
                                        text: resources.Unsuspended,
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
                            disabled: false,
                            onclick: handleReset
                        },
                        {
                            text: resources.Submit,
                            class: `btn-submit`,
                            disabled: _.isEqual(viewModel, originalViewModel),
                            onclick: this.handleSubmit
                        }].map((button, index) =>
                            <button className={button.class} key={index} disabled={button.disabled} onClick={button.onclick}>
                                {button.text}
                            </button>
                        )
                    }

                </div>
            </div >
        );
    }
}