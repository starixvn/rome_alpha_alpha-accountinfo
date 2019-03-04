import * as React from 'react';
import { Tooltip } from 'react-tippy';
import { buildTooltipProps } from '../../../helpers/accountinfo-helper';

const UplineInfo = ({ username, fetchCustomerInfoFromServer }) => {
	let maxLength = 18;
	let isTruncated = username.length > maxLength;

	let content = <span
		id="upline-info"
		className="enabled"
		onClick={() => { fetchCustomerInfoFromServer(username) }}
	>
		{isTruncated ? `${username.substring(0, maxLength)}...` : username}
	</span>

	let contentWithTooltip = <Tooltip {...buildTooltipProps(username)}>{content}</Tooltip>;

	return isTruncated ? contentWithTooltip : content;
}

export default UplineInfo; 