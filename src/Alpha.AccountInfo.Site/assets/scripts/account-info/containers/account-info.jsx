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

export default class AccountInfo extends React.Component {
    constructor(props) {
        super(props);
        let lastSearchUsername = localStorage.getItem("lastSearchUsername") !== null ? localStorage.getItem("lastSearchUsername") : '';
        let { resources, actorRoleId } = this.props;
        this.state = {
            username: lastSearchUsername,
            resources: resources,
            custId: 0,
            actorRoleId: actorRoleId,
            searchAccountRoleId: actorRoleId - 1,
            searchCounter: 0,
            showInfoStatus: false,
            customerInfo: null,
            statusData: null,
            settingData: null,
            downlineList: null
        };

        this.handleSearchFromUserLevel = this.handleSearchFromUserLevel.bind(this);
        this.fetchCustomerInfoFromServer = this.fetchCustomerInfoFromServer.bind(this);

        this.fetchCustomerAllStatusFromServer = this.fetchCustomerAllStatusFromServer.bind(this);
        this.fetchDownlineListFromServer = this.fetchDownlineListFromServer.bind(this);
        this.fetchOnlineListFromServer = this.fetchOnlineListFromServer.bind(this);

        this.handleOpenIPInfo = this.handleOpenIPInfo.bind(this);
    }

    componentDidUpdate(prevProps, prevState) {
        const { customerInfo, custId} = this.state;
        if (customerInfo !== prevState.customerInfo) {
            this.fetchCustomerAllStatusFromServer();
            if (custId !== prevState.custId) {
                this.fetchDownlineListFromServer();
                this.fetchOnlineListFromServer();
            } 
        }
    }

    fetchCustomerInfoFromServer(e, username) {
        $.blockUI();
        let self = this;
        accountInfoAPI.getCustomerInfo({ searchAccountName: username }).then((data) => {
            self.setState((prevState) => ({
                username: username,
                customerInfo: data,
                searchCounter: prevState.searchCounter + 1,
                searchAccountRoleId: data !== null ? data.RoleId : 0,
                custId: data !== null ? data.CustId : 0
            }));

            localStorage.setItem('lastSearchUsername', username);
            $.unblockUI();
        });
    }

    handleSearchFromUserLevel(e) {
        e.preventDefault();
        this.fetchCustomerInfoFromServer(e, e.currentTarget.dataset.username);
    }

    fetchCustomerAllStatusFromServer() {
        let uplineId = null;
        let uplineName = null;
        let { customerInfo, settingData } = this.state;

        if (customerInfo !== null) {
            switch (customerInfo.RoleId) {
                case UserLevel.Master:
                    uplineId = customerInfo.SuperId;
                    uplineName = customerInfo.SuperName;
                    break;
                case UserLevel.Agent:
                    uplineId = customerInfo.MasterId;
                    uplineName = customerInfo.MasterName;
                    break;
                default:
                    uplineId = customerInfo.AgentId;
                    uplineName = customerInfo.AgentName;
                    break;
            }

            var params = {
                SearchAccountId: customerInfo.CustId,
                SearchAccountName: customerInfo.UserName,
                SearchAccountRoleId: customerInfo.RoleId,
                UplineId: uplineId,
                UplineName: uplineName,
                UplineRoleId: customerInfo.RoleId + 1
            };

            accountInfoAPI.getCustomerAllStatus(params).then((data) => {
                this.setState({
                    settingData: data.Settings,
                    statusData: data.Status
                });
            });
        }
    }

    fetchDownlineListFromServer() {
        let { custId } = this.state;
        if (custId > 0) {
            accountInfoAPI
                .getDownlineList({ SearchAccountId: custId })
                .then((responeData) => {
                    this.setState({ downlineList: responeData });
                });
        }
    }

    fetchOnlineListFromServer() {
        let { custId } = this.state;
        if (custId > 0) {
            accountInfoAPI
                .getOnlineList({ SearchAccountId: custId  })
                .then((responeData) => {
                    this.setState({ onlineList: responeData });
                });
        }
    }

    handleOpenIPInfo(e) {
        e.preventDefault();
        window.top.ageWnd.open(`${this.state.customerInfo.ViewLoginIPLink}?ip=${e.currentTarget.dataset.ip}`);
    }

    render() {
        const {
            actorRoleId, resources, username, searchCounter, custId, searchAccountRoleId,
            customerInfo, downlineList, onlineList, statusData, settingData } = this.state;

        const featureList = <FeatureList
            resources={resources}
            actorRoleId={actorRoleId}
            searchAccountRoleId={searchAccountRoleId}
            customerInfo={customerInfo} />;

        return (
            <div className="accountinfo-container">
                <SearchingBlock
                    userNamePlaceHolder={resources.UserNamePlaceHolder}
                    fetchCustomerInfoFromServer={this.fetchCustomerInfoFromServer} 
                    username={username}
                />

                {
                    searchCounter > 0 && <CustomerInfoBlock
                        resources={resources}
                        data={customerInfo}
                        handleSearchFromUserLevel={this.handleSearchFromUserLevel} />
                }

                <DebugRouter>
                    <Switch>
                        <Route key={0} exact path="/" render={(props) => featureList} />
                        <Route key={1}
                            exact path="/contactInfo"
                            render={(props) => <ContactInfoBlock resources={resources} customerInfo={customerInfo} />}
                        />
                        <Route key={2}
                            exact path="/balanceInfo"
                            render={(props) => <BalanceInfoBlock {...props} resources={resources}
                                fetchCustomerInfoFromServer={this.fetchCustomerInfoFromServer}
                                customerInfo={customerInfo} />}
                        />

                        <Route key={3}
                            exact path="/downlineList"
                            render={(props) => <DownlineListBlock
                                searchAccountRoleId={searchAccountRoleId}
                                resources={resources}
                                handleSearchFromUserLevel={this.handleSearchFromUserLevel}
                                data={downlineList} />}
                        />

                        <Route key={4}
                            exact path="/status"
                            render={(props) => <StatusBlock
                                resources={resources}
                                fetchCustomerInfoFromServer={this.fetchCustomerInfoFromServer}
                                username={username}
                                statusData={statusData} />}
                        />

                        <Route key={5}
                            exact path="/settings"
                            render={(props) => <SettingBlock resources={resources} settingData={settingData} />}
                        />

                        <Route key={6}
                            exact path="/loginInfo"
                            render={(props) => <LoginInfoBlock handleOpenIPInfo={this.handleOpenIPInfo} resources={resources} customerInfo={customerInfo} />}
                        />

                        <Route key={6}
                            exact path="/onlineList"
                            render={(props) => <OnlineListBlock handleOpenIPInfo={this.handleOpenIPInfo} resources={resources} data={onlineList} />}
                        />
                    </Switch>
                </DebugRouter>
            </div>
        );
    }
}