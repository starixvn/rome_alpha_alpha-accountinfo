import * as React from 'react';
import NumberFormat from 'react-number-format';
import CellContainNumberFormat from '../common/cell-contain-number-format';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import { accountInfoAPI } from '../../api-client';
import { Button, Modal } from 'react-bootstrap';
import DraggableModalDialog from '../common/draggable-modal-dialog';

export default class GivenCreditModal extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            show: false,
            newGivenCredit: 0,
            hideMessages: true,
            updateSuccess: false,
            message: ''
        };

        this.handleOpen = this.handleOpen.bind(this);
        this.handleClose = this.handleClose.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleOpen(e) {
        let givenCreditUrl = `${accountInfoSiteAppPath}AccountInfo/GivenCredit?SearchAccountId=${this.props.custId}&SearchAccountName=${this.props.username}&SearchAccountRoleId=${this.props.roleId}&CurrentGivenCredit=${this.props.balanceInfo.GivenCredit}`;
        window.top.ageWnd.open(givenCreditUrl, "resources.UpdateGivenCredit");
        //this.setState({
        //    show: true,
        //    newGivenCredit: 0,
        //    hideMessages: true,
        //    updateSuccess: false,
        //    message: ''
        //});
    };

    handleClose (e)  {
        this.setState({ show: false });
    };

    handleSubmit(e) {
        var params = {
            SearchAccountId: this.props.custId,
            SearchAccountRoleId: this.props.roleId,
            CreditAmount: this.state.newGivenCredit
        };

        accountInfoAPI.updateGivenCredit(params).then((data) => {
            let self = this;
            this.setState({
                hideMessages: false,
                message: data.Message,
                updateSuccess: data.Code == 0
            });

            if (data.Code == 0) {  
                setTimeout(() => {
                    self.handleClose(e);
                    self.props.fetchCustomerInfoFromServer(e, this.props.username);
                } , 3000);
            }
        });
    };

    render() {
        const rowClasses = "col-xs-12 row-info";
        const { newGivenCredit, updateSuccess, message, hideMessages } = this.state;
        const { resources, balanceInfo } = this.props;

        return (

            <div className={rowClasses}>
                <div className="col-xs-6">{resources.GivenCredit}</div>
                <div className="col-xs-6 col-right given-credit" title={`<= ${balanceInfo.GivenCredit}`} >
                    <NumberFormat
                        className="link"
                        value={balanceInfo.GivenCredit}
                        displayType={'text'}
                        decimalScale="2"
                        fixedDecimalScale={true}
                        thousandSeparator={true} />

                    <span className="icon-border-color icon-edited" onClick={this.handleOpen} />

                    <Modal dialogComponentClass={DraggableModalDialog} show={this.state.show} onHide={this.handleClose}>
                            <Modal.Header closeButton>
                                <Modal.Title id="contained-modal-title">
                                    {resources.GivenCredit}
                                </Modal.Title>
                            </Modal.Header>
                            <Modal.Body>
                                <div className={hideMessages && 'hidden'}>
                                    <span className={updateSuccess ? 'msg-success' : 'msg-error'}>  {message} </span>
                                </div>
                                <div className={rowClasses}>
                                    <div className="col-xs-7">resources.CurrentGivenCredit</div>
                                    <CellContainNumberFormat title={balanceInfo.GivenCredit} classNames="col-right" />
                                </div>

                                <div className={rowClasses}>
                                    <div className="col-xs-7">resources.NewGivenCredit</div>
                                    <NumberFormat thousandSeparator={true} decimalScale="2" onValueChange={(values) => {
                                        const { formattedValue, value, floatValue } = values;
                                        this.setState({ newGivenCredit: (floatValue === undefined ? 0 : floatValue) })
                                    }} />
                                </div>

                                <div className={rowClasses}>
                                    <div className="col-xs-7">resources.Difference</div>
                                    <CellContainNumberFormat title={balanceInfo.GivenCredit - newGivenCredit} classNames="col-right" />
                                </div>
                            </Modal.Body>
                            <Modal.Footer>
                                <Button onClick={this.handleClose}>{resources.Cancel}</Button>
                                <Button onClick={this.handleSubmit}>{resources.Submit}</Button>
                            </Modal.Footer>
                        </Modal>
                </div>
            </div>
        );
    }
}
