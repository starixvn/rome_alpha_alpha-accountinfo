import React from 'react';

const Button = ({ title, classNames, targetIframe, url }) =>
    <button className={classNames}>
        <a href={url} target={targetIframe}>  {title} </a>
    </button>;

export default Button;