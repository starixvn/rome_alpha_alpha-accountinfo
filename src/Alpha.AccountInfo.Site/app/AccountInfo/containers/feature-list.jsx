import * as React from 'react';
import CustomerInfoBlock from '../components/customer-info/index';
import BalanceInfoBlock from '../components/balance-info/index';
import DownlineListBlock from '../components/downline-list/index';
import StatusBlock from '../components/status/index';
import SettingBlock from '../components/setting/index';
import LoginInfoBlock from '../components/login-info/index';
import OnlineListBlock from '../components/online-list/index';
import ShowContactInfo from './show-contact-info';
import { Accordion } from 'react-accessible-accordion';

// Demo styles, see 'Styles' section below for some notes on use.
import 'react-accessible-accordion/dist/fancy-example.css';

export default class FeatureList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            getCustomerStatusUrl: window.accountInfoSiteAppPath + "accountinfo/getcustomerstatus",
            expandedItems: this.initialExpandedItems(),
            collapseAll: false,
            firstLoadSearch: true,
            statusData: null,
            settingData: null,
        }

        this.handleCollapExpandBlock = this.handleCollapExpandBlock.bind(this);
        this.ShowHideContactInfoButton = this.ShowHideContactInfoButton.bind(this);
        this.getCustomerAllStatus = this.getCustomerAllStatus.bind(this);
    }

    getCustomerAllStatus(e) {
        console.log("load customerstatus");
        this.handleCollapExpandBlock(e);
        const xhr = new XMLHttpRequest();

        let uplineId = null;
        let uplineName = null;
        switch (this.props.customerInfo.RoleId) {
            case 3:
                uplineId = this.props.customerInfo.SuperId;
                uplineName = this.props.customerInfo.SuperName;
                break;
            case 2:
                uplineId = this.props.customerInfo.MasterId;
                uplineName = this.props.customerInfo.MasterName;
                break;
            default:
                uplineId = this.props.customerInfo.AgentId;
                uplineName = this.props.customerInfo.AgentName;
                break;
        }

        var params = JSON.stringify({
            SearchAccountId: this.props.customerInfo.CustId,
            SearchAccountName: this.props.customerInfo.UserName,
            SearchAccountRoleId: this.props.customerInfo.RoleId,
            UplientId: uplineId,
            UplineName: uplineName,
            UplineRoleId: this.props.customerInfo.RoleId + 1
        });

        xhr.open('post', this.state.getCustomerStatusUrl, true);
        xhr.setRequestHeader("Content-type", "application/json; charset=utf-8");

        xhr.onload = () => {
            var data = JSON.parse(xhr.responseText).Result;
            this.setState({
                settingData: data.Setting,
                statusData: data.Status
            });
        };
        xhr.send(params);
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

    componentWillReceiveProps(nextProps) {
        if (nextProps.customerInfo.CustId != this.props.customerInfo.CustId) {
            this.setState({
                expandedItems: this.initialExpandedItems()
            });
        }
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

    ShowHideContactInfoButton(e) {
        const { expandedItems } = this.state;
        const emptyObject = this.resetColllapseAll(expandedItems);

        this.setState({
            expandedItems: emptyObject,
            collapseAll: true
        }, function () {
            console.log(this.state.expandedItems.balanceInfo, "updated");
        });
    }

    render() {
        const { customerInfo, fetchCustomerInfoFromServer } = this.props;
        const { expandedItems, collapseAll, firstLoadSearch, statusData, settingData} = this.state;
        let collapseExpandClassName = 'account-info-group';

        if (firstLoadSearch === false && collapseAll === false) {
            collapseExpandClassName = 'account-info-group scroll-top';
        }

        let balanceInfo = null;
        let uplineId = null;
        let uplineName = null;

        switch (this.props.customerInfo.RoleId) {
            case 3:
                uplineId = customerInfo.SuperId;
                uplineName = customerInfo.SuperName;
                balanceInfo = customerInfo.MasterBalanceInfo;
                break;
            case 2:
                uplineId = customerInfo.MasterId;
                uplineName = customerInfo.MasterName;
                balanceInfo = customerInfo.AgentBalanceInfo;
                break;
            default:
                uplineId = customerInfo.AgentId;
                uplineName = customerInfo.AgentName;
                balanceInfo = customerInfo.MemberBalanceInfo;
                break;
        }

        return (
            <div className={collapseExpandClassName}>
                {
                    !this.state.collapseAll &&
                    <ShowContactInfo title="Show Contact Info"
                        classNames="visible-mobile show-more-container"
                        handleShowHide={this.ShowHideContactInfoButton} />
                }
                <Accordion>
                    <BalanceInfoBlock
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        expanded={expandedItems.balanceInfo}
                        blockId="balanceInfo"
                        custId={customerInfo.CustId}
                        roleId={customerInfo.RoleId}
                        balanceInfo={balanceInfo} />

                    {
                        customerInfo.RoleId > 1 &&
                        <DownlineListBlock
                            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer} 
                            handleCollapExpandBlock={this.handleCollapExpandBlock}
                            expanded={expandedItems.downlineList}
                            blockId="downlineList"
                            custId={this.props.customerInfo.CustId} />
                    }

                    <StatusBlock
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        getCustomerAllStatus={this.getCustomerAllStatus}
                        statusData={statusData}
                        expanded={expandedItems.status}
                        blockId="status" />

                    <SettingBlock
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        getCustomerAllStatus={this.getCustomerAllStatus}
                        settingData={settingData}
                        expanded={expandedItems.setting}
                        blockId="setting" />

                    <LoginInfoBlock
                        loginInfo={this.props.customerInfo.LoginInfo}
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        expanded={expandedItems.loginInfo}
                        blockId="loginInfo" />

                    <OnlineListBlock
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        expanded={expandedItems.onlineList}
                        blockId="onlineList" />
                </Accordion>
            </div>
        );
    }
}