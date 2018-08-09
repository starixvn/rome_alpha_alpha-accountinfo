import * as React from 'react';
import ReactDOM from 'react-dom';
import AccountInfo from './containers/account-info';

ReactDOM.render(
    <AccountInfo />,
    document.getElementById('account-info')
);

window.accountInfoResources = JSON.parse(window.accountInfoResourcesString.replace(/&quot;/g, '\"'));