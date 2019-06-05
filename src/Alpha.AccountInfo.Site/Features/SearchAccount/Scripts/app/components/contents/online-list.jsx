import React from 'react';

export const OnlineListRow = ({ onlineList, index, handleOpenIPInfo }) => {
    return <tr>
        <td className='text-center'>
            {index + 1}
        </td>
        <td>
            <a onClick={handleOpenIPInfo} data-ip={onlineList.IP}>
                {onlineList.IP}
            </a>
        </td>
        <td className='text-center'>
            {onlineList.FormattedLoginTime}
        </td>
    </tr>;
}

const OnlineList = ({ resources, onlineListData, handleOpenIPInfo }) => {
	if (onlineListData === null || onlineListData.length === 0) {
        return <div className='no-information'>{resources.ThisUserIsNotOnline}</div>;
	}

    return <div className='online-list'>
        <table className='tblRpt tblRpt-bordered width-100per'>
            <thead>
                <tr>
                    <th>{resources.NumberOrder}</th>
                    <th>{resources.IP}</th>
                    <th>{resources.LoginTime}</th>
                </tr>
            </thead>
            <tbody>
                {
                    onlineListData.map(
                        (onlineList, index) =>
                            <OnlineListRow {...{ onlineList, index, handleOpenIPInfo, key: index }} />
                    )
                }
            </tbody>
        </table>
    </div>;
}

export default OnlineList;