import React from 'react';

const Button = ({ title = '', classNames = null, handleClick = f => f }) =>
    <button className={classNames} onClick={handleClick}>
        {title}
    </button>;

export default Button;