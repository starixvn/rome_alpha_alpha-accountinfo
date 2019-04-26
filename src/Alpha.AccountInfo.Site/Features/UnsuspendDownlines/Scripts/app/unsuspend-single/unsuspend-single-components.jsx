import * as React from 'react';
import { getUserLevelName } from '../unsuspend-helper';

export const ColumnContentByLevel = ({ downlines, isAffected, downlineRoleId, redirectViewListDetail }) => {
    return downlines === 0 ? <span>0</span> : <a href='#' onClick={() => redirectViewListDetail(isAffected, downlineRoleId)}> {downlines} </a>
}

export const BodyRowContent = ({ level, redirectViewListDetail, resources }) => {
    const columns = [
        {
            downlines: level.AffectedDownlines,
            isAffected: true,
            roleId: level.RoleId
        },
        {
            downlines: level.UnchangedDownlines,
            isAffected: false,
            roleId: level.RoleId
        }];

    return (
        <tr>
            <td className="text-center">
                {
                    getUserLevelName(level, resources)
                }
            </td>
            {
                columns.map((columns, index) => {
                    return (
                        <td className="text-center" key={index}>
                            <ColumnContentByLevel {...{
                                downlines: columns.downlines,
                                isAffected: columns.isAffected,
                                downlineRoleId: columns.roleId,
                                redirectViewListDetail
                            }} />
                        </td>
                    );
                })

            }
        </tr>
    );
}

export const FooterContent = ({ affectedDownlinesTotal, unchangedDownlinesTotal, redirectViewListDetail, resources }) => {
    const columns = [
        {
            downlines: affectedDownlinesTotal,
            isAffected: true
        },
        {
            downlines: unchangedDownlinesTotal,
            isAffected: false
        }];

    return (
        <tfoot>
            <tr>
                <th className="text-center">{resources.Total}</th>
                {
                    columns.map((columns, index) => {
                        return (
                            <th className="text-center" key={index}>
                                <ColumnContentByLevel
                                    downlines={columns.downlines}
                                    isAffected={columns.isAffected}
                                    downlineRoleId={0}
                                    redirectViewListDetail={redirectViewListDetail}
                                />
                            </th>
                        );
                    })

                }
            </tr>
        </tfoot>
    );
}