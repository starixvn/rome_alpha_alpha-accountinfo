import * as React from 'react';
import CustomerInfoBlock from '../components/customer-info/index';
import BalanceInfoBlock from '../components/balance-info/index';
import DownlineListBlock from '../components/downline-list/index';
import StatusBlock from '../components/status/index';
import SettingBlock from '../components/setting/index';
import LoginInfoBlock from '../components/login-info/index';
import OnlineListBlock from '../components/online-list/index';
import FeatureList from './feature-list';
import Autosuggest from 'react-autosuggest';

const getSuggestions = (value, usernames) => {
    const inputValue = value.trim().toLowerCase();
    const inputLength = inputValue.length;

    return inputLength === 0 ? [] : usernames.filter(user =>
        user.UserName.toLowerCase().indexOf(inputValue) != -1
    );
};

const getSuggestionValue = suggestion => suggestion.UserName;

const renderSuggestion = suggestion => (
    <div>
        {suggestion.UserName}
    </div>
);

export default class AccountInfo extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showInfoStatus: false,
            username: '',
            url: window.accountInfoSiteAppPath + "accountinfo/getcustomerinfo",
            customerInfo: null,
            suggestions: []
        };

        this.handleShowContactInfo = this.handleShowContactInfo.bind(this);
        this.onChangeSearchBox = this.onChangeSearchBox.bind(this);
        this.handleSearchKeyPress = this.handleSearchKeyPress.bind(this);
        this.getCustomerInfo = this.getCustomerInfo.bind(this);        
        this.onSuggestionsFetchRequested = this.onSuggestionsFetchRequested.bind(this);
        this.onSuggestionsClearRequested = this.onSuggestionsClearRequested.bind(this);
        this.onSuggestionSelected = this.onSuggestionSelected.bind(this);
        this.handleSearchFromUserLevel = this.handleSearchFromUserLevel.bind(this);
    }

    getCustomerInfo(e) {
        this.fetchCustomerInfoFromServer(this.state.username);
    }

    fetchCustomerInfoFromServer(username) {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.state.url + "?username=" + username);
        xhr.onload = () => {
            this.setState({ customerInfo: JSON.parse(xhr.responseText).Result });

            this.handleShowContactInfo(e);
        };
        xhr.send();

    }

    handleShowContactInfo() {
        this.setState((prevStatus) => { showInfoStatus: !prevStatus })
    }

    onChangeSearchBox(e, input) {
        this.setState({ username: input.newValue });
    };

    handleSearchKeyPress(e) {
        if (e.key === 'Enter') {
            this.getCustomerInfo(e);
        }
    }

    onSuggestionsFetchRequested(input) {
        const xhr = new XMLHttpRequest();
        xhr.open('get', window.accountInfoSiteAppPath + "accountinfo/GetUsernamesSuggestion" + "?SuggestionQuery=" + input.value);
        xhr.onload = () => {
            this.setState({
                suggestions: getSuggestions(input.value, JSON.parse(xhr.responseText).Result)
            });
        };
        xhr.send();
    }

    onSuggestionsClearRequested() {
        this.setState({
            suggestions: []
        });
    }

    shouldRenderSuggestions(value) {
        return value.trim().length > 1;
    }

    handleSearchFromUserLevel(e) {
        e.preventDefault();
        const username = e.currentTarget.dataset.username;

        this.setState({ username: username }, this.fetchCustomerInfoFromServer(username));
    }

    onSuggestionSelected(e, suggestion) {
        this.fetchCustomerInfoFromServer(suggestion.suggestionValue);
    }

    componentDidMount() {
    }

    render() {
        const { showInfoStatus, username, url, customerInfo, suggestions } = this.state;

        const inputProps = {
            type: 'text',
            className: 'txt-search',
            onChange: this.onChangeSearchBox,
            onKeyPress: this.handleSearchKeyPress,
            placeholder: 'Username',
            value: username
        }

        return (
            <div className="accountinfo-container">
                <div className="block-info" id="blockInfo">
                    <div className="search-box">
                        <Autosuggest
                            suggestions={suggestions}
                            onSuggestionsFetchRequested={this.onSuggestionsFetchRequested}
                            onSuggestionsClearRequested={this.onSuggestionsClearRequested}
                            onSuggestionSelected={this.onSuggestionSelected}
                            getSuggestionValue={getSuggestionValue}
                            renderSuggestion={renderSuggestion}
                            shouldRenderSuggestions={this.shouldRenderSuggestions}
                            inputProps={inputProps}
                        />
                        <span className="icon-search" onClick={this.getCustomerInfo}></span>
                    </div>

                    {customerInfo != null && <CustomerInfoBlock data={customerInfo} handleSearchFromUserLevel={this.handleSearchFromUserLevel} />}

                </div>
                {customerInfo != null && <FeatureList handleShowContactInfo={this.handleShowContactInfo}
                    showInfoStatus={this.showInfoStatus} customerInfo={customerInfo}  />}
                       
            </div>
        );
    }
}