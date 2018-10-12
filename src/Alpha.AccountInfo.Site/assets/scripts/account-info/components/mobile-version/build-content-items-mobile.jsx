import { BuildContentItems } from '../contents/build-content-items';

export const BuildContentItemsMobile = (
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
    redirectToBalanceInfoPage,
    redirectToFeatureList) => {

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

    let contentItemsMobile = {
        featureListProps: {
            url: "/featureList",
            resources: resources,
            actorRoleId: actorRoleId,
            redirectToBalanceInfoPage: redirectToBalanceInfoPage
        },
        contactInfoProps: {
            ...contentItems.contactInfoProps,
            url: "/contactInfo",
            redirectToBalanceInfoPage: redirectToBalanceInfoPage,
            redirectToFeatureList: redirectToFeatureList
        },
        balanceInfoProps: {
            ...contentItems.balanceInfoProps,
            url: "/balanceInfo",
            redirectToFeatureList: redirectToFeatureList
        },
        downlineListProps: {
            ...contentItems.downlineListProps,
            url: "/downlineList",
            redirectToBalanceInfoPage: redirectToBalanceInfoPage,
            redirectToFeatureList: redirectToFeatureList
        },
        statusProps: {
            ...contentItems.statusProps,
            url: "/status",
            redirectToBalanceInfoPage: redirectToBalanceInfoPage,
            redirectToFeatureList: redirectToFeatureList
        },
        settingProps: {
            ...contentItems.settingProps,
            url: "/settings",
            redirectToBalanceInfoPage: redirectToBalanceInfoPage,
            redirectToFeatureList: redirectToFeatureList
        },
        loginInfoProps: {
            ...contentItems.loginInfoProps,
            url: "/loginInfo",
            redirectToBalanceInfoPage: redirectToBalanceInfoPage,
            redirectToFeatureList: redirectToFeatureList
        },
        onlineListProps: {
            ...contentItems.onlineListProps,
            url: "/onlineList",
            redirectToBalanceInfoPage: redirectToBalanceInfoPage,
            redirectToFeatureList: redirectToFeatureList
        }
    };

    return contentItemsMobile;
};
