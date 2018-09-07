import React from 'react';
import CustomizedAccordionItem from './common/customized-accordion-item';
import Paging from './common/paging';
import { accountInfoAPI } from '../api-client';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';

export default class DownlineListBlock extends React.Component{
    constructor(props) {
        super(props);
        this.state = {
            data: null
        }
        this.handleClick = this.handleClick.bind(this);
        this.renderCallback = this.renderCallback.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.custId != this.props.custId) {
            this.setState({
                custId: nextProps.custId,
                data: null
            });
        }
    }

    handleClick(e) {
        this.state.data === null && accountInfoAPI.getDownlineList({ custid: this.props.custId }).then((data) => {
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

        const content = data == null || data.DownlineList.length == 0 ?
            <div>{resources.NoInfo}</div> :
            <div>
                <Paging resourcePage={this.props.resources.Page} values={data.DownlineList} renderCallback={this.renderCallback} />
                <div><hr /></div>
                {
                    data.TotalStatusAgent != null &&
                    <TotalDownlines levelName="Agents"
                        resources={resources}
                        totalActive={data.TotalStatusAgent.TotalActive}
                        totalClosed={data.TotalStatusAgent.TotalClosed}
                        totalSuspended={data.TotalStatusAgent.TotalSuspend} />
                }
                {
                    data.TotalStatusMember != null &&
                    <TotalDownlines levelName="Members"
                        resources={resources}
                        totalActive={data.TotalStatusMember.TotalActive}
                        totalClosed={data.TotalStatusMember.TotalClosed}
                        totalSuspended={data.TotalStatusMember.TotalSuspend} />
                }
            </div>    

        return (
            <CustomizedAccordionItem
                title={resources.DownlineList}
                iconClassname="icon-downline"
                expanded={expanded}
                handleClick={this.handleClick}
                blockId={blockId}>
                {content}
            </CustomizedAccordionItem>    
        );
    }
}

const TotalDownlines = ({ resources, levelName, totalActive, totalClosed, totalSuspended }) => {
    const totalDownlineTooltip = <div>
        <div className='nowrap bold'>{resources.Active}: <span className='status-active'>{totalActive}</span></div>
        <div className='nowrap bold'>{resources.ConfirmClosedViewModelText}: <span className='status-closed'>{totalClosed}</span></div>
        <div className='nowrap bold'>{resources.ConfirmSuspendedViewModelText}: <span className='status-suspended'>{totalSuspended}</span></div>
    </div>;

    return (
        <div className="col-xs-12 row-info">
            <div className="col-xs-6 px-0">{levelName} (A/C/S)</div>
            <div className="col-xs-6 col-right px-0">
                <div className="pull-right">

                    <Tooltip
                        html={totalDownlineTooltip}
                        position="top"
                        trigger="mouseenter"
                        arrow="true"
                        theme="light">
                        <span className="status-active">{totalActive}</span>
                        /<span className="status-closed">{totalClosed}</span>
                        /<span className="status-suspended">{totalSuspended}</span>
                    </Tooltip>
                </div>
            </div>
        </div>
    );
}