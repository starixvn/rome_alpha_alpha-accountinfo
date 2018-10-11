import * as React from 'react';
import ReactDOM from 'react-dom';
import { HashRouter, Switch, Route } from 'react-router-dom';
import ContactInfoContent from '../contents/contact-info-content';
import BalanceInfoContent from '../contents/balance-info-content';
import DownlineListContent from '../contents/downline-list-content';
import SettingContent from '../contents/setting-content';
import StatusContent from '../contents/status-content';
import LoginInfoContent from '../contents/login-info-content';
import OnlineListContent from '../contents/online-list-content';
import FeatureListPage from './feature-list-page';
import MobilePage from './mobile-page';
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
        const { resources, mobileView, actorRoleId, customerInfo,
            downlineList, onlineList, statusData, settingData,
            fetchCustomerInfoFromServer, handleSearchFromUserLevel, handleOpenIPInfo } = this.props;
       
        const { redirectToBalanceInfoPage} = this.state;
        let loginInfo = customerInfo !== null ? customerInfo.LoginInfo : null;

        let buildFeatureListPage = (addAnimation = true) =>
            <FeatureListPage resources={resources}
                actorRoleId={actorRoleId}
                customerInfo={customerInfo}
                redirectToBalanceInfoPage={redirectToBalanceInfoPage}
                addAnimation={addAnimation}
            />;

        const contactInfoPage =
            <MobilePage iconClass="icon-contact-info"
                featureText={resources.ShowContactInfo}
                noInfoText={resources.NoInfo}
                redirectToBalanceInfoPage={redirectToBalanceInfoPage}
                data={customerInfo}
            >
                <ContactInfoContent resources={resources} customerInfo={customerInfo} />
            </MobilePage>;

        let buildBalanceInfoPage = (addAnimation = true) =>
            <MobilePage
                iconClass="icon-balance"
                featureText={resources.BalanceInfo}
                noInfoText={resources.NoInfo}
                data={customerInfo}
                addAnimation={addAnimation}
            >
                <BalanceInfoContent
                    resources={resources}
                    mobileView={mobileView}
                    customerInfo={customerInfo}
                    fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                />
            </MobilePage>;

        const downlineListPage =
            <MobilePage iconClass="icon-downline"
                featureText={resources.DownlineList}
                noInfoText={resources.NoInfo}
                redirectToBalanceInfoPage={redirectToBalanceInfoPage}
                data={downlineList}
            >
                <DownlineListContent
                    resources={resources}
                    data={downlineList}
                    mobileView={mobileView}
                    handleSearchFromUserLevel={handleSearchFromUserLevel}
                />
            </MobilePage>;

        const statusPage =
            <MobilePage iconClass="icon-status"
                featureText={resources.Status}
                noInfoText={resources.NoInfo}
                redirectToBalanceInfoPage={redirectToBalanceInfoPage}
                data={statusData}
            >
                <StatusContent
                    resources={resources}
                    mobileView={mobileView}
                    statusData={statusData}
                    username={customerInfo === null ? '' : customerInfo.UserName}
                    fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                />
            </MobilePage>;

        const settingPage =
            <MobilePage iconClass="icon-settings"
                featureText={resources.Setting}
                noInfoText={resources.NoInfo}
                redirectToBalanceInfoPage={redirectToBalanceInfoPage}
                data={settingData}
            >
                <SettingContent
                    resources={resources}
                    mobileView={mobileView}
                    settingData={settingData}
                />
            </MobilePage>;

        const loginInfoPage =
            <MobilePage iconClass="icon-login"
                featureText={resources.LoginInfo}
                noInfoText={resources.NoInfo}
                redirectToBalanceInfoPage={redirectToBalanceInfoPage}
                data={loginInfo}
            >
                <LoginInfoContent
                    resources={resources}
                    loginInfo={loginInfo}
                    handleOpenIPInfo={handleOpenIPInfo}
                />
            </MobilePage>;

        const onlineListPage =
            <MobilePage iconClass="icon-onlinelist"
                featureText={resources.OnlineList}
                noInfoText={resources.NoInfo}
                redirectToBalanceInfoPage={redirectToBalanceInfoPage}
                data={onlineList}
            >
                <OnlineListContent
                    resources={resources}
                    data={onlineList}
                    handleOpenIPInfo={handleOpenIPInfo}
                />
            </MobilePage>;

        return (
            <DebugRouter>
                <Switch>
                    <Route key={0} exact path="/featureList" render={(props) => buildFeatureListPage()} />
                    <Route key={1} exact path="/contactInfo" render={(props) => contactInfoPage} />
                    <Route key={2} exact path="/balanceInfo" render={(props) => buildBalanceInfoPage(props.location.state.addAnimation)} />
                    <Route key={3} exact path="/downlineList" render={(props) => downlineListPage} />
                    <Route key={4} exact path="/status" render={(props) => statusPage}  />
                    <Route key={5} exact path="/settings" render={(props) => settingPage} />
                    <Route key={6} exact path="/loginInfo" render={(props) => loginInfoPage} />
                    <Route key={6} exact path="/onlineList" render={(props) => onlineListPage} />
                    <Route key={7} exact path="/" render={(props) => customerInfo !== null ? buildBalanceInfoPage(false) : buildFeatureListPage(false)} />
                </Switch>
            </DebugRouter>
        );
    }
}