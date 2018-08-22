import React from 'react';
import Swiper from 'react-id-swiper';
import DownlineListPage from './downline-list-page';
import TotalDownlines from './total-downlines';

export default class DownlineListContent extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            slideTo: ''
        };

        this.slideTo = this.slideTo.bind(this);
    }

    chunkArray(myArray, chunk_size) {
        let index = 0;
        let arrayLength = myArray.length;
        let tempArray = [];

        for (index = 0; index < arrayLength; index += chunk_size) {
            let myChunk = myArray.slice(index, index + chunk_size);
            tempArray.push(myChunk);
        }

        return tempArray;
    }

    slideTo(e) {
        let value = e.target.value;
        this.setState({ slideTo: value });
        const pageNumber = value - 1;
        if (this.swiper) this.swiper.slideTo(pageNumber);
    }
    
    render() {
        const { resources, downlineList, handleSearchFromUserLevel } = this.props;
        const { slideTo } = this.state;

        let results = this.chunkArray(downlineList.DownlineList, 10);

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
            } ;

        return (
            <div>
                <div className="downline-list-container">
                <Swiper {...params} ref={node => { if (node) this.swiper = node.swiper } }>
                    {results.map((result, index) => {
                            return <DownlineListPage usernames={result} handleSearchFromUserLevel={handleSearchFromUserLevel} key={index} />
                    })}
                    </Swiper>
                    {results.length > 1 &&
                        <div className="page-control">
                            <span>{resources.Page}</span>
                            <input type="text" ref="slideTo" className="number" value={slideTo} onChange={this.slideTo} />
                        </div>
                    }
                </div>
                <div><hr /></div>
                {
                    downlineList.TotalStatusAgent != null &&
                    <TotalDownlines levelName="Agents"
                        resources={resources}
                        totalActive={downlineList.TotalStatusAgent.TotalActive}
                        totalClosed={downlineList.TotalStatusAgent.TotalClosed}
                        totalSuspended={downlineList.TotalStatusAgent.TotalSuspend} />
                }
                {
                    downlineList.TotalStatusMember != null &&
                    <TotalDownlines levelName="Members"
                        resources={resources}
                        totalActive={downlineList.TotalStatusMember.TotalActive}
                        totalClosed={downlineList.TotalStatusMember.TotalClosed}
                        totalSuspended={downlineList.TotalStatusMember.TotalSuspend} />
                }
            </div>    
        )
    }
}
