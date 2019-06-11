import React from 'react';
import { resizePopupOnMobile } from '../../../helpers/accountinfo-helper';

export default class ProductLevelStatus extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenPopup = this.handleOpenPopup.bind(this);
        this.reloadCustomerStatus = this.reloadCustomerStatus.bind(this);
        this.timer = undefined;
    }

    componentWillUnmount() {
        clearInterval(this.timer);
        window.top.realoadLeftPanel = false;
    }

    handleOpenPopup() {
        const { mobileView, resources, status } = this.props;
        const url = status.Url;
        const productName = status.Text;

        if (!status.Disabled) {
            window.top.ageWnd.open(url, `${resources.Update} ${productName} ${resources.Status}`);
            if (mobileView) {
                resizePopupOnMobile();
            }

            window.top.realoadLeftPanel = false;
            this.timer = setInterval(this.reloadCustomerStatus, 1000);
        }
    }

    reloadCustomerStatus() {
        const { fetchCustomerAllStatusFromServer } = this.props;
        if (window.top.realoadLeftPanel) {
            clearInterval(this.timer);
            fetchCustomerAllStatusFromServer(true);
        }
    }

    render() {
        const { status } = this.props;

        const inputProps = {
            type: 'checkbox',
            checked: status.Checked,
            onChange: () => { }
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
