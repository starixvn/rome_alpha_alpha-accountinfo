import React from 'react';
import { isEqual } from 'lodash';
import { openAPI } from '../open-api';
import { RowContent } from './open-multiple-components';
import { Markup } from 'interweave';
import { viewInfomation } from '../open-helper';

export default class OpenMultiple extends React.Component {
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
        const { viewModel, originalViewModel, searchAccountRoleId } = this.props;
        const params = [];

        viewModel.map(function (item, index) {
            if (item.IsClosed !== originalViewModel[index].IsClosed) {
                params.push({
                    SearchAccountId: item.SearchAccountId,
                    Username: item.Username,
                    IsClosed: item.IsClosed
                });
            }
        });

        openAPI.updateClose('OpenDownlines/OpenMultiple', { OpenList: params, RoleId: searchAccountRoleId })
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
                    if (top.customerListNew) {
                        top.successfulUpdatingCallbackAndReloadMainFrame(true);
                    }
                    else {
                        window.top.main.DelayReloadPage();
                        setTimeout(() => {
                            window.top.ageWnd.close();
                        }, timeoutDefault);
                    }
                }

                $.unblockUI();
            });
    }

    handleSubmit() {
        const { resources, searchAccountRoleId, viewModel, originalViewModel } = this.props;

        let message = resources.MultipleConfirm;
        if (searchAccountRoleId === 1) {
            let openUserNames = [],
                closeUserNames = [];
            viewModel.map(function (item, index) {
                if (item.IsClosed !== originalViewModel[index].IsClosed) {
                    if (item.IsClosed) {
                        closeUserNames.push(item.Username);
                    } else {
                        openUserNames.push(item.Username);
                    }
                }
            });
            
            message = openUserNames.length > 0
                ? `${resources.OpenMultipleMemberConfirmMessage.replace('<username>', openUserNames.join(', '))} <br />`
                : '';
            message += closeUserNames.length > 0
                ? `${resources.CloseMultipleMemberConfirmMessage.replace('<username>', closeUserNames.join(', '))} <br />`
                : '';
            message += resources.SubmitMultipleConfirm3;
        }

        window.top.bootboxConfirm(
            message,
            null,
            this.doUpdate
        );
    }

    render() {
        const { viewModel, originalViewModel, searchAccountRoleId, toggleCloseAll, handleChange, redirectViewListDetail, handleReset, resources } = this.props,
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
                                {resources.CloseStatus}
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
                                        text: resources.Closed,
                                        class: `btn-suspend-all`,
                                        isClosed: true
                                    },
                                    {
                                        text: resources.Opened,
                                        class: `btn-unsuspend-all`,
                                        isClosed: false
                                    }].map((button, index) =>
                                        <button className={button.class} key={index}
                                            onClick={() => toggleCloseAll(button.isClosed)}>
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