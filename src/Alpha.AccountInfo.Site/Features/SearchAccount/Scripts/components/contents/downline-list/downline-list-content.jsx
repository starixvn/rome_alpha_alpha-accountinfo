import * as React from 'react';
import { InfiniteLoader, AutoSizer, List, WindowScroller } from 'react-virtualized';

const STATUS_LOADING = 1;
const STATUS_LOADED = 2;
const HEIGHT_DESKTOP = 240;
const ROW_HEIGHT_DESKTOP = 30;
const MAX_DOM_ROWS_DESKTOP = HEIGHT_DESKTOP / ROW_HEIGHT_DESKTOP;
const ROW_HEIGHT_MOBILE = 40;
const PAGE_SIZE = 10;
const COLUMNS = 2;
const PAGE_ROWS = PAGE_SIZE / COLUMNS; 

export default class DownlineListContent extends React.PureComponent {
    constructor(props) {
        super(props);
        const { dowlineList, totalPages, pageIndex, statusId } = this.props;
        this.state = {
            loadedRowsMap: {},
            list: this.chunkDownlineList([...dowlineList]),
            totalPages: totalPages,
            pageIndex: pageIndex,
            statusId: statusId
        };

        this.timeoutIdMap = {};
        this.currentPageIndex = 0;
        this.isRowLoaded = this.isRowLoaded.bind(this);
        this.loadMoreRows = this.loadMoreRows.bind(this);
        this.rowRenderer = this.rowRenderer.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        const { dowlineList, pageIndex, statusId, totalPages } = nextProps;
        if (this.props.dowlineList !== dowlineList) {
            this.setState({
                list: this.chunkDownlineList([...dowlineList]),
                pageIndex: pageIndex,
                totalPages: totalPages,
                statusId: statusId,
                loadedRowsMap: {}
            });
        }
    }

    chunkDownlineList(downlineList) {
        var results = [];
        while (downlineList.length) {
            results.push(downlineList.splice(0, COLUMNS));
        }
        return results;
    }

    componentWillUnmount() {
        Object.keys(this.timeoutIdMap).forEach(timeoutId => {
            clearTimeout(timeoutId);
        });
    }

    getHeightDesktop() {
        let length = this.state.list.length;
        return (length > MAX_DOM_ROWS_DESKTOP ? MAX_DOM_ROWS_DESKTOP : length) * ROW_HEIGHT_DESKTOP;
    }

    isRowLoaded({ index }) {
        const { loadedRowsMap } = this.state;
        return !!loadedRowsMap[index];
    }

    loadMoreRows({ startIndex, stopIndex }) {
        let promiseResolver;
        this.fectchMoreData();
        this.setLoadedStatus(startIndex, stopIndex, STATUS_LOADING);

        const timeoutId = setTimeout(() => {
            delete this.timeoutIdMap[timeoutId];
            this.setLoadedStatus(startIndex, stopIndex, STATUS_LOADED);
            promiseResolver();
        }, 100 + Math.round(Math.random() * 2000));

        this.timeoutIdMap[timeoutId] = true;

        return new Promise(resolve => {
            promiseResolver = resolve;
        });
    }

    fectchMoreData() {
        const { pageIndex, totalPages, statusId } = this.state;
        const { fetchDownlineListFromServer } = this.props;

        if (this.currentPageIndex !== pageIndex && pageIndex < totalPages) {
            this.currentPageIndex = pageIndex;
            fetchDownlineListFromServer(pageIndex + 1, false, statusId, totalPages);
        }
    }

    setLoadedStatus(startIndex, stopIndex, loadedStatus) {
        const { loadedRowsMap } = this.state;
        for (var i = startIndex; i <= stopIndex; i++) {
            loadedRowsMap[i] = loadedStatus;
        }
    }

    rowRenderer({ index, key, style }) {
        const { loadedRowsMap, list } = this.state;
        const { fetchCustomerInfoFromServer} = this.props;

        let content = list[index].map((item, itemIndex) => {
            let { Username } = item;
            return (
                <div className='col-xs-6' key={itemIndex}>
                    {
                        loadedRowsMap[index] === STATUS_LOADED ?
                            <a href="#" onClick={() => { fetchCustomerInfoFromServer(Username) }}>{Username}</a> :
                            <div className='placeholder' style={{ width: Username.size }} />
                    }
                </div>
            );
        });

        return (
            <div {...{ style, key, className: 'col-xs-12' }} >
                {content}
            </div>
        );
    }

    buildInfiniteLoaderUseDivScroller() {
        const { list, statusId } = this.state;

        return (
            <div className='col-xs-12 customer-list' key={statusId}>
                <InfiniteLoader
                    isRowLoaded={this.isRowLoaded}
                    loadMoreRows={this.loadMoreRows}
                    rowCount={list.length}>
                    {({ onRowsRendered, registerChild }) => (
                        <AutoSizer>
                            {({ width }) => (
                                <List
                                    ref={registerChild}
                                    {...{
                                        onRowsRendered,
                                        width,
                                        height: this.getHeightDesktop(),
                                        rowCount: list.length,
                                        rowHeight: ROW_HEIGHT_DESKTOP,
                                        rowRenderer: this.rowRenderer,
                                        className: 'col-xs-12'
                                    }}
                                />
                            )}
                        </AutoSizer>
                    )}
                </InfiniteLoader>
            </div>
        );
    }

    buildInfiniteLoaderUseWindowScroller() {
        const { list, statusId } = this.state;

        return (
            <WindowScroller key={statusId}>
                {({ height, isScrolling, scrollTop }) => (
                    <InfiniteLoader
                        isRowLoaded={this.isRowLoaded}
                        loadMoreRows={this.loadMoreRows}
                        rowCount={list.length}>
                        {({ onRowsRendered, registerChild }) => (
                            <AutoSizer>
                                {({ width }) => (
                                    <List
                                        ref={registerChild}
                                        autoHeight
                                        {...{
                                            onRowsRendered,
                                            width,
                                            height,
                                            isScrolling,
                                            scrollTop,
                                            rowCount: list.length,
                                            rowHeight: ROW_HEIGHT_MOBILE,
                                            rowRenderer: this.rowRenderer
                                        }}
                                    />
                                )}
                            </AutoSizer>
                        )}
                    </InfiniteLoader>
                )}
            </WindowScroller>
        );
    }

    render() {
        const { list } = this.state;
        const { mobileView, resources } = this.props;

        if (list.length === 0) {
            return <div className="no-information">{resources.NoInfo}</div>;
        }

        return mobileView ? this.buildInfiniteLoaderUseWindowScroller() : this.buildInfiniteLoaderUseDivScroller();
    }
}
