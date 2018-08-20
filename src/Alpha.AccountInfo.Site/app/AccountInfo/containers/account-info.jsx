import * as React from 'react';
import ReactDOM from 'react-dom';
import CustomerInfoBlock from '../components/customer-info/index';
import BalanceInfoBlock from '../components/balance-info/index';
import DownlineListBlock from '../components/downline-list/index';
import StatusBlock from '../components/status/index';
import SettingBlock from '../components/setting/index';
import LoginInfoBlock from '../components/login-info/index';
import OnlineListBlock from '../components/online-list/index';
import FeatureList from './feature-list';
import Autosuggest from 'react-autosuggest';

import { accountInfoAPI } from '../api-client';

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
        let lastSearchUsername = localStorage.getItem("lastSearchUsername") != null ? localStorage.getItem("lastSearchUsername") : '';

      this.state = {
            showInfoStatus: false,
            searchCounter: 0,
            username: lastSearchUsername,
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
        this.fetchCustomerInfoFromServer = this.fetchCustomerInfoFromServer.bind(this);
    }

    getCustomerInfo(e) {
        this.fetchCustomerInfoFromServer(e, this.state.username);
    }

    fetchCustomerInfoFromServer(e, username) {
        let self = this;
        accountInfoAPI.getCustomerInfo({ username: username}).then((data) => {
            self.setState((prevState) => ({
                username: username,
                searchCounter: prevState.searchCounter + 1,
                customerInfo: data
            }));
            localStorage.setItem('lastSearchUsername', username);
            self.handleShowContactInfo(e);
        });
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
        accountInfoAPI.getAutoUsernameSuggestion({ SuggestionQuery: input.value }).then((data) => {
            this.setState({ suggestions: data });
        });
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

        this.setState({ username: username }, this.fetchCustomerInfoFromServer(e, username));
    }

    onSuggestionSelected(e, suggestion) {
        this.fetchCustomerInfoFromServer(e, suggestion.suggestionValue);
    }

    componentDidMount() {  
        if (this.state.username != '') {
            ReactDOM.findDOMNode(this.refs.searchAccount).click();
        }
    }

    render() {
        const { showInfoStatus, username, url, customerInfo, suggestions, searchCounter } = this.state;
        const { resources } = this.props;

        const inputProps = {
            type: 'text',
            className: 'txt-search',
            onChange: this.onChangeSearchBox,
            onKeyPress: this.handleSearchKeyPress,
            placeholder: resources.UserNamePlaceHolder,
            value: username
        }

        const customerInfoContent = customerInfo == null ?
            <div classNames="no-information-search">{resources.NoInfo}</div> :
            <CustomerInfoBlock resources={resources} data={customerInfo} handleSearchFromUserLevel={this.handleSearchFromUserLevel} />;

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
                        <span className="icon-search" onClick={this.getCustomerInfo} ref="searchAccount"></span>
                    </div>

                    {searchCounter > 0 && customerInfoContent}

                </div>
                {customerInfo != null &&
                    <FeatureList resources={resources}
                        handleShowContactInfo={this.handleShowContactInfo}
                        showInfoStatus={this.showInfoStatus}
                        customerInfo={customerInfo}
                        fetchCustomerInfoFromServer={this.fetchCustomerInfoFromServer} />}
                       
            </div>
        );
    }
}