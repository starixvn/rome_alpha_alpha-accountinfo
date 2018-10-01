import * as React from 'react';
import { UserLevel } from '../../constants/user-level';
import { HeaderFeature } from './header-feature';

const FeatureListPage = ({ resources, customerInfo, actorRoleId, redirectToBalanceInfoPage }) => {
    let searchAccountRoleId = actorRoleId - 1, headerCss = '';
    if (customerInfo !== null) {
        searchAccountRoleId = customerInfo.RoleId;
        headerCss = 'translate-from-left';
    }

    return (
        <div className={`${headerCss} feature-list`}>
            <nav>
                <HeaderFeature url="/contactInfo" iconClass="icon-contact-info" featureText={resources.ShowContactInfo} />
                <HeaderFeature url="/balanceInfo" iconClass="icon-balance" featureText={resources.BalanceInfo} />
                {
                    actorRoleId > UserLevel.Agent && searchAccountRoleId > UserLevel.Member &&
                    <HeaderFeature url="/downlineList" iconClass="icon-downline" featureText={resources.DownlineList} />

                }
                <HeaderFeature url="/status" iconClass="icon-status" featureText={resources.Status} />
                <HeaderFeature url="/settings" iconClass="icon-settings" featureText={resources.Setting} />
                <HeaderFeature url="/loginInfo" iconClass="icon-login" featureText={resources.LoginInfo} />
                <HeaderFeature url="/onlineList" iconClass="icon-onlinelist" featureText={resources.OnlineList} />
            </nav>
        </div>
    );
}

export default FeatureListPage;