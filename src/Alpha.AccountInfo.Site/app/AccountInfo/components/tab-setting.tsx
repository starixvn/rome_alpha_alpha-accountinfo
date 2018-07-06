import * as React from 'react';

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
                <div className="panel-heading" role="tab" id="headingSettings">
                    <h4 className="panel-title">
                        <a className="collapsed" role="button" data-toggle="collapse" data-parent="#infoTab" href="#collapseSettings" aria-expanded="false" aria-controls="collapseSettings">
                            <span className="icon-settings"></span> Settings
                            </a>
                    </h4>
                </div>
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