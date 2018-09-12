import React from 'react';
import Swiper from 'react-id-swiper';

export default class Paging extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            slideTo: ''
        };
        this.slideTo = this.slideTo.bind(this);
    }

    chunkArray(myArray, chunk_size) {
        var index = 0;
        var arrayLength = myArray.length;
        var tempArray = [];

        for (index = 0; index < arrayLength; index += chunk_size) {
            let myChunk = myArray.slice(index, index + chunk_size);
            tempArray.push(myChunk);
        }

        return tempArray;
    }

    slideTo(e) {
        const value = e.target.value;
        this.setState({ slideTo: value });
        const pageNumber = value - 1;
        if (this.swiper) this.swiper.slideTo(pageNumber);
    }

    render() {
        const { resourcePage, values, renderCallback } = this.props;
        const { slideTo } = this.state;

        let pageArray = this.chunkArray(values, 10);
        let length = pageArray.length;
        let params = length > 1 ?
            {
                pagination: '.swiper-pagination',
                paginationClickable: true,
                paginationBulletRender: (swiper, index, className) => {
                    if (index < 2 || index === length - 1) {
                        return `<span class="${className}">${index + 1}</span>`;
                    }

                    if (index > 2 && index < length - 3) {
                        return '';
                    }

                    return '.';
                },
                nextButton: '.swiper-button-next',
                prevButton: '.swiper-button-prev',
                observer: true,
                observeParents: true,
                grabCursor: true,
                containerClass: 'swiper-container block-downline-list'
            } :
            {
                observer: true,
                observeParents: true,
                grabCursor: true,
                containerClass: 'swiper-container block-downline-list'
            };

        return (
            <div className="downline-list-container">
                <Swiper {...params} ref={node => { if (node) this.swiper = node.swiper }}>
                    {pageArray.map((page, pageIndex) => {
                        return <div className="swiper-slide" key={pageIndex}>
                            {page.map((value, index) => {
                                return <div className="two-col" key={index}>
                                    {renderCallback(value)}
                                </div>;
                            })}
                        </div>;
                    })}
                </Swiper>
                {pageArray.length > 4 &&
                    <div className="page-control">
                        <span>{resourcePage}</span>
                        <input type="text" ref="slideTo" className="number" value={slideTo} onChange={this.slideTo} />
                    </div>
                }
            </div>
        );
    }
}
