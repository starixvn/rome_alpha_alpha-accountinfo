import * as React from 'react';

export default class ContactInfoContent extends React.Component {
    builRow(title, value) {
        return (
            <div className="col-xs-12 row-info">
                <div className="col-xs-5">{title}</div>
                <div className="col-xs-7 col-right">{value}</div>
            </div>
        );
    }

    render() {
        let { resources, customerInfo } = this.props;
        return (
            <div>
                {this.builRow(resources.Name, customerInfo.FullName)}
                {this.builRow(resources.Phone, customerInfo.Phone)}
                {this.builRow(resources.Fax, customerInfo.Fax)}
            </div>
        );
    }
}