import * as React from 'react';

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
                <div className="panel-heading" role="tab" id="headingDownlineList">
                    <h4 className="panel-title">
                        <a className="collapsed" role="button" data-toggle="collapse" data-parent="#infoTab" href="#collapseDownlineList" aria-expanded="false" aria-controls="collapseDownlineList">
                            <span className="icon-downline"></span> Downline list
                            </a>
                    </h4>
                </div>

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
                        <div className="col-xs-12 row-info">
                            <div className="col-xs-6">Agents (A/C/S)</div>
                            <div className="col-xs-6 col-right">
                                <div className="pull-right" data-toggle="tooltip" data-placement="top" title="Active:90 Closed:47 Suspended:2">
                                    <span className="number">90</span>/<span className="status-closed">47</span>/<span className="status-suspended">2</span>
                                </div>
                            </div>
                        </div>
                        <div className="col-xs-12 row-info">
                            <div className="col-xs-6">Members (A/C/S)</div>
                            <div className="col-xs-6 col-right">
                                <div className="pull-right" data-toggle="tooltip" data-placement="top" title="Active:80 Closed:584 Suspended:3">
                                    <span className="number">80</span>/<span className="status-closed">584</span>/<span className="status-suspended">3</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
      );
    }
}