import * as React from 'react';
import BalanceInfoBlock from '../components/balance-info-block';
import DownlineListBlock from '../components/downline-list-block';
import StatusBlock from '../components/status-block';
import SettingBlock from '../components/setting-block';
import LoginInfoBlock from '../components/login-info-block';
import OnlineListBlock from '../components/online-list-block';
import { Accordion } from 'react-accessible-accordion';
import { accountInfoAPI } from '../api-client';
import 'react-accessible-accordion/dist/fancy-example.css';

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
            UplientId: uplineId,
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

        return (
            <div className={collapseExpandClassName}>
                {
                    !this.state.collapseAll &&
                    <div className="visible-mobile show-more-container">
                        <button className="btn-default show-more" onClick={this.toogleContactInfoButton}>
                            {resources.ShowContactInfo}
                        </button>
                    </div>
                }
                <Accordion>
                    <BalanceInfoBlock
                        resources={resources}
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                        expanded={expandedItems.balanceInfo}
                        blockId="balanceInfo"
                        username={customerInfo.UserName}
                        custId={customerInfo.CustId}
                        roleId={customerInfo.RoleId}
                        balanceInfo={balanceInfo} />

                    {
                        customerInfo.RoleId > levels.Member &&
                        <DownlineListBlock
                            resources={resources}
                            handleSearchFromUserLevel={handleSearchFromUserLevel} 
                            handleCollapExpandBlock={this.handleCollapExpandBlock}
                            expanded={expandedItems.downlineList}
                            blockId="downlineList"
                            custId={this.props.customerInfo.CustId} />
                    }

                    <StatusBlock
                        resources={resources}
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        getCustomerAllStatus={this.getCustomerAllStatus}
                        fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                        fetchCustomerAllStatusFromServer={this.fetchCustomerAllStatusFromServer}
                        username={customerInfo.UserName}
                        statusData={statusData}
                        expanded={expandedItems.status}
                        blockId="status"
                    />

                    <SettingBlock
                        resources={resources}
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        getCustomerAllStatus={this.getCustomerAllStatus}
                        settingData={settingData}
                        expanded={expandedItems.setting}
                        blockId="setting"
                    />

                    <LoginInfoBlock
                        resources={resources}
                        loginInfo={customerInfo.LoginInfo}
                        viewIpInfoLink={customerInfo.ViewLoginIPLink}
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        expanded={expandedItems.loginInfo}
                        blockId="loginInfo"
                    />

                    <OnlineListBlock
                        resources={resources}
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        expanded={expandedItems.onlineList}
                        blockId="onlineList"
                        viewIpInfoLink={customerInfo.ViewLoginIPLink}
                        custId={this.props.customerInfo.CustId}
                    />
                </Accordion>
            </div>
        );
    }
}