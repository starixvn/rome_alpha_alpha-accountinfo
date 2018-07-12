import * as React from 'react';
import NoInfomation from '../../components/online-list/no-infomation';
import DownlineList from '../../components/online-list/downline-list';

interface IDownlineInfoProps {
    downlineList: string[]
}

export default class DownlineInfo extends React.Component<IDownlineInfoProps, {}> {
    constructor(props) {
        super(props);
    }
    render() {
        let content = this.props.downlineList.length > 0 ?
            <DownlineList usernames={this.props.downlineList} /> : <NoInfomation />;

        return (
            <div className="swiper-container" id="downlineListSwiperContainer">
                <div className="swiper-wrapper" id="downline-list-wrapper">
                    {content}
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
