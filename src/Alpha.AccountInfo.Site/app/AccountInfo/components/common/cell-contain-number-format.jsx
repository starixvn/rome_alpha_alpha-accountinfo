import * as React from 'react';
import Link from './link';
import Cell from './cell';

export default class CellContainNumberFormat extends React.Component{
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