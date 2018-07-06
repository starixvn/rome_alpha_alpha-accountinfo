import * as React from 'react';
import AccountInfoTab from '../components/tab-account-info';
import BalanceInfoTab from '../components/tab-balance-info';
import DownlineListTab from '../components/tab-downline-list';
import StatusTab from '../components/tab-status';
import SettingTab from '../components/tab-setting';
import LoginInfoTab from '../components/tab-login-info';
import OnlineListTab from '../components/tab-online-list';

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