import * as React from 'react';
import CellContainNumberFormat from '../../common/cell-contain-number-format';

const BalanceInfoRow = ({ title, value, mobileView = false, hrefLink = null }) => {
    const cellFormatProps = {
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