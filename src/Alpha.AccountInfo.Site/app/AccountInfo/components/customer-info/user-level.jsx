import React from 'react';
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
            iconClassname,
            handleSearchFromUserLevel
        } = this.props;

        return (
            <div className={rowClasses}>
                <div className={firstCellClass}>{levelname}</div>
                <CellContainLink title={username}
                    classNames={secondCellClass}
                    hrefLink={hrefLink}
                    iconClassname={iconClassname}
                    iconTitle="View log"
                    handleClick={handleSearchFromUserLevel} />
            </div>
        );
    }
}