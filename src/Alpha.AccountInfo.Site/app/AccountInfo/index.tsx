/// <reference path="containers/account-info.tsx" />
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import AccountInfo from './containers/account-info';
declare var window: any;

interface Window {
    reactApp: any;
}

const renderApp = (reactApp: any) => {
    ReactDOM.render(
        <AccountInfo accountInfoModel={reactApp} />,
        document.getElementById('account-info')
    );
};

renderApp(window.reactApp);