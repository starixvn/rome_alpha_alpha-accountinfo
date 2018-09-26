import * as React from 'react';
import ReactDOM from 'react-dom';
import { HashRouter, Switch, Route, Link } from 'react-router-dom';

import { accountInfoAPI } from '../api-client';
import { base64Helper } from '../../helpers/base64-helper';
import { UserLevel } from '../constants/user-level';

import SearchingBlock from '../components/searching-block';
import CustomerInfoBlock from '../components/customer-info-block';
import FeatureList from './feature-list';
import OnlineListBlock from '../components/online-list/online-list-block';
import BalanceInfoBlock from '../components/balance-info/balance-info-block';
import DownlineListBlock from '../components/downline-list/downline-list-block';
import StatusBlock from '../components/status/status-block';
import SettingBlock from '../components/setting/setting-block';
import LoginInfoBlock from '../components/login-info/login-info-block';
import ContactInfoBlock from '../components/contact-info/contact-info-block';
import DebugRouter from '../../helpers/RouterDebugger';

export default class AccountInfoMobile extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const {
            actorRoleId, resources, username, searchCounter, custId, searchAccountRoleId,
            customerInfo, downlineList, onlineList, statusData, settingData,
            fetchCustomerInfoFromServer,
            handleSearchFromUserLevel,
            handleOpenIPInfo } = this.props;

        return (
            <DebugRouter>
                <Switch>
                    <Route key={0} exact path="/" render={(props) => <FeatureList
                        resources={resources}
                        actorRoleId={actorRoleId}
                        searchAccountRoleId={searchAccountRoleId}
                        customerInfo={customerInfo} />}
                    />
                    <Route key={1}
                        exact path="/contactInfo"
                        render={(props) => <ContactInfoBlock resources={resources} customerInfo={customerInfo} />}
                    />
                    <Route key={2}
                        exact path="/balanceInfo"
                        render={(props) => <BalanceInfoBlock resources={resources}
                            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                            customerInfo={customerInfo} />}
                    />

                    <Route key={3}
                        exact path="/downlineList"
                        render={(props) => <DownlineListBlock
                            searchAccountRoleId={searchAccountRoleId}
                            resources={resources}
                            handleSearchFromUserLevel={handleSearchFromUserLevel}
                            data={downlineList} />}
                    />

                    <Route key={4}
                        exact path="/status"
                        render={(props) => <StatusBlock
                            resources={resources}
                            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                            username={username}
                            statusData={statusData} />}
                    />

                    <Route key={5}
                        exact path="/settings"
                        render={(props) => <SettingBlock resources={resources} settingData={settingData} />}
                    />

                    <Route key={6}
                        exact path="/loginInfo"
                        render={(props) => <LoginInfoBlock handleOpenIPInfo={handleOpenIPInfo} resources={resources} customerInfo={customerInfo} />}
                    />

                    <Route key={6}
                        exact path="/onlineList"
                        render={(props) => <OnlineListBlock handleOpenIPInfo={handleOpenIPInfo} resources={resources} data={onlineList} />}
                    />
                </Switch>
            </DebugRouter>
        );
    }
}