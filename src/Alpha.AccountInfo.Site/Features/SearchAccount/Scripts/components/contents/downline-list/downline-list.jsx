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
    let searchAccountIsMaster = searchAccountRoleId === UserLevel.Master;
    const totalDownlineByLevel = [
        {
            levelName: resources.Agents,
            data: downlineListData.TotalStatusAgent,
            visible: searchAccountIsMaster
        },
        {
            levelName: resources.Members,
            data: downlineListData.TotalStatusMember,
            visible: true
        }
    ];

    return (
        <div>
            <div id="total-downlines">
                {
                    totalDownlineByLevel.map((totalDownline, index) => {
                        return totalDownline.visible &&
                            <TotalDownlines
                                {... {
                                    resources,
                                    levelName: totalDownline.levelName,
                                    totalActive: totalDownline.data.TotalActive,
                                    totalClosed: totalDownline.data.TotalClosed,
                                    totalSuspended: totalDownline.data.TotalSuspend,
                                    key: index
                                }}
                            />;
                    })
                }
                <div className="col-xs-12 row-info"><hr /> </div>
                <TotalDownlinesSummary
                    {...{
                        data: (searchAccountIsMaster ? totalDownlineByLevel[0] : totalDownlineByLevel[1]).data,
                        fetchDownlineListFromServer
                    }}
                />
            </div>
            <DownlineListContent
                {...{
                    mobileView,
                    fetchDownlineListFromServer,
                    fetchCustomerInfoFromServer,
                    dowlineList: downlineListData.DownlineList,
                    pageIndex: downlineListData.PageIndex,
                    totalPages: downlineListData.TotalPages,
                    status: downlineListData.Status,
                    excludedRows: searchAccountIsMaster ? 3 : 2
                }}
            />
        </div>
    );
}

export default DownlineList;