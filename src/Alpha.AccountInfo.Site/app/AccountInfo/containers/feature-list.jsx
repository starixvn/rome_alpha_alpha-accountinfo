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
            collapseAll: false,
            firstLoadSearch: true
        }

        this.handleCollapExpandBlock = this.handleCollapExpandBlock.bind(this);
        this.ShowHideContactInfoButton = this.ShowHideContactInfoButton.bind(this);
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
        const { customerInfo } = this.props;
        const { expandedItems, collapseAll, firstLoadSearch } = this.state;
        let collapseExpandClassName = 'account-info-group';

        if (firstLoadSearch === false && collapseAll === false) {
            collapseExpandClassName = 'account-info-group scroll-top';
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
                        balanceInfo={this.getBalanceInfo()} />

                    {
                        customerInfo.RoleId > 1 &&
                        <DownlineListBlock
                            handleCollapExpandBlock={this.handleCollapExpandBlock}
                            expanded={expandedItems.downlineList}
                            blockId="downlineList"
                            custId={this.props.customerInfo.CustId} />
                    }

                    <StatusBlock
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        expanded={expandedItems.status}
                        blockId="status" />

                    <SettingBlock
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        expanded={expandedItems.setting}
                        blockId="setting" />

                    <LoginInfoBlock
                        loginInfo={this.props.customerInfo.LoginInfo}
                        handleCollapExpandBlock={this.handleCollapExpandBlock}
                        expanded={expandedItems.loginInfo}
                        blockId="loginInfo" />

                    {
                        customerInfo.RoleId > 1 &&
                        <OnlineListBlock
                            handleCollapExpandBlock={this.handleCollapExpandBlock}
                            expanded={expandedItems.onlineList}
                            blockId="onlineList" />
                    }
                </Accordion>
            </div>
        );
    }
}