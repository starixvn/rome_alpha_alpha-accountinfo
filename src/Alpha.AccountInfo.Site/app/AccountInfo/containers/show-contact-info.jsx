import * as React from 'react';
import Button from '../components/common/button';

const ShowContactInfo = ({ title, classNames, handleShowHide }) =>
    <div className={classNames}>
        <Button classNames="btn-default show-more" handleClick={handleShowHide} title={title} />
    </div>;

export default ShowContactInfo;