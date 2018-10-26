import * as React from 'react';
import Autosuggest from 'react-autosuggest';
import { accountInfoAPI } from '../../api-client';

const getSuggestionValue = suggestion => suggestion.UserName;

const renderSuggestion = suggestion =>
    <div>
        {suggestion.UserName}
        <div>{suggestion.FirstName} {suggestion.LastName}</div>
    </div>;

export default class Searching extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            username: this.props.username,
            suggestions: []
        };

        this.onChangeSearchBox = this.onChangeSearchBox.bind(this);
        this.handleSearchKeyPress = this.handleSearchKeyPress.bind(this);
        this.handleSearchClick = this.handleSearchClick.bind(this);

        this.onSuggestionsFetchRequested = this.onSuggestionsFetchRequested.bind(this);
        this.onSuggestionsClearRequested = this.onSuggestionsClearRequested.bind(this);
        this.onSuggestionSelected = this.onSuggestionSelected.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.username !== this.props.username) {
            this.setState({
                username: nextProps.username
            });
        }
    }

    onChangeSearchBox(e, input) {
        this.setState({ username: input.newValue });
    }

    handleSearchKeyPress(e) {
        if (e.key === 'Enter') {
            this.handleSearchClick(e);
        }
    }

    handleSearchClick(e) {
        let { username } = this.state;
        if (username !== '') {
            this.props.fetchCustomerInfoFromServer(username);
        }
    }

    onSuggestionsFetchRequested(input) {
        accountInfoAPI
            .getAutoUsernameSuggestion({ SuggestionQuery: input.value })
            .then((data) => {
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
        this.props.fetchCustomerInfoFromServer(suggestion.suggestionValue);
    }

    render() {
        const { username, suggestions } = this.state;
        const inputProps = {
            type: 'text',
            className: 'txt-search',
            onChange: this.onChangeSearchBox,
            onKeyPress: this.handleSearchKeyPress,
            placeholder: this.props.userNamePlaceHolder,
            value: username
        };

        const autosuggestProps = {
            suggestions: suggestions,
            onSuggestionsFetchRequested: this.onSuggestionsFetchRequested,
            onSuggestionsClearRequested: this.onSuggestionsClearRequested,
            onSuggestionSelected: this.onSuggestionSelected,
            getSuggestionValue: getSuggestionValue,
            renderSuggestion: renderSuggestion,
            shouldRenderSuggestions: this.shouldRenderSuggestions,
            inputProps: inputProps
        };

        return (
            <div className="search-box">
                <Autosuggest {...autosuggestProps}/>
                <span className="icon-search" onClick={this.handleSearchClick} />
            </div>
        );
    }
}