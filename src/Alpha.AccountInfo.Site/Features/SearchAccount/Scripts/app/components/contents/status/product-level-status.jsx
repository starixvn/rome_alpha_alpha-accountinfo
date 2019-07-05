import React from 'react';
import { resizePopupOnMobile } from '../../../helpers/accountinfo-helper';

export default class ProductLevelStatus extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenPopup = this.handleOpenPopup.bind(this);
    }

    handleOpenPopup() {
        const { mobileView, resources, status, fetchCustomerAllStatusFromServer } = this.props;
        const url = status.Url;
        const productName = status.Text;

        if (!status.Disabled) {
            window.top.ageWnd.open(url, `${resources.Update} ${productName} ${resources.Status}`);
            if (mobileView) {
                resizePopupOnMobile();
            }

            window.top.isClearInterval = false;
            window.top.reloadLeftPanel = false;
            const reloadCustomerStatusTimer = setInterval(reloadCustomerStatus, 1000);

            function reloadCustomerStatus() {
                if (window.top.isClearInterval) {
                    clearInterval(reloadCustomerStatusTimer);
                    window.top.reloadLeftPanel && fetchCustomerAllStatusFromServer(true);
                }
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
