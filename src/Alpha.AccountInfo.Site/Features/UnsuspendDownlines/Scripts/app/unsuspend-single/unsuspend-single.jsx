import * as React from 'react';
import { unsuspendAPI } from '../unsuspend-api';
import { BodyRowContent, FooterContent } from './unsuspend-single-components';

export default class UnsuspendSingle extends React.Component {
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
        const { resources, viewModel } = this.props;
        let self = this;

        unsuspendAPI.updateSuspend("UnsuspendDownlines/UnsuspendSingle",{ SearchAccountId: viewModel.SearchAccountId })
            .then((data) => {
                self.setState({
                    hideMessages: false,
                    message: data.IsSuccess ? resources.UpdateSuccessfully : data.Message.trim(),
                    updateSuccess: data.IsSuccess
                });

                if (data.IsSuccess) {
                    setTimeout(() => {
                        window.top.realoadLeftPanel = true;
                        window.top.closedPopup = true;
                        window.top.ageWnd.close();
                    }, 1500);
                }

                $.unblockUI();
            });
    }

    render() {
        const { updateSuccess, message, hideMessages } = this.state;
        const { viewModel, resources, redirectViewListDetail } = this.props;
        const affectedDownlinesTotal = viewModel.AffectedDownlinesTotal,
            unchangedDownlinesTotal = viewModel.UnchangedDownlinesTotal,
            levels = viewModel.Levels;

        return (<div>
            <div className={`msg-container ${hideMessages ? 'hidden' : ''}`}>
                <div className={`msg ${updateSuccess ? ' msg-success' : 'msg msg-error'}`}> {message} </div>
            </div>

            <p className="note">
                {`${resources.UnsuspendSingleNote} ${viewModel.SearchAccountName}`}
            </p>

            <div className="col-xs-12">
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
            <div className="col-xs-12 row-info popup-bottom list-button text-right">
                <button className="btn-submit" onClick={this.handleSubmit}>{resources.Unsuspend}</button>
            </div>
        </div >
        );
    }
}
