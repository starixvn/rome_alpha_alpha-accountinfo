import * as React from 'react';
import { Link } from 'react-router-dom';

const ContactInfoBlock = ({
    data,
    resources,
    rowClasses = "col-xs-12 row-info",
    firstCellClass = "col-xs-5",
    secondCellClass = "col-xs-7 col-right" }) => <div>
        <Link to="/" className="feature-item">
            <span className="icon-contact-info" />
            {resources.ShowContactInfo}
        </Link>

        <div className={rowClasses}>
            <div className={firstCellClass}>{resources.Name}</div>
            <div className={secondCellClass}>{data.FullName}</div>
        </div>

        <div className={rowClasses}>
            <div className={firstCellClass}>{resources.Phone}</div>
            <div className={secondCellClass}>{data.Phone}</div>
        </div>

        <div className={rowClasses}>
            <div className={firstCellClass}>{resources.Fax}</div>
            <div className={secondCellClass}>{data.Fax}</div>
        </div>
    </div>;

export default ContactInfoBlock;