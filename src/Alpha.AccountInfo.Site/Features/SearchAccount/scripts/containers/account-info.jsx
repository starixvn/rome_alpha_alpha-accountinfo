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
            downlineListData: null,
            onlineListData: null
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
                    downlineListData: null,
                    onlineListData: null
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
                    this.setState({ downlineListData: responeData });
                });
        }
    }

    fetchOnlineListFromServer() {
        let { custId } = this.state;
        if (custId > 0) {
            accountInfoAPI.getOnlineList({ SearchAccountId: custId }).then((responeData) => {
                this.setState({ onlineListData: responeData });
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
            downlineListData,
            onlineListData,
            statusData,
            settingData
        } = this.state;

        const handleOpenIPInfo = this.handleOpenIPInfo,
            fetchCustomerAllStatusFromServer = this.fetchCustomerAllStatusFromServer,
            fetchCustomerInfoFromServer = this.fetchCustomerInfoFromServer,
            handleSearchFromUserLevel = this.handleSearchFromUserLevel;

        let loginInfo = null, searchAccountRoleId = actorRoleId - 1, username = '';
        if (customerInfo !== null) {
            loginInfo = customerInfo.LoginInfo;
            searchAccountRoleId = customerInfo.RoleId;
            username = customerInfo.UserName;
        }

        const noInfoContent = <div className="no-information">{resources.NoInfo}</div>;
        let buildContent = (data, content) => data === null ? noInfoContent : content;

        let contentItems = {
            contactInfoProps: {
                iconClass: "icon-contact-info",
                featureText: resources.ShowContactInfo,
                path: "/contactInfo",
                visible: true,
                children: buildContent(customerInfo, <ContactInfo {...{ resources, customerInfo }} />)
            },
            balanceInfoProps: {
                iconClass: "icon-balance",
                featureText: resources.BalanceInfo,
                path: "/balanceInfo",
                visible: true,
                children: buildContent(
                    customerInfo,
                    <BalanceInfo {...{ resources, mobileView, customerInfo, fetchCustomerInfoFromServer }} />
                )
            },
            downlineListProps: {
                iconClass: "icon-downline",
                featureText: resources.DownlineList,
                path: "/downlineList",
                visible: actorRoleId > UserLevel.Agent && searchAccountRoleId > UserLevel.Member,
                children: buildContent(
                    downlineListData,
                    <DownlineList
                        {...{
                            resources,
                            downlineListData,
                            mobileView,
                            searchAccountRoleId,
                            handleSearchFromUserLevel
                        }}
                    />
                )
            },
            statusProps: {
                iconClass: "icon-status",
                featureText: resources.Status,
                path: "/status",
                visible: true,
                children: buildContent(
                    statusData,
                    <Status {...{
                        resources,
                        mobileView,
                        statusData,
                        fetchCustomerAllStatusFromServer,
                        fetchCustomerInfoFromServer,
                        username
                    }}
                    />
                )
            },
            settingProps: {
                iconClass: "icon-settings",
                featureText: resources.Setting,
                path: "/setting",
                visible: true,
                children: buildContent(settingData, <Settings {...{ mobileView, settingData }} />)
            },
            loginInfoProps: {
                iconClass: "icon-login",
                featureText: resources.LoginInfo,
                path: "/loginInfo",
                visible: actorRoleId !== UserLevel.Super && searchAccountRoleId !== UserLevel.Agent,
                children: buildContent(
                    loginInfo,
                    <LoginInfo {...{ resources, loginInfo, handleOpenIPInfo }} />)
            },
            onlineListProps: {
                iconClass: "icon-onlinelist",
                featureText: resources.OnlineList,
                path: "/onlineList",
                visible: true,
                children: buildContent(
                    onlineListData,
                    <OnlineList {...{ resources, onlineListData, handleOpenIPInfo }} />)
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

        const contentItems = this.buildContentItems();
        const userNamePlaceHolder = resources.UserNamePlaceHolder;
        const fetchCustomerInfoFromServer = this.fetchCustomerInfoFromServer;
        const handleSearchFromUserLevel = this.handleSearchFromUserLevel;

        const searchingProps = {
            userNamePlaceHolder,
            username,
            fetchCustomerInfoFromServer
        }

        const customerInfoProps = {
            resources,
            customerInfo,
            searchCounter,
            mobileView,
            handleSearchFromUserLevel
        }

        const contentProps = { contentItems, redirectToBalanceInfoPage };

        return (
            <div className="accountinfo-container">
                <Searching {...searchingProps} />
                <CustomerInfo {...customerInfoProps} />
                {
                    mobileView ?
                        <AccountInfoMobile {...contentProps} customerInfo={customerInfo} /> :
                        customerInfo !== null && <AccountInfoDesktop {...contentProps} />
                }
            </div>
        );
    }
}