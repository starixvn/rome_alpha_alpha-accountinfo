import React from 'react';
//import NoInfomation from '../../components/online-list/no-infomation';
//import DownlineList from '../../components/online-list/downline-list';
//import Swiper from 'swiper';
import DownlineListPaging from './downline-list-paging';
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
            pagination: '.swiper-pagination',
            paginationClickable: true,
            paginationBulletRender: (swiper, index, className) => {
                return `<span class="${className}">${index + 1}</span>`
            },
            nextButton: '.swiper-button-next',
            prevButton: '.swiper-button-prev',
            observer: true,
            observeParents: true,
            grabCursor: true
        }

        var results = this.chunkArray(this.props.downlineList, 10);

        return (
            <div className="col-xs-5">
                <Swiper {...params}>
                    {results.map((result, index) => {
                        return <DownlineListPaging usernames={result} key={index} />
                    })}
                </Swiper>
            </div>
        )
    }
}
