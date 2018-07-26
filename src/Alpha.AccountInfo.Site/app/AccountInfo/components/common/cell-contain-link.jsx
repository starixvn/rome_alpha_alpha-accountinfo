import React from 'react';
import Link from './link';

const CellContainLink = ({ classNames, title, hrefLink = '#', iconClassname = ''}) => 
    <div className={classNames}>
        <Link title={title} url={hrefLink} />
        <span className={iconClassname}></span>
    </div>

export default CellContainLink;