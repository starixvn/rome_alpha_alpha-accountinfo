import * as React from 'react';

const ContactInfoRow = ({ title, value }) => {
    return (
        <div className="col-xs-12 row-info">
            <div className="col-xs-5">{title}</div>
            <div className="col-xs-7 col-right">{value}</div>
        </div>
    );
}

const ContactInfo = ({ resources, customerInfo }) => {
    let contactInfoPropsArray = [
        {
            title: resources.Name,
            value: customerInfo.FullName
        },
        {
            title: resources.Phone,
            value: customerInfo.Phone
        },
        {
            title: resources.Fax,
            value: customerInfo.Fax
        }
    ];

    return (
        <div>
            {
                contactInfoPropsArray.map((contactInfoProps, index) => {
                    return <ContactInfoRow {...contactInfoProps} key={index} />;
                })
            }
        </div>
    );
}

export default ContactInfo;