import React from 'react';
import TotalIndirectDownlines from './total-indirect-downlines';
import TotalDirectDownlines from './total-direct-downlines';
import DownlineListContent from './downline-list-content';

const DownlineList = ({
    resources,
    downlineListData,
    showTotalIndirectDownlines,
    mobileView,
    fetchDownlineListFromServer,
    fetchCustomerInfoFromServer
}) => {
    const totalDirectDownlines = downlineListData.TotalDownlines[0];

    return (
        <div id="total-downlines">
            {
                showTotalIndirectDownlines && 
                <TotalIndirectDownlines {... { resources, totalIndirectDownline: downlineListData.TotalDownlines[1] }} />
            }
            <TotalDirectDownlines {...{ totalDirectDownlines, fetchDownlineListFromServer }} />
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