import React from 'react';
import NumberFormat from 'react-number-format';
import { LoadCustomerInfo } from '../../../constants';
import { mobileScale, resizePopupOnMobile } from '../../../helpers/accountinfo-helper';
import BalanceInfoRow from './balance-info-row';

export default class GivenCreditRow extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenGivenCreditPopup = this.handleOpenGivenCreditPopup.bind(this);
        this.givenPopupOnclosing = this.givenPopupOnclosing.bind(this);
        this.reloadLeftPanel = this.reloadLeftPanel.bind(this);
    }

    givenPopupOnclosing(username = null, updateSuccessfully = false) {
        if (updateSuccessfully === true) {
            this.reloadLeftPanel(username);
            const autoCloseGivenPopupTimeOut = 3000;

            setTimeout(() => {
                window.top.updateGivenCreditPopup.close();
            }, autoCloseGivenPopupTimeOut);
        }
        else {
            window.top.updateGivenCreditPopup.close();
        }
    }

    reloadLeftPanel(username) {
        $.blockUI();

        const reloadLeftPanelTimeOut = 300;
        setTimeout(() => {
            const { customerInfo, fetchCustomerInfoFromServer } = this.props;
            fetchCustomerInfoFromServer(username, LoadCustomerInfo.ByUpdateGivenCredit);
        }, reloadLeftPanelTimeOut);
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

        if (window.top.updateGivenCreditPopup === null || window.top.updateGivenCreditPopup === undefined) {
            window.top.updateGivenCreditPopup = window.top.ageWnd.createInstance();
            window.top.updateGivenCreditPopup.onClosing = this.givenPopupOnclosing;
        }

        window.top.popupManager.openWithHanlder(top.updateGivenCreditPopup, givenCreditUrl, `${resources.GivenCreditPopupTitle} - ${customerInfo.UserName}`, width, height);

        if (mobileView) {
            resizePopupOnMobile();
        }
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