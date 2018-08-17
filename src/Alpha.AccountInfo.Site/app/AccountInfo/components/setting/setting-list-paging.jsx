import React from 'react';

const SettingPaging = ({ models }) => 
    <div className="swiper-slide" >
        {models.map((model, index) => {
            return <div className="two-col" key={index}>
                <a href={model.Url} target="main">{model.Text}</a>
            </div>
        })}
    </div>

export default SettingPaging;