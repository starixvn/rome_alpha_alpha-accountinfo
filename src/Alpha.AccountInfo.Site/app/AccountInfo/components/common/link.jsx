import React from 'react';

const Link = ({ classNames = null, title, url = '#', handleClick = f => f }) =>
    <a href={url} onClick={handleClick} className={classNames}>{title}</a>

export default Link;