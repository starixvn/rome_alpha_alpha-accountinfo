import * as React from 'react';
import { InfiniteLoader, AutoSizer, List, WindowScroller } from 'react-virtualized';
import { Scale, buildTooltipProps } from '../../../helpers/accountinfo-helper';
import { Tooltip } from 'react-tippy';

const STATUS_LOADING = 1;
const STATUS_LOADED = 2;
const HEIGHT_DESKTOP = 240;
const ROW_HEIGHT_DESKTOP = 30;
const MAX_DOM_ROWS_DESKTOP = HEIGHT_DESKTOP / ROW_HEIGHT_DESKTOP;
const ROW_HEIGHT_MOBILE = ROW_HEIGHT_DESKTOP / Scale;
const COLUMNS = 2;

export default class DownlineListContent extends React.PureComponent {
    constructor(props) {
        super(props);
        const { dowlineList, totalPages, pageIndex, statusId } = this.props;
        this.assignState(dowlineList, pageIndex, statusId, totalPages);

        //// prevent call to get same pageIndex
        this.currentPageIndex = 0;
        this.timeoutIdMap = {};

        this.isRowLoaded = this.isRowLoaded.bind(this);
        this.loadMoreRows = this.loadMoreRows.bind(this);
        this.rowRenderer = this.rowRenderer.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        const { dowlineList, pageIndex, statusId, totalPages } = nextProps;
        if (this.props.dowlineList !== dowlineList) {
            this.assignState(dowlineList, pageIndex, statusId, totalPages);
        }
    }

    assignState(dowlineList, pageIndex, statusId, totalPages) {
        this.state = {
            loadedRowsMap: {},
            list: this.chunkDownlineList([...dowlineList]),
            totalPages: totalPages,
            pageIndex: pageIndex,
            statusId: statusId
        };
    }

    chunkDownlineList(downlineList) {
        let results = [];
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
        }, 100 + Math.round(Math.random() * 50));

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
        for (let i = startIndex; i <= stopIndex; i++) {
            loadedRowsMap[i] = loadedStatus;
        }
    }

    rowRenderer({ index, key, style }) {
        const { loadedRowsMap, list } = this.state;
        const { fetchCustomerInfoFromServer } = this.props;

        const content = list[index].map((item, itemIndex) => {
            const { Username } = item, maxLength = 13, isTruncated = Username.length > maxLength;

			const content = loadedRowsMap[index] === STATUS_LOADED ?
                <a href='#' onClick={() => { fetchCustomerInfoFromServer(Username); }}>
					{isTruncated ? `${Username.substring(0, maxLength)}...` : Username }
				</a> :
				<div className='placeholder' style={{ width: Username.size }} />;

            const contentWithTooltip = <Tooltip {...buildTooltipProps(Username)}>{content}</Tooltip>;

			return (
				<div className='col-xs-6' key={itemIndex}>
					{isTruncated ? contentWithTooltip : content}
				</div>
            );
        });

        return (
            <div {...{ style, key, className: 'col-xs-12' }} >
                {content}
            </div>
        );
    }

    buildInfiniteLoader(extraProps, mobileView = false) {
        const length = this.state.list.length;

        return (
            <InfiniteLoader
                isRowLoaded={this.isRowLoaded}
                loadMoreRows={this.loadMoreRows}
                rowCount={length}>
                {({ onRowsRendered, registerChild }) => (
                    <AutoSizer>
                        {({ width }) => (
                            <List
                                ref={registerChild}
                                autoHeight={mobileView}
                                {...{
                                    onRowsRendered,
                                    width,
                                    rowCount: length,
                                    rowRenderer: this.rowRenderer
                                }}
                                {...extraProps}
                            />
                        )}
                    </AutoSizer>
                )}
            </InfiniteLoader>
        );
    }

    render() {
        const { list, statusId } = this.state;
        const { mobileView, resources } = this.props;
        const length = list.length;

        if (length === 0) {
            return <div className='no-information'>{resources.NoInfo}</div>;
        }

        const infiniteLoaderDesktop =
            <div className='col-xs-12 customer-list' key={statusId}>
                {
                    this.buildInfiniteLoader(
                        {
                            rowHeight: ROW_HEIGHT_DESKTOP,
                            height: (length > MAX_DOM_ROWS_DESKTOP ? MAX_DOM_ROWS_DESKTOP : length) * ROW_HEIGHT_DESKTOP,
                            className: 'col-xs-12'
                        })
                }
            </div>;

        const infiniteLoaderMobile =
            <WindowScroller key={statusId}>
                {({ height, isScrolling, scrollTop }) => {
                    return this.buildInfiniteLoader(
                        {
                            rowHeight: ROW_HEIGHT_MOBILE,
                            className: 'customer-list',
                            height,
                            isScrolling,
                            scrollTop
                        }, true);
                }}
            </WindowScroller>;

        return mobileView ? infiniteLoaderMobile : infiniteLoaderDesktop;
    }
}
