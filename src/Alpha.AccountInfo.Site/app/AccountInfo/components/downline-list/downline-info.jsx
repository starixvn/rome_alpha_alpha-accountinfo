import React from 'react';
//import NoInfomation from '../../components/online-list/no-infomation';
//import DownlineList from '../../components/online-list/downline-list';
//import Swiper from 'swiper';
import DownlineListPaging from './downline-list-paging';
import Swiper from 'react-id-swiper';

export default class DownlineInfo extends React.Component {
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
            // Do something if you want with the group
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
        const { downlineList } = this.props;
        const { slideTo } = this.state;

        //let content = this.props.downlineList.length > 0 ?
        //    <DownlineList usernames={this.props.downlineList} /> : <NoInfomation />;

        const params = {
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
        }

        var results = this.chunkArray(downlineList, 10);

        return (
            <div className="downline-list-container">
                <Swiper {...params} ref={node => { if (node) this.swiper = node.swiper } }>
                    {results.map((result, index) => {
                        return <DownlineListPaging usernames={result} key={index} />
                    })}
                </Swiper>
                <div className="page-control">
                    <span>Page</span>
                    <input type="text" ref="slideTo" className="number" value={slideTo} onChange={this.slideTo} />
                </div>
            </div>
        )
    }
}
