import * as React from 'react';
import { unsuspendAPI } from '../unsuspend-api';
import { BodyRowContent, FooterContent } from './unsuspend-single-components';
import { formatMessage, usernameRegExp } from '../../../../SearchAccount/Scripts/app/helpers/accountinfo-helper';

export default class UnsuspendSingle extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            hideMessages: true,
            updateSuccess: false,
            message: ''
        };

        this.doUpdate = this.doUpdate.bind(this);
        this.viewInfomation = this.viewInfomation.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleKeyPress = this.handleKeyPress.bind(this);
    }

    handleKeyPress(e) {
        if (e.key === 'Enter') {
            this.handleSubmit(e);
        }
    }

    doUpdate(e) {
        $.blockUI();
        const { resources, viewModel } = this.props;
        let self = this;

        unsuspendAPI.updateSuspend("UnsuspendDownlines/UnsuspendSingle", { SearchAccountId: viewModel.SearchAccountId })
            .then((data) => {
                self.setState({
                    hideMessages: false,
                    message: data.IsSuccess ? resources.UpdateSuccessfully : data.Message.trim(),
                    updateSuccess: data.IsSuccess
                });

                if (data.IsSuccess) {
                    window.top.reloadCustomerList && window.top.main.DelayReloadPage();
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
        window.top.bootboxConfirm(
            formatMessage(this.props.resources.ConfirmSuspendedViewModelInCaseUnsuspendedMasterOrAgentMessage),
            e.target,
            this.doUpdate
        );
    }

    viewInfomation(e) {
        const { resources } = this.props;
        const style = `style='padding-left: 15px;'`;
        let messages = `<div>
            <div> ${resources.ViewInformation1}</div>
            <div> ${resources.ViewInformation2}</div>
            <div ${style}> ${resources.ViewInformation3}</div>
            <div ${style}> ${resources.ViewInformation4}</div>
            <div> ${resources.ViewInformation5}</div>
            <div> ${resources.ViewInformation6}</div>
        </div>`;

        window.top.bootboxAlert(messages, null, null, {width: 1000});
    }

    render() {
        const { updateSuccess, message, hideMessages } = this.state;
        const { viewModel, resources, redirectViewListDetail } = this.props;
        const affectedDownlinesTotal = viewModel.AffectedDownlinesTotal,
            unchangedDownlinesTotal = viewModel.UnchangedDownlinesTotal,
            levels = viewModel.Levels;

        return (<div className="col-xs-12">
            <div className={`msg-container ${hideMessages ? 'hidden' : ''}`}>
                <div className={`msg ${updateSuccess ? ' msg-success' : 'msg msg-error'}`}> {message} </div>
            </div>
            
            <p className="note">
                {usernameRegExp(resources.UnsuspendSingleNote, viewModel.SearchAccountName)}
                <span onClick={this.viewInfomation} className='icon-information-outline' />
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
                            levels.map((level, index) => <BodyRowContent {...{ level, redirectViewListDetail, resources, key: index }} />)
                        }
                    </tbody>
                    <FooterContent {...{
                        affectedDownlinesTotal,
                        unchangedDownlinesTotal,
                        redirectViewListDetail,
                        totalText: resources.Total
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
