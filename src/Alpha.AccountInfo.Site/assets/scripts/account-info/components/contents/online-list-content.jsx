import React from 'react';

const OnlineListContent = ({ resources, data, handleOpenIPInfo }) => <div className="online-list">
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
                (data === null || data.length === 0) &&
                <tr>
                    <td colSpan="3">{resources.ThisUserIsNotOnline}</td>
                </tr>
            }

            {
                data !== null && data.map((online, index) =>
                    <tr key={index}>
                        <td className="text-center">{index + 1}</td>
                        <td><a onClick={handleOpenIPInfo} data-ip={online.IP}>{online.IP}</a></td>
                        <td className="text-center"> {online.FormattedLoginTime}</td>
                    </tr>)
            }
        </tbody>
    </table>
</div>;

export default OnlineListContent;