//import * as React from 'react';
//import cn from 'classnames';
//import { InfiniteLoader, AutoSizer, List, WindowScroller } from 'react-virtualized';

//const STATUS_LOADING = 1;
//const STATUS_LOADED = 2;
//const ROW_HEIGHT_DESKTOP = 30;
//const ROW_PADDING = 20;
//const ROW_HEIGHT_MOBILE = (ROW_HEIGHT_DESKTOP + ROW_PADDING) / 0.7;
//const PAGE_SIZE = 20;
//const COLUMNS = 2;

//export default class DownlineListContent extends React.PureComponent {
//    constructor(props) {
//        super(props);
//        const { dowlineList, totalPages, pageIndex, statusId  } = this.props;
//        this.state = {
//            loadedRowsMap: {},
//            list: this.chunkDownlineList([...dowlineList]),
//            totalPages: totalPages,
//            pageIndex: pageIndex,
//            statusId: statusId,
//            scrollToIndex: -1,
//        };

//        this.timeoutIdMap = {};
//        this._windowScroller = null;
//        this.customElement = null;
//        this.isScrollingCustomElement = false;

//        this.clearData = this.clearData.bind(this);
//        this.isRowLoaded = this.isRowLoaded.bind(this);
//        this.loadMoreRows = this.loadMoreRows.bind(this);
//        this.rowRenderer = this.rowRenderer.bind(this);
//        this._rowRenderer = this._rowRenderer.bind(this);
//        this._onScrollToRowChange = this._onScrollToRowChange.bind(this);
//    }

//    componentWillReceiveProps(nextProps) {
//        const { dowlineList, pageIndex, statusId, totalPages } = nextProps;
//        let list = this.chunkDownlineList([...dowlineList]);
//        if (totalPages === 0 || pageIndex <= totalPages) {
//            this.setState({
//                list: list,
//                pageIndex: pageIndex,
//                totalPages: totalPages,
//                statusId: statusId
//            });
//        }
//    }

//    chunkDownlineList(downlineList) {
//        var results = [];
//        while (downlineList.length) {
//            results.push(downlineList.splice(0, COLUMNS));
//        }
//        return results;
//    }

//    componentWillUnmount() {
//        Object.keys(this.timeoutIdMap).forEach(timeoutId => {
//            clearTimeout(timeoutId);
//        });
//    }

//    render() {
//        const { list, statusId, scrollToIndex } = this.state;
//        const { mobileView, resources } = this.props;

//        if (list.length === 0) {
//            return <div className="no-information">{resources.NoInfo}</div>;
//        }

//        return (
//            <div className={`${mobileView ? '' : 'customer-list'}`} key={statusId}>
//                <WindowScroller
//                    ref={(windowScroller) => { this._windowScroller = windowScroller}}
//                    scrollElement={customElement}>
//                    {({ height, isScrolling, registerChild, onChildScroll, scrollTop }) => (
//                        <div className='WindowScrollerWrapper'>
//                            <AutoSizer disableHeight>
//                                {({ width }) => (
//                                    <div ref={registerChild}>
//                                        <List
//                                            ref={el => {
//                                                window.listEl = el;
//                                            }}
//                                            autoHeight
//                                            className='col-xs-12'
//                                            height={height}
//                                            isScrolling={isScrolling}
//                                            onScroll={onChildScroll}
//                                            overscanRowCount={mobileView ? 40 : 30}
//                                            rowCount={list.length}
//                                            rowHeight={30}
//                                            rowRenderer={this._rowRenderer}
//                                            scrollToIndex={scrollToIndex}
//                                            scrollTop={scrollTop}
//                                            width={width}
//                                        />
//                                    </div>
//                                )}
//                            </AutoSizer>
//                        </div>
//                    )}
//                </WindowScroller>
//            </div>
//        );
//    }
   

//    _setRef({ windowScroller }) {
//        this._windowScroller = windowScroller;
//    };

//    _onCheckboxChange( event){
//        this.context.setScrollingCustomElement(event.target.checked);
//    };

//    _onScrollToRowChange(event) {
//        const { loadedRowsMap, list, pageIndex, totalPages, statusId } = this.state;
//        const { fetchCustomerInfoFromServer, fetchDownlineListFromServer } = this.props;

      

//        let scrollToIndex = Math.min(
//            list.length - 1,
//            parseInt(event.target.value, 10),
//        );

//        if (isNaN(scrollToIndex)) {
//            scrollToIndex = undefined;
//        }

//        if (scrollToIndex === (pageIndex * (PAGE_SIZE / COLUMNS)) - 1 && pageIndex < totalPages) {
//            fetchDownlineListFromServer(pageIndex + 1, false, statusId, totalPages);
//        }

//        setTimeout(() => {
//            this.setState({ scrollToIndex });
//        }, 0);
//    };

//    getHeightOnDesktop(length) {
//        return (length > 7 ? 7 : length) * ROW_HEIGHT_DESKTOP;
//    }

//    getHeightOnMobile() {
//        const { excludedRows } = this.props;
//        const containerHeight = document.documentElement.offsetHeight;
//        const searchBoxHeight = document.getElementById('search-box').clientHeight;
//        const customerInfoHeight = document.getElementById('customer-info').clientHeight;
//        const navHeight = document.getElementById('nav-icon-downline').clientHeight;

//        return containerHeight - searchBoxHeight - customerInfoHeight - navHeight - (excludedRows * ROW_HEIGHT_MOBILE);
//    }

//    clearData() {
//        this.setState({
//            loadedRowsMap: {},
//        });
//    }

//    isRowLoaded({ index }) {
//        const { loadedRowsMap } = this.state;
//        return !!loadedRowsMap[index];
//    }

//    loadMoreRows({ startIndex, stopIndex }) {
//        let promiseResolver;
//        this.setLoadedStatus(startIndex, stopIndex, STATUS_LOADING);

//        const timeoutId = setTimeout(() => {
//            delete this.timeoutIdMap[timeoutId];
//            this.setLoadedStatus(startIndex, stopIndex, STATUS_LOADED);
//            promiseResolver();
//        }, 100 + Math.round(Math.random() * 200));

//        this.timeoutIdMap[timeoutId] = true;

//        return new Promise(resolve => {
//            promiseResolver = resolve;
//        });
//    }

//    setLoadedStatus(startIndex, stopIndex, loadedStatus) {
//        const { loadedRowsMap } = this.state;
//        for (var i = startIndex; i <= stopIndex; i++) {
//            loadedRowsMap[i] = loadedStatus;
//        }
//    }

//    _rowRenderer({ index, isScrolling, isVisible, key, style }) {
//        const { loadedRowsMap, list, pageIndex, totalPages, statusId } = this.state;
//        const { fetchCustomerInfoFromServer, fetchDownlineListFromServer } = this.props;

      

//        if (list[index] === null || list[index] === undefined) {
          
//            return <div {...{ style, key, className: `col-xs-12 row className` }} key={index}>
//                <div className='col-xs-6'>
//                    <div className='placeholder' style={{ width: 10 }} />
//                </div>
//                <div className='col-xs-6'>
//                    <div className='placeholder' style={{ width: 10 }} />
//                </div>
//            </div>
//        }

//        let content = list[index].map((item, itemIndex) => {
//            let { Username } = item;
//            return (
//                <div className='col-xs-6' key={itemIndex}>
//                    <a href="#" onClick={() => { fetchCustomerInfoFromServer(Username) }}>{Username}</a>
//                </div>
//            );
//        });

//        return (
//            <div {...{ style, key, className: `col-xs-12 row className` }} >
//                {content}
//            </div>
//        );
//    };

//    rowRenderer({ index, key, style }) {
//        const { loadedRowsMap, list, pageIndex, totalPages, statusId } = this.state;
//        const { fetchCustomerInfoFromServer, fetchDownlineListFromServer } = this.props;

//        if (index === (pageIndex * (PAGE_SIZE / COLUMNS)) - 1 && pageIndex < totalPages) {
//            fetchDownlineListFromServer(pageIndex + 1, false, statusId, totalPages);
//        }

//        let content = list[index].map((item, itemIndex) => {
//            let { Username } = item;
//            return (
//                <div className='col-xs-6' key={itemIndex}>
//                    {
//                        loadedRowsMap[index] === STATUS_LOADED ?
//                            <a href="#" onClick={() => { fetchCustomerInfoFromServer(Username) }}>{Username}</a> :
//                            <div className='placeholder' style={{ width: Username.size }} />
//                    }
//                </div>
//            );
//        });

//        return (
//            <div {...{ style, key, className: 'col-xs-12' }} >
//                {content}
//            </div>
//        );
//    }
//}
