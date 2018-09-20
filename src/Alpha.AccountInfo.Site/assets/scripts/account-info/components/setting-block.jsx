import React from 'react';
import { NoInformFeature } from '../components/common/no-inform-feature';
import { HeaderFeature } from '../components/common/header-feature';

const SettingBlock = ({ resources, settingData }) => {
    if (settingData !== null) {
        return (
            <div className="feature-detail block-settings">
                <HeaderFeature  />
                <div className="setting">
                    {settingData.map((value, index) => {
                        return <div className="col-xs-6" key={index}>
                            <a href={value.Url} target="main">{value.Text}</a>
                        </div>;
                    })}
                </div>
            </div>
        );
    } else {
        return <NoInformFeature url="/" iconClass="icon-settings" featureText={resources.Setting} noInform={resources.NoInfo} />;
    }
};

export default SettingBlock;