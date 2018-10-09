import * as React from 'react';
import DownlineListContent from '../contents/downline-list-content';
import MobilePage from './mobile-page';

const DownlineListPage = ({
    resources,
    data,
    redirectToBalanceInfoPage,
    handleSearchFromUserLevel }) =>
    <MobilePage iconClass="icon-downline"
        featureText={resources.DownlineList}
        noInfoText={resources.NoInfo}
        redirectToBalanceInfoPage={redirectToBalanceInfoPage}
        data={data}
    >
        <DownlineListContent
            resources={resources}
            data={data}
            handleSearchFromUserLevel={handleSearchFromUserLevel}
        />
    </MobilePage>;

export default DownlineListPage;