import React from 'react';

const SettingContent = ({ resources, settingData }) =>
    <div className="setting">
        {
            settingData.map((value, index) => {
                return <div className="col-xs-6" key={index}>
                    <a href={value.Url} target="main">{value.Text}</a>
                </div>;
            })}
    </div>;

export default SettingContent;