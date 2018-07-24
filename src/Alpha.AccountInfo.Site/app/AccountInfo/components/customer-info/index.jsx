import * as React from 'react';
import Button from '../common/button';

import Cell from '../common/cell';
import Search from './search';
import UserLevel from './user-level';

export default class CustomerInfoBlock extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            username: '',
            url: window.accountInfoSiteAppPath + "accountinfo/getcustomerinfoasync01",
            data: {},
            rowClasses: "col-xs-12 row-info",
            firstCellClass: "col-xs-5",
            secondCellClass: "col-xs-7 col-right",
        }

        this.handleSearch = this.handleSearch.bind(this);
        this.loadCutomerInfoFromServer = this.loadCutomerInfoFromServer.bind(this);
    }

    handleBetListClick ()  { }

    handleStatementClick () { }

    handleSearch(e) {
        this.setState({ username: e.target.value });
        this.loadCutomerInfoFromServer();
    }

    loadCutomerInfoFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.state.url + "?username=" + this.state.username);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();
    }

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
            <div className="block-info" id="blockInfo">
              
                <div className="search-box">
                    <input type="text"
                        className="txt-search"
                        placeholder="Username" value={this.state.username}
                        onChange={this.handleSearch} />
                    <span className="icon-search" onClick={this.loadCutomerInfoFromServer}></span>
                </div>


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