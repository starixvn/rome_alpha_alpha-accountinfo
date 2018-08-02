import React from 'react';

const SettingPaging = ({ models, oldMainPath }) => 
    <div className="swiper-slide" >
        {models.map((model, index) => {
            return <div className="two-col" key={index}>
                <a href={oldMainPath + model.Url} target="main">{model.Text}</a>
            </div>
        })}
    </div>

export default SettingPaging;