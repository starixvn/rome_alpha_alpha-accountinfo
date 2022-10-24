import React from 'react';
import CellContainNumberFormat from '../../common/cell-contain-number-format';

const BalanceInfoRow = ({
    title,
    value,
    id = '',
    mobileView = false,
    hrefLink = null
}) => {
    if (hrefLink && hrefLink.indexOf(top.menuBar.domElements.reportDomain) !== -1 && hrefLink.indexOf('(S(') !== -1) {
        hrefLink = hrefLink.replace(top.menuBar.domElements.sessionId, top.menuBar.domElements.reportSessionId);
    }

    return <div className='row-info'>
        <div className='col'>{title}</div>
        <CellContainNumberFormat
            {...{
                id,
                value,
                mobileView,
                hrefLink,
                classNames: 'col col-right'
            }}
        />
    </div>;
}

export default BalanceInfoRow;