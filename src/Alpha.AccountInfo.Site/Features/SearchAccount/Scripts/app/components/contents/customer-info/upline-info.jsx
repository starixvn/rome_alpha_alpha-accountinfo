import * as React from 'react';
import { Tooltip } from 'react-tippy';

const UplineInfo = ({ username, fetchCustomerInfoFromServer }) => {
	let maxLength = 18;
	let isTruncated = username.length > maxLength;
	let tooltipProps = {
		title: username,
		position: "bottom",
		trigger: "mouseenter",
		arrow: "true"
	};

	let content = <span
		id="upline-info"
		className="enabled"
		onClick={() => { fetchCustomerInfoFromServer(username) }}
	>
		{isTruncated ? `${username.substring(0, maxLength)}...` : username}
	</span>

	let contentWithTooltip = <Tooltip {...tooltipProps}>{content}</Tooltip>;

	return isTruncated ? contentWithTooltip : content;
}

export default UplineInfo; 