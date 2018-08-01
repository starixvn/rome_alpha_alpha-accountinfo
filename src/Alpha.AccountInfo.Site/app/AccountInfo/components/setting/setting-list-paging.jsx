import React from 'react';

export default class SettingPaging extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { models, oldMainPath} = this.props;

        return (
            <div className="swiper-slide" >
                {models.map((model, index) => {
                    return <div className="two-col" key={index}>
                        <a href={oldMainPath + model.Url} target="main">{model.Text}</a>
                        </div>
                })}
            </div>
        )
    }
}
