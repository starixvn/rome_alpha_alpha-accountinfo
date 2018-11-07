import React from 'react';
import TotalDownlines from './total-downlines';
import { UserLevel } from '../../../constants';
import InfiniteLoaderExample from './render-downline-list';

const DownlineList = ({
    resources,
    downlineListData,
    mobileView,
    searchAccountRoleId,
    fetchCustomerInfoFromServer }) => {
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
                        let totalDownlinesProps = {
                            resources,
                            levelName: totalDownline.levelName,
                            totalActive: totalDownline.data.TotalActive,
                            totalClosed: totalDownline.data.TotalClosed,
                            totalSuspended: totalDownline.data.TotalSuspend,
                            key: index
                        }

                        return totalDownline.visible && <TotalDownlines {...totalDownlinesProps} />;
                    })
                }
                <div className="col-xs-12 row-info"><hr /> </div>

            <div className="totalstatus-list">
                <div className="radio">
                    <input type="radio" id="active" value="active" name="totalStatus" defaultChecked="true" />
                    <label className="label-text" htmlFor="active">A <span className="status-active">(3)</span></label>
                </div>
                <div className="radio">
                    <input type="radio" id="closed" value="closed" name="totalStatus" />
                    <label className="label-text" htmlFor="closed">C <span className="status-closed">(152)</span></label>
                </div>
                <div className="radio">
                    <input type="radio" id="suspended" value="suspended" name="totalStatus" />
                    <label className="label-text" htmlFor="suspended">S <span className="status-suspended">(0)</span></label>
                </div>   
            </div>
                       

            </div>
            <InfiniteLoaderExample
                {...{
                    mobileView,
                    fetchCustomerInfoFromServer,
                    dowlineList: downlineListData.DownlineList,
                    totalDownlinesRows: searchAccountIsMaster ? 2 : 1
                }}
            />
        </div>
    );
}

export default DownlineList;