import React from 'react';

export default class HeadTable extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        const { children} = this.props;
        return (
            <thead>
                {children}
            </thead>
        );
    }
}