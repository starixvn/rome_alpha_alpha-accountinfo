import React from 'react';
import CustomizedAccordionItem from './common/customized-accordion-item';
import { accountInfoAPI } from '../api-client';

export default class StatusBlock extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenPopupForProductStatus = this.handleOpenPopupForProductStatus.bind(this);
        this.handleOpenPopupForCheckbox = this.handleOpenPopupForCheckbox.bind(this);
    }

    handleOpenPopupForCheckbox(e) {
        let self = this;
        let url = e.currentTarget.dataset.url;
        let confirmMessage = e.currentTarget.dataset.message;
        let param = e.currentTarget.dataset.param;

        confirmMessage == undefined ? window.top.ageWnd.open(url) :
            window.top.bootboxConfirm(confirmMessage, e.target,
                function () {
                    $.blockUI();
                    accountInfoAPI.updateStatus(url, JSON.parse(param)).then((data) => {
                        if (data.IsSuccess) {
                            setTimeout(() => {
                                $.unblockUI();
                                self.props.fetchCustomerAllStatusFromServer();
                                self.props.fetchCustomerInfoFromServer(e, self.props.username);
                            }, 2000);
                        }
                    });
                });
    };

    handleOpenPopupForProductStatus(e) {
        window.top.ageWnd.open(e.currentTarget.dataset.url);
    };

    render() {
        const { resources,
            username,
            getCustomerAllStatus,
            fetchCustomerAllStatusFromServer,
            fetchCustomerInfoFromServer,
            blockId,
            expanded,
            statusData } = this.props;

        return (
            <CustomizedAccordionItem
                title={resources.Status}
                iconClassname="icon-status"
                accordionItemBodyClassName="accordion__body block-status"
                expanded={expanded}
                handleClick={getCustomerAllStatus}
                blockId={blockId}>
                {
                    statusData != null && statusData.Settings.map((status, index) => {
                        return status.IsEditOnProductLevel != undefined && !status.IsEditOnProductLevel ?
                            (status.Visible &&
                                <div key={index} className="col">
                                    <a onClick={this.handleOpenPopupForProductStatus} data-url={status.Url}>{status.Text}</a>
                                    <span className="icon-ic_list"></span>
                                </div>) :
                            (status.Visible &&
                                <div key={index}
                                className={`${status.Disabled && "disabled"} col checkbox`}
                                onClick={this.handleOpenPopupForCheckbox}
                                data-url={status.Url}
                                data-param={status.Param}
                                data-message={status.ConfirmMessage}>
                                    <input id={status.displayOrder}
                                        type="checkbox"
                                        disabled={status.Disabled}
                                        checked={status.Checked}/>
                                    <label className="label-text" htmlFor={status.displayOrder}>{status.Text}</label>
                                </div>)
                    })
                }
            </CustomizedAccordionItem>
        );
    }
}
