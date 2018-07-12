import * as React from 'react';
import Link from './link';
import Cell from './cell';

interface ICellContainNumberFormatProps {
    classNames?: string;
    text: number
}

interface ICellContainNumberFormatState {
    text: number
}

export default class CellContainNumberFormat extends React.Component<ICellContainNumberFormatProps, ICellContainNumberFormatState> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className={this.props.classNames}>
                <span className={this.props.text < 0 ? "number-red" : "number"} >{this.props.text.toLocaleString('en-us', { minimumFractionDigits: 2 })}</span>
            </div>
      );
    }
}