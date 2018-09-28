import * as React from 'react';
import { NoInformFeature } from './no-inform-feature';
import { HeaderFeature } from './header-feature';
import DownlineListContent from '../contents/downline-list-content';

const DownlineListPage = ({ resources, data, redirectToBalanceInfoPage, handleSearchFromUserLevel }) =>
    data === null ?
        <NoInformFeature url="/featureList" iconClass="icon-downline" featureText={resources.DownlineList} noInform={resources.NoInfo} /> :
        <div className="feature-detail with-push-menu">
            <HeaderFeature url="/featureList" iconClass="icon-downline" featureText={resources.DownlineList} redirectToBalanceInfoPage={redirectToBalanceInfoPage} />
            <DownlineListContent resources={resources} data={data} handleSearchFromUserLevel={handleSearchFromUserLevel} />
        </div>;

export default DownlineListPage;