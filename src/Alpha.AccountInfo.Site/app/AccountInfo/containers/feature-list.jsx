import * as React from 'react';
import CustomerInfoBlock from '../components/customer-info/index';
import BalanceInfoBlock from '../components/balance-info/index';
import DownlineListBlock from '../components/downline-list/index';
import StatusBlock from '../components/status/index';
import SettingBlock from '../components/setting/index';
import LoginInfoBlock from '../components/login-info/index';
import OnlineListBlock from '../components/online-list/index';
import ShowContactInfo from '../components/common/show-contact-info';
import { Accordion } from 'react-accessible-accordion';

// Demo styles, see 'Styles' section below for some notes on use.
import 'react-accessible-accordion/dist/fancy-example.css';

export default class FeatureList extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const accountInfoModel = this.props.accountInfoModel;

        return (
            <div className="account-info-group" ref="_accountInfoGroup">
                <ShowContactInfo text="Show Contact Info" onClick={this.props.handleShowContactInfo} />
                <Accordion>
                    <BalanceInfoBlock balanceInfo={accountInfoModel.BalanceInfo} />
                    <DownlineListBlock downlineList={accountInfoModel.DownlineList} />
                    <StatusBlock />
                    <SettingBlock />
                    <LoginInfoBlock loginInfo={accountInfoModel.LoginInfo} />
                    <OnlineListBlock />
                </Accordion>
            </div>
        );
    }
}