import React from 'react';
import { accountInfoAPI } from '../api-client';
import TotalDownlines from './total-downlines';
import { Link } from 'react-router-dom';

export default class DownlineListBlock extends React.Component{
    constructor(props) {
        super(props);

        this.state = {
            data: null
        };

        this.handleClick();
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.custId !== this.props.custId) {
            this.handleClick();
        }
    }

    handleClick() {
            accountInfoAPI
            .getDownlineList({ SearchAccountId: this.props.custId })
            .then((responeData) => {
                this.setState({ data: responeData });
        });
    }

    render() {

        const { resources, handleSearchFromUserLevel } = this.props;
        const { data } = this.state;

        const content = data === null || data.DownlineList.length === 0 ?
            <div>{resources.NoInfo}</div> :
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
                
                
            </div>;   

        return (
            <div className="feature-detail customer-list">
                <Link to="/" className="feature-item">
                    <span className="icon-downline" />
                    {resources.DownlineList}</Link>
                {content}
            </div>    
        );
    }
}