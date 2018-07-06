import * as React from 'react';

interface IButtonProps {
    classnames: string,
    text: string
}

interface IButtonState {
    text: string
}

export default class Button extends React.Component<IButtonProps, IButtonState> {
    constructor(props) {
        super(props);
        this.state = {
            text: this.props.text || ''
        };
    }
    render() {
        return (
            <button className={this.props.classnames}>{this.state.text}</button>
      );
    }
}