import React from 'react';
import { handleError } from '../../../../../../Shared/Scripts/api-client';
import { accountInfoAPI } from '../../../accountinfo-api';
import { LoadCustomerInfo, LoadCustomerAllStatus } from '../../../constants';
import { resizeBootboxOnMobile, resizePopupOnMobile, buildTooltipProps, formatMessage, usernameRegExp } from '../../../helpers/accountinfo-helper';
import { Tooltip } from 'react-tippy';

export default class ConfirmStatus extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenPopup = this.handleOpenPopup.bind(this);
        this.openBootboxConfirm = this.openBootboxConfirm.bind(this);
        this.openPopup = this.openPopup.bind(this);
        this.updateCustomerStatus = this.updateCustomerStatus.bind(this);

        this.confirmPopupOnclosing = this.confirmPopupOnclosing.bind(this);
        this.reloadLeftPanel = this.reloadLeftPanel.bind(this);
    }

    confirmPopupOnclosing(username = null, updateSuccessfully = false) {
        if (updateSuccessfully === true) {
            const autoClosePopupTimeOut = 1000;
            this.reloadLeftPanel(username);

            setTimeout(() => {
                window.top.confirmPopupPopup.close();
            }, autoClosePopupTimeOut);
        }
        else {
            window.top.confirmPopupPopup.close();
        }
    }

    reloadLeftPanel(username) {
        const reloadLeftPanelTimeOut = 1500;
        setTimeout(() => {
            const searchAccountNameText = $('#search-account-id').attr('data-search-account-username');
            if (username === searchAccountNameText) {
                const { fetchCustomerAllStatusFromServer, fetchCustomerInfoFromServer } = this.props;
                fetchCustomerInfoFromServer(username, LoadCustomerInfo.ByUpdateStatus);
                fetchCustomerAllStatusFromServer(LoadCustomerAllStatus.ByUpdateStatus);
            }
        }, reloadLeftPanelTimeOut);
    }

    updateCustomerStatus() {
        $.blockUI();
        const { status } = this.props;
        const params = JSON.parse(status.JsonParams);

        if (params.Func === "UpdateAutoTransfer") {
            this.updateAutoTransfer();
        } else {
            this.updateStatus();
        }
    }

    handleOpenPopup(e) {
        const { status } = this.props;

        if (!status.Disabled) {
            status.IsPopup ? this.openPopup(e) : this.openBootboxConfirm(e);
        }
    }

    openPopup() {
        if (window.top.confirmPopupPopup === null || window.top.confirmPopupPopup === undefined) {
            window.top.confirmPopupPopup = window.top.ageWnd.createInstance();
        }

        const { username, status, mobileView } = this.props;
        const headerPopupText = usernameRegExp(status.PopupHeaderText, username);

        window.top.confirmPopupPopup.onClosing = this.confirmPopupOnclosing;
        window.top.popupManager.openWithHanlder(top.confirmPopupPopup, status.Url, headerPopupText, 700, 500);

        if (mobileView) {
            resizePopupOnMobile();
        }
    }

    openBootboxConfirm(e) {
        const { status, mobileView } = this.props;
        window.top.bootboxConfirm(
            formatMessage(status.ConfirmMessage),
            e.target,
            this.updateCustomerStatus
        );

        if (mobileView) {
            resizeBootboxOnMobile();
        }

        const params = JSON.parse(status.JsonParams);
        if (params.Func === "UpdateAutoTransfer") {
            this.bindEditAutoTransferEvents();
        }
    }

    render() {
        const { status } = this.props;

        const inputProps = {
            type: 'checkbox',
            checked: status.Checked,
            onChange: () => {
                // onChange on parent elememt
            }
        };

        const content = <div>
            <input {...inputProps} />
            <label className='label-text' htmlFor={status.displayOrder}>
                {status.Text}
            </label>
        </div>;

        return (<div
            id={`confirm-${status.DisplayOrder}`}
            className={`col-xs-6 checkbox ${status.Disabled ? 'disabled' : ''}`}
            onClick={this.handleOpenPopup}
        >
            {(typeof status.Tooltip === 'undefined') || status.Tooltip === '' ?
                content : <Tooltip {...buildTooltipProps(status.Tooltip)}>{content}</Tooltip>}

        </div>);
    }

    bindEditAutoTransferEvents() {
        var self = this;
        top.$("#opt-schedule-all").click(() => { this.checkAllAutoTransferSchedule(); });
        top.$(".opt-schedule").click(function () { self.scheduleChanged(this); });
        this.validateAutoTransferSchedule();
    }

    checkAllAutoTransferSchedule() {
        var chkAll = top.$("#opt-schedule-all");
        var isCheck = chkAll.is(":checked");

        var chks = top.$('.opt-schedule');
        chks.each((_, e) => {
            top.$(e).prop("checked", isCheck);
        });

        this.validateAutoTransferSchedule();
    };
    scheduleChanged(elm) {
        var $this = top.$(elm);
        var checkedCount = top.$(".opt-schedule:checked").length;
        top.$('.opt-schedule-all').prop("checked", checkedCount === top.$(".opt-schedule").length);
        this.validateAutoTransferSchedule();
    };
    validateAutoTransferSchedule() {
        var btnOk = top.$("#opt-schedule-all").closest('.modal-content').find(".btn-primary");

        if (top.$(".opt-schedule:checked").length === 0) {
            btnOk.attr('disabled', 'disabled');
        } else {
            btnOk.attr('disabled', null);
        }
    }

    updateStatus() {
        const { fetchCustomerAllStatusFromServer, fetchCustomerInfoFromServer, username, status, mobileView } = this.props;
        const params = JSON.parse(status.JsonParams);
        accountInfoAPI.updateCustomerStatus(status.Url, params)
            .then(data => {
                if (data.IsSuccess) {
                    setTimeout(() => {
                        if (status.ReloadCustomerInfo) {
                            fetchCustomerInfoFromServer(username, LoadCustomerInfo.ByUpdateStatus);
                        }

                        fetchCustomerAllStatusFromServer(LoadCustomerAllStatus.ByUpdateStatus);
                    }, 1500);
                } else {
                    this.reloadLeftPanel(username);
                    window.top.bootboxAlert(data.Message, mobileView ? null : $(`#confirm-${status.DisplayOrder}`)[0]);
                    $.unblockUI();
                }
            }).catch(handleError);
    }

    updateAutoTransfer() {
        const { fetchCustomerAllStatusFromServer, fetchCustomerInfoFromServer, username, status, mobileView } = this.props;
        const params = JSON.parse(status.JsonParams);

        var schedule = [];
        var chks = top.$('.opt-schedule:checked');
        chks.each((_, e) => {
            schedule.push(top.$(e).attr("data-val"));
        });

        params.Schedule = schedule.join(',');

        accountInfoAPI.updateCustomerStatusFromDifferentSite(status.Url, params)
            .then(data => {
                switch (data.Code) {
                    case 108:
                        window.top.indexManager.updateCurrentUrl("adbt=False");
                    case 105:
                        window.top.indexManager.addCustomCookies('messageForReloadSite', data.Message);
                        window.top.location.reload();
                        return;
                    default:
                        break;
                }

                if (data.IsSuccessful) {
                    window.top.bootboxAlert(data.Message, mobileView ? null : $(`#confirm-${status.DisplayOrder}`)[0]);

                    setTimeout(() => {
                        if (status.ReloadCustomerInfo) {
                            fetchCustomerInfoFromServer(username, LoadCustomerInfo.ByUpdateStatus);
                        }

                        fetchCustomerAllStatusFromServer(LoadCustomerAllStatus.ByUpdateStatus);
                    }, 1500);
                } else {
                    this.reloadLeftPanel(username);
                    window.top.bootboxAlert(data.Message, mobileView ? null : $(`#confirm-${status.DisplayOrder}`)[0]);
                    $.unblockUI();
                }

            }).catch(handleError);
    }
}
