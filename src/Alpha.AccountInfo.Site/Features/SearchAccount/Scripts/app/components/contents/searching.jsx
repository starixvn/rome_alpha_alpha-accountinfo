import * as React from 'react';
import Autosuggest from 'react-autosuggest';
import { accountInfoAPI } from '../../accountinfo-api';
import { zoomEnable, zoomDisable } from '../../helpers/accountinfo-helper';
import $ from 'jquery';

export const getSuggestionValue = suggestion => suggestion.UserName;

export const renderSuggestion = suggestion =>
    <div id={`${suggestion.UserName}-suggestion`}>
        {suggestion.UserName} {suggestion.UserName2 !== null && suggestion.UserName2 !== '' && `(${suggestion.UserName2})`}
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

	hideKeyboard() {
		$('.txt-search').blur();
	}

    handleSearchClick(e) {
        let { username } = this.state;
        if (username !== '') {
			this.props.fetchCustomerInfoFromServer(username);
			this.onSuggestionsClearRequested();
			this.hideKeyboard();
        }
    }

    onSuggestionsFetchRequested(input) {
		accountInfoAPI
			.getAutoUsernameSuggestion({ SuggestionQuery: input.value.trim() })
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
			type: 'search',
            className: 'txt-search',
            onChange: this.onChangeSearchBox,
            onKeyPress: this.handleSearchKeyPress,
			placeholder: this.props.userNamePlaceHolder,
			onFocus: zoomDisable,
			onBlur: zoomEnable,
            value: username
        };

        const onSuggestionsFetchRequested = this.onSuggestionsFetchRequested,
            onSuggestionsClearRequested = this.onSuggestionsClearRequested,
            onSuggestionSelected = this.onSuggestionSelected,
            shouldRenderSuggestions = this.shouldRenderSuggestions;

        return (
            <div id="search-box" className="search-box">
                <Autosuggest
                    {...{
                        suggestions,
                        onSuggestionsFetchRequested,
                        onSuggestionsClearRequested,
                        onSuggestionSelected,
                        getSuggestionValue,
                        renderSuggestion,
                        shouldRenderSuggestions,
						inputProps,
						focusInputOnSuggestionClick: false
                    }}
                />
                <span className="icon-search" onClick={this.handleSearchClick} />
            </div>
        );
    }
}