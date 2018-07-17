import React from 'react';
//import NoInfomation from '../../components/online-list/no-infomation';
//import DownlineList from '../../components/online-list/downline-list';
//import Swiper from 'swiper';


export default class DownlineInfo extends React.Component {
    constructor(props) {
        super(props);

   
    }
    chunkArray (myArray, chunk_size) {
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
    getMembers (separate) {
    var members = ['Member 01', 'Member 02', 'Member 03', 'Member 04', 'Member 05',
        'Member 06', 'Member 07', 'Member 08', 'Member 09', 'Member 10',
        'Member 11', 'Member 12', 'Member 13', 'Member 14', 'Member 15',
        'Member 16', 'Member 17', 'Member 18', 'Member 19', 'Member 20'];
    var slideContainer = '';
    for (var i = 0; i < members.length; i++) {

        if (i % separate == 0) {
            if (i != 0) {
                slideContainer += '</div>';
            }

            slideContainer += '<div class="swiper-slide">';

        }

        var username = members[i];
        slideContainer += '<div class="two-col"><a href="#">' + username + '</a></div>';
    }

    if (i % separate) {
        slideContainer += '</div>';
    }

    //$('#downline-list-wrapper').empty().html(slideContainer);

    return slideContainer;
}
    render() {
        //let content = this.props.downlineList.length > 0 ?
        //    <DownlineList usernames={this.props.downlineList} /> : <NoInfomation />;
       

        //let downlineListSwiper = new Swiper('#downlineListSwiperContainer', {
        //    // If we need pagination
        //    pagination: {
        //        el: '#downlineListPagination',
        //        clickable: true,
        //        renderBullet: function (index, className) {
        //            return '<span class="' + className + '">' + (index + 1) + '</span>';
        //        },
        //    },

        //    // Navigation arrows
        //    navigation: {
        //        nextEl: '.swiper-button-next',
        //        prevEl: '.swiper-button-prev',
        //    },
        //    observer: true,
        //    observeParents: true,
        //    grabCursor: true
        //});
       
        // downlineListSwiper.appendSlide(this.getMembers(6));
        //{ results.map((result, index) => <NavigationSwiper downlineList={result} key={index} />) }
        var results = this.chunkArray(this.props.downlineList, 10);
        return (  
            <div className="swiper-container" id="downlineListSwiperContainer">
                <div className="swiper-wrapper" id="downline-list-wrapper">
                   
                </div>
                <div className="swiper-bottom" id="downlineListPagination"></div>
            </div>
        )
    }
}
