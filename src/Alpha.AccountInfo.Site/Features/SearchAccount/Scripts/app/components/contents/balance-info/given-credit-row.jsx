import React from 'react';
import NumberFormat from 'react-number-format';
import { mobileScale, resizePopupOnMobile } from '../../../helpers/accountinfo-helper';
import BalanceInfoRow from './balance-info-row';

export default class GivenCreditRow extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenGivenCreditPopup = this.handleOpenGivenCreditPopup.bind(this);
    }

    handleOpenGivenCreditPopup(e) {
        let self = this;
        let { resources, mobileView, customerInfo } = this.props;
        let width = 470, height = 300;
        const accountinfoUrl = top.alpha.urls.accountInfoRootPath;

        let givenCreditUrl = `${accountinfoUrl}SearchAccount/GivenCredit` +
            `?SearchAccountId=${customerInfo.CustId}&SearchAccountName=${customerInfo.UserName}` +
            `&SearchAccountRoleId=${customerInfo.RoleId}&CurrentGivenCredit=${customerInfo.BalanceInfo.GivenCredit}` +
            `&GivenCreditMin=${customerInfo.BalanceInfo.GivenCreditMin}&GivenCreditMax=${customerInfo.BalanceInfo.GivenCreditMax}`;

        if (mobileView) {
            width = mobileScale(width);
            height = mobileScale(height);
        }

        window.top.ageWnd.open(givenCreditUrl, resources.GivenCreditPopupTitle, undefined, undefined, width, height);

        if (mobileView) {
            resizePopupOnMobile();
        }

        window.top.closedPopup = false;
        window.top.realoadLeftPanel = false;
        let timer = setInterval(reloadBalanceInfo, 1000);

        function reloadBalanceInfo() {
            if (window.top.closedPopup) {
                clearInterval(timer);
                window.top.realoadLeftPanel && self.props.fetchCustomerInfoFromServer(customerInfo.UserName);
            }
        }
    }

    render() {
        let { resources, customerInfo } = this.props;
        let balanceInfo = customerInfo.BalanceInfo;

        const numberFormatProps = {
            className: "number-link",
            value: balanceInfo.GivenCredit,
            displayType: 'text',
            decimalScale: 2,
            fixedDecimalScale: true,
            thousandSeparator: true
        }

		let editableGivenCredit = <div className="row-info">
			<div className="col">{resources.GivenCredit}</div>
			<div id="given-credit-popup" className="col col-right" onClick={this.handleOpenGivenCreditPopup} >
				<div className="given-credit">
					<span className="icon-border-color icon-edited" />
					<NumberFormat {...numberFormatProps} />
				</div>
			</div>
		</div>;

		let nonEditableGivenCredit =
			<BalanceInfoRow
				id="non-editable-given-credit"
				title={resources.GivenCredit}
			    value={balanceInfo.GivenCredit}
			/>

		return balanceInfo.CanUpdateAccount ? editableGivenCredit : nonEditableGivenCredit;
    }
}