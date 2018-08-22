import React from 'react';
import SettingPaging from './setting-list-paging';
import Swiper from 'react-id-swiper';

export default class SettingContent extends React.Component {
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
        const { resources, settings } = this.props;
        const { slideTo } = this.state;

        var results = this.chunkArray(settings, 10);

        let params = results.length > 1 ?
            {
                pagination: '.swiper-pagination',
                paginationClickable: true,
                paginationBulletRender: (swiper, index, className) => {
                    return `<span class="${className}">${index + 1}</span>`
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
                <Swiper {...params} ref={node => { if (node) this.swiper = node.swiper } }>
                    {results.map((result, index) => {
                        return <SettingPaging models={result} key={index} />
                    })}
                </Swiper>
                {results.length > 4 &&
                    <div className="page-control">
                        <span>{resources.Page}</span>
                        <input type="text" ref="slideTo" className="number" value={slideTo} onChange={this.slideTo} />
                    </div>
                }
            </div>
        )
    }
}
