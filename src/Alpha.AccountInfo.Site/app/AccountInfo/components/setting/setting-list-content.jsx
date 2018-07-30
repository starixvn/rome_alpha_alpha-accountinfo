import React from 'react';
import SettingInfo from './setting-info';
import CustomizedAccordionItem from '../common/customized-accordion-item';

export default class SettingContent extends React.Component{
    constructor(props) {
        super(props);

    }

    render() {
        const { setting } = this.props;

        return (
            <div>
                <SettingInfo settings={setting.Settings} />
            </div>    
        );
    }
}