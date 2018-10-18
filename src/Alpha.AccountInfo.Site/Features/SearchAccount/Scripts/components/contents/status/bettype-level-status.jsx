import React from 'react';
import { mobileScale, resizePopupOnMobile } from '../../../helpers/accountinfo-helper';
import { ProductConstants } from '../../../constants';

export default class BettypeLevelStatus extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenPopup = this.handleOpenPopup.bind(this);
    }

    handleOpenPopup(e) {
        let { resources, mobileView, status } = this.props;

        let productId = status.ProductId;
        let productName = status.Text;
        let popupTitle = `${resources.Update} ${productName} ${resources.Status}`;


        let left = 200, top = 200, width = 475;
        let height = productId === ProductConstants.Keno ? 530 : productId >= ProductConstants.AllBet ? 300 : 200;

        window.top.ageWnd.open(status.Url, popupTitle, left, top, width, mobileView ? mobileScale(height) : height);

        if (mobileView) {
            resizePopupOnMobile();
        }
    }

    render() {
        const { status } = this.props;
        return (
            <div className="col-xs-6" onClick={this.handleOpenPopup}>
                <span className="icon-ic_list" />
                <span className="link">{status.Text}</span>
            </div>
        );
    }
}