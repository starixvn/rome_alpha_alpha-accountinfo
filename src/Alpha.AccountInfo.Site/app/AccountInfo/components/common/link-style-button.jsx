import React from 'react';

const LinkStyleButton = ({ title = '', classNames = null, targetIframe = null, url = '#' }) =>
    <a className={classNames} href={url} target={targetIframe}>  {title} </a>

export default LinkStyleButton;