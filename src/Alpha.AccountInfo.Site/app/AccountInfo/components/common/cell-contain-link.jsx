import React from 'react';
import Link from './link';

export default class CellContainLink extends React.Component{
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className={this.props.classNames}>
                <Link title={this.props.title} url={this.props.hrefLink} />
                <span className={this.props.iconClassname}></span>
            </div>
      );
    }
}