import * as React from 'react';
import ReactDOM from 'react-dom';
import CustomerInfoBlock from '../components/customer-info-block';
import FeatureList from './feature-list';
import Autosuggest from 'react-autosuggest';
import { accountInfoAPI } from '../api-client';
import { base64Helper } from '../../helpers/base64-helper';

const getSuggestions = (value, suggestions) => {
    const inputValue = value.trim().toLowerCase();
    const inputLength = inputValue.length;

    return inputLength === 0 ? [] : suggestions.filter(suggestion =>
        suggestion.UserName.toLowerCase().indexOf(inputValue) != -1
    );
};

const getSuggestionValue = suggestion => suggestion.UserName;

const renderSuggestion = suggestion => (
    <div>
        {suggestion.UserName}
        <div>{suggestion.FirstName} {suggestion.LastName}</div>
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
            suggestions: [],
          resources: base64Helper.decodeResources(this.props.resources)
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
        $.blockUI();
        let self = this;
        accountInfoAPI.getCustomerInfo({ username: username}).then((data) => {
            self.setState((prevState) => ({
                username: username,
                searchCounter: prevState.searchCounter + 1,
                customerInfo: data
            }));

            localStorage.setItem('lastSearchUsername', username);
            self.handleShowContactInfo(e);
            $.unblockUI();
        });
    }

    handleSearchFromUserLevel(e) {
        e.preventDefault();
        const username = e.currentTarget.dataset.username;

        this.setState({ username: username }, this.fetchCustomerInfoFromServer(e, username));
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

    onSuggestionSelected(e, suggestion) {
        this.fetchCustomerInfoFromServer(e, suggestion.suggestionValue);
    }

    componentDidMount() {  
        if (this.state.username != '') {
            ReactDOM.findDOMNode(this.refs.searchAccount).click();
        }
    }

    render() {
        const { showInfoStatus, username, url, customerInfo, suggestions, searchCounter, resources } = this.state;
        const inputProps = {
            type: 'text',
            className: 'txt-search',
            onChange: this.onChangeSearchBox,
            onKeyPress: this.handleSearchKeyPress,
            placeholder: resources.UserNamePlaceHolder,
            value: username
        }

        const customerInfoContent = customerInfo == null ?
            <div className="no-information-search">{resources.NoInfo}</div> :
            <CustomerInfoBlock
                resources={resources}
                data={customerInfo}
                handleSearchFromUserLevel={this.handleSearchFromUserLevel} />;

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
                        handleSearchFromUserLevel={this.handleSearchFromUserLevel}
                        fetchCustomerInfoFromServer={this.fetchCustomerInfoFromServer} />}
            </div>
        );
    }
}