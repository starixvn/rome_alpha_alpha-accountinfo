import * as React from 'react';
import Button from '../components/common/button';

const ShowContactInfo = ({ title, classNames, handleClick }) =>
    <div className={classNames}>
        <Button classNames="btn-default show-more" handleClick={handleClick} title={title} />
    </div>;

export default ShowContactInfo;