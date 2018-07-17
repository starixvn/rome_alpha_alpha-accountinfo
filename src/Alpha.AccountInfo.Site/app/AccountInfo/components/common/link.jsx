import React from 'react';

export default class Link extends React.Component{
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <a href={this.props.url}>{this.props.text}</a>
      );
    }
}