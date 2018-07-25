import * as React from 'react';
import CustomerInfoBlock from '../components/customer-info/index';
import BalanceInfoBlock from '../components/balance-info/index';
import DownlineListBlock from '../components/downline-list/index';
import StatusBlock from '../components/status/index';
import SettingBlock from '../components/setting/index';
import LoginInfoBlock from '../components/login-info/index';
import OnlineListBlock from '../components/online-list/index';
import ShowContactInfo from '../components/common/show-contact-info';
import { Accordion } from 'react-accessible-accordion';

// Demo styles, see 'Styles' section below for some notes on use.
import 'react-accessible-accordion/dist/fancy-example.css';

export default class FeatureList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            expandedItems: {
                balanceInfo: true,
                downlineList: false,
                status: false,
                setting: false,
                loginInfo: false,
                onlineList: false
            },
            collapseAll: false
        }

        this.handleClick = this.handleClick.bind(this);
        this.handleShowContactInfo = this.handleShowContactInfo.bind(this);
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

    render() {
        const accountInfoModel = this.props.accountInfoModel;
        const expandedItems = this.state.expandedItems;
        const collapseExpandClassName = this.state.collapseAll ? 'account-info-group' : 'account-info-group scroll-top';

        return (
            <div className={collapseExpandClassName}>
                {
                    !this.state.collapseAll && 
                    <ShowContactInfo text="Show Contact Info" onClick={this.handleShowContactInfo} />
                }                
                <Accordion>
                    <BalanceInfoBlock
                        balanceInfo={accountInfoModel.BalanceInfo}
                        handleClick={this.handleClick}
                        expanded={expandedItems.balanceInfo}
                        dataId="balanceInfo" />
                    <DownlineListBlock
                        downlineList={accountInfoModel.DownlineList}
                        handleClick={this.handleClick}
                        expanded={expandedItems.downlineList}
                        dataId="downlineList" />
                    <StatusBlock                       
                        handleClick={this.handleClick}
                        expanded={expandedItems.status}
                        dataId="status" />
                    <SettingBlock
                        handleClick={this.handleClick}
                        expanded={expandedItems.setting}
                        dataId="setting" />
                    <LoginInfoBlock
                        loginInfo={accountInfoModel.LoginInfo}
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