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
    const levels = searchAccountRoleId - 1,
        isNotMember = searchAccountRoleId > 1,
        showValue = originaldataRow.Suspended !== dataRow.Suspended,
        header = dataRow.Suspended ?
            { text: resources.Suspended, buttonClass: 'suspend-button' } :
            { text: resources.Unsuspended, buttonClass: 'unsuspend-button' };

    return (
        <tr>
            <td> {dataRow.Username} </td>
            <td className="text-center">
                <div onClick={() => handleChange(index, !dataRow.Suspended)} className={header.buttonClass}>
                    {header.text}
                </div>
            </td>
            {
                isNotMember && [true, false].map((isAffected, index) =>
                    <td className="text-center" key={index}>
                        {
                            showValue && (originaldataRow.Suspended ?
                                <ColumnContent {...{ dataRow, levels, redirectViewListDetail, isAffected }} /> :
                                <span>{resources.All}</span>)
                        }
                    </td>)
            }
        </tr>);
}

export const ColumnContent = ({ dataRow, levels, isAffected, redirectViewListDetail }) => {
    let currentLevels = dataRow.Downlines.length;
    while (currentLevels < levels) {
        dataRow.Downlines.push({
            CustId: 0,
            DownlineRoleId: 0,
            AffectedDownlines: 0,
            UnchangedDownlines: 0
        });

        currentLevels++;
    }

    return (<div>
        {
            dataRow.Downlines.map((level, index) => {
                const downlines = isAffected ? level.AffectedDownlines : level.UnchangedDownlines;

                return <span key={index}>
                    {
                        downlines === 0 ? downlines :
                            <a key={index} href='#'
                                onClick={() => redirectViewListDetail(dataRow.CustId, dataRow.Username, level.DownlineRoleId, isAffected)}>
                                {downlines}
                            </a>
                    }
                    {index < currentLevels - 1 ? ` / ` : ``}
                </span>;
            })
        }
    </div>);
}
