import * as React from 'react';
import Button from '../common/button';

import Cell from '../common/cell';
import Search from './search';
import UserLevel from './user-level';

export default class CustomerInfoBlock extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            data: this.props.data,
            rowClasses: "col-xs-12 row-info",
            firstCellClass: "col-xs-5",
            secondCellClass: "col-xs-7 col-right",
        }
    }

    componentWillReceiveProps(props) {
        this.setState({ data: props.data });
    }

    handleBetListClick() { }

    handleStatementClick() { }

    buildLevelRow(username, levelName, iconClassname) {
        return <UserLevel
            username={username}
            levelname={levelName}
            rowClasses={this.state.rowClasses}
            firstCellClass={this.state.firstCellClass}
            secondCellClass={this.state.secondCellClass}
            hrefLink="#"
            iconClassname={iconClassname} />
    }

    render() {
        const {
            rowClasses,
            firstCellClass,
            secondCellClass,
            data
        } = this.state;

        let roleId = data.RoleId;

        return (
            <div >
                {this.state.data.ShowMasterName ? this.buildLevelRow(this.state.data.MasterName, "Master", roleId == 3 ? "icon-viewlog" : null) : null}

                {this.state.data.ShowAgentName ? this.buildLevelRow(this.state.data.AgentName, "Agent", roleId == 2 ? "icon-viewlog" : null) : null}

                {this.state.data.ShowMemberName ? this.buildLevelRow(this.state.data.MemberName, "Customer", "icon-viewlog") : null}

                <div className={rowClasses}>
                    <Cell title="Name" classNames={firstCellClass} />
                    <Cell text={this.state.data.username} classNames={secondCellClass} />
                </div>

                <div className={rowClasses}>
                    <Cell title="Phone" classNames={firstCellClass} />
                    <Cell title={this.state.data.Phone} classNames={secondCellClass} />
                </div>

                <div className={rowClasses}>
                    <Cell title="Fax" classNames={firstCellClass} />
                    <Cell title={this.state.data.Fax} classNames={secondCellClass} />
                </div>

                <div className={rowClasses + " text-center list-button"}>
                    <Button title="Bet List" classNames="btn-default with-level" handleClick={this.handleBetListClick} />
                    <Button title="Statement" classNames="btn-default with-level" handleClick={this.handleStatementClick} />
                </div>
            </div>
        );
    }
}