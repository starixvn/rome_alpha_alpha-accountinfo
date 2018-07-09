import * as React from 'react';
import HeadingTab from '../common/tab-heading';

interface ISettingTabProps {
}

interface ISettingTabState {
}

export default class SettingTab extends React.Component<ISettingTabProps, ISettingTabState> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="panel panel-default">
                <HeadingTab text="Settings" id="headingSettings" iconClassname="icon-settings" tabHref="collapseSettings" />

                <div id="collapseSettings" className="panel-collapse collapse" role="tabpanel" aria-labelledby="headingSettings">
                    <div className="panel-body panel-settings">
                        <div className="swiper-container" id="settingsSwiperContainer">
                                <div className="swiper-wrapper settings" id="setting-wrapper">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
      );
    }
}