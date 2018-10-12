import { BuildContentItems } from '../contents/build-content-items';

export const BuildContentItemsDesktop = (
    resources,
    mobileView,
    actorRoleId,
    customerInfo,
    downlineList,
    onlineList,
    statusData,
    settingData,
    fetchCustomerInfoFromServer,
    handleSearchFromUserLevel,
    handleOpenIPInfo,
    expandedItems,
    handleCollapExpandBlock) => {

    let loginInfo = customerInfo !== null ? customerInfo.LoginInfo : null;

    const contentItems = BuildContentItems(
        resources,
        mobileView,
        actorRoleId,
        customerInfo,
        downlineList,
        onlineList,
        statusData,
        settingData,
        fetchCustomerInfoFromServer,
        handleSearchFromUserLevel,
        handleOpenIPInfo);

    let contentItemsDesktop = {
        contactInfoProps: {
            ...contentItems.contactInfoProps,
            accordionItemBodyClassName: "contact-info",
            expanded: expandedItems.contactInfo,
            clickHanlder: handleCollapExpandBlock,
            blockId: "contactInfo"
        },
        balanceInfoProps: {
            ...contentItems.balanceInfoProps,
            accordionItemBodyClassName: "balance-info",
            expanded: expandedItems.balanceInfo,
            clickHanlder: handleCollapExpandBlock,
            blockId: "balanceInfo"
        },
        downlineListProps: {
            ...contentItems.downlineListProps,
            accordionItemBodyClassName: "downline-list",
            expanded: expandedItems.downlineList,
            clickHanlder: handleCollapExpandBlock,
            blockId: "downlineList"
        },
        statusProps: {
            ...contentItems.statusProps,
            accordionItemBodyClassName: "status",
            expanded: expandedItems.status,
            clickHanlder: handleCollapExpandBlock,
            blockId: "status"
        },
        settingProps: {
            ...contentItems.settingProps,
            accordionItemBodyClassName: "settings",
            expanded: expandedItems.setting,
            clickHanlder: handleCollapExpandBlock,
            blockId: "setting"
        },
        loginInfoProps: {
            ...contentItems.loginInfoProps,
            accordionItemBodyClassName: "login-info",
            expanded: expandedItems.loginInfo,
            clickHanlder: handleCollapExpandBlock,
            blockId: "loginInfo",
            visible: true
        },
        onlineListProps: {
            ...contentItems.onlineListProps,
            accordionItemBodyClassName: "online-list",
            expanded: expandedItems.onlineList,
            clickHanlder: handleCollapExpandBlock,
            blockId: "onlineList"
        }
    };

    return contentItemsDesktop;
};
