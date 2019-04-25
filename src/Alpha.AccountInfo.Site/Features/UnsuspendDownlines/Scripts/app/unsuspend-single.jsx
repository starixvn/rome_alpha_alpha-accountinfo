import * as React from 'react';
import ReactDOM from 'react-dom';
import { unsuspendAPI } from './unsuspend-api';
import UnsuspendViewList from './unspend-view-list';
import { UserLevel } from '../../../SearchAccount/Scripts/app/constants';
import { decodeResources } from '../../../SearchAccount/Scripts/app/helpers/accountinfo-helper';

window.renderUnsuspendSingle = function (viewModel, resources) {
    ReactDOM.render(
        <UnsuspendSingle  {...{ viewModel, resources: decodeResources(resources)}}/>,
        document.getElementById('unsuspend-single')
    );
};

export default class UnsuspendSingle extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showViewList: false,
            isAffected: false,
            downlineRoleId: 1
        };

        this.redirectViewListDetail = this.redirectViewListDetail.bind(this);
        this.redirectMainPage = this.redirectMainPage.bind(this);
    }

    redirectViewListDetail(isAffected, downlineRoleId) {
        this.setState({
            showViewList: true,
            isAffected: isAffected,
            downlineRoleId: downlineRoleId
        });
    }

    redirectMainPage() {
        this.setState({ showViewList: false });
    }

    render() {
        const { isAffected, downlineRoleId } = this.state;
        const { viewModel, resources } = this.props;

        return this.state.showViewList ?
            <UnsuspendViewList {...{
                isAffected,
                downlineRoleId,
                searchAccountId: viewModel.SearchAccountId,
                redirectMainPage: this.redirectMainPage
            }} /> :
            <SingleView {...{ viewModel, resources, redirectViewListDetail: this.redirectViewListDetail }} />;
    }
}

export class SingleView extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            hideMessages: true,
            updateSuccess: false,
            message: '',
            searchAccountId: this.props.viewModel.SearchAccountId,
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleKeyPress = this.handleKeyPress.bind(this);
    }

    handleKeyPress(e) {
        if (e.key === 'Enter') {
            this.handleSubmit(e);
        }
    }

    handleSubmit(e) {
        $.blockUI();
        const { resources } = this.props;
        let self = this;

        unsuspendAPI.updateSuspend(
            "UnsuspendDownlines/UnsuspendSingle",
            { SearchAccountId: this.state.SearchAccountId })
            .then((data) => {
                self.setState({
                    hideMessages: false,
                    message: data.IsSuccess ? resources.UpdateSuccessfully : data.Message.trim(),
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

    render() {
        const { updateSuccess, message, hideMessages } = this.state;
        const { viewModel, resources, redirectViewListDetail } = this.props;

        return (
            < div className="col-xs-12 " >
                <div className={hideMessages ? `hidden` : ``}>
                    <span className={updateSuccess ? 'col-xs-12 msg msg-success' : 'col-xs-12 msg msg-error'}>  {message} </span>
                </div>

                <div className="note">
                    {resources.UnsuspendSingleNote}
                </div>

                < div className="col-xs-12 " >
                    <table className="tblRpt tblRpt-bordered width-100per">
                        <thead>
                            <tr>
                                <th>{resources.Level}</th>
                                <th>{resources.AffectedDownlines}</th>
                                <th>{resources.RemainUnchangedDownlines}</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                viewModel.UnsuspendByLevel.map((level, index) =>
                                    <BodyRowContent {...{ level, redirectViewListDetail, resources, key: index }}/>)
                            }
                        </tbody>
                        <FooterContent {...{
                            affectedDownlinesTotal: viewModel.AffectedDownlinesTotal,
                            unchangedDownlinesTotal: viewModel.UnchangedDownlinesTotal,
                            redirectViewListDetail,
                            resources
                        }} />
                    </table>
                </ div>
                <div className="col-xs-12 row-info popup-bottom list-button text-right">
                    <button className="btn-submit" onClick={this.handleSubmit}>{resources.Unsuspend}</button>
                </div>
            </div >
        );
    }
}

export const FooterContent = ({ affectedDownlinesTotal, unchangedDownlinesTotal, redirectViewListDetail, resources }) => {
    const columns = [
        {
            downlines: affectedDownlinesTotal,
            isAffected: true
        },
        {
            downlines: unchangedDownlinesTotal,
            isAffected: false
        }];

    return (
        <tfoot>
            <tr>
                <th className="text-center">{resources.Total}</th>
                {
                    columns.map((columns, index) => {
                        return (
                            <th className="text-center" key={index}>
                                <ColumnContentByLevel
                                    downlines={columns.downlines}
                                    isAffected={columns.isAffected}
                                    downlineRoleId={0}
                                    redirectViewListDetail={redirectViewListDetail}
                                />
                            </th>
                        );
                    })

                }
            </tr>
        </tfoot>
    );
}

export const BodyRowContent = ({ level, redirectViewListDetail, resources }) => {
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

    const levelNames = [
        {
            LevelId: UserLevel.Master,
            LevelName: resources.Master
        },
        {
            LevelId: UserLevel.Agent,
            LevelName: resources.Agent
        },
        {
            LevelId: UserLevel.Member,
            LevelName: resources.Member
        }];

    return (
        <tr>
            <td className="text-center">
                {
                    levelNames.filter(function (item) {
                        return (item.LevelId === level.RoleId);
                    })[0].LevelName
                }
            </td>
            {
                columns.map((columns, index) => {
                    return (
                        <td className="text-center" key={index}>
                            <ColumnContentByLevel {...{
                                downlines: columns.downlines,
                                isAffected: columns.isAffected,
                                downlineRoleId: columns.roleId,
                                redirectViewListDetail
                            }} />
                        </td>
                    );
                })

            }
        </tr>
    );
}

export const ColumnContentByLevel = ({ downlines, isAffected, downlineRoleId, redirectViewListDetail }) => {
    return downlines === 0 ? <span>0</span> : <a href='#' onClick={() => redirectViewListDetail(isAffected, downlineRoleId)}> {downlines} </a>
}
