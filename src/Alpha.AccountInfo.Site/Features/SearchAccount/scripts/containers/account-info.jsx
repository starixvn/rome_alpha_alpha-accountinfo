import * as React from 'react';
import { accountInfoAPI } from '../api-client';
import { UserLevel } from '../constants';
import Searching from '../components/contents/searching';
import CustomerInfo from '../components/contents/customer-info/customer-info';
import ContactInfo from '../components/contents/contact-info';
import BalanceInfo from '../components/contents/balance-info/balance-info';
import DownlineList from '../components/contents/downline-list/downline-list';
import Settings from '../components/contents/settings';
import Status from '../components/contents/status/status';
import LoginInfo from '../components/contents/login-info';
import OnlineList from '../components/contents/online-list';
import AccountInfoMobile from '../components/mobile-version/account-info-mobile';
import AccountInfoDesktop from '../components/desktop-version/account-info-desktop';
import * as Raven from 'raven-js';

export default class AccountInfo extends React.Component {
    constructor(props) {
        super(props);
        let { searchAccountName, actorRoleId, actorName, mobileView, resources } = this.props;
        let username = searchAccountName;

        if (username === '' && localStorage.getItem("actorName") === actorName) {
            username = localStorage.getItem("lastSearchUsername") !== null ? localStorage.getItem("lastSearchUsername") : '';
        }
        else {
            localStorage.setItem('actorName', actorName);
            localStorage.removeItem('lastSearchUsername');
        }

        this.state = {
            resources: resources,
            username: username,
            custId: 0,
            actorRoleId: actorRoleId,
            mobileView: mobileView,
            redirectToBalanceInfoPage: true,
            searchCounter: 0,
            customerInfo: null,
            statusData: null,
            settingData: null,
            downlineList: null,
            onlineList: null
        };

        this.handleSearchFromUserLevel = this.handleSearchFromUserLevel.bind(this);
        this.fetchCustomerInfoFromServer = this.fetchCustomerInfoFromServer.bind(this);
        this.fetchCustomerAllStatusFromServer = this.fetchCustomerAllStatusFromServer.bind(this);
        this.fetchDownlineListFromServer = this.fetchDownlineListFromServer.bind(this);
        this.fetchOnlineListFromServer = this.fetchOnlineListFromServer.bind(this);
        this.handleOpenIPInfo = this.handleOpenIPInfo.bind(this);
    }

    componentDidMount() {
        let { username } = this.state;
        if (username !== '') {
            this.fetchCustomerInfoFromServer(username);
        }
    }

    componentDidUpdate(prevProps, prevState) {
        const { customerInfo, custId } = this.state;
        if (customerInfo !== prevState.customerInfo) {
            this.fetchCustomerAllStatusFromServer();
            if (custId !== prevState.custId) {
                this.fetchDownlineListFromServer();
                this.fetchOnlineListFromServer();
            }
        }
    }

    fetchCustomerInfoFromServer(username, redirectToBalanceInfoPage = true) {
        $.blockUI();
        let self = this;
        accountInfoAPI.getCustomerInfo({ searchAccountName: username }).then((data) => {
            self.setState((prevState) => ({
                username: username,
                custId: data !== null ? data.CustId : 0,
                customerInfo: data,
                searchCounter: prevState.searchCounter + 1,
                redirectToBalanceInfoPage: redirectToBalanceInfoPage
            }));

            if (data === null) {
                self.setState(() => ({
                    statusData: null,
                    settingData: null,
                    downlineList: null,
                    onlineList: null
                }));

                localStorage.removeItem('lastSearchUsername');
            }
            else {
                localStorage.setItem('lastSearchUsername', username);
            }

            $.unblockUI();
        });
    }

    handleSearchFromUserLevel(e) {
        this.fetchCustomerInfoFromServer(e.currentTarget.dataset.username);
    }

    fetchCustomerAllStatusFromServer() {
        let uplineId = null;
        let uplineName = null;
        let { customerInfo } = this.state;

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
                    statusData: data.Status,
                    redirectToBalanceInfoPage: false
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
            accountInfoAPI.getOnlineList({ SearchAccountId: custId }).then((responeData) => {
                    this.setState({ onlineList: responeData });
                });
        }
    }

    handleOpenIPInfo(e) {
        window.top.ageWnd.open(`${this.state.customerInfo.ViewLoginIPLink}?ip=${e.currentTarget.dataset.ip}`);
    }

    buildContentItems() {
        const {
            actorRoleId,
            resources,
            mobileView,
            customerInfo,
            downlineList,
            onlineList,
            statusData,
            settingData
        } = this.state;

        let loginInfo = customerInfo !== null ? customerInfo.LoginInfo : null;
        let searchAccountRoleId = customerInfo !== null ? customerInfo.RoleId : actorRoleId - 1;

        const noInfoContent = <div className="no-information">{resources.NoInfo}</div>;
        let buildContent = (data, content) => data === null ? noInfoContent : content;

        let contentItems = {
            contactInfoProps: {
                iconClass: "icon-contact-info",
                featureText: resources.ShowContactInfo,
                path: "/contactInfo",
                visible: true,
                children: buildContent(customerInfo,
                    <ContactInfo
                        resources={resources}
                        customerInfo={customerInfo}
                    />)
            },
            balanceInfoProps: {
                iconClass: "icon-balance",
                featureText: resources.BalanceInfo,
                path: "/balanceInfo",
                visible: true,
                children: buildContent(customerInfo,
                    <BalanceInfo
                        resources={resources}
                        mobileView={mobileView}
                        customerInfo={customerInfo}
                        fetchCustomerInfoFromServer={this.fetchCustomerInfoFromServer}
                    />)
            },
            downlineListProps: {
                iconClass: "icon-downline",
                featureText: resources.DownlineList,
                path: "/downlineList",
                visible: actorRoleId > UserLevel.Agent && searchAccountRoleId > UserLevel.Member,
                children: buildContent(downlineList,
                    <DownlineList
                        resources={resources}
                        data={downlineList}
                        mobileView={mobileView}
                        searchAccountRoleId={searchAccountRoleId}
                        handleSearchFromUserLevel={this.handleSearchFromUserLevel}
                    />)
            },
            statusProps: {
                iconClass: "icon-status",
                featureText: resources.Status,
                path: "/status",
                visible: true,
                children: buildContent(statusData,
                    <Status
                        resources={resources}
                        mobileView={mobileView}
                        statusData={statusData}
                        username={customerInfo === null ? '' : customerInfo.UserName}
                        fetchCustomerAllStatusFromServer={this.fetchCustomerAllStatusFromServer}
                        fetchCustomerInfoFromServer={this.fetchCustomerInfoFromServer}
                    />)
            },
            settingProps: {
                iconClass: "icon-settings",
                featureText: resources.Setting,
                path: "/setting",
                visible: true,
                children: buildContent(settingData, <Settings mobileView={mobileView} settingData={settingData} />)
            },
            loginInfoProps: {
                iconClass: "icon-login",
                featureText: resources.LoginInfo,
                path: "/loginInfo",
                visible: actorRoleId !== UserLevel.Super && searchAccountRoleId !== UserLevel.Agent,
                children: buildContent(loginInfo,
                    <LoginInfo
                        resources={resources}
                        loginInfo={loginInfo}
                        handleOpenIPInfo={this.handleOpenIPInfo}
                    />)
            },
            onlineListProps: {
                iconClass: "icon-onlinelist",
                featureText: resources.OnlineList,
                path: "/onlineList",
                visible: true,
                children: buildContent(onlineList,
                    <OnlineList
                        resources={resources}
                        data={onlineList}
                        handleOpenIPInfo={this.handleOpenIPInfo}
                    />)
            }
        };

        return contentItems;
    }

    componentDidCatch(error, errorInfo) {
        this.setState({ error });
        Raven.captureException(error, { extra: errorInfo });
    }

    render() {
        const {
            resources,
            username,
            searchCounter,
            redirectToBalanceInfoPage,
            mobileView,
            customerInfo
        } = this.state;

        const contentProps = {
            contentItems: this.buildContentItems(),
            redirectToBalanceInfoPage: redirectToBalanceInfoPage
        };

        return (
            <div className="accountinfo-container">
                <Searching
                    userNamePlaceHolder={resources.UserNamePlaceHolder}
                    fetchCustomerInfoFromServer={this.fetchCustomerInfoFromServer}
                    username={username}
                />

                <CustomerInfo
                    resources={resources}
                    customerInfo={customerInfo}
                    handleSearchFromUserLevel={this.handleSearchFromUserLevel}
                    searchCounter={searchCounter}
                    mobileView={mobileView}
                />

                {
                    mobileView ?
                        <AccountInfoMobile {...contentProps} customerInfo={customerInfo} /> :
                        customerInfo !== null && <AccountInfoDesktop {...contentProps} />   
                }

            </div>
        );
    }
}