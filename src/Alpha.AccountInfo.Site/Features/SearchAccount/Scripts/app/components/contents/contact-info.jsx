import * as React from 'react';
import { Tooltip } from 'react-tippy';
import { buildTooltipProps } from '../../helpers/accountinfo-helper';

export const ContactInfoRow = ({ title, value }) => {
	let maxLength = 15;
	let isTruncated = value.length > maxLength;
	let content = <div>{isTruncated ? `${value.substring(0, maxLength)}...` : value}</div>;

	let contentWithTooltip = <Tooltip {...buildTooltipProps(value)}>{content}</Tooltip>;

    return (
		<div className="col-xs-12 row-info">
			<div className="col-xs-5">{title}</div>
			<div className="col-xs-7 col-right">
				{isTruncated ? contentWithTooltip : content}
			</div>
        </div>
    );
}

const ContactInfo = ({ resources, customerInfo }) => {
	if (customerInfo === null) {
		return <div className="no-information">{resources.NoInfo}</div>
	}

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