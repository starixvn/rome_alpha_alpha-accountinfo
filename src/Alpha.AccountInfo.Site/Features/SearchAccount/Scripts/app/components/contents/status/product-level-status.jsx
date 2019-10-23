import React from 'react';
import { LoadCustomerAllStatus } from '../../../constants';
import { mobileScale, resizePopupOnMobile } from '../../../helpers/accountinfo-helper';

export default class ProductLevelStatus extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenPopup = this.handleOpenPopup.bind(this);
        this.productStatusOnclosing = this.productStatusOnclosing.bind(this);
        this.reloadLeftPanel = this.reloadLeftPanel.bind(this);
    }

    productStatusOnclosing(username = null, updateSuccessfully = false) {
        if (updateSuccessfully === true) {
            const autoPopupTimeOut = 1000;
            this.reloadLeftPanel(username);

            setTimeout(() => {
                window.top.updateProductStatusPopup.close();
            }, autoPopupTimeOut);
        }
        else {
            window.top.updateProductStatusPopup.close();
        }
    }

    reloadLeftPanel(username) {
        const reloadLeftPanelTimeOut = 1500;
        setTimeout(() => {
            const searchAccountNameText = $('#search-account-id').attr('data-search-account-username');
            if (username === searchAccountNameText) {
                this.props.fetchCustomerAllStatusFromServer(LoadCustomerAllStatus.ByUpdateStatus);
            }
        }, reloadLeftPanelTimeOut);
    }

    handleOpenPopup() {
        const { mobileView, resources, status, username } = this.props;

        if (!status.Disabled) {
            if (window.top.updateProductStatusPopup === null || window.top.updateProductStatusPopup === undefined) {
                window.top.updateProductStatusPopup = window.top.ageWnd.createInstance();
            }

            const popupTitle = `${resources.Update} ${status.Text} ${resources.Status} - ${username}`;
            const width = 475, height = 400;
            window.top.updateProductStatusPopup.onClosing = this.productStatusOnclosing;
            window.top.popupManager.openWithHanlder(top.updateProductStatusPopup, status.Url, popupTitle, width, mobileView ? mobileScale(height) : height);

            if (mobileView) {
                resizePopupOnMobile();
            }
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

        return (
            <div
                id={`product-${status.Text}`}
                className={`col-xs-6 checkbox ${status.Disabled ? 'disabled' : ''}`}
                onClick={this.handleOpenPopup}
            >
                <input {...inputProps} />
                <label
                    className='label-text'
                    htmlFor={status.displayOrder}
                >
                    {status.Text}
                </label>
            </div>
        );
    }
}
