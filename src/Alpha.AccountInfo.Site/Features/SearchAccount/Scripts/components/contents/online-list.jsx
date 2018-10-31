import React from 'react';

export const OnlineListRow = ({ onlineList, index, handleOpenIPInfo }) => {
    return (
        <tr>
            <td className="text-center">
                {index + 1}
            </td>
            <td>
                <a onClick={handleOpenIPInfo} data-ip={onlineList.IP}>
                    {onlineList.IP}
                </a>
            </td>
            <td className="text-center">
                {onlineList.FormattedLoginTime}
            </td>
        </tr>
    );
}

export const OnlineListNoInfoRow = ({ resources }) => {
    return (
        <tr>
            <td colSpan="3">{resources.ThisUserIsNotOnline}</td>
        </tr>
    );
}

const OnlineList = ({ resources, data, handleOpenIPInfo }) => {
    return (
        <div className="online-list">
            <table className="tblRpt tblRpt-bordered width-100per">
                <thead>
                    <tr>
                        <th>{resources.NumberOrder}</th>
                        <th>{resources.IP}</th>
                        <th>{resources.FormattedLoginTime}</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        data.length === 0 ?
                            <OnlineListNoInfoRow resources={resources}/> :
                            data.map((onlineList, index) =>
                                <OnlineListRow
                                    onlineList={onlineList}
                                    index={index}
                                    handleOpenIPInfo={handleOpenIPInfo}
                                    key={index}
                                />
                            )
                    }
                </tbody>
            </table>
        </div>
    );
}

export default OnlineList;