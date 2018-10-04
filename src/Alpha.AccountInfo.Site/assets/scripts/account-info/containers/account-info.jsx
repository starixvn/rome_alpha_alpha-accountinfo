import * as React from 'react';
import ReactDOM from 'react-dom';
import { accountInfoAPI } from '../api-client';
import { UserLevel } from '../constants/user-level';
import SearchingBlock from '../components/searching-block';
import CustomerInfoBlock from '../components/customer-info-block';
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
            searchAccountRoleId: actorRoleId - 1,
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

    fetchCustomerInfoFromServer(e, username, redirectToBalanceInfoPage = true) {
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

    render() {
        const { actorRoleId, resources, username, searchCounter, custId, redirectToBalanceInfoPage,
            mobileView, customerInfo, downlineList, onlineList, statusData, settingData } = this.state;

        const accountInfoMobile =
            <AccountInfoMobile
                resources={resources}
                mobileView={mobileView}
                actorRoleId={actorRoleId}
                customerInfo={customerInfo}
                downlineList={downlineList}
                onlineList={onlineList}
                statusData={statusData}
                settingData={settingData}
                redirectToBalanceInfoPage={redirectToBalanceInfoPage}
                fetchCustomerInfoFromServer={this.fetchCustomerInfoFromServer}
                handleSearchFromUserLevel={this.handleSearchFromUserLevel}
                handleOpenIPInfo={this.handleOpenIPInfo}
            />;

        const accountInfoDesktop =
            <AccountInfoDesktop
                resources={resources}
                mobileView={mobileView}
                actorRoleId={actorRoleId}
                customerInfo={customerInfo}
                downlineList={downlineList}
                onlineList={onlineList}
                statusData={statusData}
                settingData={settingData}
                redirectToBalanceInfoPage={redirectToBalanceInfoPage}
                fetchCustomerInfoFromServer={this.fetchCustomerInfoFromServer}
                handleSearchFromUserLevel={this.handleSearchFromUserLevel}
                handleOpenIPInfo={this.handleOpenIPInfo}
            />;

        return (
            <div className="accountinfo-container">
                <SearchingBlock
                    userNamePlaceHolder={resources.UserNamePlaceHolder}
                    fetchCustomerInfoFromServer={this.fetchCustomerInfoFromServer}
                    username={username}
                />

                {
                    searchCounter > 0 &&
                    <CustomerInfoBlock
                        resources={resources}
                        customerInfo={customerInfo}
                        handleSearchFromUserLevel={this.handleSearchFromUserLevel}
                    />
                }

                {
                    mobileView ? accountInfoMobile : customerInfo !== null && accountInfoDesktop
                }

            </div>);
    }
}