import * as React from 'react';
import CustomerInfoBlock from '../components/customer-info/index';
import BalanceInfoBlock from '../components/balance-info/index';
import DownlineListBlock from '../components/downline-list/index';
import StatusBlock from '../components/status/index';
import SettingBlock from '../components/setting/index';
import LoginInfoBlock from '../components/login-info/index';
import OnlineListBlock from '../components/online-list/index';
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
                <CustomerInfoBlock customerInfoModel={this.props.customerInfoModel} />
                <div id="account-tabs" className="account-tabs">
                    <div className="panel-group account-info" id="infoTab" role="tablist" aria-multiselectable="true">
                        <BalanceInfoBlock />
                        <DownlineListBlock />
                        <StatusBlock />
                        <SettingBlock />
                        <LoginInfoBlock />
                        <OnlineListBlock />
                    </div>
                </div>
            </div>
        );
    }
}