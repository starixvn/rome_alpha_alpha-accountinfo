import * as React from 'react';
import Button from '../common/button';

import Cell from '../common/cell';
import CellContainLink from '../common/cell-contain-link';
import Search from './search';
import UserLevel from './user-level';

import CustomerInfoViewModel from '../../models/customer-info-model'

interface ICustomerInfoTabProps {
    customerInfoModel: CustomerInfoViewModel
}

interface ICustomerInfoTabState {
    data: CustomerInfoViewModel,
    rowClasses: string,
    firstCellClass: string,
    secondCellClass: string,
}

export default class CustomerInfoTab extends React.Component<ICustomerInfoTabProps, ICustomerInfoTabState> {
    constructor(props) {
        super(props);
        this.state = {
            data: this.props.customerInfoModel,
            rowClasses: "col-xs-12 row-info",
            firstCellClass: "col-xs-5",
            secondCellClass: "col-xs-7 col-right",
        }
    }

    handleBetListClick = () => { }

    handleStatementClick = () => { }

    buildLevelRow = (username: string, levelName: string, iconClassname?: string): JSX.Element => 
        <UserLevel
            username={username}
            levelname={levelName}
            rowClasses={this.state.rowClasses}
            firstCellClass={this.state.firstCellClass}
            secondCellClass={this.state.secondCellClass}
            hrefLink="#"
            iconClassname={iconClassname} />

    render() {
        const rowClasses = "col-xs-12 row-info";
        const firstCellClass = "col-xs-5";
        const secondCellClass = "col-xs-7 col-right";

        let roleId = this.state.data.RoleId;
        let actorRoleId = this.state.data.ActorRoleId;

        let masterRow = actorRoleId == 4 && roleId < 4 ?
            this.buildLevelRow(this.state.data.MasterName, "Master", roleId == 3 ? "icon-viewlog" : null) : null;

        let agentRow = (actorRoleId == 4 || actorRoleId == 3) && this.state.data.RoleId < 3 ?
            this.buildLevelRow(this.state.data.AgentName, "Agent", roleId == 2 ? "icon-viewlog" : null) : null;

        let memberRow = roleId == 1 ?
            this.buildLevelRow(this.state.data.MemberName, "Customer", "icon-viewlog") : null;

        return (
            <div className="block-info" id="blockInfo">
                <Search username={this.state.data.UserName} />

                {masterRow}
                {agentRow}
                {memberRow}

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