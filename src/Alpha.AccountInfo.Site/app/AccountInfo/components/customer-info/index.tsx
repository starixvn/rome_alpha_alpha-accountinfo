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

    buildMasterRow = (actorRoleId: number, roleId: number, username: string) => {
        if (this.state.data.roleId == 3) {
            return this.buildLevelRow(username, "Master", roleId == 4 ? "icon-viewlog" : null);
        }
        else {
            return null;
        }
    }

    buildAgentRow = (actorRoleId: number, roleId: number, username: string) => {
        if (this.state.data.actorRoleId == 2) {
            return this.buildLevelRow(username, "Agent", roleId == 3? "icon-viewlog" : null);
        }
        else {
            return null;
        }
    }

    buildMemberRow = (actorRoleId: number, roleId: number, username: string) => {
        if (this.state.data.actorRoleId == 1) {
            return this.buildLevelRow(username, "Customer", roleId == 2 ? "icon-viewlog" : null);
        }
        else {
            return null;
        }
    }

    buildLevelRow = (username: string, levelName: string, iconClassname?: string) => {
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
        const rowClasses = "col-xs-12 row-info";
        const firstCellClass = "col-xs-5";
        const secondCellClass = "col-xs-7 col-right";

        return (
            <div className="block-info" id="blockInfo">
                <Search username={this.state.data.userName}/>

                <div className={rowClasses}>
                    <Cell text="Master" classnames={firstCellClass} />
                    <CellContainLink text="Master01"
                        classnames={secondCellClass + " font-weight-normal"}
                        hrefLink="#" />
                </div>

                <div className={rowClasses}>
                    <Cell text="Agent" classnames={firstCellClass} />
                    <CellContainLink text="Agent01"
                        classnames={secondCellClass + " font-weight-normal"}
                        hrefLink="#" />
                </div>

                <div className={rowClasses + " highlight"}>
                    <Cell text="Customer" classnames={firstCellClass} />
                    <CellContainLink text={this.state.data.userName}
                        classnames={secondCellClass}
                        hrefLink="#"
                        iconClassname="icon-viewlog" />
                </div>

                <div className={rowClasses}>
                    <Cell text="Name" classnames={firstCellClass} />
                    <Cell text="Thien Nguyen" classnames={secondCellClass} />
                </div>

                <div className={rowClasses}>
                    <Cell text="Phone" classnames={firstCellClass} />
                    <Cell text={this.state.data.phone} classnames={secondCellClass} />
                </div>

                <div className={rowClasses}>
                    <Cell text="Fax" classnames={firstCellClass} />
                    <Cell text={this.state.data.fax} classnames={secondCellClass} />
                </div>

                <div className={rowClasses + " text-center list-button"}>
                    <Button text="Bet List" classnames="btn-default with-level" onClick={this.handleBetListClick} />
                    <Button text="Statement" classnames="btn-default with-level" onClick={this.handleStatementClick} />
                </div>
            </div>
        );
    }
}