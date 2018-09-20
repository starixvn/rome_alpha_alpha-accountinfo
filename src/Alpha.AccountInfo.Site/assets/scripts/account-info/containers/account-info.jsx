import * as React from 'react';
import ReactDOM from 'react-dom';
import { HashRouter, Switch, Route, Link } from 'react-router-dom';

import { accountInfoAPI } from '../api-client';
import { base64Helper } from '../../helpers/base64-helper';
import { UserLevel } from '../constants/user-level';

import SearchingBlock from '../components/searching-block';
import CustomerInfoBlock from '../components/customer-info-block';
import FeatureList from './feature-list';
import OnlineListBlock from '../components/online-list-block';
import BalanceInfoBlock from '../components/balance-info-block';
import DownlineListBlock from '../components/downline-list-block';
import StatusBlock from '../components/status-block';
import SettingBlock from '../components/setting-block';
import LoginInfoBlock from '../components/login-info-block';
import ContactInfoBlock from '../components/contact-info-block';

export default class AccountInfo extends React.Component {
    constructor(props) {
        super(props);
        let lastSearchUsername = localStorage.getItem("lastSearchUsername") !== null ? localStorage.getItem("lastSearchUsername") : '';
        this.state = {
            username: lastSearchUsername,
            custId: 0,
            actorRoleId: this.props.actorRoleId,
            showInfoStatus: false,
            customerInfo: null,
            statusData: null,
            settingData: null,
            downlineList: null,
            resources: this.props.resources
        };

        this.handleSearchFromUserLevel = this.handleSearchFromUserLevel.bind(this);
        this.fetchCustomerInfoFromServer = this.fetchCustomerInfoFromServer.bind(this);

        this.fetchCustomerAllStatusFromServer = this.fetchCustomerAllStatusFromServer.bind(this);
        this.fetchDownlineListFromServer = this.fetchDownlineListFromServer.bind(this);
        this.fetchOnlineListFromServer = this.fetchOnlineListFromServer.bind(this);

        this.handleOpenIPInfo = this.fetchDownlineListFromServer.bind(this);
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
            self.setState({
                username: username,
                custId: data.CustId,
                customerInfo: data,
                downlineList: null,
                onlineList: null,
                settingData: null,
                statusData: null
            });

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
        let self = this;
        let customerInfo = this.state.customerInfo;

        if (customerInfo !== null && this.state.settingData === null) {
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
                self.setState(() => ({
                    settingData: data.Settings,
                    statusData: data.Status
                }));
            });
        }
    }

    fetchDownlineListFromServer() {
        let self = this;
        if (this.state.custId > 0 && this.state.downlineList === null) {
            accountInfoAPI
                .getDownlineList({ SearchAccountId: this.state.custId })
                .then((responeData) => {
                    self.setState({ downlineList: responeData });
                });
        }
    }

    fetchOnlineListFromServer() {
        let self = this;
        if (this.state.custId > 0 && this.state.onlineList === null) {
            accountInfoAPI
                .getOnlineList({ SearchAccountId: this.state.custId  })
                .then((responeData) => {
                    self.setState({ onlineList: responeData });
                });
        }
    }

    handleOpenIPInfo(e) {
        e.preventDefault();
        window.top.ageWnd.open(`${this.props.customerInfo.ViewLoginIPLink}?ip=${e.currentTarget.dataset.ip}`);
    }

    render() {
        const { actorRoleId, resources, username, custId, customerInfo, downlineList, onlineList, statusData, settingData } = this.state;

        return (
            <div className="accountinfo-container">
                <SearchingBlock
                    userNamePlaceHolder={resources.UserNamePlaceHolder}
                    fetchCustomerInfoFromServer={this.fetchCustomerInfoFromServer} 
                    username={username}
                />

                {
                    customerInfo !== null &&
                    <CustomerInfoBlock
                        resources={resources}
                        data={customerInfo}
                        handleSearchFromUserLevel={this.handleSearchFromUserLevel}
                    />
                }

                <HashRouter>
                    <Switch>
                        <Route key={0} exact path="/" render={(props) => <FeatureList
                            resources={resources}
                            getCustomerAllStatus={this.fetchCustomerAllStatusFromServer}
                            fetchCustomerInfoFromServer={this.fetchCustomerInfoFromServer}
                            fetchDownlineListFromServer={this.fetchDownlineListFromServer}
                            fetchOnlineListFromServer={this.fetchOnlineListFromServer}/>}
                        />
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

                        {
                            actorRoleId > UserLevel.Agent  &&
                            <Route key={3}
                                exact path="/downlineList"
                                render={(props) => <DownlineListBlock
                                    resources={resources}
                                    handleSearchFromUserLevel={this.handleSearchFromUserLevel}
                                    data={downlineList} />}
                            />
                        }

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
                            render={(props) => <LoginInfoBlock resources={resources} customerInfo={customerInfo} handleOpenIPInfo={this.handleOpenIPInfo}/>}
                        />

                        <Route key={6}
                            exact path="/onlineList"
                            render={(props) => <OnlineListBlock resources={resources} custId={custId} data={onlineList} handleOpenIPInfo={this.handleOpenIPInfo}/>}
                        />
                    </Switch>
                </HashRouter>
            </div>
        );
    }
}