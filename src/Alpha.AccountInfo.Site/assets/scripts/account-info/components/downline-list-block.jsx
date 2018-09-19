import React from 'react';
import { accountInfoAPI } from '../api-client';
import TotalDownlines from './total-downlines';
import { Link } from 'react-router-dom';
import { NoInfo } from '../components/common/no-info';

export default class DownlineListBlock extends React.Component{
    constructor(props) {
        super(props);

        this.state = {
            data: null
        };

        this.handleClick();
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.custId !== this.props.customerInfo.CustId ) {
            this.handleClick();
        }
    }

    handleClick() {
            accountInfoAPI
                .getDownlineList({ SearchAccountId: this.props.customerInfo.CustId })
                .then((responeData) => {
                    this.setState({ data: responeData });
        });
    }

    render() {
        const { resources, customerInfo, handleSearchFromUserLevel } = this.props;
        const header = <Link to="/" className="feature-item">
                <span className="icon-downline" />
                {resources.DownlineList}
            </Link>;

        if (customerInfo !== null) {
            const { data } = this.state;
            return (
                <div className="feature-detail customer-list">
                    {header}
                    <div>
                        {
                            data.TotalStatusAgent !== null &&
                            <TotalDownlines levelName="Agents"
                                resources={resources}
                                totalActive={data.TotalStatusAgent.TotalActive}
                                totalClosed={data.TotalStatusAgent.TotalClosed}
                                totalSuspended={data.TotalStatusAgent.TotalSuspend}
                            />
                        }
                        {
                            data.TotalStatusMember !== null &&
                            <TotalDownlines levelName="Members"
                                resources={resources}
                                totalActive={data.TotalStatusMember.TotalActive}
                                totalClosed={data.TotalStatusMember.TotalClosed}
                                totalSuspended={data.TotalStatusMember.TotalSuspend}
                            />
                        }

                        <div><hr /></div>
                        <div className="col-xs-12 customer-list">
                            {
                                data.DownlineList.map((downline, index) => {
                                    return <div className="col-xs-6" key={index}>
                                        <a href="#" data-username={downline} onClick={this.props.handleSearchFromUserLevel}>{downline}</a>
                                    </div>;
                                })
                            }
                        </div>
                    </div>
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
    }
}