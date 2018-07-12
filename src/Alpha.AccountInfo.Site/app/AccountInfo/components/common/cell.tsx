import * as React from 'react';
import Link from './link';

interface ICellProps {
    classNames?: string;
    text: string
}

interface ICellState {
    text: string
}

export default class Cell extends React.Component<ICellProps, ICellState> {
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