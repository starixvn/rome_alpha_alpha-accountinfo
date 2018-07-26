import React from 'react';
import TotalDownlineList from './total-downline-list';
import DownlineInfo from './downline-info';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import DownlineListContent from './downline-list-content';

export default class DownlineListBlock extends React.Component{
    constructor(props) {
        super(props);
        this.state = {
            username: '',
            url: window.accountInfoSiteAppPath + "accountinfo/getdownlinelist",
            data: null
        }
        this.handleClick = this.handleClick.bind(this);
    }
    componentDidMount() {
    
    }

    handleClick(e) {
        this.props.collapExpandMethod(e);
        this.setState({
            data: this.loadDownlineListFromServer()
        });
        //if (this.state.data === null) {
        //    this.setState({
        //        data: this.loadDownlineListFromServer()
        //    });
        //}
    }

    loadDownlineListFromServer() {
        console.log("load downline list");

        const xhr = new XMLHttpRequest();
        xhr.open('get', this.state.url + "?custid=" + this.props.custId);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();

        //return {
        //    DownlineList: ["user1", "user2"],
        //    TotalStatusAgent: {
        //        TotalActive: 456,
        //        TotalClosed: 123,
        //        TotalSuspend:787
        //    },
        //    TotalStatusMember: {
        //        TotalActive: 576,
        //        TotalClosed: 789780,
        //        TotalSuspend: 2543
        //    }
        //}
    }

    render() {
        const { handleClick, dataId, expanded } = this.props;
        const { data } = this.state;

        return (
            <CustomizedAccordionItem
                title="Downline list"
                iconClassname="icon-downline"
                expanded={expanded}
                handleClick={this.handleClick}
                dataId={dataId}>
                {data != null && <DownlineListContent downlineList={data} />}
            </CustomizedAccordionItem>    
        );
    }
}