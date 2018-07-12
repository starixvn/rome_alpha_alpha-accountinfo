import * as React from 'react';
import Link from './link';
import Cell from './cell';

interface ICellContainLinkProps {
    classNames?: string,
    hrefLink?: string,
    text: string,
    iconClassname?: string
}

interface ICellContainLinkState {
    text: string
}

export default class CellContainLink extends React.Component<ICellContainLinkProps, ICellContainLinkState> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className={this.props.classNames}>
                <Link text={this.props.text} url={this.props.hrefLink} />
                <span className={this.props.iconClassname}></span>
            </div>
      );
    }
}