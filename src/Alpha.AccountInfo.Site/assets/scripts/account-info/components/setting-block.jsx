import React from 'react';
import { Link } from 'react-router-dom';
import { NoInfo } from '../components/common/no-info';

const SettingBlock = ({ resources, settingData }) => {
    const header = <Link to="/" className="feature-item">
        <span className="icon-settings" />
        {resources.Setting}
    </Link>;

    const content = settingData !== null ?
        settingData.map((value, index) => {
            return <div className="col-xs-6" key={index}>
                <a href={value.Url} target="main">{value.Text}</a>
            </div>;
        }) : <NoInfo text={resources.NoInfo} />;

    return (
        <div className="feature-detail block-settings">
            {header}
            <div className="setting">
                {content}
            </div>            
        </div>
    );
};

export default SettingBlock;