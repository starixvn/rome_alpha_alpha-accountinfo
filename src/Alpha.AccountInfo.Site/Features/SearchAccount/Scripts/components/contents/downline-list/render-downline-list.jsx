import * as React from 'react';
import { InfiniteLoader, AutoSizer, List } from 'react-virtualized';

const STATUS_LOADING = 1;
const STATUS_LOADED = 2;
const ROW_HEIGHT_DESKTOP = 30;
const ROW_PADDING = 20;
const ROW_HEIGHT_MOBILE = (ROW_HEIGHT_DESKTOP + ROW_PADDING) / 0.7;

export default class InfiniteLoaderExample extends React.PureComponent {
    constructor(props) {
        super(props);
        const { dowlineList } = this.props;
        this.state = {
            loadedRowsMap: {},
            list: this.chunkDownlineList([...dowlineList])
        };

        this.timeoutIdMap = {};

        this.clearData = this.clearData.bind(this);
        this.isRowLoaded = this.isRowLoaded.bind(this);
        this.loadMoreRows = this.loadMoreRows.bind(this);
        this.rowRenderer = this.rowRenderer.bind(this);
    }

    chunkDownlineList(downlineList) {
        var results = [];

        while (downlineList.length) {
            results.push(downlineList.splice(0, 2));
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
                                    height={mobileView ?
                                        this.getHeightOnMobile() :
                                        this.getHeightOnDesktop(list.length)
                                    }
                                    onRowsRendered={onRowsRendered}
                                    rowCount={list.length}
                                    rowHeight={30}
                                    rowRenderer={this.rowRenderer}
                                    width={width}
                                    className='col-xs-12'
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
        const { totalDownlinesRows } = this.props;
        const containerHeight = document.documentElement.offsetHeight;
        const searchBoxHeight = document.getElementById('search-box').clientHeight;
        const customerInfoHeight = document.getElementById('customer-info').clientHeight;
        const navHeight = document.getElementById('nav-icon-downline').clientHeight;

        return containerHeight - searchBoxHeight - customerInfoHeight - navHeight - (totalDownlinesRows * ROW_HEIGHT_MOBILE);
    }

    clearData() {
        this.setState({
            loadedRowsMap: {},
        });
    }

    isRowLoaded({ index }) {
        const { loadedRowsMap } = this.state;
        return !!loadedRowsMap[index]; // STATUS_LOADING or STATUS_LOADED
    }

    loadMoreRows({ startIndex, stopIndex }) {
        const { loadedRowsMap } = this.state;

        for (var i = startIndex; i <= stopIndex; i++) {
            loadedRowsMap[i] = STATUS_LOADING;
        }

        const timeoutId = setTimeout(() => {
            delete this.timeoutIdMap[timeoutId];

            for (var i = startIndex; i <= stopIndex; i++) {
                loadedRowsMap[i] = STATUS_LOADED;
            }

            promiseResolver();
        }, 100 + Math.round(Math.random() * 200));

        this.timeoutIdMap[timeoutId] = true;

        let promiseResolver;

        return new Promise(resolve => {
            promiseResolver = resolve;
        });
    }

    rowRenderer({ index, key, style }) {
        const { loadedRowsMap, list } = this.state;
        const { fetchCustomerInfoFromServer } = this.props;

        const row = list[index];

        let content = row.map((item, itemIndex) => {
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
            <div {...{ style, key, className: 'col-xs-12'}} >
                {content}
            </div>
        );
    }
}
