import * as React from 'react';
import { NoInformFeature } from '../../components/common/no-inform-feature';
import { HeaderFeature } from '../../components/common/header-feature';
import OnlineListContent from './online-list-content';

const OnlineListBlock = ({ resources, data, handleOpenIPInfo }) => data === null ?
    <NoInformFeature url="/" iconClass="icon-onlinelist" featureText={resources.OnlineList} noInform={resources.NoInfo} /> :
    <div className="feature-detail">
        <HeaderFeature url="/" iconClass="icon-onlinelist" featureText={resources.OnlineList} />
        <OnlineListContent resources={resources} data={data} handleOpenIPInfo={handleOpenIPInfo}/>
    </div>;

export default OnlineListBlock;
