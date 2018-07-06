/// <reference path="containers/account-info.tsx" />
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import AccountInfo from './containers/account-info';

const renderApp = () => {
    ReactDOM.render(
        <AccountInfo />,
        document.getElementById('account-info')
    );
};

renderApp();