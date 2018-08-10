import * as React from 'react';
import ReactDOM from 'react-dom';
import AccountInfo from './containers/account-info';

ReactDOM.render(
    <AccountInfo resources={accountInfoResources}/>,
    document.getElementById('account-info')
);