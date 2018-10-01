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
import DebugRouter from '../../../helpers/router-debugger';

export default class AccountInfoMobile extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            redirectToBalanceInfoPage: false
        };
    }

    componentWillReceiveProps(nextProps) {
        let { customerInfo } = this.props;
        this.setState({
            redirectToBalanceInfoPage: nextProps.customerInfo !== customerInfo
        });
    }

    render() {
        const { resources, mobileView, actorRoleId, customerInfo, downlineList, onlineList, statusData, settingData,
                fetchCustomerInfoFromServer, handleSearchFromUserLevel, handleOpenIPInfo } = this.props;

        const { redirectToBalanceInfoPage} = this.state;

        let buildFeatureListPage = (addAnimation = true) =>
            <FeatureListPage resources={resources}
                actorRoleId={actorRoleId}
                customerInfo={customerInfo}
                redirectToBalanceInfoPage={redirectToBalanceInfoPage}
                addAnimation={addAnimation}
            />;

        const contactInfoPage = <ContactInfoPage resources={resources} customerInfo={customerInfo} redirectToBalanceInfoPage={redirectToBalanceInfoPage}/>;

        const balanceInfoPage = <BalanceInfoPage resources={resources} mobileView={mobileView} customerInfo={customerInfo} fetchCustomerInfoFromServer={fetchCustomerInfoFromServer} />;

        const downlineListPage =
            <DownlineListPage
                resources={resources}
                handleSearchFromUserLevel={handleSearchFromUserLevel}
                data={downlineList}
                redirectToBalanceInfoPage={redirectToBalanceInfoPage}
            />;

        const statusPage =
            <StatusPage resources={resources}
                mobileView={mobileView}
                fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                username={customerInfo === null ? '' : customerInfo.UserName}
                statusData={statusData}
                redirectToBalanceInfoPage={redirectToBalanceInfoPage && this.props.redirectToBalanceInfoPage}
            />;

        const settingPage = <SettingPage resources={resources} mobileView={mobileView} settingData={settingData} redirectToBalanceInfoPage={redirectToBalanceInfoPage}/>;

        const loginInfoPage =
            <LoginInfoPage resources={resources}
                customerInfo={customerInfo}
                redirectToBalanceInfoPage={redirectToBalanceInfoPage}
                handleOpenIPInfo={handleOpenIPInfo}
            />;

        const onlineListPage = <OnlineListPage resources={resources} data={onlineList} redirectToBalanceInfoPage={redirectToBalanceInfoPage} handleOpenIPInfo={handleOpenIPInfo} />;

        return (
            <HashRouter>
                <Switch>
                    <Route key={0} exact path="/featureList" render={(props) => buildFeatureListPage()} />
                    <Route key={1} exact path="/contactInfo" render={(props) => contactInfoPage}/>
                    <Route key={2} exact path="/balanceInfo" render={(props) => balanceInfoPage}/>
                    <Route key={3} exact path="/downlineList" render={(props) => downlineListPage} />
                    <Route key={4} exact path="/status" render={(props) => statusPage}  />
                    <Route key={5} exact path="/settings" render={(props) => settingPage} />
                    <Route key={6} exact path="/loginInfo" render={(props) => loginInfoPage} />
                    <Route key={6} exact path="/onlineList" render={(props) => onlineListPage} />
                    <Route key={7} exact path="/" render={(props) => customerInfo !== null ? balanceInfoPage : buildFeatureListPage(false)} />
                </Switch>
            </HashRouter>
        );
    }
}