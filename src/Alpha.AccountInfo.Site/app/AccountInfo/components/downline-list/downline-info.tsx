import * as React from 'react';

interface IDownlineInfoProps {

}

export default class DownlineInfo extends React.Component<IDownlineInfoProps, {}> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="swiper-container" id="downlineListSwiperContainer">
                <div className="swiper-wrapper" id="downline-list-wrapper"></div>
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
