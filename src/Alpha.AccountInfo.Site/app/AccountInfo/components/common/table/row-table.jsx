import React from 'react';

export default class RowTable extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        const { children } = this.props;
        return (            
            <tr>
                {children}
            </tr>
        );
    }
}