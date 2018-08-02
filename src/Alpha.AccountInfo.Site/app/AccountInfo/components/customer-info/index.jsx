import * as React from 'react';
import LinkStyleButton from '../common/link-style-button';
import Cell from '../common/cell';
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
                    <Cell title="Name" classNames={firstCellClass} />
                    <Cell title={data.username} classNames={secondCellClass} />
                </div>

                <div className={rowClasses}>
                    <Cell title="Phone" classNames={firstCellClass} />
                    <Cell title={data.Phone} classNames={secondCellClass} />
                </div>

                <div className={rowClasses}>
                    <Cell title="Fax" classNames={firstCellClass} />
                    <Cell title={data.Fax} classNames={secondCellClass} />
                </div>

                <div className={rowClasses + " text-center list-button"}>
                    {
                        roleId == 1 && <LinkStyleButton title="Bet List" classNames="btn-default with-level" targetIframe="main" url={data.BetListLink} />
                    }

                    {
                        roleId == 2 && <LinkStyleButton title="Member List" classNames="btn-default with-level" targetIframe="main" url={data.CustomerListLink} />
                    }

                    {
                        roleId == 3 && <LinkStyleButton title="Agent List" classNames="btn-default with-level" targetIframe="main" url={data.CustomerListLink} />
                    }

                    <LinkStyleButton title="Statement" classNames="btn-default with-level" targetIframe="main" url={data.StatemenLink} />
                </div>
            </div>
        );
    }
}