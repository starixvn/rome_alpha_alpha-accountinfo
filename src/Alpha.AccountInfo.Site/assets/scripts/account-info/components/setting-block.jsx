import React from 'react';
import Paging from './common/paging';
import { Link } from 'react-router-dom';

export default class SettingBlock extends React.Component {
    constructor(props) {
        super(props);
    }

    renderCallback(value) {
        return <a href={value.Url} target="main">{value.Text}</a>;
    }

    render() {
        const { resources, blockId, expanded, settingData, getCustomerAllStatus } = this.props;
        return (
            <div className="feature-detail block-settings">
                <Link to="/" className="feature-item">
                    <span className="icon-settings" />
                    {resources.Setting}</Link>
                {
                    settingData !== null &&
                    <Paging resourcePage={this.props.resources.Page} values={settingData} renderCallback={this.renderCallback} />
                }

            </div>
        );
    }
}
