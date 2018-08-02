import React from 'react';
import NoInformationRow from './no-infomation-row';

export default class OnlineListContent extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        const { onlineListData } = this.props;

        return (
            <tbody>
                {onlineListData === null && <NoInformationRow classNames="text-center" colspan="3" /> }

                { onlineListData !== null &&
                    onlineListData.map(rowObj =>
                        <tr key={rowObj.id}>
                            <td className="text-center">{rowObj.id}</td>
                            <td>{rowObj.ip}</td>
                            <td className="text-center"> {rowObj.dateTime}</td>
                        </tr>
                    )
                }
            </tbody>
            
        );
    }
}