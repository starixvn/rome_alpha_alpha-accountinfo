import * as React from 'react';
import * as _ from 'lodash';

export const RowContent = ({ originalViewModel, item, downlineRoleId, index, handleChange, redirectViewListDetail, resources }) => {
    const length = item.UnsuspendByLevel.length;
    const showValue = originalViewModel[index].Suspended !== item.Suspended;

    return (
        <tr key={index}>
            <td className="text-center">
                {item.Username}
            </td>
            <td className="text-center">
                <div onClick={() => handleChange(index, !item.Suspended)} className={item.Suspended ? `suspend-button` : `unsuspend-button`}>
                    {item.Suspended ? resources.Suspended : resources.Unsuspended}
                </div>
            </td>
            {
                downlineRoleId > 1 && <td className="text-center">
                    {
                        showValue && (originalViewModel[index].Suspended ? item.UnsuspendByLevel.map((level, index) => {
                            return <ColumnContentByLevel
                                key={index}
                                downlines={level.AffectedDownlines}
                                showSlash={index < length - 1}
                                isAffected={true}
                                downlineRoleId={level.RoleId}
                                searchAccountId={item.CustId}
                                redirectViewListDetail={redirectViewListDetail}
                            />;
                        }) : resources.All)
                    }
                </td>
            }

            {
                downlineRoleId > 1 && <td className="text-center">
                    {
                        showValue && (originalViewModel[index].Suspended ? item.UnsuspendByLevel.map((level, index) => {
                            return <ColumnContentByLevel
                                key={index}
                                downlines={level.UnchangedDownlines}
                                showSlash={index < length - 1}
                                isAffected={false}
                                downlineRoleId={level.RoleId}
                                searchAccountId={item.CustId}
                                redirectViewListDetail={redirectViewListDetail}
                            />;
                        }) : resources.All)
                    }
                </td>
            }
        </tr>);
}

export const ColumnContentByLevel = ({ downlines, showSlash, isAffected, downlineRoleId, searchAccountId, redirectViewListDetail }) => {
    const text = `${downlines} ${showSlash ? '/' : ''}`;
    return downlines === 0 ? <span>{text}</span> :
        <a href='#' onClick={() => redirectViewListDetail(searchAccountId, downlineRoleId, isAffected)}> {text}</a>;
}