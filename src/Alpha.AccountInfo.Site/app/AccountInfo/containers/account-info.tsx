import * as React from 'react';
import AccountInfoTab from '../components/account-info/index';
import BalanceInfoTab from '../components/balance-info/index';
import DownlineListTab from '../components/downline-list/index';
import StatusTab from '../components/status/index';
import SettingTab from '../components/setting/index';
import LoginInfoTab from '../components/login-info/index';
import OnlineListTab from '../components/online-list/index';

interface IAccountInfoProps {
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
                <AccountInfoTab />
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