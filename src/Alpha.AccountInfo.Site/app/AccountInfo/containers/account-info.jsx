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
        this.state = {
            showInfoStatus: false
        };

        this.handleShowContactInfo = this.handleShowContactInfo.bind(this);
    }

    handleShowContactInfo() {
        this.setState((prevStatus) => { showInfoStatus: !prevStatus })
    }

    render() {
        return (
            <div className="accountinfo-container">
                <CustomerInfoBlock customerInfoModel={this.props.accountInfoModel.CustomerInfo} />
                <FeatureList accountInfoModel={this.props.accountInfoModel}
                    handleShowContactInfo={this.handleShowContactInfo}
                    showInfoStatus={this.showInfoStatus} />                
            </div>
        );
    }
}