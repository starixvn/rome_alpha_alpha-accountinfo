import * as React from 'react';
import ReactDOM from 'react-dom';
import Autosuggest from 'react-autosuggest';
import { getSuggestions, getSuggestionValue, renderSuggestion } from './auto-suggestion';
import { accountInfoAPI } from '../api-client';

export default class SearchingBlock extends React.Component {
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

    componentDidMount() {
        if (this.state.username !== '') {
            ReactDOM.findDOMNode(this.refs.searchAccount).click();
        }
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
        this.props.fetchCustomerInfoFromServer(e, this.state.username);
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
        this.props.fetchCustomerInfoFromServer(e, suggestion.suggestionValue);
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

        return (
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

                <span className="icon-search" onClick={this.handleSearchClick} ref="searchAccount" />
            </div>
        );
    }
}