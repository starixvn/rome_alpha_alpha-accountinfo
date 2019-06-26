import React from 'react';
import { unsuspendAPI } from '../unsuspend-api';
import { BodyRowContent, FooterContent } from './unsuspend-single-components';
import { usernameRegExp } from '../../../../SearchAccount/Scripts/app/helpers/accountinfo-helper';
import { viewInfomation } from '../unsuspend-helper';

export default class UnsuspendSingle extends React.Component {
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
        const self = this;

        unsuspendAPI.updateSuspend('UnsuspendDownlines/UnsuspendSingle', { SearchAccountId: viewModel.SearchAccountId })
            .then(data => {
                const message = <div className='msg-container'>
                    <div className={`msg ${data.IsSuccess ? 'msg-success' : 'msg msg-error'}`}>
                        {data.IsSuccess ? resources.UpdateSuccessfully : data.Message.trim()}
                    </div>
                </div>;

                self.setState({
                    hideMessages: false,
                    message: message
                });

                if (data.IsSuccess) {
                    if (window.top.reloadCustomerList
                        && window.top.main.DelayReloadPage
                        && typeof (window.top.main.DelayReloadPage) == 'function') {
                        window.top.main.DelayReloadPage();
                    }

                    setTimeout(() => {
                        window.top.realoadLeftPanel = true;
                        window.top.closedPopup = true;
                        window.top.ageWnd.close();
                    }, 1500);
                }

                $.unblockUI();
            });
    }

    handleSubmit(e) {
        const { resources, viewModel } = this.props;
        const message = viewModel.AffectedDownlinesTotal + viewModel.UnchangedDownlinesTotal === 0 ?
            resources.UnsuspendedWithNoDownlinesMessages :
            resources.MultipleConfirm;

        window.top.bootboxConfirm(
            message,
            e.target,
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
                {usernameRegExp(resources.UnsuspendSingleNote, viewModel.SearchAccountName)}
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
                <button className="btn-submit" onClick={this.handleSubmit}>{resources.Unsuspended}</button>
            </div>
        </div >
        );
    }
}
