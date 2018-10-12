import * as React from 'react';
import { UserLevel } from '../../constants/user-level';
import ContactInfoContent from './contact-info-content';
import BalanceInfoContent from './balance-info-content';
import DownlineListContent from './downline-list-content';
import SettingContent from './setting-content';
import StatusContent from './status-content';
import LoginInfoContent from './login-info-content';
import OnlineListContent from './online-list-content';

export const BuildContentItems = (
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
    handleOpenIPInfo) => {

    let loginInfo = customerInfo !== null ? customerInfo.LoginInfo : null;
    let searchAccountRoleId = customerInfo !== null ? customerInfo.RoleId : actorRoleId - 1;

    const noInfoContent = <div className="no-information">{resources.NoInfo}</div>;
    let buildContent = (data, content) => data === null ? noInfoContent : content;

    let contentItems = {
        contactInfoProps: {
            iconClass: "icon-contact-info",
            featureText: resources.ShowContactInfo,
            data: customerInfo,
            visible: true,
            children: buildContent(customerInfo,
                <ContactInfoContent
                    resources={resources}
                    customerInfo={customerInfo}
                />)
        },
        balanceInfoProps: {
            iconClass: "icon-balance",
            featureText: resources.BalanceInfo,
            data: customerInfo,
            visible: true,
            children: buildContent(customerInfo,
                <BalanceInfoContent
                    resources={resources}
                    mobileView={mobileView}
                    customerInfo={customerInfo}
                    fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                />)
        },
        downlineListProps: {
            iconClass: "icon-downline",
            featureText: resources.DownlineList,
            data: downlineList,
            visible: actorRoleId > UserLevel.Agent && searchAccountRoleId > UserLevel.Member,
            children: buildContent(downlineList,
                < DownlineListContent
                    resources={resources}
                    data={downlineList}
                    mobileView={mobileView}
                    handleSearchFromUserLevel={handleSearchFromUserLevel}
                />)
        },
        statusProps: {
            iconClass: "icon-status",
            featureText: resources.Status,
            data: statusData,
            visible: true,
            children: buildContent(statusData,
                <StatusContent
                    resources={resources}
                    mobileView={mobileView}
                    statusData={statusData}
                    username={customerInfo === null ? '' : customerInfo.UserName}
                    fetchCustomerInfoFromServer={fetchCustomerInfoFromServer}
                />)
        },
        settingProps: {
            iconClass: "icon-settings",
            featureText: resources.Setting,
            data: settingData,
            visible: true,
            children: buildContent(settingData,
                <SettingContent
                    resources={resources}
                    mobileView={mobileView}
                    settingData={settingData}
                />)
        },
        loginInfoProps: {
            iconClass: "icon-login",
            featureText: resources.LoginInfo,
            data: loginInfo,
            visible: true,
            children: buildContent(loginInfo,
                <LoginInfoContent
                    resources={resources}
                    loginInfo={loginInfo}
                    handleOpenIPInfo={handleOpenIPInfo}
                />)
        },
        onlineListProps: {
            iconClass: "icon-onlinelist",
            featureText: resources.OnlineList,
            data: onlineList,
            visible: true,
            children: buildContent(onlineList,
                <OnlineListContent
                    resources={resources}
                    data={onlineList}
                    handleOpenIPInfo={handleOpenIPInfo}
                />)
        }
    };

    return contentItems;
};
