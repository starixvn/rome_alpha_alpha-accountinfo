import React from 'react';
import TotalIndirectDownlines from './total-indirect-downlines';
import TotalDirectDownlines from './total-direct-downlines';
import DownlineListContent from './downline-list-content';
import { UserLevel } from '../../../constants';

const DownlineList = ({
    resources,
    totalDownlines,
    downlineListData,
    directMembersData,
    searchAccountRoleId,
    mobileView,
    fetchDownlineListFromServer,
    fetchDirectMembersFromServer,
    fetchCustomerInfoFromServer
}) => {
    if (!mobileView && downlineListData === null) {
        return null;
    }

    const noInfoRow = <div className="no-information">{resources.NoInfo}</div>;

    if (mobileView && downlineListData === null && totalDirectDownlines === null) {
        return noInfoRow;
    }

    const totalDirectDownlines = totalDownlines.find(x => x.RoleId === (searchAccountRoleId - 1) && !x.IsDirectMember);
    const totalDirectMembers = totalDownlines.find(x => x.IsDirectMember);
    const searchAccountIsMaster = searchAccountRoleId === UserLevel.Master;

    if (totalDownlines.length === 0) {
        return noInfoRow;
    }

    return (
        <div id="total-downlines">
            {
                searchAccountIsMaster &&
                <TotalIndirectDownlines {... { resources, totalIndirectDownline: totalDownlines.find(x => x.RoleId === UserLevel.Member && !x.IsDirectMember) }} />
            }
            {totalDirectDownlines && <TotalDirectDownlines {...{ resources, searchAccountIsMaster, totalDirectDownlines, fetchDownlineListFromServer }} />}
            {downlineListData.DownlineList && downlineListData.DownlineList.length > 0 &&
                < DownlineListContent
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
            }
            {totalDirectMembers && <TotalDirectDownlines {...{
                resources,
                searchAccountIsMaster,
                totalDirectDownlines: totalDirectMembers,
                fetchDownlineListFromServer: fetchDirectMembersFromServer,
                isDirectMember: true
            }} />}
            {directMembersData && directMembersData.DownlineList && directMembersData.DownlineList.length > 0 && < DownlineListContent
                {...{
                    resources,
                    mobileView,
                    fetchDownlineListFromServer: fetchDirectMembersFromServer,
                    fetchCustomerInfoFromServer,
                    dowlineList: directMembersData.DownlineList,
                    pageIndex: directMembersData.PageIndex,
                    totalPages: directMembersData.TotalPages,
                    statusId: directMembersData.StatusId
                }}
            />
            }
        </div>
    );
};

export default DownlineList;