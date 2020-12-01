import React from 'react';
import { isEqual } from 'lodash';
import { unsuspendAPI } from '../unsuspend-api';
import { RowContent } from './unsuspend-multiple-components';
import { Markup } from 'interweave';
import { viewInfomation } from '../unsuspend-helper';

export default class UnsuspendMultiple extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            hideMessages: true,
            message: ''
        };

        this.doUpdate = this.doUpdate.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    doUpdate() {
        $.blockUI();
        const self = this;
        const { viewModel, originalViewModel } = this.props;
        const params = [];

        viewModel.map(function (item, index) {
            if (item.Suspended !== originalViewModel[index].Suspended) {
                params.push({
                    SearchAccountId: item.SearchAccountId,
                    Username: item.Username,
                    Suspended: item.Suspended
                });
            }
        });

        unsuspendAPI.updateSuspend('UnsuspendDownlines/UnsuspendMultiple', { UnsuspendList: params })
            .then(data => {
                let isSuccess = true;
                const timeoutDefault = 3000;
                const message = <div className='msg-container'>
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
                    message: message
                });

                if (isSuccess) {
                    setTimeout(() => {
                        const mainFrame = window.top.$('#main');
                        var currSrc = mainFrame.attr("src");
                        mainFrame.attr("src", currSrc);
                    }, timeoutDefault);
                }

                $.unblockUI();
            });
    }

    handleSubmit() {
        const { resources } = this.props;
        window.top.bootboxConfirm(
            resources.MultipleConfirm,
            null,
            this.doUpdate
        );
    }

    render() {
        const { viewModel, originalViewModel, searchAccountRoleId, toggleSuspendAll, handleChange, redirectViewListDetail, handleReset, resources } = this.props,
            isNotMember = searchAccountRoleId > 1,
            headers = searchAccountRoleId === 3 ?
                [resources.AffectedDownlinesAgentMember, resources.RemainUnchangedDownlinesAgentMember] :
                [resources.AffectedDownlinesMember, resources.RemainUnchangedDownlinesMember];

        return (
            <div className="col-xs-12">
                {this.state.message}
                <p className="note">
                    <span onClick={() => viewInfomation(resources)} className='icon-information-outline' />
                </p>
                <table className="tblRpt tblRpt-bordered width-100per">
                    <thead>
                        <tr>
                            <th rowSpan='2'>{resources.Username}</th>
                            <th>
                                {resources.SuspendStatus}
                            </th>
                            {
                                isNotMember && headers.map((text, index) =>
                                    <th rowSpan='2' key={index}><Markup content={text} /></th>)
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
                <div className="row-info popup-bottom list-button text-right">
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
                            disabled: isEqual(viewModel, originalViewModel),
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