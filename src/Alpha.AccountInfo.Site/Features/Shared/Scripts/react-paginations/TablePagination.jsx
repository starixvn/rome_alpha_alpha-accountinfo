/* eslimt react/no-set-state: 0 */
import * as React from 'react';
import Anchor from '@trendmicro/react-anchor';
import classNames from 'classnames';
import PropTypes from 'prop-types';
import AutosizeInput from './AutosizeInput';

// Normalize the value by bringing it within the range.
// If value is greater than max, max will be returned.
// If value is less than min, min will be returned.
// Otherwise, value is returned unaltered. Both ends of this range are inclusive.
const limit = (value, min, max) => {
    return Math.max(min, Math.min(max, value));
};

class TablePagination extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            page: this.props.page
        };
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.page !== this.state.page) {
            this.setState({ page: Number(nextProps.page) });
        }
    }

    changePage(options) {
        const totalRecords = this.props.totalRecords;
        const {
            page = this.props.page,
            pageLength = this.props.pageLength
        } = { ...options };
        const pageMin = 1;
        const pageMax = Math.max(Math.ceil(totalRecords / pageLength), 1);
        this.props.onPageChange({
            page: limit(page, pageMin, pageMax),
            pageLength
        });
    }

    render() {
        const {
            type,
            totalRecords = 0,
            pageLengthMenu,
            prevPageRenderer,
            nextPageRenderer,
            pageRecordsRenderer,
            firstPageRenderer,
            lastPageRenderer,
            className,
            ...props
        } = this.props;

        const pageLength = this.props.pageLength || pageLengthMenu[0] || 10;
        const totalPages = totalRecords > 0 ? Math.ceil(totalRecords / pageLength) : 1;
        const page = limit(this.props.page, 1, totalPages);
        const from = limit((page - 1) * pageLength + 1, 1, totalRecords);
        const to = limit((page - 1) * pageLength + pageLength, 1, totalRecords);
        const prevPageDisabled = (page <= 1);
        const nextPageDisabled = (page >= totalPages);

        delete props.pageLength;
        delete props.page;
        delete props.onPageChange;

        return (
            <div
                {...props}
                className={classNames(className, 'table-pagination')}
            >
                <div className='table-pagination-block'>
                    {(type !== 'minor') &&

                        <div>
                        <select onChange={(e) => {
                            const pageSize =  parseFloat(e.currentTarget.value);
                                this.changePage({
                                    page: (pageSize !== pageLength) ? 1 : page,
                                    pageLength: pageSize
                                });
                            }}>
                                {pageLengthMenu.map(val =>
                                    <option
                                        key={val}
                                    value={val}

                                    className={classNames('pagination-page-list',
                                        { ['selected']: pageLength === val }
                                    )}
                                    >
                                    {val}
                                    </option>
                                )}
                            </select>
                        </div>
                    }
                    <div>
                        <div className='pagination'>
                            <div
                                className={classNames({
                                    ['disabled']: prevPageDisabled
                                })}
                            >
                                <Anchor
                                    disabled={prevPageDisabled}
                                    onClick={(event) => {
                                        const prevPage = 1;

                                        if (prevPage !== this.state.page) {
                                            this.setState({ page: prevPage });
                                        }

                                        this.changePage({ page: prevPage });
                                    }}
                                >
                                    {firstPageRenderer()}
                                </Anchor>
                            </div>
                            <div
                                className={classNames({
                                    ['disabled']: prevPageDisabled
                                })}
                            >
                                <Anchor
                                    disabled={prevPageDisabled}
                                    onClick={(event) => {
                                        const prevPage = page > 1 ? page - 1 : page;

                                        if (prevPage !== this.state.page) {
                                            this.setState({ page: prevPage });
                                        }

                                        this.changePage({ page: prevPage });
                                    }}
                                >
                                    {prevPageRenderer()}
                                </Anchor>
                            </div>
                            {(type !== 'reduced' && type !== 'minor') &&
                                <div className='pagination-input'>
                                    {'Page '}
                                    <AutosizeInput
                                        value={this.state.page}
                                        onChange={(event) => {
                                            const page = Number(event.target.value);
                                            if (isNaN(page)) {
                                                return;
                                            }
                                            this.setState({ page: limit(page, 1, totalPages) });
                                        }}
                                        onKeyPress={(event) => {
                                            if (event.key !== 'Enter') {
                                                return;
                                            }

                                            let { page } = this.state;
                                            page = limit(page, 1, totalPages);

                                            if (page !== this.state.page) {
                                                this.setState({ page: page });
                                            }

                                            this.changePage({ page: page });
                                        }}
                                    />
                                    {' of '}
                                    {totalPages}
                                </div>
                            }
                            <div
                                className={classNames({
                                    ['disabled']: nextPageDisabled
                                })}
                            >
                                <Anchor
                                    disabled={nextPageDisabled}
                                    onClick={(event) => {
                                        const nextPage = page < totalPages ? page + 1 : page;

                                        if (nextPage !== this.state.page) {
                                            this.setState({ page: nextPage });
                                        }

                                        this.changePage({ page: nextPage });
                                    }}
                                >
                                    {nextPageRenderer()}
                                </Anchor>
                            </div>
                            <div
                                className={classNames({
                                    ['disabled']: nextPageDisabled
                                })}
                            >
                                <Anchor
                                    disabled={nextPageDisabled}
                                    onClick={(event) => {
                                        const nextPage = totalPages;

                                        if (nextPage !== this.state.page) {
                                            this.setState({ page: nextPage });
                                        }

                                        this.changePage({ page: nextPage });
                                    }}
                                >
                                    {lastPageRenderer()}
                                </Anchor>
                            </div>
                        </div>
                    </div>
                    <div className='pagination-records'>
                        {pageRecordsRenderer({ totalRecords, from, to })}
                    </div>
                </div>
            </div>
        );
    }
}

TablePagination.propTypes = {
    type: PropTypes.oneOf(['full', 'reduced', 'minor']),
    page: PropTypes.number,
    pageLength: PropTypes.number,
    pageLengthMenu: PropTypes.array,
    totalRecords: PropTypes.number,
    onPageChange: PropTypes.func,
    prevPageRenderer: PropTypes.func,
    nextPageRenderer: PropTypes.func,
    pageRecordsRenderer: PropTypes.func,
    firstPageRenderer: PropTypes.func,
    lastPageRenderer: PropTypes.func
};

TablePagination.defaultProps = {
    type: 'full',
    page: 1,
    pageLength: 10,
    pageLengthMenu: [10, 25, 50, 100],
    totalRecords: 0,
    onPageChange: () => { },
    prevPageRenderer: () => {
        return '‹';
    },
    nextPageRenderer: () => {
        return '›';
    },
    firstPageRenderer: () => {
        return '‹‹';
    },
    lastPageRenderer: () => {
        return '››';
    },
    pageRecordsRenderer: ({ totalRecords, from, to }) => {
        if (totalRecords > 0) {
            return `Displaying ${from} to ${to} of ${totalRecords}`;
        }

        return `Records: ${totalRecords}`;
    }
};

export default TablePagination;