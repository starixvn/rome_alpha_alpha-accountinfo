import React from 'react';
import TotalDownlines from './total-downlines';
import { UserLevel } from '../../../constants';
import DownlineListContent from './downline-list-content';
import TotalDownlinesSummary from './total-downlines-summary';

const DownlineList = ({
    resources,
    downlineListData,
    mobileView,
    searchAccountRoleId,
    fetchDownlineListFromServer,
    fetchCustomerInfoFromServer
}) => {
    const totalDownlineByLevel = downlineListData.TotalDownlines;

    return (
        <div>
            <div id="total-downlines">
                {
                    totalDownlineByLevel.map((totalDownline, index) => {
                        return <TotalDownlines {... { resources, totalDownline, key: index }} />;
                    })
                }
                <div className="col-xs-12 row-info"><hr /> </div>
                <TotalDownlinesSummary {...{ totalDownline: totalDownlineByLevel[0], fetchDownlineListFromServer }} />
            </div>
            <DownlineListContent
                {...{
                    resources,
                    mobileView,
                    fetchDownlineListFromServer,
                    fetchCustomerInfoFromServer,
                    dowlineList: downlineListData.DownlineList,
                    pageIndex: downlineListData.PageIndex,
                    totalPages: downlineListData.TotalPages,
                    statusId: downlineListData.StatusId,
                    excludedRows: totalDownlineByLevel.length + 1
                }}
            />
        </div>
    );
}

export default DownlineList;