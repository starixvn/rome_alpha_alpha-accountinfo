import React from 'react';
import { accountInfoAPI } from '../../api-client';
import { mobileScale } from '../../../helpers/accountinfo-helper';

export default class StatusContent extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenPopupForProductStatus = this.handleOpenPopupForProductStatus.bind(this);
        this.handleOpenPopupForCheckbox = this.handleOpenPopupForCheckbox.bind(this);
        this.handleOpenConfirmPopup = this.handleOpenConfirmPopup.bind(this);
    }

    handleOpenPopupForCheckbox(e) {
        let url = e.currentTarget.dataset.url;
        let productName = e.currentTarget.dataset.name;
        let resources = this.props.resources;
        if (JSON.parse(e.currentTarget.dataset.enabled)) {
            e.currentTarget.dataset.message === undefined ?
                window.top.ageWnd.open(url, `${resources.Update} ${productName} ${resources.Status}`) :
                this.handleOpenConfirmPopup(e);
        }
    }

    handleOpenConfirmPopup(e) {
        let self = this;
        let url = e.currentTarget.dataset.url;
        let param = e.currentTarget.dataset.param;

        window.top.bootboxConfirm(e.currentTarget.dataset.message, e.target,
            function () {
                $.blockUI();
                accountInfoAPI.updateCustomerStatus(url, JSON.parse(param)).then((data) => {
                    if (data.IsSuccess) {
                        setTimeout(() => {
                            self.props.fetchCustomerInfoFromServer(e, self.props.username, false);
                        }, 3000);
                    }
                    $.unblockUI();
                });
            });
    }

    handleOpenPopupForProductStatus(e) {
        let productId = parseInt(e.currentTarget.dataset.id);
        let productName = e.currentTarget.dataset.name;
        let resources = this.props.resources;
        let popupTitle = `${resources.Update} ${productName} ${resources.Status}`;
        let width = 475, height = 200, left = 200, top = 200;

        if (productId === 8) {
            height = 530;
        }
        else if (productId >= 21) {
            height = 300;
        }

        if (this.props.mobileView) {
            width = mobileScale(width);
            height = mobileScale(height);
        }

        
        window.top.ageWnd.open(e.currentTarget.dataset.url, popupTitle, left, top, width, height);

        if (this.props.mobileView) {
            let topWindow = window.top.document.querySelector('[id^="page-popup-"]').id;
            // assign css for popup
        }
    }

    render() {
        const { resources, username, statusData } = this.props;
        return (
            <div className="status-setting">
                {
                    statusData !== null && statusData.map((status, index) => {
                        const disabled = status.Disabled ? "disabled" : "";
                        return status.IsEditOnProductLevel !== undefined && !status.IsEditOnProductLevel ?
                            <div key={index}
                                className="col-xs-6"
                                data-id={status.ProductId}
                                data-name={status.Text}
                                onClick={this.handleOpenPopupForProductStatus}
                                data-url={status.Url}
                            >
                                <span className="icon-ic_list" />
                                <span className="link">{status.Text}</span>
                            </div> :
                            <div key={index}
                                className={`col-xs-6 checkbox ${disabled}`}
                                onClick={this.handleOpenPopupForCheckbox}
                                data-enabled={!status.Disabled}
                                data-url={status.Url}
                                data-param={status.JsonParams}
                                data-message={status.ConfirmMessage}
                                data-name={status.Text}
                            >
                                <input id={status.displayOrder}
                                    type="checkbox"
                                    disabled={status.Disabled}
                                    checked={status.Checked}
                                    onChange={() => { }}
                                />
                                <label className="label-text" htmlFor={status.displayOrder}>{status.Text}</label>
                            </div>;
                    })
                }
            </div>
        );
    }
}
