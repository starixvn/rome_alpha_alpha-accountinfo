import React from 'react';

export default class BodyTable extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        const { children } = this.props;
        return (
            <tbody>
                {children}
            </tbody>
        );
    }
}