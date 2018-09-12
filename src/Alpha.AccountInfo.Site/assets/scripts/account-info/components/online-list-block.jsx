import React from 'react';
import CustomizedAccordionItem from './common/customized-accordion-item';
import { accountInfoAPI } from '../api-client';

export default class OnlineListBlock extends React.Component{
    constructor(props) {
        super(props);
        this.state = {
            data: null
        };

        this.handleClick = this.handleClick.bind(this);
        this.handleOpenIPInfo = this.handleOpenIPInfo.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.custId !== this.props.custId) {
            this.setState({
                custId: nextProps.custId,
                data: null
            });
        }
    }

    handleClick(e) {
        this.state.data === null && accountInfoAPI.getOnlineList({ custid: this.props.custId }).then((data) => {
            this.setState({ data: data });
        });

        this.props.handleCollapExpandBlock(e);
    }

    handleOpenIPInfo(e) {
        e.preventDefault();
        window.top.ageWnd.open(`${this.props.viewIpInfoLink}?ip=${e.currentTarget.dataset.ip}`);
    }

    render() {
        const { resources, expanded, handleCollapExpandBlock, blockId } = this.props;
        const { data } = this.state;
        
        return (
            <CustomizedAccordionItem
                title={resources.OnlineList}
                iconClassname="icon-onlinelist"
                expanded={expanded}
                handleClick={this.handleClick}
                blockId={blockId}
            >

                {
                    <table className="tblRpt tblRpt-bordered width-100per online-list">
                        <thead>
                            <tr>
                                <th>{resources.NumberOrder}</th>
                                <th>{resources.IP}</th>
                                <th>{resources.LoginTime}</th>
                            </tr>
                        </thead>
                        <tbody>
                        {
                            (data === null || data.length === 0) &&
                            <tr>
                                <td colSpan="3">{resources.ThisUserIsNotOnline}</td>
                            </tr>
                        }

                        {
                            data !== null && data.map((online, index) =>
                                <tr key={index}>
                                    <td className="text-center">{index + 1}</td>
                                        <td><a onClick={this.handleOpenIPInfo} data-ip={online.IP}>{online.IP}</a></td>
                                        <td className="text-center"> {online.LoginTime}</td>
                                </tr>)
                        }
                        </tbody>
                    </table>
                }
            </CustomizedAccordionItem>
      );
    }
}