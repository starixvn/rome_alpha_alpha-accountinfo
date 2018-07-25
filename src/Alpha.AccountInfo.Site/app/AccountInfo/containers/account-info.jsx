import * as React from 'react';
import CustomerInfoBlock from '../components/customer-info/index';
import BalanceInfoBlock from '../components/balance-info/index';
import DownlineListBlock from '../components/downline-list/index';
import StatusBlock from '../components/status/index';
import SettingBlock from '../components/setting/index';
import LoginInfoBlock from '../components/login-info/index';
import OnlineListBlock from '../components/online-list/index';
import FeatureList from './feature-list';

export default class AccountInfo extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { accountInfoModel, } = this.props;

        return (
            <div className="accountinfo-container">
                <CustomerInfoBlock customerInfoModel={accountInfoModel.CustomerInfo} />
                <FeatureList accountInfoModel={accountInfoModel} />                
            </div>
        );
    }
}