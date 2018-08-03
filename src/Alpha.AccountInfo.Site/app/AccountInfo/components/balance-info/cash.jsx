import React from 'react';
import CellContainNumberFormat from '../common/cell-contain-number-format';

export default class Cast extends React.Component{
    constructor(props) {
        super(props);
    }

    render() {
        const rowClasses = "col-xs-12 row-info";

        return (
            <div>
                <div className={rowClasses}>
                    <div className="col-xs-6">Total Cash</div>
                    <CellContainNumberFormat title={this.props.TotalCash} classNames="col-xs-6 col-right" />
                </div>
                <div className={rowClasses}>
                    <div className="col-xs-6">Yesterday Cash</div>
                    <CellContainNumberFormat title={this.props.YesterdayCash} classNames="col-xs-6 col-right" />
                </div>
            </div>
      );
    }
}