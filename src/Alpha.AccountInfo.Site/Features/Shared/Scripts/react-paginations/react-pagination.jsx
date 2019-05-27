import * as React from 'react';
import classNames from 'classnames';
import PropTypes from 'prop-types';
import AutosizeInput from './autosize-input';

const limit = (value, min, max) => {
    return Math.max(min, Math.min(max, value));
};

class ReactPagination extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            page: this.props.page
        };

        this.setCurrentPage = this.setCurrentPage.bind(this);
        this.changePage = this.changePage.bind(this);
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

    setCurrentPage(page) {
        if (page !== this.state.page) {
            this.setState({ page: page });
        }
    }

    render() {
        const {
            pageResource,
            ofResource,
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

        const pageLength = this.props.pageLength || pageLengthMenu[0] || 50;
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
                    <div>
                        <select className='pagination-page-list'
                            value={pageLength}
                            onChange={(e) => {
                                const pageSize = parseFloat(e.currentTarget.value);
                                this.changePage({
                                    page: (pageSize !== pageLength) ? 1 : page,
                                    pageLength: pageSize
                                });
                            }}>
                            {pageLengthMenu.map(val =>
                                <option
                                    key={val}
                                    value={val}
                                    className={classNames({ ['selected']: pageLength === val })}
                                >
                                    {val}
                                </option>
                            )}
                        </select>
                    </div>
                    <div className='pagination'>
                        <PaginationButton
                            disabled={prevPageDisabled}
                            page={1}
                            pageRender={firstPageRenderer}
                            changePage={this.changePage}
                            setCurrentPage={this.setCurrentPage}
                        />
                        <PaginationButton
                            disabled={prevPageDisabled}
                            page={page > 1 ? page - 1 : page}
                            pageRender={prevPageRenderer}
                            changePage={this.changePage}
                            setCurrentPage={this.setCurrentPage}
                        />

                        <div className='pagination-input'>
                            {`${pageResource} `}
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

                                    this.setCurrentPage(page);
                                    this.changePage({ page: page });
                                }}
                            />
                            {` ${ofResource} `}
                            {totalPages}
                        </div>

                        <PaginationButton
                            disabled={nextPageDisabled}
                            page={page < totalPages ? page + 1 : page}
                            pageRender={nextPageRenderer}
                            changePage={this.changePage}
                            setCurrentPage={this.setCurrentPage}
                        />
                        <PaginationButton
                            disabled={nextPageDisabled}
                            page={totalPages}
                            pageRender={lastPageRenderer}
                            changePage={this.changePage}
                            setCurrentPage={this.setCurrentPage}
                        />
                    </div>
                    <div className='pagination-records'>
                        {pageRecordsRenderer({ totalRecords, from, to })}
                    </div>
                </div>
            </div>
        );
    }
}

export const PaginationButton = ({ disabled, page, pageRender, changePage, setCurrentPage }) => {
    return <div
        className={classNames({
            ['disabled']: disabled
        })}
    >
        <div
            className='page-button'
            disabled={disabled}
            onClick={(event) => {
                setCurrentPage(page);
                changePage({ page: page });
            }}
        >
            {pageRender()}
        </div>
    </div>
}

ReactPagination.propTypes = {
    pageResource: PropTypes.string,
    ofResource: PropTypes.string,
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

ReactPagination.defaultProps = {
    pageResource: 'Page',
    ofResource: 'of',
    page: 1,
    pageLength: 50,
    pageLengthMenu: [50, 100, 200, 500],
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
        return `Displaying ${from} to ${to} of ${totalRecords}`;
    }
};

export default ReactPagination;