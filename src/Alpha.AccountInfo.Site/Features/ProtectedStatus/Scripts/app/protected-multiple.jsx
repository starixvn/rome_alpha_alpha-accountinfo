import React from 'react';
import { isEqual } from 'lodash';
import { protectedAPI } from './protected-api';
import { RowContent } from './protected-multiple-components';
import { Array } from 'core-js';

export default class ProtectedMultiple extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            hideMessages: true,
            message: ''
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleEdit = this.handleEdit.bind(this);
    }

    handleSubmit() {
        const { resources } = this.props;
        window.top.bootboxConfirm(resources.EditMultipleConfirmMessage, null, this.handleEdit);
    }

    handleEdit() {
        $.blockUI();
        const self = this;
        const { viewModel, originalViewModel } = this.props;
        const formData = [];

        viewModel.map(function (item, index) {
            if (item.IsProtectedMode !== originalViewModel[index].IsProtectedMode) {
                formData.push(item);
            }
        });

        protectedAPI.editMultipleProtectedStatus({ ProtectedStatus: formData })
            .then(data => {
                let isSuccess = true;
                const dataResults = Array.isArray(data) ? data : [data];

                const message = <div className='msg-container'>
                    {
                        dataResults.map((result, index) => {
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
                    } else {
                        window.top.main.DelayReloadPage();
                        setTimeout(() => {
                            window.top.ageWnd.close();
                        }, 3000);
                    }
                }

                $.unblockUI();
            });
    }

    render() {
        const { viewModel, originalViewModel, resources, toggleProtectedAll, handleChange, handleReset } = this.props;

        return (
            <div className="col-xs-12">
                {this.state.message}
                <table className="tblRpt tblRpt-bordered width-100per">
                    <thead>
                        <tr>
                            <th rowSpan="2">{resources.Username}</th>
                            <th>
                                {resources.ProtectionStatus}
                            </th>
                        </tr>
                        <tr>
                            <th>
                                <button className="btn-protected-all" onClick={() => toggleProtectedAll(true)}>
                                    {resources.Protected}
                                </button>
                                <button className="btn-unprotected-all" onClick={() => toggleProtectedAll(false)}>
                                    {resources.Unprotected}
                                </button>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            viewModel.map((dataRow, index) =>
                                <RowContent {...{
                                    dataRow,
                                    index,
                                    handleChange,
                                    resources,
                                    key: index
                                }} />)
                        }
                    </tbody>
                </table>
                <div className="row-info popup-bottom list-button text-right">
                    <button className="btn-reset" onClick={handleReset}>
                        {resources.Reset}
                    </button>
                    <button className="btn-submit" disabled={isEqual(viewModel, originalViewModel)} onClick={this.handleSubmit}>
                        {resources.Submit}
                    </button>
                </div>
            </div>
        );
    }
}