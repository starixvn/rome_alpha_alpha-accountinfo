import * as React from 'react';

interface ISwiperItemProps {
    username: string
}

export default class SwiperItem extends React.Component<ISwiperItemProps, {}> {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="two-col">
                <a href="#">{this.props.username}</a>
            </div>
        )
    }
}
