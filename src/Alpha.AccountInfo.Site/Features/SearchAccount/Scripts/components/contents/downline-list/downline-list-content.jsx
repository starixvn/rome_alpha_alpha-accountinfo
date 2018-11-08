import * as React from 'react';
import { InfiniteLoader, AutoSizer, List } from 'react-virtualized';

const STATUS_LOADING = 1;
const STATUS_LOADED = 2;
const ROW_HEIGHT_DESKTOP = 30;
const ROW_PADDING = 20;
const ROW_HEIGHT_MOBILE = (ROW_HEIGHT_DESKTOP + ROW_PADDING) / 0.7;
const PAGE_SIZE = 20;
const COLUMNS = 2;

export default class DownlineListContent extends React.PureComponent {
    constructor(props) {
        super(props);
        const { dowlineList, totalPages, pageIndex, status } = this.props;
        this.state = {
            loadedRowsMap: {},
            list: this.chunkDownlineList([...dowlineList]),
            totalPages: totalPages,
            pageIndex: pageIndex,
            status: status
        };

        this.timeoutIdMap = {};

        this.clearData = this.clearData.bind(this);
        this.isRowLoaded = this.isRowLoaded.bind(this);
        this.loadMoreRows = this.loadMoreRows.bind(this);
        this.rowRenderer = this.rowRenderer.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        let self = this;
        const { dowlineList, pageIndex, status } = nextProps;

        if (pageIndex <= this.state.totalPages) {
            this.setState({
                list: self.chunkDownlineList([...dowlineList]),
                pageIndex: pageIndex,
                status: status
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

    render() {
        const { list } = this.state;
        const { mobileView } = this.props;

        return (
            <div className={`${mobileView ? '' : 'customer-list'}`}>
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
                                        height: mobileView ? this.getHeightOnMobile() : this.getHeightOnDesktop(list.length),
                                        rowCount: list.length,
                                        rowHeight: mobileView ? 40 : ROW_HEIGHT_DESKTOP,
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

    getHeightOnDesktop(length) {
        return (length > 7 ? 7 : length) * ROW_HEIGHT_DESKTOP;
    }

    getHeightOnMobile() {
        const { excludedRows } = this.props;
        const containerHeight = document.documentElement.offsetHeight;
        const searchBoxHeight = document.getElementById('search-box').clientHeight;
        const customerInfoHeight = document.getElementById('customer-info').clientHeight;
        const navHeight = document.getElementById('nav-icon-downline').clientHeight;

        return containerHeight - searchBoxHeight - customerInfoHeight - navHeight - (excludedRows * ROW_HEIGHT_MOBILE);
    }

    clearData() {
        this.setState({
            loadedRowsMap: {},
        });
    }

    isRowLoaded({ index }) {
        const { loadedRowsMap } = this.state;
        return !!loadedRowsMap[index];
    }

    loadMoreRows({ startIndex, stopIndex }) {
        let promiseResolver;
        this.setLoadedStatus(startIndex, stopIndex, STATUS_LOADING);

        const timeoutId = setTimeout(() => {
            delete this.timeoutIdMap[timeoutId];
            this.setLoadedStatus(startIndex, stopIndex, STATUS_LOADED);
            promiseResolver();
        }, 100 + Math.round(Math.random() * 200));

        this.timeoutIdMap[timeoutId] = true;

        return new Promise(resolve => {
            promiseResolver = resolve;
        });
    }

    setLoadedStatus(startIndex, stopIndex, loadedStatus) {
        const { loadedRowsMap } = this.state;
        for (var i = startIndex; i <= stopIndex; i++) {
            loadedRowsMap[i] = loadedStatus;
        }
    }

    rowRenderer({ index, key, style }) {
        const { loadedRowsMap, list, pageIndex, totalPages, status } = this.state;
        const { fetchCustomerInfoFromServer, fetchDownlineListFromServer } = this.props;

        if (index === (pageIndex * (PAGE_SIZE / COLUMNS)) - 1 && pageIndex < totalPages) {
            fetchDownlineListFromServer(pageIndex + 1, false, status);
        }

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
}
