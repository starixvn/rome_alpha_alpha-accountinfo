import React from 'react';
import Button from '../common/button';

import Cell from '../common/cell';
import CellWithLink from '../common/cell-contain-link';

export default class Search extends React.Component {
    constructor(props) {
        super(props);
    }

    handleChange () {

    }

    render() {
        const { username } = this.props;

        return (
            <div className="search-box">
                <input type="text"
                    className="txt-search"
                    placeholder="Username" value={username}
                    onChange={this.handleChange} />
                <span className="icon-search"></span>
            </div>

        );
    }
}