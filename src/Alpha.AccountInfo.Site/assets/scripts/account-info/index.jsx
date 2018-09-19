import * as React from 'react';
import ReactDOM from 'react-dom';
import AccountInfo from './containers/account-info';
import { base64Helper } from '../helpers/base64-helper';

window.renderAccountInfo = function (accountInfoResources) {
    localStorage.removeItem('lastSearchUsername');
    ReactDOM.render(
        <AccountInfo resources={base64Helper.decodeResources(accountInfoResources)} />,
        document.getElementById('account-info')
    );
};
