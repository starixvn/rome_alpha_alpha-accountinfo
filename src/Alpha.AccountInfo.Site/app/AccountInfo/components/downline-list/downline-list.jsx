import React from 'react';

export default class DownlineList extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="swiper-slide" >
                {this.props.usernames.map((username, index) => {
                    return <div className="two-col" key={index}>
                        <a href="#">{username}</a>
                        </div>
                })}
            </div>
        )
    }
}
