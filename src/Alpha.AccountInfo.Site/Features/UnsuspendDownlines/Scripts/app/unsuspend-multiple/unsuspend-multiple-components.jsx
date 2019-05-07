import * as React from 'react';
import * as _ from 'lodash';

export const RowContent = ({
    originaldataRow,
    dataRow,
    searchAccountRoleId,
    index,
    handleChange,
    redirectViewListDetail,
    resources }) => {
    const levels = searchAccountRoleId - 1;
    const isNotMember = searchAccountRoleId > 1;
    const showValue = originaldataRow.Suspended !== dataRow.Suspended;

    return (
        <tr>
            <td> {dataRow.Username} </td>
            <td className="text-center">
                <div onClick={() => handleChange(index, !dataRow.Suspended)}
                    className={dataRow.Suspended ? `suspend-button` : `unsuspend-button`}
                >
                    {dataRow.Suspended ? resources.Suspended : resources.Unsuspended}
                </div>
            </td>
            {
                isNotMember && [true, false].map((isAffected, index) => {
                    return <td className="text-center" key={index}>
                        {
                            showValue && (originaldataRow.Suspended ?
                                <ColumnContent {...{ dataRow, levels, redirectViewListDetail, isAffected}} /> :
                                <span>{resources.All}</span>)
                        }
                    </td>
                })
            }
        </tr>);
}

export const ColumnContent = ({ dataRow, levels, isAffected, redirectViewListDetail }) => {
    let count = dataRow.UnsuspendByLevel.length;
    while (count < levels) {
        dataRow.UnsuspendByLevel.push({
            CustId: 0,
            DownlineRoleId: 0,
            AffectedDownlines: 0,
            UnchangedDownlines: 0
        });

        count++;
    }

    return (<div>
        {
            dataRow.UnsuspendByLevel.map((level, index) => {
                const downlines = isAffected ? level.AffectedDownlines : level.UnchangedDownlines;

                return <span key={index}>
                    {
                        downlines === 0 ? downlines :
                            <a key={index} href='#'
                                onClick={() => redirectViewListDetail(dataRow.CustId, level.DownlineRoleId, isAffected)}>
                                {downlines} </a>
                    }
                    {index < count - 1 ? ` / ` : ``}
                </span>;
            })
        }
    </div>);
}
