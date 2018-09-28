import * as React from 'react';

export default class ContactInfoContent extends React.Component {
    render() {
        let { resources, customerInfo } = this.props;

        let row = (title, value) =>
            <div className="col-xs-12 row-info">
                <div className="col-xs-5">{title}</div>
                <div className="col-xs-7 col-right">{value}</div>
            </div>;

        return (
            <div>
                {row(resources.Name, customerInfo.FullName)}
                {row(resources.Phone, customerInfo.Phone)}
                {row(resources.Fax, customerInfo.Fax)}
            </div>
        );
    }
}