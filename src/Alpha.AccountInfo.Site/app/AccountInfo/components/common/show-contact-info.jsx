import * as React from 'react';
import Button from './button';

export default class ShowContactInfo extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        const { text, onClick } = this.props;

        return (
            <div className="visible-mobile show-more-container">
                <Button classNames="btn-default show-more" onClick={onClick} text={text} />
            </div>
        );
    }
}