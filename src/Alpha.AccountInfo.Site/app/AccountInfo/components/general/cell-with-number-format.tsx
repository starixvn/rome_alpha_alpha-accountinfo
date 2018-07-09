import * as React from 'react';
import Link from './link';
import Cell from './cell';

interface ICellWithNumberFormatProps {
    classnames?: string;
    text: number
}

interface ICellWithNumberFormatState {
    text: number
}

export default class CellWithNumberFormat extends React.Component<ICellWithNumberFormatProps, ICellWithNumberFormatState> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className={this.props.classnames}>
                <span className={this.props.text < 0 ? "number-red" : "number"} >{this.props.text.toLocaleString('en-us', { minimumFractionDigits: 2 })}</span>
            </div>
      );
    }
}