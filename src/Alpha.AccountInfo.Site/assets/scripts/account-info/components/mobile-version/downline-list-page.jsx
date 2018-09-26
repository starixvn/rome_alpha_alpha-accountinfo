import * as React from 'react';
import { NoInformFeature } from '../../components/common/no-inform-feature';
import { HeaderFeature } from '../../components/common/header-feature';
import DownlineListContent from '../contents/downline-list-content';
import { Redirect } from 'react-router-dom';

const DownlineListPage = ({ resources, data, searchAccountRoleId, handleSearchFromUserLevel }) => {
    if (searchAccountRoleId === 1) {
        return <Redirect to='/' />;
    }

    return data === null ?
        <NoInformFeature url="/" iconClass="icon-downline" featureText={resources.DownlineList} noInform={resources.NoInfo} /> :
        <div className="feature-detail with-push-menu">
            <HeaderFeature url="/" iconClass="icon-downline" featureText={resources.DownlineList} />
            <DownlineListContent resources={resources} data={data} handleSearchFromUserLevel={handleSearchFromUserLevel} />
        </div>;
};

export default DownlineListPage;