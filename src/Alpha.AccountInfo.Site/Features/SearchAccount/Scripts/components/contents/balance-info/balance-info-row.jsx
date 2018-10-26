import * as React from 'react';
import CellContainNumberFormat from '../../common/cell-contain-number-format';

const BalanceInfoRow = ({ title, value, id='', mobileView = false, hrefLink = null }) => {
    const cellFormatProps = {
        id: id,
        value: value,
        mobileView: mobileView,
        classNames: "col col-right",
        hrefLink: hrefLink
    };

    return (
        <div className="row-info">
            <div className="col">{title}</div>
            <CellContainNumberFormat {...cellFormatProps} />
        </div>
    );
};

export default BalanceInfoRow;