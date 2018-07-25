import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';


export default class SettingBlock extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        const { handleClick, dataId, expanded } = this.props;

        return (
            <CustomizedAccordionItem
                title="Settings"
                iconClassname="icon-settings"
                expanded={expanded}
                handleClick={handleClick}
                dataId={dataId}>
                <div className="swiper-container" id="settingsSwiperContainer">
                    <div className="swiper-wrapper settings" id="setting-wrapper">
                    </div>
                </div>
            </CustomizedAccordionItem>
      );
    }
}