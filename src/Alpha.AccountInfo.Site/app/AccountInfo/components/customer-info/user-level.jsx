import React from 'react';
import Cell from '../common/cell';
import CellContainLink from '../common/cell-contain-link';

export default class UserLevel extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className={this.props.rowClasses}>
                <Cell text={this.props.levelname} classNames={this.props.firstCellClass} />
                <CellContainLink text={this.props.username}
                    classNames={this.props.secondCellClass}
                    hrefLink={this.props.hrefLink}
                    iconClassname={this.props.iconClassname} />
            </div>
        );
    }
}