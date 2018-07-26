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
            expandedItems: this.initialExpandedItems(),
            collapseAll: false
        }

        this.handleClick = this.handleClick.bind(this);
        this.handleShowContactInfo = this.handleShowContactInfo.bind(this);
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

        this.setState({
            expandedItems: newExpandedItems,
            collapseAll
        });
    }

    handleClick(e) {
        e.preventDefault();
        const blockId = e.currentTarget.dataset.id;

        this.updateState(blockId);
    }

    handleShowContactInfo(e) {
        const { expandedItems } = this.state;
        const emptyObject = this.resetColllapseAll(expandedItems);

        this.setState({
            expandedItems: emptyObject,
            collapseAll: true
        }, function () {
            console.log(this.state.expandedItems.balanceInfo, "updated");
        });
    }

    getBalanceInfo() {
        switch (this.props.customerInfo.RoleId) {
            case 3:
                return this.props.customerInfo.MasterBalanceInfo;
            case 2: 
                return this.props.customerInfo.AgentBalanceInfo;
            default:
                return this.props.customerInfo.MemberBalanceInfo;
        }
    }


    render() {
        const { accountInfoModel } = this.props;
        const { expandedItems, collapseAll } = this.state;

        const collapseExpandClassName = collapseAll ? 'account-info-group' : 'account-info-group scroll-top';

        const balanceinfo = this.getBalanceInfo();

        return (
            <div className={collapseExpandClassName}>
                {
                    !this.state.collapseAll &&
                    <ShowContactInfo title="Show Contact Info"
                        classNames="visible-mobile show-more-container"
                        handleClick={this.handleShowContactInfo} />
                }
                <Accordion>
                    <BalanceInfoBlock
                        collapExpandMethod={this.handleClick}
                        expanded={expandedItems.balanceInfo}
                        dataId="balanceInfo"
                        custId={this.props.customerInfo.CustId}
                        roleId={this.props.customerInfo.RoleId}
                        balanceInfo={this.getBalanceInfo()} />
                    <DownlineListBlock
                        collapExpandMethod={this.handleClick}
                        expanded={expandedItems.downlineList}
                        dataId="downlineList"
                        custId={this.props.customerInfo.CustId} />
                    <StatusBlock
                        handleClick={this.handleClick}
                        expanded={expandedItems.status}
                        dataId="status" />
                    <SettingBlock
                        handleClick={this.handleClick}
                        expanded={expandedItems.setting}
                        dataId="setting" />
                    <LoginInfoBlock
                        loginInfo={this.props.customerInfo.LoginInfo}
                        handleClick={this.handleClick}
                        expanded={expandedItems.loginInfo}
                        dataId="loginInfo" />
                    <OnlineListBlock
                        handleClick={this.handleClick}
                        expanded={expandedItems.onlineList}
                        dataId="onlineList" />
                </Accordion>
            </div>
        );
    }
}