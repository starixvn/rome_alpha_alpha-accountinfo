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

// Imagine you have a list of languages that you'd like to autosuggest.
const listUserName = [
    {
        id: 1,
        name: 'ACGUUS38'
    },
    {
        id: 2,
        name: 'ACGUUS3838'
    },
    {
        id: 3,
        name: 'ACGUUS3838033'
    }
];

// Teach Autosuggest how to calculate suggestions for any given input value.
const getSuggestions = value => {
    const inputValue = value.trim().toLowerCase();
    const inputLength = inputValue.length;

    return inputLength === 0 ? [] : listUserName.filter(user =>
        user.name.toLowerCase().slice(0, inputLength) === inputValue
    );
};

// When suggestion is clicked, Autosuggest needs to populate the input
// based on the clicked suggestion. Teach Autosuggest how to calculate the
// input value for every given suggestion.
const getSuggestionValue = suggestion => suggestion.name;

// Use your imagination to render suggestions.
const renderSuggestion = suggestion => (
    <div>
        {suggestion.name}
    </div>
);

export default class AccountInfo extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showInfoStatus: false,
            username: '',
            url: window.accountInfoSiteAppPath + "accountinfo/getcustomerinfo",
            data: null,
            suggestions: []
        };

        this.handleShowContactInfo = this.handleShowContactInfo.bind(this);
        this.handleSearch = this.handleSearch.bind(this);
        this.handleSearchKeyPress = this.handleSearchKeyPress.bind(this);
        this.getCustomerInfo = this.getCustomerInfo.bind(this);        
        this.onSuggestionsFetchRequested = this.onSuggestionsFetchRequested.bind(this);
        this.onSuggestionsClearRequested = this.onSuggestionsClearRequested.bind(this);
        this.handleSearchFromUserLevel = this.handleSearchFromUserLevel.bind(this);
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

    handleSearch(e, input) {
        this.setState({ username: input.newValue });
    };

    handleSearchKeyPress(e) {
        if (e.key === 'Enter') {
            this.getCustomerInfo(e);
        }
    }

    // Autosuggest will call this function every time you need to update suggestions.
    // You already implemented this logic above, so just use it.
    onSuggestionsFetchRequested(input) {
        this.setState({
            suggestions: getSuggestions(input.value)
        });
    }

    // Autosuggest will call this function every time you need to clear suggestions.
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

        this.setState({ username: username }, this.getCustomerInfo);
    }

    render() {
        const { showInfoStatus, username, url, data, suggestions } = this.state;

        const inputProps = {
            type: 'text',
            className: 'txt-search',
            onChange: this.handleSearch,
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
                            getSuggestionValue={getSuggestionValue}
                            renderSuggestion={renderSuggestion}
                            shouldRenderSuggestions={this.shouldRenderSuggestions}
                            inputProps={inputProps}
                        />
                        <span className="icon-search" onClick={this.getCustomerInfo}></span>
                    </div>

                    {data != null && <CustomerInfoBlock data={data} handleSearchFromUserLevel={this.handleSearchFromUserLevel} />}

                </div>
                {data != null && <FeatureList handleShowContactInfo={this.handleShowContactInfo}
                    showInfoStatus={this.showInfoStatus} customerInfo={data}  />}
                       
            </div>
        );
    }
}