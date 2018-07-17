import  React from 'react';

export default class Downline extends React.Component{
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="two-col">
                <a href="#">{this.props.username}</a>
            </div>
        );
    }
}