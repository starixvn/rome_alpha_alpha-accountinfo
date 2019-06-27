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
    fetchDownlineListFromServer,
    fetchCustomerInfoFromServer
}) => {
    if (!mobileView && downlineListData === null) {
        return null;
    }

    const noInfoRow = <div className="no-information">{resources.NoInfo}</div>;

    if (mobileView && downlineListData === null) {
        return noInfoRow;
    }

    const totalDirectDownlines = downlineListData.TotalDownlines[0];
    const searchAccountIsMaster = searchAccountRoleId === UserLevel.Master;

    if (downlineListData.TotalDownlines.length === 0) {
        return noInfoRow;
    }

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
};

export default DownlineList;