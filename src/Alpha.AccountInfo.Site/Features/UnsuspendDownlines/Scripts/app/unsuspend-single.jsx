import * as React from 'react';
import ReactDOM from 'react-dom';
import { unsuspendAPI } from './unsuspend-api';
import UnsuspendViewList from './unspend-view-list';

window.renderUnsuspendSingle = function (viewModel) {
	ReactDOM.render(
        <UnsuspendSingle viewModel={viewModel} />,
        document.getElementById('unsuspend-single')
	);
};

export default class UnsuspendSingle extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			hideMessages: true,
			updateSuccess: false,
            message: '',
            accountinfoUrl: top.alpha.urls.accountInfoRootPath,
            searchAccountId: this.props.viewModel.SearchAccountId,
            showViewList: false
		};

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleKeyPress = this.handleKeyPress.bind(this);
        this.hrefViewlist = this.hrefViewlist.bind(this);
        this.hrefBack = this.hrefBack.bind(this);
	}

	handleKeyPress(e) {
		if (e.key === 'Enter') {
			this.handleSubmit(e);
		}
	}

    hrefViewlist() {
        this.setState({ showViewList: true });
    }

    hrefBack() {
        this.setState({ showViewList: false });
    }

	showErrorMessage(message) {
		this.setState((prevState) => ({
			hideMessages: false,
			message: message,
			updateSuccess: false
		}));
	}

	buildNote() {
		return (
			<div className="note">
				List of downlines whose status are changed/unchanged after going to unsuspend 'selected customer'
            </div>
		);
	}

    handleSubmit(e) {
        let self = this;

        $.blockUI();

        unsuspendAPI.updateSuspend(
            "UnsuspendDownlines/UnsuspendSingle",
            { SearchAccountId: this.state.SearchAccountId })
            .then((data) => {
            self.setState({
                hideMessages: false,
                message: data.IsSuccess ? 'Update Successfully' : data.Message.trim(),
                updateSuccess: data.IsSuccess
            });

            if (data.IsSuccess) {
                setTimeout(() => {
                    window.top.realoadLeftPanel = true;
                    window.top.closedPopup = true;
                    window.top.ageWnd.close();
                }, 1500);
            }

            $.unblockUI();
        });
    }

    buildViewListUrl(downlines, IsAffected, downlineRoleId) {
        const { accountinfoUrl, searchAccountId } = this.state;

        const href = `${accountinfoUrl}/UnsuspendDownlines/ViewUnsuspendList` +
            `?SearchAccountId=${searchAccountId}&DownlineRoleId=${downlineRoleId}` +
            `&IsAffected=${IsAffected}`;

        return downlines === 0 ? 0 : <a href='#' onClick={this.hrefViewlist}> {downlines} </a>
    }

    buildBodyRow(level, index) {
        const columns = [
            {
                downlines: level.AffectedDownlines,
                isAffected: true,
                roleId: level.RoleId
            },
            {
                downlines: level.UnchangedDownlines,
                isAffected: false,
                roleId: level.RoleId
            }];

        return (
            <tr key={index}>
                <td className="text-center">
                    {level.RoleId == 3 ? `Master` : level.RoleId == 2 ? `Agent` :  `Member`}
                </td>
                {
                    columns.map((columns, index01) => {
                        return (
                            <td className="text-center" key={index01}>
                                {
                                    this.buildViewListUrl(columns.downlines, columns.isAffected, columns.roleId)
                                }
                            </td>
                        );
                    })

                }
            </tr>
        );
    }

    buildFooterRow(AffectedDownlinesTotal, UnchangedDownlinesTotal) {
        const columns = [
            {
                downlines: AffectedDownlinesTotal,
                isAffected: true,
                roleId: 0
            },
            {
                downlines: UnchangedDownlinesTotal,
                isAffected: false,
                roleId: 0
            }];

        return (
            <tr>
                <th className="text-center">Total</th>
                {
                    columns.map((columns, index01) => {
                        return (
                            <th className="text-center" key={index01}>
                                {
                                    this.buildViewListUrl(columns.downlines, columns.isAffected, columns.roleId)
                                }
                            </th>
                        );
                    })

                }
            </tr>
        );
    }

	render() {
		const { updateSuccess, message, hideMessages, resources } = this.state;
        const { viewModel } = this.props;

        const singleView = (
            < div className="col-xs-12 " >
                <div className={hideMessages ? `hidden` : ``}>
                    <span className={updateSuccess ? 'col-xs-12 msg msg-success' : 'col-xs-12 msg msg-error'}>  {message} </span>
                </div>

                {this.buildNote()}
                < div className="col-xs-12 " >
                    <table className="tblRpt tblRpt-bordered width-100per">
                        <thead>
                            <tr>
                                <th>Level</th>
                                <th>Affected Downlines</th>
                                <th>Remain Unchanged Downlines</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                viewModel.UnsuspendByLevel.map((level, index) => this.buildBodyRow(level, index))
                            }
                        </tbody>
                        <tfoot>
                            {
                                this.buildFooterRow(viewModel.AffectedDownlinesTotal, viewModel.UnchangedDownlinesTotal)
                            }
                        </tfoot>
                    </table>
                </ div>
                <div className="col-xs-12 row-info popup-bottom list-button text-right">
                    <button className="btn-submit" onClick={this.handleSubmit}>Unsuspend</button>
                </div>
            </div >
        );

        const viewList = <UnsuspendViewList hrefBack={this.hrefBack} />;
        return this.state.showViewList ? viewList : singleView; 
	}
}
