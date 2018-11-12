import React from 'react';

const ACTIVE_ID = 1;
const CLOSED_ID = 2;
const SUSPENDED_ID = 3;

export default class TotalDirectDownlines extends React.Component {
    constructor(props) {
        super(props);
        const { totalDirectDownlines } = this.props;
        const { TotalActive, TotalClosed, TotalSuspended } = totalDirectDownlines;

        this.state = {
            checkedStatusId: ACTIVE_ID,
            data: [
                {
                    statusId: ACTIVE_ID,
                    className: 'status-active',
                    title: 'A',
                    value: TotalActive
                },
                {
                    statusId: CLOSED_ID,
                    className: 'status-closed',
                    title: 'C',
                    value: TotalClosed
                },
                {
                    statusId: SUSPENDED_ID,
                    className: 'status-suspended',
                    title: 'S',
                    value: TotalSuspended
                }
            ]
        };

        this.handleOnChange = this.handleOnChange.bind(this);
    }

    handleOnChange(statusId) {
        const { fetchDownlineListFromServer } = this.props;
        this.setState({
            checkedStatusId: statusId
        });

        fetchDownlineListFromServer(1, true, statusId);
    }

    render() {
        const { data, checkedStatusId } = this.state;

        return (
            <div>
                <div className="row-info bold">Agents (A/C/S)</div>
                <div className="totalstatus-list">
                    {
                        data.map((item, index) => {
                            let { statusId, title, className, value } = item;
                            let id = `${title}-${statusId}`;
                            return (
                                <div className="radio" key={index}>
                                    <input
                                        {...{
                                            id,
                                            checked: checkedStatusId === item.statusId,
                                            type: "radio",
                                            name: "totalStatus",
                                            onChange: () => { this.handleOnChange(statusId); }
                                        }}
                                    />
                                    <label className="label-text" htmlFor={id} >
                                        {title}
                                        <span className={className}>
                                            {`(${value})`}
                                        </span>
                                    </label>
                                </div>
                            );
                        })
                    }
                </div>
            </div>
            
        );
    }
}
