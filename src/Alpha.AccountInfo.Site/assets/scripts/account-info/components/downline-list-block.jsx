import React from 'react';
import Paging from './common/paging';
import { accountInfoAPI } from '../api-client';
import TotalDownlines from './total-downlines';

export default class DownlineListBlock extends React.Component{
    constructor(props) {
        super(props);
        this.state = {
            data: null
        };

        this.handleClick = this.handleClick.bind(this);
        this.renderCallback = this.renderCallback.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.custId !== this.props.custId) {
            this.setState({
                custId: nextProps.custId,
                data: null
            });
        }
    }

    handleClick(e) {
        this.state.data === null && accountInfoAPI.getDownlineList({ SearchAccountId: this.props.custId }).then((data) => {
            this.setState({ data: data });
        });

        this.props.handleCollapExpandBlock(e);
    }

    renderCallback(downline) {
        return <a href="#" data-username={downline} onClick={this.props.handleSearchFromUserLevel}>{downline}</a>;
    }

    render() {
        const { resources, blockId, expanded, handleSearchFromUserLevel } = this.props;
        const { data } = this.state;

        const content = data === null || data.DownlineList.length === 0 ?
            <div>{resources.NoInfo}</div> :
            <div>
                <Paging resourcePage={this.props.resources.Page} values={data.DownlineList} renderCallback={this.renderCallback} />
                <div><hr /></div>
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
            </div>;   

        return (
            <div
                title={resources.DownlineList}
                iconClassname="icon-downline"
                expanded={expanded}
                handleClick={this.handleClick}
                blockId={blockId}
            >
                {content}
            </div>    
        );
    }
}