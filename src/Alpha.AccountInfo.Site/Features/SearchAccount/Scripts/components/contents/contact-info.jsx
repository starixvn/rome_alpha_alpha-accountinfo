import * as React from 'react';

export const ContactInfoRow = ({ title, value }) => {
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
            title: resources.Nickname,
            value: customerInfo.NickName,
            visible: customerInfo.ShowNickName
        },
        {
            title: resources.Name,
            value: customerInfo.FullName,
            visible: true
        },
        {
            title: resources.Phone,
            value: customerInfo.Phone,
            visible: true
        },
        {
            title: resources.Fax,
            value: customerInfo.Fax,
            visible: true
        }
    ];

    return (
        <div>
            {
                contactInfoPropsArray.map((contactInfoProps, index) => {
                    return contactInfoProps.visible && <ContactInfoRow {...contactInfoProps} key={index} />;
                })
            }
        </div>
    );
}

export default ContactInfo;