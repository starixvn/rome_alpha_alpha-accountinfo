import ReactDOM from 'react-dom';
import AccountInfo from './containers/account-info';

window.renderAccountInfo = function (accountInfoResources) {
    ReactDOM.render(
        <AccountInfo resources={accountInfoResources} />,
        document.getElementById('account-info')
    );
};
