import * as React from 'react';
import Link from './link';
import Cell from './cell';

const CellContainNumberFormat = ({ classNames, title }) =>
    <div className={classNames}>
        <span className={title < 0 ? "number-red" : "number"} >
            {title.toLocaleString('en-us', { minimumFractionDigits: 2 })}
        </span>
    </div>

export default CellContainNumberFormat;