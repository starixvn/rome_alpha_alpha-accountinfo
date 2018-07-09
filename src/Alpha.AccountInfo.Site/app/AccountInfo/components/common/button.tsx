import * as React from 'react';

interface IButtonProps {
    classnames: string,
    text: string,
    onClick(): void
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

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick() {
        this.props.onClick();
    }


    render() {
        return (
            <button className={this.props.classnames} onClick={this.handleClick}>
                {this.state.text}
            </button>
      );
    }
}