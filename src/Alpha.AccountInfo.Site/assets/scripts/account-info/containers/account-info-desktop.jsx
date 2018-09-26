import * as React from 'react';
import ContactInfoBlock from '../components/contact-info/contact-info-desktop';
import BalanceInfoBlock from '../components/balance-info/balance-info-desktop';
import DownlineListBlock from '../components/downline-list/downline-list-desktop';
import StatusBlock from '../components/status/status-desktop';
import SettingBlock from '../components/setting/setting-desktop';
import LoginInfoBlock from '../components/login-info/login-info-desktop';
import OnlineListBlock from '../components/online-list/online-list-desktop';
import { Accordion } from 'react-accessible-accordion';
import { accountInfoAPI } from '../api-client';
import 'react-accessible-accordion/dist/fancy-example.css';

export default class AccountInfoDesktop extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            expandedItems: this.initialExpandedItems(),
            collapseAll: false,
            firstLoadSearch: true,
            levels: {
                Master: 3,
                Agent: 2,
                Member: 1
            }
        };

        this.handleCollapExpandBlock = this.handleCollapExpandBlock.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.customerInfo !== this.props.customerInfo) {
            this.setState({
                expandedItems: this.initialExpandedItems()
            });
        }
    }

    initialExpandedItems() {
        return {
            contactInfo: false,
            balanceInfo: true,
            downlineList: false,
            status: false,
            setting: false,
            loginInfo: false,
            onlineList: false
        };
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

    render() {
       
        const { expandedItems, collapseAll, firstLoadSearch, levels } = this.state;
        let collapseExpandClassName = 'account-info-group';

        if (firstLoadSearch === false && collapseAll === false) {
            collapseExpandClassName = 'account-info-group scroll-top';
        }


        const {
            actorRoleId, resources, username, searchCounter, custId, searchAccountRoleId,
            customerInfo, downlineList, onlineList, statusData, settingData,
            fetchCustomerInfoFromServer,
            handleSearchFromUserLevel,
            handleOpenIPInfo} = this.props;

        return (
            <div className={collapseExpandClassName}>
                <Accordion>
                    <ContactInfoBlock
                        resources={resources}
                        customerInfo={customerInfo} 
                        expanded={expandedItems.contactInfo}
                        blockId="contactInfo"
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                    />

                    <BalanceInfoBlock
                        resources={resources}
                        fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                        customerInfo={customerInfo}
                        expanded={expandedItems.balanceInfo}
                        blockId="balanceInfo"
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                    />

                    {
                        customerInfo !== null && customerInfo.RoleId > levels.Member &&

                        <DownlineListBlock
                            searchAccountRoleId={searchAccountRoleId}
                            resources={resources}
                            handleSearchFromUserLevel={handleSearchFromUserLevel}
                            data={downlineList}
                            handleCollapExpandBlock={this.handleCollapExpandBlock}
                            expanded={expandedItems.downlineList}
                            blockId="downlineList"/>
                    }

                    <StatusBlock
                        resources={resources}
                        fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                        username={username}
                        statusData={statusData}
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        expanded={expandedItems.status}
                        blockId="status"
                    />

                    <SettingBlock
                        resources={resources}
                        settingData={settingData}
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        expanded={expandedItems.setting}
                        blockId="setting"
                    />

                    <LoginInfoBlock
                        handleOpenIPInfo={handleOpenIPInfo}
                        resources={resources}
                        customerInfo={customerInfo}
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        expanded={expandedItems.loginInfo}
                        blockId="loginInfo" />

                    <OnlineListBlock
                        handleOpenIPInfo={handleOpenIPInfo}
                        resources={resources}
                        data={onlineList}
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        expanded={expandedItems.onlineList}
                        blockId="onlineList"
                    />
                </Accordion>
            </div>
        );
    }
}