import React from 'react';
import ReactDOM from 'react-dom';
import { cloneDeep, forEach } from 'lodash';
import { decodeResources } from '../../../../SearchAccount/Scripts/app/helpers/accountinfo-helper';
import { unsuspendAPI } from '../unsuspend-api';

class SuspendMultipleConfirmContainer extends React.Component {
    constructor(props) {
        super(props);
        const { viewModel, resources } = this.props;

        this.state = {
            resources: resources,
            viewModel: viewModel,
            hideMore: true,
            hideMessages: true,
            message: ''
        };

        this.showMore = this.showMore.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleSubmit() {
        $.blockUI();
        const self = this;
        const { viewModel } = this.props;

        unsuspendAPI.updateSuspend('UnsuspendDownlines/UnsuspendMultiple', { UnsuspendList: viewModel.UnsuspendList, RoleId: viewModel.RoleId })
            .then(data => {
                let isSuccess = true;
                const timeoutDefault = 3000;
                const results = data.Results ? data.Results : [data];

                const message = <div className='msg-container'>
                    {
                        results.map((result, index) => {
                            if (!result.IsSuccess && isSuccess) {
                                isSuccess = false;
                            }

                            return <div key={index} className={result.IsSuccess ? 'msg msg-success' : 'msg msg-error'}>
                                {result.Message}
                            </div>;
                        })}
                </div>;
                self.setState({
                    hideMessages: false,
                    message: message
                });

                if (isSuccess) {
                    if (top.customerListNew) {
                        top.successfulUpdatingCallbackAndReloadMainFrame(true);
                    }
                    else {
                        window.top.main.DelayReloadPage();
                        setTimeout(() => {
                            window.top.ageWnd.close();
                        }, timeoutDefault);
                    }
                }
                $.unblockUI();
            });
    }

    handleCancel() {
        window.top["popupManager"].close();
    }

    showMore() {
        this.state.hideMore ? (
            this.setState({ hideMore: false })
        ) : (
            this.setState({ hideMore: true })
        )
    }

    render() {
        const { viewModel, resources, hideMore, hideMessages, message } = this.state;

        return (
            <div className="content-page">
                <div id="message-container" className="message-container">
                    {hideMessages ? <div></div> : message}
                </div>
                <div className="review-content">
                    {viewModel.DisplayShowAllAccountOption ?
                        <div>
                            <div className="shorten-content">
                                <span className="total">
                                    {viewModel.TotalSelectedText}&nbsp;
                                </span>
                                {viewModel.ShortenUsernames.join(", ")}
                            </div>

                            <span style={hideMore ? { display: "inline" } : { display: "none" }} className="more-ellipses">... </span>
                            <div className="more-content">
                                <span style={hideMore ? { display: "none" } : { display: "inline" }}>
                                    , {viewModel.MoreDownlineUsernames.join(", ")} &nbsp;
                                </span>
                            </div>
                            <a className="toggle" href='javascript:void(0)' onClick={this.showMore}>
                                {this.state.hideMore ? 
                                    (<span>{resources.ShowMore}</span>) :
                                    (<span>{resources.ShowLess}</span>)
                                }
                            </a>
                        </div> :
                        <div className="shorten-content">
                            <span className="total">
                                {viewModel.TotalSelectedText}
                            </span>
                            {viewModel.ShortenUsernames.join(", ")}
                        </div>
                    }
                </div>

                <div className="list-button text-right">
                    {
                        [{
                            text: resources.Cancel,
                            class: `btn-cancel`,
                            onclick: this.handleCancel
                        },
                        {
                            text: resources.Submit,
                            class: `btn-submit`,
                            onclick: this.handleSubmit
                        }].map((button, index) =>
                            <button className={button.class} key={index} onClick={button.onclick}>
                                {button.text}
                            </button>
                        )
                    }
                </div>

            </div>
        );
    }
}

export default SuspendMultipleConfirmContainer;

export const renderSuspendMultipeConfirm = (viewModel, resources) => {
    ReactDOM.render(
        <SuspendMultipleConfirmContainer {...{
            viewModel: viewModel.Data,
            resources: decodeResources(resources)
        }} />,
        document.getElementById('container')
    );
};

window.renderSuspendMultipeConfirm = renderSuspendMultipeConfirm;