import React from 'react';

import CellContainNumberFormat from '../common/cell-contain-number-format';
import Cell from '../common/cell';

export default class Cast extends React.Component{
    constructor(props) {
        super(props);
    }

    render() {
        const rowClasses = "col-xs-12 row-info";

        return (
            <div>
                <div className={rowClasses}>
                    <Cell title="Total Cash" classNames="col-xs-6" />
                    <CellContainNumberFormat title={this.props.TotalCash} classNames="col-xs-6 col-right" />
                </div>
                <div className={rowClasses}>
                    <Cell title="Yesterday Cash" classNames="col-xs-6" />
                    <CellContainNumberFormat title={this.props.YesterdayCash} classNames="col-xs-6 col-right" />
                </div>
            </div>
      );
    }
}