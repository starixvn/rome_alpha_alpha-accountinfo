import * as React from 'react';
import ReactDOM from 'react-dom';
import { HashRouter, Switch, Route, Link } from 'react-router-dom';
import FeatureListPage from './feature-list-page';
import ContactInfoPage from './contact-info-page';
import OnlineListPage from './online-list-page';
import BalanceInfoPage from './balance-info-page';
import DownlineListPage from './downline-list-page';
import StatusPage from './status-page';
import SettingPage from './setting-page';
import LoginInfoPage from './login-info-page';
import DebugRouter from '../../../helpers/RouterDebugger';

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
                    <Route key={0} exact path="/" render={(props) => <FeatureListPage
                        resources={resources}
                        actorRoleId={actorRoleId}
                        searchAccountRoleId={searchAccountRoleId}
                        customerInfo={customerInfo} />}
                    />
                    <Route key={1}
                        exact path="/contactInfo"
                        render={(props) => <ContactInfoPage resources={resources} customerInfo={customerInfo} />}
                    />
                    <Route key={2}
                        exact path="/balanceInfo"
                        render={(props) => <BalanceInfoPage resources={resources}
                            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                            customerInfo={customerInfo} />}
                    />

                    <Route key={3}
                        exact path="/downlineList"
                        render={(props) => <DownlineListPage
                            searchAccountRoleId={searchAccountRoleId}
                            resources={resources}
                            handleSearchFromUserLevel={handleSearchFromUserLevel}
                            data={downlineList} />}
                    />

                    <Route key={4}
                        exact path="/status"
                        render={(props) => <StatusPage
                            resources={resources}
                            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                            username={username}
                            statusData={statusData} />}
                    />

                    <Route key={5}
                        exact path="/settings"
                        render={(props) => <SettingPage resources={resources} settingData={settingData} />}
                    />

                    <Route key={6}
                        exact path="/loginInfo"
                        render={(props) => <LoginInfoPage handleOpenIPInfo={handleOpenIPInfo} resources={resources} customerInfo={customerInfo} />}
                    />

                    <Route key={6}
                        exact path="/onlineList"
                        render={(props) => <OnlineListPage handleOpenIPInfo={handleOpenIPInfo} resources={resources} data={onlineList} />}
                    />
                </Switch>
            </DebugRouter>
        );
    }
}