import * as React from 'react';
import ReactDOM from 'react-dom';
import AccountInfo from './containers/account-info';

ReactDOM.render(
    <AccountInfo accountInfoModel={window.reactApp} />,
    document.getElementById('account-info')
);