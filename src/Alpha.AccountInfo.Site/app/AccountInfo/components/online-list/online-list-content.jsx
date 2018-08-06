import React from 'react';
import NoInformationRow from './no-infomation-row';

export default class OnlineListContent extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        const { data } = this.props;

        return (
            <tbody>
                {onlineListData === null && <NoInformationRow colspan="3" /> }

                {data != null && data.map((online, index) =>
                        <tr key={index}>
                            <td className="text-center">{index + 1}</td>
                            <td>{online.IP}</td>
                            <td className="text-center"> {online.Reload_Time}</td>
                        </tr>
                    )
                }
            </tbody>
            
        );
    }
}