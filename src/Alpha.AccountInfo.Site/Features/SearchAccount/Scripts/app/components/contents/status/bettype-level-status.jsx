import React from 'react';
import { mobileScale, resizePopupOnMobile } from '../../../helpers/accountinfo-helper';
import { ProductConstants } from '../../../constants';

export default class BettypeLevelStatus extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            left: 200,
            top: 200,
			width: 475,
			height: this.getPopupHeightByProduct(this.props.status.ProductId)
        };

        this.handleOpenPopup = this.handleOpenPopup.bind(this);
    }

	getPopupHeightByProduct(productId) {
		switch (productId) {
			case ProductConstants.Keno: return 530;
			case ProductConstants.AllBet: return 300;
			case ProductConstants.PokerGames: return 400;
			default: return 200;
		}
	}

    handleOpenPopup() {
        const { resources, mobileView, status } = this.props;
        const { left, top, width, height } = this.state;

        const productName = status.Text;
        const popupTitle = `${resources.Update} ${productName} ${resources.Status}`;

        window.top.ageWnd.open(status.Url, popupTitle, left, top, width, mobileView ? mobileScale(height) : height);

        if (mobileView) {
            resizePopupOnMobile();
        }
    }

    render() {
        const { status } = this.props;
        return (
            <div
                id={`product-${status.Text}`}
                className='col-xs-6'
                onClick={this.handleOpenPopup}
            >
                <span className='icon-ic_list' />
                <span className='link'>{status.Text}</span>
            </div>
        );
    }
}