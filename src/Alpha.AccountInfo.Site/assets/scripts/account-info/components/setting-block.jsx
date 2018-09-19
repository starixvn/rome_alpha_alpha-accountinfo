import React from 'react';
import { Link } from 'react-router-dom';

export default class SettingBlock extends React.Component {
    constructor(props) {
        super(props);
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
                    settingData.map((value, index) => {
                        return <div className="two-col" key={index}>
                            <a href={value.Url} target="main">{value.Text}</a>
                        </div>;
                    })
                }

            </div>
        );
    }
}
