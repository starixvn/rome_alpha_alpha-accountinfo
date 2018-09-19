import * as React from 'react';
import { render } from 'react-dom';
import { accountInfoAPI } from '../api-client';

import { HashRouter, Route, Link, withRouter } from 'react-router-dom';
import Drilldown from 'react-router-drilldown';

import OnlineListBlock from '../components/online-list-block';
import BalanceInfoBlock from '../components/balance-info-block';
import DownlineListBlock from '../components/downline-list-block';
import StatusBlock from '../components/status-block';
import SettingBlock from '../components/setting-block';
import LoginInfoBlock from '../components/login-info-block';
import ContactInfoBlock from '../components/contact-info-block';

import UserLevel from '../constants/user-level';

export default class FeatureList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            firstLoadSearch: true,
            statusData: null,
            settingData: null,
            levels: {
                Master: 3,
                Agent: 2,
                Member: 1
            }
        };

        this.getCustomerAllStatus = this.getCustomerAllStatus.bind(this);
        this.fetchCustomerAllStatusFromServer = this.fetchCustomerAllStatusFromServer.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.customerInfo.CustId !== this.props.customerInfo.CustId) {
            this.setState({
                statusData: null,
                settingData: null
            });
        }
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
        this.fetchCustomerAllStatusFromServer(e);
    }

    render() {
        const { resources, customerInfo, fetchCustomerInfoFromServer, handleSearchFromUserLevel } = this.props;
        const { firstLoadSearch, statusData, settingData, levels } = this.state;
        let collapseExpandClassName = 'account-info-group';

        if (firstLoadSearch === false) {
            collapseExpandClassName = 'account-info-group scroll-top';
        }

        return (
            <HashRouter>
                <Drilldown>
                    <Route key={0} exact path="/" render={(props) => <Home {...props}
                        resources={resources}
                        getCustomerAllStatus={this.getCustomerAllStatus}
                        fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                        fetchCustomerAllStatusFromServer={this.fetchCustomerAllStatusFromServer}/>}
                    />
                    <Route key={1}
                        exact path="/contactInfo"
                        render={(props) => <ContactInfoBlock
                            resources={resources}
                            data={customerInfo}/>}
                    />
                    <Route key={2}
                        exact path="/balanceInfo"
                        render={(props) => <BalanceInfoBlock {...props} resources={resources}
                            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                            username={customerInfo.UserName}
                            custId={customerInfo.CustId}
                            roleId={customerInfo.RoleId}
                            balanceInfo={customerInfo.BalanceInfo} />}
                    />
                    {
                        customerInfo.RoleId > levels.Member &&
                        <Route key={3}
                            exact path="/downlineList"
                            render={(props) => <DownlineListBlock
                                resources={resources}
                                handleSearchFromUserLevel={handleSearchFromUserLevel}
                                custId={this.props.customerInfo.CustId} />}
                        />
                    }

                    <Route key={4}
                        exact path="/status"
                        render={(props) => <StatusBlock
                            resources={resources}
                            getCustomerAllStatus={this.getCustomerAllStatus}
                            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                            fetchCustomerAllStatusFromServer={this.fetchCustomerAllStatusFromServer}
                            username={customerInfo.UserName}
                            statusData={statusData}/>}
                    />

                    <Route key={5}
                        exact path="/settings"
                        render={(props) => <SettingBlock
                            resources={resources}
                            getCustomerAllStatus={this.getCustomerAllStatus}
                            settingData={settingData}/>}
                    />

                    <Route key={6}
                        exact path="/loginInfo"
                        render={(props) => <LoginInfoBlock
                            resources={resources}
                            loginInfo={customerInfo.LoginInfo}
                            viewIpInfoLink={customerInfo.ViewLoginIPLink}/>}
                    />

                    <Route key={6}
                        exact path="/onlineList"
                        render={(props) => <OnlineListBlock
                            resources={resources}
                            viewIpInfoLink={customerInfo.ViewLoginIPLink}
                            custId={this.props.customerInfo.CustId}/>}
                    />
                </Drilldown>
            </HashRouter>
        );
    }
}


const Home = ({ getCustomerAllStatus }) => (
    <div className="feature-list">
        <nav>
            <Link to="/contactInfo" className="feature-item">
                <span className="icon-contact-info" />
                Contact Info
            </Link>
            <Link to="/balanceInfo" className="feature-item">
                <span className="icon-balance"></span>
                Banlance Info</Link>

            <Link to="/downlineList" className="feature-item">
                <span className="icon-downline"></span>
                Downline List</Link>

            <Link to="/status" className="feature-item" onClick={getCustomerAllStatus}>
                <span className="icon-status"></span>
                Status</Link>

            <Link to="/settings" className="feature-item" onClick={getCustomerAllStatus}>
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