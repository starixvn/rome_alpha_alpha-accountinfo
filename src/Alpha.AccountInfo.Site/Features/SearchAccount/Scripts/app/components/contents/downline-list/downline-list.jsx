import React from 'react';
import TotalIndirectDownlines from './total-indirect-downlines';
import TotalDirectDownlines from './total-direct-downlines';
import DownlineListContent from './downline-list-content';
import { UserLevel } from '../../../constants';

const DownlineList = ({
	resources,
	downlineListData,
	searchAccountRoleId,
	mobileView,
	searchCounter,
	fetchDownlineListFromServer,
	fetchCustomerInfoFromServer
}) => {
	if ((mobileView || searchCounter > 0) && downlineListData === null) {
		return <div className="no-information">{resources.NoInfo}</div>
	}

	const totalDirectDownlines = downlineListData.TotalDownlines[0];
	const searchAccountIsMaster = searchAccountRoleId === UserLevel.Master;

	return (
		<div id="total-downlines">
			{
				searchAccountIsMaster &&
				<TotalIndirectDownlines {... { resources, totalIndirectDownline: downlineListData.TotalDownlines[1] }} />
			}
			<TotalDirectDownlines {...{ resources, searchAccountIsMaster, totalDirectDownlines, fetchDownlineListFromServer }} />
			<DownlineListContent
				{...{
					resources,
					mobileView,
					fetchDownlineListFromServer,
					fetchCustomerInfoFromServer,
					dowlineList: downlineListData.DownlineList,
					pageIndex: downlineListData.PageIndex,
					totalPages: downlineListData.TotalPages,
					statusId: downlineListData.StatusId
				}}
			/>
		</div>
	);
}

export default DownlineList;