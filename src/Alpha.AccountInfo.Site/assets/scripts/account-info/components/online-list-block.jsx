import React from 'react';
import { NoInformFeature } from '../components/common/no-inform-feature';
import { HeaderFeature } from '../components/common/header-feature';

const OnlineListBlock = ({ resources, data, handleOpenIPInfo }) => {

    if (data !== null) {
        return (
            <div className="feature-detail">
                <HeaderFeature url="/" iconClass="icon-onlinelist" featureText={resources.OnlineList} />
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
        return <NoInformFeature url="/" iconClass="icon-onlinelist" featureText={resources.OnlineList} noInform={resources.NoInfo} />
    }
};

export default OnlineListBlock;