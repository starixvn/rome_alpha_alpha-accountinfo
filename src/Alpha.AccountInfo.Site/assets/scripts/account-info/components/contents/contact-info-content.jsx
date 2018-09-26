import * as React from 'react';

const ContactInfoContent = ({
    customerInfo,
    resources,
    rowClasses = "col-xs-12 row-info",
    firstCellClass = "col-xs-5",
    secondCellClass = "col-xs-7 col-right" }) => <div>
        <div className={rowClasses}>
            <div className={firstCellClass}>{resources.Name}</div>
            <div className={secondCellClass}>{customerInfo.FullName}</div>
        </div>

        <div className={rowClasses}>
            <div className={firstCellClass}>{resources.Phone}</div>
            <div className={secondCellClass}>{customerInfo.Phone}</div>
        </div>

        <div className={rowClasses}>
            <div className={firstCellClass}>{resources.Fax}</div>
            <div className={secondCellClass}>{customerInfo.Fax}</div>
        </div>
    </div>;

export default ContactInfoContent;