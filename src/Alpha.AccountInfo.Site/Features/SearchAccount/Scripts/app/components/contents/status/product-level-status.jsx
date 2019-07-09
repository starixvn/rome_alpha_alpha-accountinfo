import React from 'react';
import { LoadCustomerAllStatus } from '../../../constants';
import { resizePopupOnMobile } from '../../../helpers/accountinfo-helper';

export default class ProductLevelStatus extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenPopup = this.handleOpenPopup.bind(this);
        this.reloadCustomerPoductLevelStatus = this.reloadCustomerPoductLevelStatus.bind(this);

        this.reloadCustomerPoductLevelStatusTimer = null;
    }

    componentDidUpdate() {
        clearInterval(this.reloadCustomerPoductLevelStatusTimer);
    }

    componentWillUnmount() {
        clearInterval(this.reloadCustomerPoductLevelStatusTimer);
    }

    reloadCustomerPoductLevelStatus() {
        if (window.top.isClearInterval) {
            clearInterval(this.reloadCustomerPoductLevelStatusTimer);
            window.top.reloadLeftPanel && this.props.fetchCustomerAllStatusFromServer(LoadCustomerAllStatus.ByUpdateStatus);
        }
    }

    handleOpenPopup() {
        const { mobileView, resources, status, username } = this.props;
        const url = status.Url;
        const productName = status.Text;

        if (!status.Disabled) {
            window.top.ageWnd.open(url, `${resources.Update} ${productName} ${resources.Status} - ${username}`);
            if (mobileView) {
                resizePopupOnMobile();
            }

            window.top.isClearInterval = false;
            window.top.reloadLeftPanel = false;
            this.reloadCustomerPoductLevelStatusTimer = setInterval(this.reloadCustomerPoductLevelStatus, 1000);
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
