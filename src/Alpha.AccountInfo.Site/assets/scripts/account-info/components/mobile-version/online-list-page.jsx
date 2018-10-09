import * as React from 'react';
import OnlineListContent from '../contents/online-list-content';
import MobilePage from './mobile-page';

const OnlineListPage = ({
    resources,
    data,
    handleOpenIPInfo,
    redirectToBalanceInfoPage }) =>
    <MobilePage iconClass="icon-onlinelist"
        featureText={resources.OnlineList}
        noInfoText={resources.NoInfo}
        redirectToBalanceInfoPage={redirectToBalanceInfoPage}
        data={data}
    >
        <OnlineListContent
            resources={resources}
            data={data}
            handleOpenIPInfo={handleOpenIPInfo}
        />
    </MobilePage>;

export default OnlineListPage;