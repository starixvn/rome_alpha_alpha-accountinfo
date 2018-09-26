import * as React from 'react';
import ReactDOM from 'react-dom';
import { HashRouter, Switch, Route, Link, Redirect } from 'react-router-dom';
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
        this.state = {
            redirectToBalanceInfoPage: false,
            firstLoad: true
        };
    }

    componentWillReceiveProps(nextProps) {
        if (this.props.customerInfo !== null && nextProps.customerInfo !== this.props.customerInfo) {
            this.setState({
                redirectToBalanceInfoPage: true,
                firstLoad: false
            });
        }
    }

    render() {
        const {
            actorRoleId, resources, username, searchCounter, custId, searchAccountRoleId,
            customerInfo, downlineList, onlineList, statusData, settingData,
            fetchCustomerInfoFromServer,
            handleSearchFromUserLevel,
            handleOpenIPInfo } = this.props;

        const { redirectToBalanceInfoPage } = this.state;

        const featureListPage = <FeatureListPage
            resources={resources}
            actorRoleId={actorRoleId}
            searchAccountRoleId={searchAccountRoleId}
            customerInfo={customerInfo}
            firstLoad={firstLoad}/>;

        const contactInfoPage = <ContactInfoPage resources={resources} customerInfo={customerInfo} />;

        const balanceInfoPage = <BalanceInfoPage resources={resources}
            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
            customerInfo={customerInfo} />;

        const downlineListPage = <DownlineListPage
            searchAccountRoleId={searchAccountRoleId}
            resources={resources}
            handleSearchFromUserLevel={handleSearchFromUserLevel}
            data={downlineList}
            redirectToBalanceInfoPage={redirectToBalanceInfoPage}/>;

        const statusPage = <StatusPage
            resources={resources}
            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
            username={username}
            statusData={statusData} />;

        const settingPage = <SettingPage resources={resources} settingData={settingData} />;

        const loginInfoPage = <LoginInfoPage handleOpenIPInfo={handleOpenIPInfo} resources={resources} customerInfo={customerInfo} />;

        const onlineListPage = <OnlineListPage handleOpenIPInfo={handleOpenIPInfo} resources={resources} data={onlineList} />;

        return (
            <DebugRouter>
                <Switch>
                    <Route key={0} exact path="/featureList" render={(props) => featureListPage} />
                    <Route key={1} exact path="/contactInfo" render={(props) => contactInfoPage}/>
                    <Route key={2} exact path="/balanceInfo" render={(props) => balanceInfoPage}/>
                    <Route key={3} exact path="/downlineList" render={(props) => downlineListPage} />
                    <Route key={4} exact path="/status" render={(props) => statusPage}  />
                    <Route key={5} exact path="/settings" render={(props) => settingPage} />
                    <Route key={6} exact path="/loginInfo" render={(props) => loginInfoPage} />
                    <Route key={6} exact path="/onlineList" render={(props) => onlineListPage} />
                    <Route key={7} exact path="/" render={(props) => customerInfo !== null ? balanceInfoPage : featureListPage} />
                </Switch>
            </DebugRouter>
        );
    }
}