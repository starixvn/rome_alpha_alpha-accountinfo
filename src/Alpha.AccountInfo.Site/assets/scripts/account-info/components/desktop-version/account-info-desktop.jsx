import * as React from 'react';
import { Accordion } from 'react-accessible-accordion';
import { UserLevel } from '../../constants/user-level';
import 'react-accessible-accordion/dist/fancy-example.css';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import ContactInfoContent from '../contents/contact-info-content';
import BalanceInfoContent from '../contents/balance-info-content';
import DownlineListContent from '../contents/downline-list-content';
import SettingContent from '../contents/setting-content';
import StatusContent from '../contents/status-content';
import LoginInfoContent from '../contents/login-info-content';
import OnlineListContent from '../contents/online-list-content';

export default class AccountInfoDesktop extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            expandedItems: this.initialExpandedItems(),
            collapseAll: false,
            firstLoadSearch: true
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

        const { expandedItems, collapseAll, firstLoadSearch } = this.state;
        let collapseExpandClassName = 'account-info-group';

        if (firstLoadSearch === false && collapseAll === false) {
            collapseExpandClassName = 'account-info-group scroll-top';
        }

        const {
            actorRoleId, resources, username, searchCounter, custId, searchAccountRoleId,
            customerInfo, downlineList, onlineList, statusData, settingData,
            fetchCustomerInfoFromServer,
            handleSearchFromUserLevel,
            handleOpenIPInfo } = this.props;

        const noInfoContent = <div className="no-information">{resources.NoInfo}</div>;

        return customerInfo === null ?
            <div className="no-information">{resources.NoInfo}</div> :
            <div className={collapseExpandClassName}>
                <Accordion>
                    <CustomizedAccordionItem
                        title={resources.ShowContactInfo}
                        iconClassname="icon-contact-info"
                        accordionItemBodyClassName="accordion__body contact-info"
                        expanded={expandedItems.contactInfo}
                        handleClick={this.handleCollapExpandBlock}
                        blockId="contactInfo"
                    >
                        <ContactInfoContent resources={resources} customerInfo={customerInfo} />
                    </CustomizedAccordionItem>

                    <CustomizedAccordionItem
                        title={resources.BalanceInfo}
                        iconClassname="icon-balance"
                        accordionItemBodyClassName="accordion__body balance-info"
                        expanded={expandedItems.balanceInfo}
                        handleClick={this.handleCollapExpandBlock}
                        blockId="balanceInfo"
                    >
                        <BalanceInfoContent resources={resources} customerInfo={customerInfo} fetchCustomerInfoFromServer={fetchCustomerInfoFromServer} />
                    </CustomizedAccordionItem>


                    {
                        customerInfo !== null && customerInfo.RoleId > UserLevel.Member &&
                        <CustomizedAccordionItem
                            title={resources.DownlineList}
                            iconClassname="icon-downline"
                            accordionItemBodyClassName="accordion__body downline-list"
                            expanded={expandedItems.downlineList}
                            handleClick={this.handleCollapExpandBlock}
                            blockId="downlineList"
                        >
                            {
                                downlineList === null ? noInfoContent :
                                    <DownlineListContent resources={resources} data={downlineList} handleSearchFromUserLevel={handleSearchFromUserLevel} />
                            }
                            
                        </CustomizedAccordionItem>
                    }

                    <CustomizedAccordionItem
                        title={resources.Status}
                        iconClassname="icon-status"
                        accordionItemBodyClassName="accordion__body status"
                        expanded={expandedItems.status}
                        handleClick={this.handleCollapExpandBlock}
                        blockId="status"
                    >
                        {
                            statusData === null ? noInfoContent :
                                <StatusContent
                                    resources={resources}
                                    statusData={statusData}
                                    username={username}
                                    fetchCustomerInfoFromServer={fetchCustomerInfoFromServer} />
                        }
                       
                    </CustomizedAccordionItem>

                    <CustomizedAccordionItem
                        title={resources.Setting}
                        iconClassname="icon-settings"
                        accordionItemBodyClassName="accordion__body settings"
                        expanded={expandedItems.setting}
                        handleClick={this.handleCollapExpandBlock}
                        blockId="setting"
                    >
                        {  settingData === null ? noInfoContent :<SettingContent resources={resources} settingData={settingData} /> }
                    </CustomizedAccordionItem>
            

                    <CustomizedAccordionItem
                        title={resources.LoginInfo}
                        iconClassname="icon-login"
                        accordionItemBodyClassName="accordion__body login-info"
                        expanded={expandedItems.loginInfo}
                        handleClick={this.handleCollapExpandBlock}
                        blockId="loginInfo"
                    >
                        {
                            customerInfo === null ? noInfoContent :
                                <LoginInfoContent resources={resources} loginInfo={customerInfo.LoginInfo} handleOpenIPInfo={handleOpenIPInfo} />
                        }
                    </CustomizedAccordionItem>

                    <CustomizedAccordionItem
                        title={resources.OnlineList}
                        iconClassname="icon-onlinelist"
                        accordionItemBodyClassName="accordion__body online-list"
                        expanded={expandedItems.onlineList}
                        handleClick={this.handleCollapExpandBlock}
                        blockId="onlineList"
                    >
                        {
                            onlineList === null ? noInfoContent :
                                <OnlineListContent resources={resources} data={onlineList} handleOpenIPInfo={handleOpenIPInfo} />
                        }
                    </CustomizedAccordionItem>
                </Accordion>
            </div>;
    }
}