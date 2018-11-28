import React from 'react';
import { DownlineListData } from './down-line-list-sample';
import { Resources } from './resources-sample';
import { Super } from './actor-is-super-sample';
import ContactInfo from 'ContactInfo';
import BalanceInfo from 'BalanceInfo';
import DownlineList from 'DownlineList';
import Status from 'Status';
import Settings from 'Settings';
import LoginInfo from 'LoginInfo';
import OnlineList from 'OnlineList';

export const AccountInfoProps = {
    redirectToBalanceInfoPage: false,
    contentItems: {
        contactInfoProps: {
            iconClass: "icon-contact-info",
            featureText: 'Contact Info',
            path: "/contactInfo",
            visible: true,
            children:
                <ContactInfo
                    resources={Resources}
                    customerInfo={Super.ViewMasterInfo}
                />
        },
        balanceInfoProps: {
            iconClass: "icon-balance",
            featureText: 'Balance Info',
            path: "/balanceInfo",
            visible: true,
            children:
                <BalanceInfo
                    resources={Resources}
                    mobileView={true}
                    customerInfo={Super.ViewMasterInfo}
                    fetchCustomerInfoFromServer={jest.fn()}
                />
        },
        downlineListProps: {
            iconClass: "icon-downline",
            featureText: 'Downline List',
            path: "/downlineList",
            visible: true,
            children:
                <DownlineList
                    resources={Resources}
                    downlineListData={DownlineListData}
                    mobileView={true}
                    searchAccountRoleId={3}
                    fetchCustomerInfoFromServer={jest.fn()}
                />
        },
        statusProps: {
            iconClass: "icon-status",
            featureText: 'Status',
            path: "/status",
            visible: true,
            children:
                <Status
                    resources={Resources}
                    mobileView={true}
                    statusData={[]}
                    username=''
                    fetchCustomerInfoFromServer={jest.fn()}
                />
        },
        settingProps: {
            iconClass: "icon-settings",
            featureText: 'Settings ',
            path: "/setting",
            visible: true,
            children:
                <Settings mobileView={true} settingData={[]} />
        },
        loginInfoProps: {
            iconClass: "icon-login",
            featureText: 'Login Info',
            path: "/loginInfo",
            visible: true,
            children:
                <LoginInfo
                    resources={Resources}
                    loginInfo={Super.ViewMasterInfo.LoginInfo}
                    handleOpenIPInfo={jest.fn()}
                />
        },
        onlineListProps: {
            iconClass: "icon-onlinelist",
            featureText: 'Online List',
            path: "/onlineList",
            visible: true,
            children:
                <OnlineList
                    resources={Resources}
                    onlineListData={[]}
                    handleOpenIPInfo={jest.fn()}
                />
        }
    },
};
