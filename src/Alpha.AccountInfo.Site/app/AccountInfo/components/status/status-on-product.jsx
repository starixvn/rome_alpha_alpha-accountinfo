import React from 'react';
import CellContainLink from '../common/cell-contain-link';
import { Button, Modal } from 'react-bootstrap';

export default class StatusOnProduct extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            show: false,
            value : ''
        }
        this.handleOpen = this.handleOpen.bind(this);
        this.handleClose = this.handleClose.bind(this);
    }

    handleOpen(e) {
        this.setState({ show: true});
    };

    handleClose(e) {
        this.setState({ show: false });
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

                <Modal show={this.state.show} onHide={this.handleClose}>
                    <Modal.Header closeButton>
                        <Modal.Title id="contained-modal-title">
                            {confirmMessage == null && `${title}`}
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <iframe width="475" height="220" src={url} frameborder="0" allowfullscreen></iframe>
                    </Modal.Body>
                </Modal>
                </div>
            
        );
    }
}