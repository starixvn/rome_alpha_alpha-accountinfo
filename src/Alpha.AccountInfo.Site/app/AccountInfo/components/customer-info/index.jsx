import * as React from 'react';
import Button from '../common/button';

import Cell from '../common/cell';
import CellContainLink from '../common/cell-contain-link';
import Search from './search';
import UserLevel from './user-level';

export default class CustomerInfoBlock extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            data: this.props.customerInfoModel,
            rowClasses: "col-xs-12 row-info",
            firstCellClass: "col-xs-5",
            secondCellClass: "col-xs-7 col-right",
        }
    }

    handleBetListClick ()  { }

    handleStatementClick () { }

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
        const rowClasses = this.state.rowClasses;
        const firstCellClass = this.state.firstCellClass;
        const secondCellClass = this.state.secondCellClass;

        let roleId = this.state.data.RoleId;

        return (
            <div className="block-info" id="blockInfo">
                <Search username={this.state.data.UserName} />

                {this.state.data.ShowMasterName ? this.buildLevelRow(this.state.data.MasterName, "Master", roleId == 3 ? "icon-viewlog" : null) : null}

                {this.state.data.ShowAgentName ? this.buildLevelRow(this.state.data.AgentName, "Agent", roleId == 2 ? "icon-viewlog" : null) : null}

                {this.state.data.ShowMemberName ? this.buildLevelRow(this.state.data.MemberName, "Customer", "icon-viewlog") : null}

                <div className={rowClasses}>
                    <Cell text="Name" classNames={firstCellClass} />
                    <Cell text="Thien Nguyen" classNames={secondCellClass} />
                </div>

                <div className={rowClasses}>
                    <Cell text="Phone" classNames={firstCellClass} />
                    <Cell text={this.state.data.Phone} classNames={secondCellClass} />
                </div>

                <div className={rowClasses}>
                    <Cell text="Fax" classNames={firstCellClass} />
                    <Cell text={this.state.data.Fax} classNames={secondCellClass} />
                </div>

                <div className={rowClasses + " text-center list-button"}>
                    <Button text="Bet List" classNames="btn-default with-level" onClick={this.handleBetListClick} />
                    <Button text="Statement" classNames="btn-default with-level" onClick={this.handleStatementClick} />
                </div>
            </div>
        );
    }
}