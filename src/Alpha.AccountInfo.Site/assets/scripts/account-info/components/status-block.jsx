import React from 'react';
import { accountInfoAPI } from '../api-client';
import { Link } from 'react-router-dom';
import { NoInfo } from '../components/common/no-info';

export default class StatusBlock extends React.Component {
    constructor(props) {
        super(props);
        this.handleOpenPopupForProductStatus = this.handleOpenPopupForProductStatus.bind(this);
        this.handleOpenPopupForCheckbox = this.handleOpenPopupForCheckbox.bind(this);
        this.handleOpenConfirmPopup = this.handleOpenConfirmPopup.bind(this);
    }

    handleOpenPopupForCheckbox(e) {
        let url = e.currentTarget.dataset.url;
        let productName = e.currentTarget.dataset.name;
        let resources = this.props.resources;
        if (JSON.parse(e.currentTarget.dataset.enabled)) {
            e.currentTarget.dataset.message === undefined ?
                window.top.ageWnd.open(url, `${resources.Update} ${productName} ${resources.Status}`) :
                this.handleOpenConfirmPopup(e);
        }
    }

    handleOpenConfirmPopup(e) {
        let self = this;
        let url = e.currentTarget.dataset.url;
        let param = e.currentTarget.dataset.param;

        window.top.bootboxConfirm(e.currentTarget.dataset.message, e.target,
            function () {
                $.blockUI();
                accountInfoAPI.updateCustomerStatus(url, JSON.parse(param)).then((data) => {
                    if (data.IsSuccess) {
                        setTimeout(() => {
                            $.unblockUI();
                            self.props.fetchCustomerAllStatusFromServer();
                            self.props.fetchCustomerInfoFromServer(e, self.props.username);
                        }, 3000);
                    }
                });
            });
    }

    handleOpenPopupForProductStatus(e) {
        let productId = parseInt(e.currentTarget.dataset.id);
        let productName = e.currentTarget.dataset.name;
        let resources = this.props.resources;
        let popupTitle = `${resources.Update} ${productName} ${resources.Status}`;
        let width = 475, height = 200, left = 200, top = 200;

        if (productId === 8) {
            height = 530;
        }
        else if (productId >= 21) {
            height = 300;
        }

        window.top.ageWnd.open(e.currentTarget.dataset.url, popupTitle, left, top, width, height);
    }

    render() {
        const { resources, username, statusData } = this.props;
        const header = <Link to="/" className="feature-item">
            <span className="icon-status" />
            {resources.Status}
        </Link>;

        if (statusData !== null) {
            return (
                <div className="feature-detail">
                    {header}
                    <div className="status-setting">
                        {
                            statusData !== null && statusData.map((status, index) => {
                                const disabled = status.Disabled ? "disabled" : "";
                                return status.IsEditOnProductLevel !== undefined && !status.IsEditOnProductLevel ?
                                    <div key={index}
                                        className="col"
                                        data-id={status.ProductId}
                                        data-name={status.Text}
                                        onClick={this.handleOpenPopupForProductStatus}
                                        data-url={status.Url}
                                    >
                                        <span className="icon-ic_list" />
                                        <span className="link">{status.Text}</span>                                        
                                    </div> :
                                    <div key={index}
                                        className={`col checkbox ${disabled}`}
                                        onClick={this.handleOpenPopupForCheckbox}
                                        data-enabled={!status.Disabled}
                                        data-url={status.Url}
                                        data-param={status.JsonParams}
                                        data-message={status.ConfirmMessage}
                                        data-name={status.Text}
                                    >
                                        <input id={status.displayOrder}
                                            type="checkbox"
                                            disabled={status.Disabled}
                                            checked={status.Checked}
                                        />
                                        <label className="label-text" htmlFor={status.displayOrder}>{status.Text}</label>
                                    </div>;
                            })
                        }
                    </div>
                    
                </div>
            );
        }
        else {
            return (
                <div>
                    {header}
                    <NoInfo text={resources.NoInfo} />
                </div>
            );
        }
    }
}
