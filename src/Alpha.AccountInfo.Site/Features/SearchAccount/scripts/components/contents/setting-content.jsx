import React from 'react';
import CustomAnchorTag from '../common/custom-anchor-tag';

const SettingContent = ({ resources, settingData, mobileView }) =>
    <div className="setting">
        {
            settingData.map((value, index) => {
                return <div className="col-xs-6" key={index}>
                    <CustomAnchorTag href={value.Url} mobileView={mobileView}>{value.Text} </CustomAnchorTag>
                </div>;
            })}
    </div>;

export default SettingContent;