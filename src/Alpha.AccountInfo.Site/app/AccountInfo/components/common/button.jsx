import React from 'react';

export default class Button extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            text: this.props.text || ''
        };

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick() {
        this.props.onClick();
    }


    render() {
        return (
            <button className={this.props.classNames} onClick={this.handleClick}>
                {this.state.text}
            </button>
      );
    }
}