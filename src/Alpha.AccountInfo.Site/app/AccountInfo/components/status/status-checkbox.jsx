import React from 'react';
import { Button, Modal } from 'react-bootstrap';
import DraggableModalDialog from '../common/draggable-modal-dialog';

export default class StatusCheckbox extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            show: false,
            value: ''
        }
        this.handleOpen = this.handleOpen.bind(this);
        this.handleClose = this.handleClose.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleOpen(e) {

        this.props.confirmMessage == undefined ? window.top.ageWnd.open(this.props.url) :window.top.bootboxConfirm(this.props.confirmMessage);
        this.setState({
            show: true
        });
    };

    handleClose(e) {
        this.setState({ show: false });
    };
    handleSubmit(e) {
        this.handleClose(e);
    };

    render() {
        const { resources, classNames, checked, disabled, title, id, confirmMessage, url } = this.props;
        const checkBoxClass = disabled ? `${classNames} disabled` : classNames;
        //<Modal dialogComponentClass={DraggableModalDialog} show={this.state.show} onHide={this.handleClose}>
        //    <Modal.Header closeButton>
        //        <Modal.Title id="contained-modal-title">
        //            {confirmMessage == null && `${title}`}
        //        </Modal.Title>
        //    </Modal.Header>
        //    <Modal.Body>
        //        <div className="col-xs-12 row-info popup-bottom list-button">
        //            {
        //                confirmMessage != null &&
        //                <div>
        //                    {confirmMessage}
        //                </div>
        //            }

        //            {
        //                confirmMessage == null &&
        //                <iframe width="475" height="220" src={url} frameborder="0" allowfullscreen></iframe>
        //            }
        //        </div>
        //    </Modal.Body>
        //    {
        //        confirmMessage != null &&
        //        <Modal.Footer>
        //            <Button onClick={this.handleClose}>{resources.Cancel}</Button>
        //            <Button onClick={this.handleSubmit}>{resources.Submit}</Button>
        //        </Modal.Footer>
        //    }
        //</Modal>
        return (
            <div>
                <div className={checkBoxClass} onClick={this.handleOpen}>
                    <input id={id}
                        type="checkbox"
                        disabled={disabled}
                        checked={checked}
                        onChange={this.handleOpen} />
                    <label className="label-text" htmlFor={id}>{title}</label>
                </div>
            </div>
        );
    }
}