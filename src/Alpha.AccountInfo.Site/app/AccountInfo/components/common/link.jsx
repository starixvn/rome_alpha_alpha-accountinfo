import React from 'react';

const Link = ({ classNames = null, title = '', url = '#', handleClick = f => f, isDataSet = false }) => {
    let linkContent = <a href={url} onClick={handleClick} className={classNames}>{title}</a>;

    if (isDataSet) {
        linkContent = <a href={url} onClick={handleClick} className={classNames} data-username={title}>{title}</a>;
    }

    return linkContent;
};

export default Link;