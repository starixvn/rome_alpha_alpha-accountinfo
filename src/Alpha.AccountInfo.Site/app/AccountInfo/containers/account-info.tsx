import * as React from 'react';
import CustomerInfoTab from '../components/customer-info/index';
import BalanceInfoTab from '../components/balance-info/index';
import DownlineListTab from '../components/downline-list/index';
import StatusTab from '../components/status/index';
import SettingTab from '../components/setting/index';
import LoginInfoTab from '../components/login-info/index';
import OnlineListTab from '../components/online-list/index';
import CustomerInfoViewModel from '../models/customer-info-model'

interface IAccountInfoProps {
    customerInfoModel: CustomerInfoViewModel
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
                <CustomerInfoTab customerInfoModel={this.props.customerInfoModel} />
                <div id="account-tabs" className="account-tabs">
                    <div className="panel-group account-info" id="infoTab" role="tablist" aria-multiselectable="true">
                        <BalanceInfoTab />
                        <DownlineListTab />
                        <StatusTab />
                        <SettingTab />
                        <LoginInfoTab />
                        <OnlineListTab />
                    </div>
                </div>
            </div>
        );
    }
}