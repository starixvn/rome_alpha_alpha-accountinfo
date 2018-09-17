import * as React from 'react';
import { render } from 'react-dom';
import { accountInfoAPI } from '../api-client';

import { HashRouter, Route, Link, withRouter } from 'react-router-dom';
import Drilldown from 'react-router-drilldown';

import BalanceInfoBlock from '../components/balance-info-block';
import DownlineListBlock from '../components/downline-list-block';
import StatusBlock from '../components/status-block';
import SettingBlock from '../components/setting-block';
import LoginInfoBlock from '../components/login-info-block';
import OnlineListBlock from '../components/online-list-block';

import UserLevel from '../constants/user-level';

export default class FeatureList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            expandedItems: this.initialExpandedItems(),
            collapseAll: false,
            firstLoadSearch: true,
            statusData: null,
            settingData: null,
            levels: {
                Master: 3,
                Agent: 2,
                Member: 1
            }
        };

        this.handleCollapExpandBlock = this.handleCollapExpandBlock.bind(this);
        this.toogleContactInfoButton = this.toogleContactInfoButton.bind(this);
        this.getCustomerAllStatus = this.getCustomerAllStatus.bind(this);
        this.fetchCustomerAllStatusFromServer = this.fetchCustomerAllStatusFromServer.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.customerInfo.CustId !== this.props.customerInfo.CustId) {
            this.setState({
                expandedItems: this.initialExpandedItems(),
                statusData: null,
                settingData: null
            });
        }
    }

    initialExpandedItems() {
        return {
            balanceInfo: true,
            downlineList: false,
            status: false,
            setting: false,
            loginInfo: false,
            onlineList: false
        };
    }

    fetchCustomerAllStatusFromServer(e) {
        let uplineId = null;
        let uplineName = null;
        let customerInfo = this.props.customerInfo;
        let self = this;

        switch (customerInfo.RoleId) {
            case this.state.levels.Master:
                uplineId = customerInfo.SuperId;
                uplineName = customerInfo.SuperName;
                break;
            case this.state.levels.Agent:
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

    getCustomerAllStatus(e) {
        this.state.settingData === null && this.fetchCustomerAllStatusFromServer(e);
        this.handleCollapExpandBlock(e);
    }

    resetColllapseAll(expandedItems) {
        const emptyObject = {};
        const keys = Object.keys(expandedItems);
        for (let i = 0; i < keys.length; i++) {
            emptyObject[keys[i]] = false;
        }

        return emptyObject;
    }

    updateState(blockId) {
        const { expandedItems } = this.state;
        const currentStatus = expandedItems[blockId];
        const emptyObject = this.resetColllapseAll(expandedItems);

        const newExpandedItems = Object.assign({}, emptyObject, { [blockId]: !currentStatus });
        const collapseAll = Object.keys(expandedItems).filter(key => newExpandedItems[key] === true).length === 0;

        if (this.state.firstLoadSearch) {
            this.setState({
                firstLoadSearch: false
            });
        }

        this.setState({
            expandedItems: newExpandedItems,
            collapseAll
        });
    }

    handleCollapExpandBlock(e) {
        e.preventDefault();
        const blockId = e.currentTarget.dataset.blockid;

        this.updateState(blockId);
    }

    toogleContactInfoButton(e) {
        const { expandedItems } = this.state;
        const emptyObject = this.resetColllapseAll(expandedItems);

        this.setState({
            expandedItems: emptyObject,
            collapseAll: true
        });
    }

    render() {
        const { resources, customerInfo, fetchCustomerInfoFromServer, handleSearchFromUserLevel } = this.props;
        const { expandedItems, collapseAll, firstLoadSearch, statusData, settingData, levels } = this.state;
        let collapseExpandClassName = 'account-info-group';

        if (firstLoadSearch === false && collapseAll === false) {
            collapseExpandClassName = 'account-info-group scroll-top';
        }

        let balanceInfo = customerInfo.BalanceInfo;

        const balanceInfoComponent = <BalanceInfoBlock
            resources={resources}
            handleCollapExpandBlock={this.handleCollapExpandBlock}
            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
            expanded={expandedItems.balanceInfo}
            blockId="balanceInfo"
            username={customerInfo.UserName}
            custId={customerInfo.CustId}
            roleId={customerInfo.RoleId}
            balanceInfo={balanceInfo} />;

        const downlineComponent = <DownlineListBlock
            resources={resources}
            handleSearchFromUserLevel={handleSearchFromUserLevel}
            handleCollapExpandBlock={this.handleCollapExpandBlock}
            expanded={expandedItems.downlineList}
            blockId="downlineList"
            custId={this.props.customerInfo.CustId} />;

        const statusComponent = <StatusBlock
            resources={resources}
            handleCollapExpandBlock={this.handleCollapExpandBlock}
            getCustomerAllStatus={this.getCustomerAllStatus}
            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
            fetchCustomerAllStatusFromServer={this.fetchCustomerAllStatusFromServer}
            username={customerInfo.UserName}
            statusData={statusData}
            expanded={expandedItems.status}
            blockId="status" />;

        const settingComponent = <SettingBlock
            resources={resources}
            handleCollapExpandBlock={this.handleCollapExpandBlock}
            getCustomerAllStatus={this.getCustomerAllStatus}
            settingData={settingData}
            expanded={expandedItems.setting}
            blockId="setting" />;

        const loginInfoComponent = <LoginInfoBlock
            resources={resources}
            loginInfo={customerInfo.LoginInfo}
            viewIpInfoLink={customerInfo.ViewLoginIPLink}
            handleCollapExpandBlock={this.handleCollapExpandBlock}
            expanded={expandedItems.loginInfo}
            blockId="loginInfo" />;

        const onlineListComponent = <OnlineListBlock
            resources={resources}
            handleCollapExpandBlock={this.handleCollapExpandBlock}
            expanded={expandedItems.onlineList}
            blockId="onlineList"
            viewIpInfoLink={customerInfo.ViewLoginIPLink}
            custId={this.props.customerInfo.CustId} />;

        return (

            <HashRouter>
                <Drilldown>
                    <Route key={0} exact path="/" component={Home} />
                    <Route key={1}
                        exact path="/balanceInfo"
                        render={(props) => <BalanceInfoBlock {...props} resources={resources}
                            handleCollapExpandBlock={this.handleCollapExpandBlock}
                            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                            expanded={expandedItems.balanceInfo}
                            blockId="balanceInfo"
                            username={customerInfo.UserName}
                            custId={customerInfo.CustId}
                            roleId={customerInfo.RoleId}
                            balanceInfo={balanceInfo} />}
                    />
                    {
                        customerInfo.RoleId > levels.Member &&
                        <Route key={2}
                            exact path="/downlineList"
                            render={(props) => <DownlineListBlock
                                resources={resources}
                                handleSearchFromUserLevel={handleSearchFromUserLevel}
                                handleCollapExpandBlock={this.handleCollapExpandBlock}
                                expanded={expandedItems.downlineList}
                                blockId="downlineList"
                                custId={this.props.customerInfo.CustId} />}
                        />
                    }

                    <Route key={3}
                        exact path="/status"
                        render={(props) => <StatusBlock
                            resources={resources}
                            handleCollapExpandBlock={this.handleCollapExpandBlock}
                            getCustomerAllStatus={this.getCustomerAllStatus}
                            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                            fetchCustomerAllStatusFromServer={this.fetchCustomerAllStatusFromServer}
                            username={customerInfo.UserName}
                            statusData={statusData}
                            expanded={expandedItems.status}
                            blockId="status"/>}
                    />

                    <Route key={4}
                        exact path="/settings"
                        render={(props) => <SettingBlock
                            resources={resources}
                            handleCollapExpandBlock={this.handleCollapExpandBlock}
                            getCustomerAllStatus={this.getCustomerAllStatus}
                            settingData={settingData}
                            expanded={expandedItems.setting}
                            blockId="setting"/>}
                    />

                    <Route key={5}
                        exact path="/loginInfo"
                        render={(props) => <LoginInfoBlock
                            resources={resources}
                            loginInfo={customerInfo.LoginInfo}
                            viewIpInfoLink={customerInfo.ViewLoginIPLink}
                            handleCollapExpandBlock={this.handleCollapExpandBlock}
                            expanded={expandedItems.loginInfo}
                            blockId="loginInfo" />}
                    />

                    <Route key={6}
                        exact path="/onlineList"
                        render={(props) => <OnlineListBlock
                            resources={resources}
                            handleCollapExpandBlock={this.handleCollapExpandBlock}
                            expanded={expandedItems.onlineList}
                            blockId="onlineList"
                            viewIpInfoLink={customerInfo.ViewLoginIPLink}
                            custId={this.props.customerInfo.CustId}/>}
                    />
                </Drilldown>
            </HashRouter>
        );
    }
}


const Home = ({ roleId }) => (
    <div className="feature-list">
        <nav>


            <Link to="/balanceInfo" className="feature-item">
                <span className="icon-balance"></span>
                Banlance Info</Link>

            <Link to="/downlineList" className="feature-item">
                <span className="icon-downline"></span>
                Downline List</Link>

            <Link to="/status" className="feature-item">
                <span className="icon-status"></span>
                Status</Link>

            <Link to="/settings" className="feature-item">
                <span className="icon-settings"></span>
                Settings</Link>

            <Link to="/loginInfo" className="feature-item">
                <span className="icon-login"></span>
                Login Info</Link>

            <Link to="/onlineList" className="feature-item">
                <span className="icon-onlinelist"></span>
                Online List</Link>
        </nav>
    </div>
)