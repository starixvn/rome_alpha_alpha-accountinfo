import * as React from 'react';
import ReactDOM from 'react-dom';
import CustomerInfoBlock from '../components/customer-info-block';
import SearchingBlock from '../components/searching-block';
import FeatureList from './feature-list';
import { accountInfoAPI } from '../api-client';
import { base64Helper } from '../../helpers/base64-helper';

export default class AccountInfo extends React.Component {
    constructor(props) {
        super(props);
        let lastSearchUsername = localStorage.getItem("lastSearchUsername") !== null ? localStorage.getItem("lastSearchUsername") : '';
        this.state = {
            username: lastSearchUsername,
            searchCounter: 0,
            showInfoStatus: false,
            customerInfo: null,
            resources: base64Helper.decodeResources(this.props.resources)
        };

        this.handleShowContactInfo = this.handleShowContactInfo.bind(this);
        this.handleSearchFromUserLevel = this.handleSearchFromUserLevel.bind(this);
        this.fetchCustomerInfoFromServer = this.fetchCustomerInfoFromServer.bind(this);
    }

    fetchCustomerInfoFromServer(e, username) {
        $.blockUI();
        let self = this;
        accountInfoAPI.getCustomerInfo({ username: username }).then((data) => {
            self.setState((prevState) => ({
                username: username,
                searchCounter: prevState.searchCounter + 1,
                customerInfo: data
            }));

            localStorage.setItem('lastSearchUsername', username);
            self.handleShowContactInfo(e);
            $.unblockUI();
        });
    }

    handleSearchFromUserLevel(e) {
        e.preventDefault();
        this.fetchCustomerInfoFromServer(e, e.currentTarget.dataset.username);
    }

    handleShowContactInfo() {
        this.setState((prevStatus) => { showInfoStatus: !prevStatus; });
    }

    render() {
        const { showInfoStatus, customerInfo, resources, searchCounter, username } = this.state;

        const customerInfoContent = customerInfo !== null ?
            <CustomerInfoBlock
                resources={resources}
                data={customerInfo}
                handleSearchFromUserLevel={this.handleSearchFromUserLevel}
            /> :
            <div className="no-information-search">{resources.NoInfo}</div>;

        return (
            <div className="accountinfo-container">
                <SearchingBlock
                    userNamePlaceHolder={resources.UserNamePlaceHolder}
                    fetchCustomerInfoFromServer={this.fetchCustomerInfoFromServer} 
                    username={username}
                />

                <div id="blockInfo" className="block-info">
                    {searchCounter > 0 && customerInfoContent}
                </div>

                {customerInfo !== null &&
                    <FeatureList resources={resources}
                        handleShowContactInfo={this.handleShowContactInfo}
                        showInfoStatus={this.showInfoStatus}
                        customerInfo={customerInfo}
                        handleSearchFromUserLevel={this.handleSearchFromUserLevel}
                        fetchCustomerInfoFromServer={this.fetchCustomerInfoFromServer}
                    />}
            </div>
        );
    }
}