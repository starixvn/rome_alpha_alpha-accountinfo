import * as React from 'react';
import CustomerInfoBlock from '../components/customer-info/index';
import BalanceInfoBlock from '../components/balance-info/index';
import DownlineListBlock from '../components/downline-list/index';
import StatusBlock from '../components/status/index';
import SettingBlock from '../components/setting/index';
import LoginInfoBlock from '../components/login-info/index';
import OnlineListBlock from '../components/online-list/index';
import AccountInfoViewModel from '../models/account-info-model'

interface IAccountInfoProps {
    accountInfoModel: AccountInfoViewModel
}

interface IAccountInfoState {
}

export default class AccountInfo extends React.Component<IAccountInfoProps, IAccountInfoState> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="accountinfo-container">
                <CustomerInfoBlock customerInfoModel={this.props.accountInfoModel.CustomerInfo} />
                <div id="account-tabs" className="account-tabs">
                    <div className="panel-group account-info" id="infoTab" role="tablist" aria-multiselectable="true">
                        <BalanceInfoBlock balanceInfo={this.props.accountInfoModel.BalanceInfo} />
                        <DownlineListBlock downlineList={this.props.accountInfoModel.DownlineList} />
                        <StatusBlock />
                        <SettingBlock />
                        <LoginInfoBlock loginInfo={this.props.accountInfoModel.LoginInfo} />
                        <OnlineListBlock />
                    </div>
                </div>
            </div>
        );
    }
}