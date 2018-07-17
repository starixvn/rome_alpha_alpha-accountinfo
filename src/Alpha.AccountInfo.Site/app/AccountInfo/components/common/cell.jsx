import React from 'react';

export default class Cell extends React.Component{
    constructor(props) {
        super(props);
        this.state = {
            text: this.props.text || ''
        };
    }
    render() {
        return (
            <div className={this.props.classNames}>{this.state.text}</div>
      );
    }
}