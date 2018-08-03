import * as React from 'react';
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

    buildLevelRow(username, levelName, iconClassname, handleSearchFromUserLevel) {
        return <UserLevel
            username={username}
            levelname={levelName}
            rowClasses={this.state.rowClasses}
            firstCellClass={this.state.firstCellClass}
            secondCellClass={this.state.secondCellClass}
            hrefLink="#"
            iconClassname={iconClassname}
            handleSearchFromUserLevel={handleSearchFromUserLevel} />
    }

    render() {
        const { handleSearchFromUserLevel } = this.props;

        const {
            rowClasses,
            firstCellClass,
            secondCellClass,
            data
        } = this.state;

        let roleId = data.RoleId;


        return (
            <div >
                {data.ShowMasterName ? this.buildLevelRow(data.MasterName, "Master", roleId == 3 ? "icon-viewlog" : null, handleSearchFromUserLevel) : null}

                {data.ShowAgentName ? this.buildLevelRow(data.AgentName, "Agent", roleId == 2 ? "icon-viewlog" : null, handleSearchFromUserLevel) : null}

                {data.ShowMemberName ? this.buildLevelRow(data.MemberName, "Customer", "icon-viewlog", handleSearchFromUserLevel) : null}

                <div className={rowClasses}>
                    <div className={firstCellClass}>Name</div>
                    <div className={secondCellClass}>{data.username}</div>
                </div>

                <div className={rowClasses}>
                    <div className={firstCellClass}>Phone</div>
                    <div className={secondCellClass}>{data.Phone}</div>
                </div>

                <div className={rowClasses}>
                    <div className={firstCellClass}>Fax</div>
                    <div className={secondCellClass}>{data.Fax}</div>
                </div>

                <div className={rowClasses + " text-center list-button"}>
                    {
                        roleId == 1 &&
                        <a className="btn-default with-level" href={data.BetListLink} target="main">Bet List</a>
                    }

                    {
                        roleId == 2 &&
                        <a className="btn-default with-level" href={data.CustomerListLink} target="main">Member List</a>
                    }

                    {
                        roleId == 3 &&
                        <a className="btn-default with-level" href={data.CustomerListLink} target="main">Agent List</a>
                    }

                    <a className="btn-default with-level" href={data.StatemenLink} target="main">Statement</a>
                </div>
            </div>
        );
    }
}