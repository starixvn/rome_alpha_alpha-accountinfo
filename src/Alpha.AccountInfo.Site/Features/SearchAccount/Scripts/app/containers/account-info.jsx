import React from 'react';
import { accountInfoAPI } from '../accountinfo-api';
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
import { resizePopupOnMobile } from '../helpers/accountinfo-helper';
import Raven from 'raven-js';

export default class AccountInfo extends React.Component {
    constructor(props) {
        super(props);
        let { searchAccountName, actorRoleId, actorName, mobileView, showLoginInfo, resources } = this.props;
        let username = searchAccountName;

        if (username === '' && localStorage.getItem('actorName') === actorName) {
            username = localStorage.getItem('lastSearchUsername') !== null ? localStorage.getItem('lastSearchUsername') : '';
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
            showLoginInfo: showLoginInfo,
            redirectToBalanceInfoPage: true,
            searchCounter: 0,
            customerInfo: null,
            statusData: null,
            settingData: null,
            downlineListPageIndex: 1,
            downlineListData: null,
            onlineListData: null
        };

        this.fetchCustomerInfoFromServer = this.fetchCustomerInfoFromServer.bind(this);
        this.fetchCustomerAllStatusFromServer = this.fetchCustomerAllStatusFromServer.bind(this);
        this.fetchDownlineListFromServer = this.fetchDownlineListFromServer.bind(this);
        this.fetchOnlineListFromServer = this.fetchOnlineListFromServer.bind(this);
        this.handleOpenIPInfo = this.handleOpenIPInfo.bind(this);

        $(window).on({
            orientationchange: function () {
                if (mobileView) {
                    const iframe = window.top.$('#accountinfo-content');
                    const src = $(iframe).attr('src');
                    $(iframe).attr('src', '').css('height', 0).attr('src', src);
                }
            }
        });
    }

    componentDidMount() {
        const { username } = this.state;
        if (username !== '') {
            this.fetchCustomerInfoFromServer(username);
        }
    }

    blockUIWrapper() {
        if ($('.ui-widget-overlay:visible').length > 0) {
            $.blockUI();
        }
    }

    fetchCustomerInfoFromServer(username, redirectToBalanceInfoPage = true, resetSetting = true) {
        this.blockUIWrapper();

        const self = this;
        accountInfoAPI.getCustomerInfo({ searchAccountName: username }).then(
            responeData => {
                if (typeof responeData !== 'undefined' && responeData !== null) {
                    self.setState(prevState => ({
                        username: username,
                        custId: responeData.CustId,
                        customerInfo: responeData,
                        searchCounter: prevState.searchCounter + 1,
                        redirectToBalanceInfoPage: redirectToBalanceInfoPage,
                        onlineListData: prevState.onlineListData,
                        downlineListData: prevState.downlineListData,
                        statusData: resetSetting ? null : prevState.statusData,
                        settingData: resetSetting ? null : prevState.settingData
                    }));

                    localStorage.setItem('lastSearchUsername', username);
                }
                else {
                    self.setState(prevState => ({
                        username: '',
                        custId: 0,
                        customerInfo: null,
                        searchCounter: prevState.searchCounter + 1,
                        onlineListData: null,
                        downlineListData: null,
                        statusData: null,
                        settingData: null
                    }));

                    localStorage.removeItem('lastSearchUsername');
                }

                $.unblockUI();
            });
    }

    fetchCustomerAllStatusFromServer(reload = false) {
        let uplineId = null;
        let uplineName = null;
        let { customerInfo, statusData } = this.state;

        if (customerInfo !== null && (statusData === null || reload)) {
            this.blockUIWrapper();

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

            const params = {
                SearchAccountId: customerInfo.CustId,
                SearchAccountName: customerInfo.UserName,
                SearchAccountRoleId: customerInfo.RoleId,
                UplineId: uplineId,
                UplineName: uplineName,
                UplineRoleId: customerInfo.RoleId + 1
            };

            accountInfoAPI.getCustomerAllStatus(params).then(
                responeData => {
                    this.setState({
                        settingData: responeData.Settings,
                        statusData: responeData.Status,
                        redirectToBalanceInfoPage: false
                    });

                    $.unblockUI();
                });
        }
    }

    fetchDownlineListFromServer(pageIndex = 1, reloadByStatus = false, statusId = 1, totalPages = 0) {
        let { custId, downlineListData, customerInfo } = this.state;
        if (custId > 0 && (downlineListData === null || pageIndex > 1 || reloadByStatus)) {
            $.blockUI();
            const params = {
                SearchAccountId: custId,
                PageIndex: pageIndex,
                StatusId: statusId,
                RoleId: customerInfo.RoleId,
                TotalPages: totalPages
            };

            accountInfoAPI.getDownlineList(params).then(responeData => {
                    responeData.PageIndex = pageIndex;
                    responeData.StatusId = statusId;

                    responeData.DownlineList = (downlineListData === null || reloadByStatus) ?
                        responeData.DownlineList :
                        [...downlineListData.DownlineList, ...responeData.DownlineList];

                    if (pageIndex > 1) {
                        responeData.TotalDownlines = downlineListData.TotalDownlines;
                    }

                    this.setState({
                        downlineListData: responeData,
                        redirectToBalanceInfoPage: false
                    });

                    $.unblockUI();
                });
        }
    }

    fetchOnlineListFromServer() {
        let { custId, onlineListData } = this.state;
        if (custId > 0 && onlineListData === null) {
            $.blockUI();
            accountInfoAPI.getOnlineList({ SearchAccountId: custId }).then(
                responeData => {
                    this.setState({
                        onlineListData: responeData,
                        redirectToBalanceInfoPage: false
                    });

                    $.unblockUI();
                });
        }
    }

    handleOpenIPInfo(e) {
        const IPInfoPath = `${this.state.customerInfo.ViewLoginIPLink}?ip=${e.currentTarget.dataset.ip}`;
        window.top.ageWnd.open(IPInfoPath);

        resizePopupOnMobile();
    }

    buildContentItems() {
        const {
            actorRoleId,
            resources,
            mobileView,
            showLoginInfo,
            customerInfo,
            downlineListData,
            onlineListData,
            statusData,
            settingData
        } = this.state;

        const {
            handleOpenIPInfo,
            fetchCustomerAllStatusFromServer,
            fetchCustomerInfoFromServer,
            fetchDownlineListFromServer,
            fetchOnlineListFromServer
        } = this;

        let loginInfo = null, searchAccountRoleId = actorRoleId - 1, username = '';
        if (customerInfo !== null) {
            loginInfo = customerInfo.LoginInfo;
            searchAccountRoleId = customerInfo.RoleId;
            username = customerInfo.UserName;
        }

        const contentItems = {
            contactInfoProps: {
                iconClass: 'icon-contact-info',
                featureText: resources.ShowContactInfo,
                backText: resources.Back,
                pathname: '/contactInfo',
                visible: true,
                children: <ContactInfo {...{ resources, customerInfo }} />
            },
            balanceInfoProps: {
                iconClass: 'icon-balance',
                featureText: resources.BalanceInfo,
                pathname: '/balanceInfo',
                visible: true,
                children: <BalanceInfo {...{ resources, mobileView, customerInfo, fetchCustomerInfoFromServer }} />
            },
            downlineListProps: {
                iconClass: 'icon-downline',
                featureText: resources.DownlineList,
                pathname: '/downlineList',
                visible: actorRoleId > UserLevel.Agent && searchAccountRoleId > UserLevel.Member,
                fetchDataFromServer: fetchDownlineListFromServer,
                children:
                    <DownlineList
                        {...{
                            resources,
                            downlineListData,
                            mobileView,
                            searchAccountRoleId,
                            fetchDownlineListFromServer,
                            fetchCustomerInfoFromServer
                        }
                        }
                    />
            },
            statusProps: {
                iconClass: 'icon-status',
                featureText: resources.Status,
                pathname: '/status',
                visible: true,
                fetchDataFromServer: fetchCustomerAllStatusFromServer,
                children:
                    <Status
                        {...{
                            resources,
                            mobileView,
                            username,
                            statusData,
                            fetchCustomerAllStatusFromServer,
                            fetchCustomerInfoFromServer
                        }}
                    />
            },
            settingProps: {
                iconClass: 'icon-settings',
                featureText: resources.Setting,
                pathname: '/setting',
                visible: true,
                fetchDataFromServer: fetchCustomerAllStatusFromServer,
                children: <Settings {...{ resources, mobileView, settingData }} />
            },
            loginInfoProps: {
                iconClass: 'icon-login',
                featureText: resources.LoginInfo,
                pathname: '/loginInfo',
                visible: showLoginInfo,
                children: <LoginInfo {...{ resources, loginInfo, handleOpenIPInfo }} />
            },
            onlineListProps: {
                iconClass: 'icon-onlinelist',
                featureText: resources.OnlineList,
                pathname: '/onlineList',
                visible: true,
                fetchDataFromServer: fetchOnlineListFromServer,
                children: <OnlineList {...{ resources, onlineListData, handleOpenIPInfo }} />
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

        return (
            <div className="accountinfo-container">
                <Searching {...{ userNamePlaceHolder, username, fetchCustomerInfoFromServer }} />
                <CustomerInfo {...{ resources, customerInfo, searchCounter, mobileView, fetchCustomerInfoFromServer }} />
                {
                    mobileView ?
                        <AccountInfoMobile {...{ contentItems, redirectToBalanceInfoPage, customerInfo }} /> :
                        customerInfo !== null && <AccountInfoDesktop {...{ contentItems, redirectToBalanceInfoPage }} />
                }
            </div>
        );
    }
}