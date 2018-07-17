import * as React from 'react';
import CustomerInfoBlock from '../components/customer-info/index';
import BalanceInfoBlock from '../components/balance-info/index';
import DownlineListBlock from '../components/downline-list/index';
import StatusBlock from '../components/status/index';
import SettingBlock from '../components/setting/index';
import LoginInfoBlock from '../components/login-info/index';
import OnlineListBlock from '../components/online-list/index';
import { Accordion } from 'react-accessible-accordion';

// Demo styles, see 'Styles' section below for some notes on use.
import 'react-accessible-accordion/dist/fancy-example.css';

export default class AccountInfo extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="accountinfo-container">
                <CustomerInfoBlock customerInfoModel={this.props.accountInfoModel.CustomerInfo} />
                <Accordion>
                    <BalanceInfoBlock balanceInfo={this.props.accountInfoModel.BalanceInfo} />
                    <DownlineListBlock downlineList={this.props.accountInfoModel.DownlineList} />
                    <StatusBlock />
                    <SettingBlock />
                    <LoginInfoBlock loginInfo={this.props.accountInfoModel.LoginInfo} />
                    <OnlineListBlock />
                </Accordion>
            </div>
        );
    }
}