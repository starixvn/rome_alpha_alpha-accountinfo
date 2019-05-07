import * as React from 'react';
import { getUserLevelName } from '../unsuspend-helper';

export const ColumnContentByLevel = ({
    downlines,
    isAffected,
    downlineRoleId,
    redirectViewListDetail }) => <span>
        {
            downlines === 0 ? downlines :
                <a href='#'
                    onClick={() => redirectViewListDetail(isAffected, downlineRoleId)}>
                    {downlines}
                </a>
        }
    </span>;

export const BodyRowContent = ({
    level,
    redirectViewListDetail,
    resources }) => <tr>
        <td>
            {
                getUserLevelName(level, resources)
            }
        </td>
        {
            [{
                downlines: level.AffectedDownlines,
                isAffected: true,
                downlineRoleId: level.RoleId
            },
            {
                downlines: level.UnchangedDownlines,
                isAffected: false,
                downlineRoleId: level.RoleId
            }].map((column, index) => {
                const { downlines, downlineRoleId, isAffected } = column;
                return (
                    <td className="text-center" key={index}>
                        <ColumnContentByLevel {...{ downlines, isAffected, downlineRoleId, redirectViewListDetail }} />
                    </td>
                );
            })
        }
    </tr>;

export const FooterContent = ({
    affectedDownlinesTotal,
    unchangedDownlinesTotal,
    redirectViewListDetail,
    totalText = 'Total' }) => <tfoot>
        <tr className="tbl-footer">
            <td>{totalText}</td>
            {
                [{
                    downlines: affectedDownlinesTotal,
                    isAffected: true
                },
                {
                    downlines: unchangedDownlinesTotal,
                    isAffected: false
                }].map((column, index) => {
                    const { downlines, isAffected } = column;
                    return (
                        <td className="text-center" key={index}>
                            <ColumnContentByLevel {...{ downlines, isAffected, redirectViewListDetail, downlineRoleId: 0 }} />
                        </td>
                    );
                })
            }
        </tr>
    </tfoot>;