import * as React from 'react';
import CustomerInfoBlock from '../components/customer-info/index';
import BalanceInfoBlock from '../components/balance-info/index';
import DownlineListBlock from '../components/downline-list/index';
import StatusBlock from '../components/status/index';
import SettingBlock from '../components/setting/index';
import LoginInfoBlock from '../components/login-info/index';
import OnlineListBlock from '../components/online-list/index';
import FeatureList from './feature-list';

export default class AccountInfo extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showInfoStatus: false,
            username: '',
            url: window.accountInfoSiteAppPath + "accountinfo/getcustomerinfo",
            data: null
        };

        this.handleShowContactInfo = this.handleShowContactInfo.bind(this);
        this.handleSearch = this.handleSearch.bind(this);
        this.getCustomerInfo = this.getCustomerInfo.bind(this);
        this.handleKeyPress = this.handleKeyPress.bind(this);
    }

    handleSearch(e) {
        this.setState({ username: e.target.value });
        // TO DO suggestion
    }

    handleKeyPress(e) {
        if (e.key === 'Enter') {
            this.getCustomerInfo(e);
        }
    }

    getCustomerInfo(e) {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.state.url + "?username=" + this.state.username);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });

            this.handleShowContactInfo(e);
        };
        xhr.send();
    }

    handleShowContactInfo() {
        this.setState((prevStatus) => { showInfoStatus: !prevStatus })
    }

    render() {
        const inputProps = {
            type: 'text',
            className: 'txt-search',
            onChange: this.handleSearch,
            onKeyPress: this.handleKeyPress,
            placeholder: 'Username',
            value: this.state.username
        }

        return (
            <div className="accountinfo-container">
                <div className="block-info" id="blockInfo">
                    <div className="search-box">
                        <input {...inputProps} />
                        <span className="icon-search" onClick={this.getCustomerInfo}></span>
                    </div>

                    {this.state.data != null && <CustomerInfoBlock data={this.state.data} />}

                </div>
                {this.state.data != null && <FeatureList handleShowContactInfo={this.handleShowContactInfo}
                    showInfoStatus={this.showInfoStatus} customerInfo={this.state.data}  />}
                       
            </div>
        );
    }
}