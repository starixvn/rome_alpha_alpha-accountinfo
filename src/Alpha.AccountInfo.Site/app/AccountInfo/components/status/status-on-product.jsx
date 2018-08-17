import React from 'react';
import CellContainLink from '../common/cell-contain-link';

export default class StatusOnProduct extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpen = this.handleOpen.bind(this);
    }

    handleOpen(e) {
        window.top.ageWnd.open(this.props.url);
    };

    render() {
        const { resources, disabled, statusId, title, confirmMessage, url } = this.props;

        return (
            <div>
                <CellContainLink title={title}
                    statusId={statusId}
                    handleClick={this.handleOpen}
                    classNames="col"
                    iconClassname="icon-ic_list"
                    hrefLink="#" />
            </div>  
        );
    }
}