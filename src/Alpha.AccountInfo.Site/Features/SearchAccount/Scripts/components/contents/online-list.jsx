import React from 'react';

const OnlineList = ({ resources, data, handleOpenIPInfo }) => {
    let noInfoRow = <tr>
        <td colSpan="3">{resources.ThisUserIsNotOnline}</td>
    </tr>;

    let content = data.map((online, index) =>
        <tr key={index}>
            <td className="text-center">
                {index + 1}
            </td>
            <td>
                <a onClick={handleOpenIPInfo} data-ip={online.IP}>
                    {online.IP}
                </a>
            </td>
            <td className="text-center">
                {online.FormattedLoginTime}
            </td>
        </tr>
    );

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
                        data.length === 0 ? noInfoRow : content
                    }
                </tbody>
            </table>
        </div>
    );
}

export default OnlineList;