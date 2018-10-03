import * as React from 'react';
import { NoInformFeature } from './no-inform-feature';
import { HeaderFeature } from './header-feature';
import OnlineListContent from '../contents/online-list-content';

const OnlineListPage = ({ resources, data, handleOpenIPInfo, redirectToBalanceInfoPage }) =>
    data === null ?
        <NoInformFeature url="/featureList" iconClass="icon-onlinelist" featureText={resources.OnlineList} noInform={resources.NoInfo} /> :
        <div className="feature-detail translate-from-left">
            <HeaderFeature url="/featureList" iconClass="icon-onlinelist" featureText={resources.OnlineList} redirectToBalanceInfoPage={redirectToBalanceInfoPage} />
            <div className="online-list">
                <OnlineListContent resources={resources} data={data} handleOpenIPInfo={handleOpenIPInfo} />
            </div>
        </div>;

export default OnlineListPage;
