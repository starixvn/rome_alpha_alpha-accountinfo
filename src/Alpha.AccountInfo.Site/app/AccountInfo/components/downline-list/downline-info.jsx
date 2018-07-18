import React from 'react';
//import NoInfomation from '../../components/online-list/no-infomation';
//import DownlineList from '../../components/online-list/downline-list';
//import Swiper from 'swiper';
import DownlineList from './downline-list';
import Swiper from '../../../../node_modules/react-id-swiper/lib/index';

export default class DownlineInfo extends React.Component {
    constructor(props) {
        super(props);


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
    render() {
        //let content = this.props.downlineList.length > 0 ?
        //    <DownlineList usernames={this.props.downlineList} /> : <NoInfomation />;


        const params = {
            pagination: {
                el: '#downlineListPagination',
                clickable: true,
                renderBullet: function (swiper, index, className) {
                    return '<span class="' + className + '">' + (index + 1) + '</span>';
                },
            },
            navigation: {
                nextEl: '.swiper-button-next',
                prevEl: '.swiper-button-prev',
            },
            spaceBetween: 30,
            observer: true,
            observeParents: true,
            grabCursor: true
        }

        var results = this.chunkArray(this.props.downlineList, 10);

        return (
            <div className="swiper-container" id="downlineListSwiperContainer">
                <div className="swiper-wrapper" id="downline-list-wrapper">
                    <Swiper {...params}>
                        {results.map((result, index) => {
                            return <DownlineList usernames={result} key={index} />
                        })}
                    </Swiper>
                </div>
                <div className="swiper-bottom">
                    <div className="swiper-button-prev"><span className="icon-chevron-left"></span></div>
                    <div className="swiper-button-next"><span className="icon-chevron-right"></span></div>
                    <div className="swiper-pagination" id="downlineListPagination"></div>
                    <div className="page-control">
                        <span>Page</span>
                        <input type="text" className="number" value="2" />
                    </div>
                </div>
            </div>


        )
    }
}
