import * as React from 'react';
import { UserLevel } from '../../constants/user-level';
import CustomizedAccordionItem from './customized-accordion-item';
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
            collapseAll: false
        };

        this.handleCollapExpandBlock = this.handleCollapExpandBlock.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.redirectToBalanceInfoPage) {
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

    buildAccordionItems() {
        const { expandedItems, collapseAll } = this.state;

        const { resources, mobileView, actorRoleId, customerInfo, downlineList, onlineList, statusData, settingData,
            redirectToBalanceInfoPage, fetchCustomerInfoFromServer, handleSearchFromUserLevel, handleOpenIPInfo } = this.props;

        const noInfoContent = <div className="no-information">{resources.NoInfo}</div>;

        let items = [
            {
                content: <ContactInfoContent
                    resources={resources}
                    customerInfo={customerInfo}
                />,
                title: resources.ShowContactInfo,
                iconClassname: "icon-contact-info",
                accordionItemBodyClassName: "contact-info",
                expanded: expandedItems.contactInfo,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "contactInfo",
                visibled: true
            },
            {
                content: <BalanceInfoContent
                    resources={resources}
                    mobileView={mobileView}
                    customerInfo={customerInfo}
                    fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                />,
                title: resources.BalanceInfo,
                iconClassname: "icon-balance",
                accordionItemBodyClassName: "balance-info",
                expanded: expandedItems.balanceInfo,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "balanceInfo",
                visibled: true
            },
            {
                content: downlineList === null
                    ? noInfoContent
                    : <DownlineListContent
                        resources={resources}
                        data={downlineList}
                        mobileView={mobileView}
                        handleSearchFromUserLevel={handleSearchFromUserLevel}
                    />,
                title: resources.DownlineList,
                iconClassname: "icon-downline",
                accordionItemBodyClassName: "downline-list",
                expanded: expandedItems.downlineList,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "downlineList",
                visibled: customerInfo !== null && customerInfo.RoleId > UserLevel.Member
            },
            {
                content: statusData === null
                    ? noInfoContent
                    : <StatusContent
                        resources={resources}
                        mobileView={mobileView}
                        statusData={statusData}
                        username={customerInfo.UserName}
                        fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                    />,
                title: resources.Status,
                iconClassname: "icon-status",
                accordionItemBodyClassName: "status",
                expanded: expandedItems.status,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "status",
                visibled: true
            },
            {
                content: settingData === null
                    ? noInfoContent
                    : <SettingContent
                        resources={resources}
                        mobileView={mobileView}
                        settingData={settingData}
                    />,
                title: resources.Setting,
                iconClassname: "icon-settings",
                accordionItemBodyClassName: "settings",
                expanded: expandedItems.setting,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "setting",
                visibled: true
            },
            {
                content: customerInfo === null
                    ? noInfoContent
                    : <LoginInfoContent
                        resources={resources}
                        loginInfo={customerInfo.LoginInfo}
                        handleOpenIPInfo={handleOpenIPInfo}
                    />,
                title: resources.LoginInfo,
                iconClassname: "icon-login",
                accordionItemBodyClassName: "login-info",
                expanded: expandedItems.loginInfo,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "loginInfo",
                visibled: true
            },
            {
                content: onlineList === null
                    ? noInfoContent
                    : <OnlineListContent
                        resources={resources}
                        data={onlineList}
                        handleOpenIPInfo={handleOpenIPInfo}
                    />,
                title: resources.OnlineList,
                iconClassname: "icon-onlinelist",
                accordionItemBodyClassName: "online-list",
                expanded: expandedItems.onlineList,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "onlineList",
                visibled: true
            }
        ];

        return items;
    }

    render() {
        let accordionItems = this.buildAccordionItems();

        return <div className={`${this.state.animation} accordions`}>
            {
                accordionItems.map((item, index) => {
                    return item.visibled &&
                        <CustomizedAccordionItem
                            title={item.title}
                            iconClassname={item.iconClassname}
                            accordionItemBodyClassName={item.accordionItemBodyClassName}
                            expanded={item.expanded}
                            handleClick={item.clickHanlder}
                            blockId={item.blockId}
                            key={index}
                        >
                            {item.content}
                        </CustomizedAccordionItem>;
                })}
        </div>;
    }
}