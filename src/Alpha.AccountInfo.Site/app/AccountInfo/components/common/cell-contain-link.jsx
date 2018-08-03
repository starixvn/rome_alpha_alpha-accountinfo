import React from 'react';

const CellContainLink = ({ classNames = null, title = '', hrefLink = '#', iconClassname = null, iconTitle = null, handleClick = f => f }) => 
    <div className={classNames}>
        <a href={hrefLink} onClick={handleClick} data-username={title}>{title}</a>
        <span className={iconClassname} title={iconTitle}></span>
    </div>

export default CellContainLink;