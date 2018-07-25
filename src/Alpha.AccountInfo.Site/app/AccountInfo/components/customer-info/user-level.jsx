import React from 'react';
import Cell from '../common/cell';
import CellContainLink from '../common/cell-contain-link';

export default class UserLevel extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const {
            rowClasses,
            levelname,
            firstCellClass,
            username,
            secondCellClass,
            hrefLink,
            iconClassname
        } = this.props;

        return (
            <div className={rowClasses}>
                <Cell title={levelname} classNames={firstCellClass} />
                <CellContainLink title={username}
                    classNames={secondCellClass}
                    hrefLink={hrefLink}
                    iconClassname={iconClassname} />
            </div>
        );
    }
}