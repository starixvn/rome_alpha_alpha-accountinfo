import React from 'react';

const Cell = ({ classNames = null, title = '' }) =>
    <div className={classNames}>{title}</div>

export default Cell;