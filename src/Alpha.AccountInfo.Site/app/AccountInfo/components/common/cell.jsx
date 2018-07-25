import React from 'react';

const Cell = ({ classNames, title = ''}) =>
    <div className={classNames}>{title}</div>

export default Cell;