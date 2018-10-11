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
            expandedItems: this.initialExpandedItems(this.props.customerInfo !== null),
            collapseAll: this.props.customerInfo === null,
            animation: this.props.mobileView ? "translate-from-left" : ""
        };

        this.handleCollapExpandBlock = this.handleCollapExpandBlock.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.redirectToBalanceInfoPage) {
            this.setState({
                expandedItems: this.initialExpandedItems(nextProps.customerInfo !== null),
                collapseAll: nextProps.customerInfo === null
            });
        }
    }

    initialExpandedItems(hasData) {
        return {
            contactInfo: false,
            balanceInfo: hasData,
            downlineList: false,
            status: false,
            setting: false,
            loginInfo: false,
            onlineList: false
        };
    }

    initialExpandedItemsForMobileWithNoData() {
        return {
            contactInfo: true,
            balanceInfo: true,
            downlineList: true,
            status: true,
            setting: true,
            loginInfo: true,
            onlineList: true
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
        const { mobileView } = this.props;
        const currentStatus = expandedItems[blockId];
        const emptyObject = this.resetColllapseAll(expandedItems);

        const newExpandedItems = Object.assign({}, emptyObject, { [blockId]: !currentStatus });
        const collapseAll = Object.keys(expandedItems).filter(key => newExpandedItems[key] === true).length === 0;

        if (mobileView) {
            this.setState((prevState) => ({
                animation: currentStatus ? "translate-from-left" : "translate-from-right"
            }));
        }

        if (mobileView) {
            this.setState({
                expandedItems: currentStatus ? this.resetColllapseAll(this.state.expandedItems) : newExpandedItems,
                collapseAll
            });
        }
        else {
            this.setState({
                expandedItems: newExpandedItems,
                collapseAll
            });
        }

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
                content: customerInfo === null
                    ? noInfoContent : <ContactInfoContent resources={resources} customerInfo={customerInfo} />,
                title: resources.ShowContactInfo,
                iconClassname: "icon-contact-info",
                accordionItemBodyClassName: "contact-info",
                expanded: expandedItems.contactInfo,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "contactInfo",
                visibled: mobileView ? collapseAll || expandedItems.contactInfo : true
            },
            {
                content: customerInfo === null
                    ? noInfoContent : <BalanceInfoContent
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
                visibled: mobileView ? collapseAll || expandedItems.balanceInfo : true
            },
            {
                content: downlineList === null
                    ? noInfoContent
                    : <DownlineListContent
                        resources={resources}
                        data={downlineList}
                        handleSearchFromUserLevel={handleSearchFromUserLevel}
                    />,
                title: resources.DownlineList,
                iconClassname: "icon-downline",
                accordionItemBodyClassName: "downline-list",
                expanded: expandedItems.downlineList,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "downlineList",
                visibled: customerInfo !== null && customerInfo.RoleId > UserLevel.Member && (mobileView ? collapseAll || expandedItems.downlineList : true)
            },
            {
                content: statusData === null
                    ? noInfoContent
                    : <StatusContent
                        resources={resources}
                        mobileView={mobileView}
                        statusData={statusData}
                        username={customerInfo === null ? '' : customerInfo.UserName}
                        fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                    />,
                title: resources.Status,
                iconClassname: "icon-status",
                accordionItemBodyClassName: "status",
                expanded: expandedItems.status,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "status",
                visibled: mobileView ? collapseAll || expandedItems.status : true
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
                visibled: mobileView ? collapseAll || expandedItems.setting : true
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
                visibled: mobileView ? collapseAll || expandedItems.loginInfo : true
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
                visibled: mobileView ? collapseAll || expandedItems.onlineList : true
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