import * as React from 'react';
import { NoInformFeature } from '../../components/common/no-inform-feature';
import { HeaderFeature } from '../../components/common/header-feature';
import DownlineListContent from '../contents/downline-list-content';
import { Redirect } from 'react-router-dom';

const DownlineListPage = ({ resources, data, redirectToBalanceInfoPage, handleSearchFromUserLevel }) => {
    if (redirectToBalanceInfoPage) {
        return <Redirect to='/balanceInfo' />;
    }

    return data === null ?
        <NoInformFeature url="/featureList" iconClass="icon-downline" featureText={resources.DownlineList} noInform={resources.NoInfo} /> :
        <div className="feature-detail with-push-menu">
            <HeaderFeature url="/featureList" iconClass="icon-downline" featureText={resources.DownlineList} />
            <DownlineListContent resources={resources} data={data} handleSearchFromUserLevel={handleSearchFromUserLevel} />
        </div>;
};

export default DownlineListPage;