import React from 'react';
import NumberFormat from 'react-number-format';
import { LoadCustomerInfo } from '../../../constants';
import { mobileScale, resizePopupOnMobile } from '../../../helpers/accountinfo-helper';
import BalanceInfoRow from './balance-info-row';

export default class GivenCreditRow extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenGivenCreditPopup = this.handleOpenGivenCreditPopup.bind(this);
        this.reloadBalanceInfo = this.reloadBalanceInfo.bind(this);
        this.reloadBalanceInfoTimer = null;
    }

    componentDidUpdate() {
        clearInterval(this.reloadBalanceInfoTimer);
    }

    componentWillUnmount() {
        clearInterval(this.reloadBalanceInfoTimer);
    }

    reloadBalanceInfo() {
        const { customerInfo, fetchCustomerInfoFromServer } = this.props;
        if (window.top.isClearInterval) {
            clearInterval(this.reloadBalanceInfoTimer);
            window.top.reloadLeftPanel && fetchCustomerInfoFromServer(customerInfo.UserName, LoadCustomerInfo.ByUpdateGivenCredit);
        }
    }

    handleOpenGivenCreditPopup() {
        const { resources, mobileView, customerInfo } = this.props,
            givenCreditUrl = `${top.alpha.urls.accountInfoRootPath}SearchAccount/GivenCredit` +
                `?SearchAccountId=${customerInfo.CustId}&SearchAccountName=${customerInfo.UserName}` +
                `&SearchAccountRoleId=${customerInfo.RoleId}&CurrentGivenCredit=${customerInfo.BalanceInfo.GivenCredit}` +
                `&GivenCreditMin=${customerInfo.BalanceInfo.GivenCreditMin}&GivenCreditMax=${customerInfo.BalanceInfo.GivenCreditMax}`;
        let width = 470, height = 300;

        if (mobileView) {
            width = mobileScale(width);
            height = mobileScale(height);
        }

        window.top.ageWnd.open(givenCreditUrl, `${resources.GivenCreditPopupTitle} - ${customerInfo.UserName}`, undefined, undefined, width, height);

        if (mobileView) {
            resizePopupOnMobile();
        }

        window.top.isClearInterval = false;
        window.top.reloadLeftPanel = false;
        this.reloadBalanceInfoTimer = setInterval(this.reloadBalanceInfo, 500);
    }

    render() {
        const { resources, customerInfo } = this.props;
        const balanceInfo = customerInfo.BalanceInfo;

        const numberFormatProps = {
            className: 'number-link',
            value: balanceInfo.GivenCredit,
            displayType: 'text',
            decimalScale: 2,
            fixedDecimalScale: true,
            thousandSeparator: true
        };

        const editableGivenCredit = <div className='row-info'>
            <div className='col'>{resources.GivenCredit}</div>
            <div id='given-credit-popup' className='col col-right' onClick={this.handleOpenGivenCreditPopup} >
                <div className='given-credit'>
                    <span className='icon-border-color icon-edited' />
                    <NumberFormat {...numberFormatProps} />
                </div>
            </div>
        </div>;

        const nonEditableGivenCredit =
            <BalanceInfoRow
                id='non-editable-given-credit'
                title={resources.GivenCredit}
                value={balanceInfo.GivenCredit}
            />;

        return balanceInfo.CanUpdateAccount ? editableGivenCredit : nonEditableGivenCredit;
    }
}