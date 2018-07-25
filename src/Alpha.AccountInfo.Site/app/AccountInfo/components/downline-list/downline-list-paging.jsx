import React from 'react';

export default class DownlineListPaging extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { usernames } = this.props;

        return (
            <div className="swiper-slide" >
                {usernames.map((username, index) => {
                    return <div className="two-col" key={index}>
                        <a href="#">{username.UserName}</a>
                        </div>
                })}
            </div>
        )
    }
}
