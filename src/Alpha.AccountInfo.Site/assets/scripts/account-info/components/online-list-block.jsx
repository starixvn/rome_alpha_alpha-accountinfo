import React from 'react';
import { Link } from 'react-router-dom';
import { NoInfo } from '../components/common/no-info';

const DownlineListBlock = ({ resources, custId, data, handleOpenIPInfo }) => {
    const header = <Link to="/" className="feature-item">
        <span className="icon-login" />
        {resources.OnlineList}
    </Link>;

    if (data !== null) {
        return (
            <div className="feature-detail">
                {header}
                {
                    <table className="tblRpt tblRpt-bordered width-100per online-list">
                        <thead>
                            <tr>
                                <th>{resources.NumberOrder}</th>
                                <th>{resources.IP}</th>
                                <th>{resources.LoginTime}</th>
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
                                        <td className="text-center"> {online.LoginTime}</td>
                                    </tr>)
                            }
                        </tbody>
                    </table>
                }
            </div>
        );
    }
    else {
        return (
            <div>
                {header}
                <NoInfo text={resources.NoInfo} />
            </div>
        );
    }
};

export default DownlineListBlock;