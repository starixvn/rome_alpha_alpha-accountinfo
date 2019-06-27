import React from 'react';
import CustomAnchorTag from '../common/custom-anchor-tag';

const Settings = ({ resources, settingData, mobileView }) => {
    if (settingData === null || settingData.length === 0) {
        return mobileView ? <div className='no-information'>{resources.NoInfo}</div> : null;
    }

    return <div className='setting'>
        {
            settingData.map((value, index) => {
                return (
                    <div className='col-xs-6' key={index}>
                        <CustomAnchorTag href={value.Url} mobileView={mobileView}>
                            {value.Text}
                        </CustomAnchorTag>
                    </div>
                );
            })}
    </div>;
};

export default Settings;