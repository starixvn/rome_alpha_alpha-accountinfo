import React from 'react';
import { accountInfoAPI } from '../../api-client';
import { mobileScale, resizePopupOnMobile, resizeBootboxOnMobile } from '../../helpers/accountinfo-helper';
import { ProductConstants } from '../../constants';

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
        let enabled = JSON.parse(e.currentTarget.dataset.enabled);
        let { mobileView, resources } = this.props;

        if (enabled) {
            e.currentTarget.dataset.message === undefined ?
                window.top.ageWnd.open(url, `${resources.Update} ${productName} ${resources.Status}`) :
                this.handleOpenConfirmPopup(e);
            if (mobileView) {
                resizePopupOnMobile();
            }
        }
    }

    updateCustomerStatus(url, param) {
        $.blockUI();
        let { fetchCustomerInfoFromServer, username } = this.props;

        accountInfoAPI.updateCustomerStatus(url, JSON.parse(param)).then((data) => {
            if (data.IsSuccess) {
                setTimeout(() => {
                    $.unblockUI();
                    fetchCustomerInfoFromServer(username, false);
                }, 3000);
            }
            else {
                $.unblockUI();
            }
        });
    }

    handleOpenConfirmPopup(e) {
        let url = e.currentTarget.dataset.url;
        let param = e.currentTarget.dataset.param;
        let message = e.currentTarget.dataset.message;

        window.top.bootboxConfirm(message, e.target, () => this.updateCustomerStatus(url, param));
    }

    handleOpenPopupForProductStatus(e) {
        let { resources, mobileView } = this.props;

        let productId = parseInt(e.currentTarget.dataset.id);
        let productName = e.currentTarget.dataset.name;
        let popupTitle = `${resources.Update} ${productName} ${resources.Status}`;


        let left = 200, top = 200, width = 475;
        let height = productId === ProductConstants.Keno ? 530 : productId >= ProductConstants.AllBet ? 300 : 200;

        window.top.ageWnd.open(e.currentTarget.dataset.url, popupTitle, left, top, width, mobileView ? mobileScale(height) : height);

        if (mobileView) {
            resizePopupOnMobile();
        }
    }

    buildPopupForEditOnProductLevel(status, index) {
        return (
            <div
                className="col-xs-6"
                data-id={status.ProductId}
                data-name={status.Text}
                onClick={this.handleOpenPopupForProductStatus}
                data-url={status.Url}
                key={index}
            >
                <span className="icon-ic_list" />
                <span className="link">{status.Text}</span>
            </div>
        );
    }

    buildPopupForEditStatusOrNonProductLevel(status, index) {
        const disabled = status.Disabled ? "disabled" : "";
        return (
            <div
                className={`col-xs-6 checkbox ${disabled}`}
                onClick={this.handleOpenPopupForCheckbox}
                data-enabled={!status.Disabled}
                data-url={status.Url}
                data-param={status.JsonParams}
                data-message={status.ConfirmMessage}
                data-name={status.Text}
                key={index}
            >
                <input id={status.displayOrder}
                    type="checkbox"
                    disabled={status.Disabled}
                    checked={status.Checked}
                    onChange={() => { }}
                />
                <label className="label-text" htmlFor={status.displayOrder}>{status.Text}</label>
            </div>
        );
    }

    render() {
        const { resources, username, statusData } = this.props;
        return (
            <div className="status-setting">
                {
                    statusData !== null &&
                    statusData.map((status, index) => {
                        return status.IsEditOnProductLevel !== undefined && !status.IsEditOnProductLevel ?
                            this.buildPopupForEditOnProductLevel(status, index) :
                            this.buildPopupForEditStatusOrNonProductLevel(status, index);
                    })
                }
            </div>
        );
    }
}
