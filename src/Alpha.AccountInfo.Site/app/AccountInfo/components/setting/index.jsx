import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';


export default class SettingBlock extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <CustomizedAccordionItem title="Settings" iconClassname="icon-settings">
                <div className="swiper-container" id="settingsSwiperContainer">
                    <div className="swiper-wrapper settings" id="setting-wrapper">
                    </div>
                </div>
            </CustomizedAccordionItem>
      );
    }
}