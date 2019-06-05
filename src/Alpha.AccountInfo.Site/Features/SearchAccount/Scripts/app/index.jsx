import React from 'react';
import ReactDOM from 'react-dom';
import AccountInfo from './containers/account-info';
import { decodeResources } from './helpers/accountinfo-helper';

window.renderAccountInfo = function (
    searchAccountName,
    actorRoleId,
    actorName,
    mobileView,
    showLoginInfo,
    accountInfoResources) {
    ReactDOM.render(
        <AccountInfo
            searchAccountName={searchAccountName}
            actorRoleId={actorRoleId}
            actorName={actorName}
            mobileView={mobileView}
            showLoginInfo={showLoginInfo}
            resources={decodeResources(accountInfoResources)}
        />,
        document.getElementById('account-info')
    );
};