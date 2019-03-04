import * as React from 'react';
import ReactDOM from 'react-dom';
import AccountInfo from './containers/account-info';

window.renderAccountInfo = function (searchAccountName, actorRoleId, actorName, mobileView, showLoginInfo, accountInfoResources) {
    ReactDOM.render(
        <AccountInfo
            searchAccountName={searchAccountName}
            actorRoleId={actorRoleId}
            actorName={actorName}
			mobileView={mobileView}
			showLoginInfo={showLoginInfo}
            resources={accountInfoResources}
        />,
        document.getElementById('account-info')
    );
};