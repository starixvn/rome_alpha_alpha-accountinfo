import React from 'react';

export default class TotalDownlinesSummary extends React.Component {
    constructor(props) {
        super(props);
        const { data } = this.props;

        this.state = {
            checkedStatusId: 'active',
            data: [
                {
                    id: 'active',
                    className: 'status-active',
                    title: 'A',
                    value: data.TotalActive
                },
                {
                    id: 'closed',
                    className: 'status-closed',
                    title: 'C',
                    value: data.TotalClosed
                },
                {
                    id: 'suspended',
                    className: 'status-suspended',
                    title: 'S',
                    value: data.TotalSuspend
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
            <div className="totalstatus-list row-info">
                {
                    data.map((item, index) => {
                        let { id, title, className, value } = item;
                        return (
                            <div className="radio" key={index}>
                                <input
                                    {...{
                                        id,
                                        checked: checkedStatusId === item.id,
                                        type: "radio",
                                        name: "totalStatus",
                                        onChange: () => { this.handleOnChange(id); }
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
        );
    }
}
