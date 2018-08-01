import React, { Children } from 'react';

export default class Table extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { classNames, children } = this.props;

        return (
            <table className={classNames}>
                {children}
            </table>
        );
    }
}