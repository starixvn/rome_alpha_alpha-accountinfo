import * as React from 'react';
import CellContainNumberFormat from '../../common/cell-contain-number-format';

const BalanceInfoRow = ({
    title,
    value,
    id = '',
    mobileView = false,
    hrefLink = null
}) =>
    <div className="row-info">
        <div className="col">{title}</div>
        <CellContainNumberFormat
            {...{
                id,
                value,
                mobileView,
                hrefLink,
                classNames: "col col-right"
            }}
        />
    </div>;

export default BalanceInfoRow;