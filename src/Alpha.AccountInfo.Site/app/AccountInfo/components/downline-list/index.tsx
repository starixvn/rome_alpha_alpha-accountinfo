import * as React from 'react';
import HeadingTab from '../general/tab-heading';

interface ITotalDownlineListProps {
    levelName: string,
    totalActive: number,
    totalClosed: number,
    totalSuspended: number
}

class TotalDownlineList extends React.Component<ITotalDownlineListProps, {}> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="col-xs-12 row-info">
                <div className="col-xs-6">{this.props.levelName} (A/C/S)</div>
                <div className="col-xs-6 col-right">
                    <div className="pull-right"
                        data-toggle="tooltip"
                        data-placement="top"
                        title={"Active:" + this.props.totalActive + " Closed:" + this.props.totalClosed + " Suspended:" + this.props.totalSuspended}>
                        <span className="number">{this.props.totalActive}</span>/<span className="status-closed">{this.props.totalClosed}</span>/<span className="status-suspended">{this.props.totalSuspended}</span>
                    </div>
                </div>
            </div>
        )
    }
}

interface IDownlineListTabProps {
}

interface IDownlineListTabState {
}

export default class DownlineListTab extends React.Component<IDownlineListTabProps, IDownlineListTabState> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="panel panel-default">
                <HeadingTab text="Downline list" id="headingDownlineList" iconClassname="icon-downline" tabHref="collapseDownlineList" />

                <div id="collapseDownlineList" className="panel-collapse collapse" role="tabpanel" aria-labelledby="headingDownlineList">
                    <div className="panel-body panel-downline-list">
                        <div className="swiper-container" id="downlineListSwiperContainer">
                            <div className="swiper-wrapper" id="downline-list-wrapper"></div>
                            <div className="swiper-bottom">
                                <div className="swiper-button-prev"><span className="icon-chevron-left"></span></div>
                                <div className="swiper-button-next"><span className="icon-chevron-right"></span></div>
                                <div className="swiper-pagination" id="downlineListPagination"></div>
                                <div className="page-control">
                                    <span>Page</span>
                                    <input type="text" className="number" value="2" />
                                </div>
                            </div>
                        </div>

                        <div className="col-xs-12 row-info"><hr /></div>

                        <TotalDownlineList levelName="Agents" totalActive={90} totalClosed={47} totalSuspended={2} />
                        <TotalDownlineList levelName="Members" totalActive={80} totalClosed={584} totalSuspended={3} />
                    </div>
                </div>
            </div>
        );
    }
}