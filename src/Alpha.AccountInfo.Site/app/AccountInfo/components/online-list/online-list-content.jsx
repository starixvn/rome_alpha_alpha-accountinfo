import React from 'react';

const OnlineListContent = ({ resources, data}) =>
    <tbody>
        {
            (data === null || data.length == 0) && 

            <tr>
                <td colSpan="3">{resources.ThisUserIsNotOnline}</td>
            </tr>
        }

        {
            data != null && data.map((online, index) =>
            <tr key={index}>
                <td className="text-center">{index + 1}</td>
                <td>{online.IP}</td>
                <td className="text-center"> {online.Reload_Time}</td>
            </tr>)
        }
    </tbody>

export default OnlineListContent;