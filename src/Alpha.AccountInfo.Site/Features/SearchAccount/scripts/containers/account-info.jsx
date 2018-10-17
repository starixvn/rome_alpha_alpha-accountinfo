import * as React from 'react';
import ReactDOM from 'react-dom';
import { accountInfoAPI } from '../api-client';
import { UserLevel } from '../constants';
import SearchingContent from '../components/contents/searching/searching-content';
import CustomerInfoContent from '../components/contents/customer-info/customer-info-content';
import ContactInfoContent from '../components/contents/contact-info-content';
import BalanceInfoContent from '../components/contents/balance-info-content';
import DownlineListContent from '../components/contents/downline-list/downline-list-content';
import SettingContent from '../components/contents/setting-content';
import StatusContent from '../components/contents/status-content';
import LoginInfoContent from '../components/contents/login-info-content';
import OnlineListContent from '../components/contents/online-list-content';
import AccountInfoMobile from '../components/mobile-version/account-info-mobile';
import AccountInfoDesktop from '../components/desktop-version/account-info-desktop';

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
                self.setState((prevState) => ({
                    statusData: null,
                    settingData: null,
                    downlineList: null,
                    onlineList: null
                }));
            }

            localStorage.setItem('lastSearchUsername', username);
            $.unblockUI();
        });
    }

    handleSearchFromUserLevel(e) {
        e.preventDefault();
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
                .getOnlineList({ SearchAccountId: custId })
                .then((responeData) => {
                    this.setState({ onlineList: responeData });
                });
        }
    }

    handleOpenIPInfo(e) {
        e.preventDefault();
        window.top.ageWnd.open(`${this.state.customerInfo.ViewLoginIPLink}?ip=${e.currentTarget.dataset.ip}`);
    }

    buildContentItems(
        resources,
        mobileView,
        actorRoleId,
        customerInfo,
        downlineList,
        onlineList,
        statusData,
        settingData,
        fetchCustomerInfoFromServer,
        handleSearchFromUserLevel,
        handleOpenIPInfo) {

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
                    <ContactInfoContent
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
                    <BalanceInfoContent
                        resources={resources}
                        mobileView={mobileView}
                        customerInfo={customerInfo}
                        fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                    />)
            },
            downlineListProps: {
                iconClass: "icon-downline",
                featureText: resources.DownlineList,
                path: "/downlineList",
                visible: actorRoleId > UserLevel.Agent && searchAccountRoleId > UserLevel.Member,
                children: buildContent(downlineList,
                    < DownlineListContent
                        resources={resources}
                        data={downlineList}
                        mobileView={mobileView}
                        handleSearchFromUserLevel={handleSearchFromUserLevel}
                    />)
            },
            statusProps: {
                iconClass: "icon-status",
                featureText: resources.Status,
                path: "/status",
                visible: true,
                children: buildContent(statusData,
                    <StatusContent
                        resources={resources}
                        mobileView={mobileView}
                        statusData={statusData}
                        username={customerInfo === null ? '' : customerInfo.UserName}
                        fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                    />)
            },
            settingProps: {
                iconClass: "icon-settings",
                featureText: resources.Setting,
                path: "/setting",
                visible: true,
                children: buildContent(settingData, <SettingContent mobileView={mobileView} settingData={settingData} />)
            },
            loginInfoProps: {
                iconClass: "icon-login",
                featureText: resources.LoginInfo,
                path: "/loginInfo",
                visible: actorRoleId !== UserLevel.Super && searchAccountRoleId !== UserLevel.Agent,
                children: buildContent(loginInfo,
                    <LoginInfoContent
                        resources={resources}
                        loginInfo={loginInfo}
                        handleOpenIPInfo={handleOpenIPInfo}
                    />)
            },
            onlineListProps: {
                iconClass: "icon-onlinelist",
                featureText: resources.OnlineList,
                path: "/onlineList",
                visible: true,
                children: buildContent(onlineList,
                    <OnlineListContent
                        resources={resources}
                        data={onlineList}
                        handleOpenIPInfo={handleOpenIPInfo}
                    />)
            }
        };

        return contentItems;
    }

    render() {
        const {
            actorRoleId,
            resources,
            username,
            searchCounter,
            custId,
            redirectToBalanceInfoPage,
            mobileView,
            customerInfo,
            downlineList,
            onlineList,
            statusData,
            settingData
        } = this.state;

        const contentItems = this.buildContentItems(
            resources,
            mobileView,
            actorRoleId,
            customerInfo,
            downlineList,
            onlineList,
            statusData,
            settingData,
            this.fetchCustomerInfoFromServer,
            this.handleSearchFromUserLevel,
            this.handleOpenIPInfo);

        const accountInfoMobile = <AccountInfoMobile contentItems={contentItems} customerInfo={customerInfo} redirectToBalanceInfoPage={redirectToBalanceInfoPage} />;

        const accountInfoDesktop = <AccountInfoDesktop contentItems={contentItems} redirectToBalanceInfoPage={redirectToBalanceInfoPage} />;

        return (
            <div className="accountinfo-container">
                <SearchingContent
                    userNamePlaceHolder={resources.UserNamePlaceHolder}
                    fetchCustomerInfoFromServer={this.fetchCustomerInfoFromServer}
                    username={username}
                />

                {
                    searchCounter > 0 &&
                    <CustomerInfoContent
                        resources={resources}
                        customerInfo={customerInfo}
                        handleSearchFromUserLevel={this.handleSearchFromUserLevel}
                        mobileView={mobileView}
                    />
                }

                {
                    mobileView ?
                        (mobileView && searchCounter === 0 && username === '' || mobileView && searchCounter > 0) && accountInfoMobile
                        : customerInfo !== null && accountInfoDesktop
                }

            </div>);
    }
}