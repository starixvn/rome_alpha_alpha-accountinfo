import React from 'react';
import Downline from './downline';

export default class DownlineList extends React.Component{
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div >
                {this.props.usernames.map((username, index) => <Downline username={username} key={index}/>)}
            </div>
        );
    }
}