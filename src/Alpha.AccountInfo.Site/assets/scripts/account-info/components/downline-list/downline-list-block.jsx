import * as React from 'react';
import { NoInformFeature } from '../../components/common/no-inform-feature';
import { HeaderFeature } from '../../components/common/header-feature';
import DownlineListContent from './downline-list-content';

const DownlineListBlock = ({ resources, data, searchAccountRoleId, handleSearchFromUserLevel }) => data === null ?
    <NoInformFeature url="/" iconClass="icon-downline" featureText={resources.DownlineList} noInform={resources.NoInfo} /> :
    <div className="feature-detail">
        <HeaderFeature url="/" iconClass="icon-downline" featureText={resources.DownlineList} />
        <DownlineListContent resources={resources} data={data} searchAccountRoleId={searchAccountRoleId} handleSearchFromUserLevel={handleSearchFromUserLevel} />
    </div>;

export default DownlineListBlock;