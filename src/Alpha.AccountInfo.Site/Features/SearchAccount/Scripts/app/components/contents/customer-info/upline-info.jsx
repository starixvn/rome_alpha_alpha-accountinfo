import * as React from 'react';
import { Tooltip } from 'react-tippy';
import { buildTooltipProps } from '../../../helpers/accountinfo-helper';

const UplineInfo = ({ username, fetchCustomerInfoFromServer }) => {
    const maxLength = 15;
    const isTruncated = username.length > maxLength;

    const content = <span
        id='upline-info'
        className='enabled'
        onClick={() => { fetchCustomerInfoFromServer(username); }}
    >
        {isTruncated ? `${username.substring(0, maxLength)}...` : username}
    </span>;

    const contentWithTooltip = <Tooltip {...buildTooltipProps(username)}>{content}</Tooltip>;

    return isTruncated ? contentWithTooltip : content;
};

export default UplineInfo;