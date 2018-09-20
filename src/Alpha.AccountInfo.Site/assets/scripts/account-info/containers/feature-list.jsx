import * as React from 'react';
import { UserLevel } from '../constants/user-level';
import { HeaderFeature } from '../components/common/header-feature';

const FeatureList = ({
    resources,
    actorRoleId,
    searchAccountRoleId,
    getCustomerAllStatus,
    fetchDownlineListFromServer,
    fetchOnlineListFromServer
}) => <div className="feature-list">
        <nav>
            <HeaderFeature url="/contactInfo" iconClass="icon-contact-info" featureText={resources.ShowContactInfo} />
            <HeaderFeature url="/balanceInfo" iconClass="icon-balance" featureText={resources.BalanceInfo} />

            {
                actorRoleId > UserLevel.Agent && searchAccountRoleId > UserLevel.Member &&
                <HeaderFeature url="/downlineList" iconClass="icon-downline" featureText={resources.DownlineList} onClick={fetchDownlineListFromServer} />
                
            }

            <HeaderFeature url="/status" iconClass="icon-status" featureText={resources.Status} onClick={getCustomerAllStatus} />
            <HeaderFeature url="/settings" iconClass="icon-settings" featureText={resources.Setting} onClick={getCustomerAllStatus} />
            <HeaderFeature url="/loginInfo" iconClass="icon-login" featureText={resources.LoginInfo} />
            <HeaderFeature url="/onlineList" iconClass="icon-onlinelist" featureText={resources.OnlineList} onClick={fetchOnlineListFromServer} />
        </nav>
    </div>;

export default FeatureList;