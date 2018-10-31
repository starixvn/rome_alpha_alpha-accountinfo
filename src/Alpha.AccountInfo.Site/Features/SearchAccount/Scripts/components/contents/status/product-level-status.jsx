import React from 'react';
import { resizePopupOnMobile } from '../../../helpers/accountinfo-helper';

export default class ProductLevelStatus extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenPopup = this.handleOpenPopup.bind(this);
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
        }
    }

    render() {
        const { status } = this.props;
        const disabled = status.Disabled ? "disabled" : "";

        const inputProps = {
            id: status.displayOrder,
            type: "checkbox",
            disabled: disabled,
            checked: status.Checked,
            onChange: () => { }
        };

        return (
            <div id={`product-${status.Text}`} className={`col-xs-6 checkbox ${disabled}`} onClick={this.handleOpenPopup} >
                <input {...inputProps} />
                <label className="label-text" htmlFor={status.displayOrder}>{status.Text}</label>
            </div>
        );
    }
}
