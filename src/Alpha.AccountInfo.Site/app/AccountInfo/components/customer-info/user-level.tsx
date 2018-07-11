import * as React from 'react';
import Cell from '../common/cell';
import CellContainLink from '../common/cell-contain-link';

interface IUserLevelProps {
    levelname: string,
    username: string,
    roleId?: number,
    actorRoleId?: number,
    rowClasses: string,
    firstCellClass: string,
    secondCellClass: string,
    hrefLink: string,
    iconClassname?: string
}

interface IUserLevelState {
}

export default class UserLevel extends React.Component<IUserLevelProps, IUserLevelState> {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className={this.props.rowClasses}>
                <Cell text={this.props.levelname} classnames={this.props.firstCellClass} />
                <CellContainLink text={this.props.username}
                    classnames={this.props.secondCellClass}
                    hrefLink={this.props.hrefLink}
                    iconClassname={this.props.iconClassname} />
            </div>
        );
    }
}