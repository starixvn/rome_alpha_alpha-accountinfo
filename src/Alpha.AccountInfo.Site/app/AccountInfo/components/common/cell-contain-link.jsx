import React from 'react';
import Link from './link';

const CellContainLink = ({ classNames, title, hrefLink = '#', iconClassname = '', handleClick = f => f, isDataSet = false }) => 
    <div className={classNames}>
        <Link title={title} url={hrefLink} handleClick={handleClick} isDataSet={isDataSet} />
        <span className={iconClassname}></span>
    </div>

export default CellContainLink;