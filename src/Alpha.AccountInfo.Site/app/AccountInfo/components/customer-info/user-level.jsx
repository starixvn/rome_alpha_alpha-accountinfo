import React from 'react';
import CellContainLink from '../common/cell-contain-link';

const UserLevel = ({
    rowClasses,
    levelname,
    firstCellClass,
    username,
    secondCellClass,
    hrefLink,
    iconClassname,
    handleSearchFromUserLevel }) =>
    <div className={rowClasses}>
        <div className={firstCellClass}>{levelname}</div>
        <CellContainLink title={username}
            classNames={secondCellClass}
            hrefLink={hrefLink}
            iconClassname={iconClassname}
            iconTitle="View log"
            handleClick={handleSearchFromUserLevel} />
    </div>

export default UserLevel;