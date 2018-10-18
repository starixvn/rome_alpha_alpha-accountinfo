import * as React from 'react';

export const getSuggestions = (value, suggestions) => {
    const values = value.trim().toLowerCase();

    return values.length === 0 ? [] : suggestions.filter(suggestion =>
        suggestion.UserName.toLowerCase().indexOf(values) !== -1
    );
};

export const getSuggestionValue = suggestion => suggestion.UserName;

export const renderSuggestion = suggestion =>
    <div>
        {suggestion.UserName}
        <div>{suggestion.FirstName} {suggestion.LastName}</div>
    </div>;
