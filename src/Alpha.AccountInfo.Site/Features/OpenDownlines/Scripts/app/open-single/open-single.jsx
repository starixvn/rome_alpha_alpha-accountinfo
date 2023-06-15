import React from 'react';
import { handleError } from '../../../../Shared/Scripts/api-client';
import { openAPI } from '../open-api';
import { BodyRowContent, FooterContent } from './open-single-components';
import { usernameRegExp } from '../../../../SearchAccount/Scripts/app/helpers/accountinfo-helper';
import { viewInfomation } from '../open-helper';

export default class OpenSingle extends React.Component {
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
        const { resources, viewModel } = this.props;

        openAPI.updateClose('OpenDownlines/OpenSingle', { SearchAccountId: viewModel.SearchAccountId })
            .then(data => {
                const message = <div className='msg-container'>
                    <div className={`msg ${data.IsSuccess ? 'msg-success' : 'msg msg-error'}`}>
                        {data.IsSuccess ? resources.UpdateSuccessfully : data.Message.trim()}
                    </div>
                </div>;

                this.setState({
                    hideMessages: false,
                    message: message
                });

                if (data.IsSuccess) {
                    if (top.needReloadMainFrameAfterUpdating) {
                        top.successfulUpdatingCallbackAndReloadMainFrame(true);
                    }

                    if (window.top.reloadCustomerList
                        && window.top.main.DelayReloadPage
                        && typeof (window.top.main.DelayReloadPage) == 'function') {
                        window.top.main.DelayReloadPage();
                    }

                    window.top.confirmPopupPopup && window.top.confirmPopupPopup.onClosing && window.top.confirmPopupPopup.onClosing(viewModel.SearchAccountName, true);
                }

                $.unblockUI();
            }).catch(handleError);
    }

    handleSubmit(e) {
        const { resources, viewModel } = this.props;
        const message = viewModel.AffectedDownlinesTotal + viewModel.UnchangedDownlinesTotal === 0 ?
            resources.OpenedWithNoDownlinesMessages :
            resources.SingleConfirm;

        window.top.bootboxConfirm(
            message,
            undefined,
            this.doUpdate
        );
    }

    render() {
        const { viewModel, resources, redirectViewListDetail } = this.props;
        const affectedDownlinesTotal = viewModel.AffectedDownlinesTotal,
            unchangedDownlinesTotal = viewModel.UnchangedDownlinesTotal,
            levels = viewModel.Levels;

        return (<div className="col-xs-12">
            {this.state.message}
            <p className="note">
                {usernameRegExp(resources.OpenSingleNote, viewModel.SearchAccountName)}
                <span onClick={() => viewInfomation(resources)} className='icon-information-outline' />
            </p>

            <div>
                <table className="tblRpt tblRpt-bordered width-100per">
                    <thead>
                        <tr>
                            {
                                [resources.Level, resources.AffectedDownlines, resources.RemainUnchangedDownlines]
                                    .map((text, index) => <th key={index}>{text} </th>)
                            }
                        </tr>
                    </thead>
                    <tbody>
                        {
                            affectedDownlinesTotal + unchangedDownlinesTotal > 0 &&
                            levels.map((level, index) => <BodyRowContent {...{ level, redirectViewListDetail, resources, key: index }} />)
                        }
                    </tbody>
                    <FooterContent {...{
                        affectedDownlinesTotal,
                        unchangedDownlinesTotal,
                        redirectViewListDetail,
                        totalText: resources.Total,
                        noData: resources.NoData
                    }} />
                </table>
            </ div>
            <div className="row-info popup-bottom text-right">
                <button className="btn-submit" onClick={this.handleSubmit}>{resources.Open}</button>
            </div>
        </div >
        );
    }
}