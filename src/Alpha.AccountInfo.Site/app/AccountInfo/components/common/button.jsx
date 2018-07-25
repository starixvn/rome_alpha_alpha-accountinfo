import React from 'react';


const Button = ({ title, classNames, handleClick }) =>
    <button className={classNames} onClick={handleClick}>
        {title}
    </button>;

export default Button;