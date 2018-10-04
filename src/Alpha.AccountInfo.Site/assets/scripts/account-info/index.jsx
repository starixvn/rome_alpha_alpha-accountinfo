import * as React from 'react';
import ReactDOM from 'react-dom';
import AccountInfo from './containers/account-info';
import { base64Helper } from '../helpers/accountinfo-helper';

window.renderAccountInfo = function (searchAccountName, actorRoleId, actorName, mobileView, accountInfoResources) {
    ReactDOM.render(
        <AccountInfo
            searchAccountName={searchAccountName}
            actorRoleId={actorRoleId}
            actorName={actorName}
            mobileView={mobileView}
            resources={base64Helper.decodeResources(accountInfoResources)}
        />,
        document.getElementById('account-info')
    );
};
