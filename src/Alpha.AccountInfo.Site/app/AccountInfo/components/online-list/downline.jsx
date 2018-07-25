import  React from 'react';

export default class Downline extends React.Component{
    constructor(props) {
        super(props);
    }
    render() {
        const { username } = this.props;

        return (
            <div className="two-col">
                <a href="#">{username}</a>
            </div>
        );
    }
}