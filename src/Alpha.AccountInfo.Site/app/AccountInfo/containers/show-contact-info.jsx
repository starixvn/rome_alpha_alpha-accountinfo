import * as React from 'react';

const ShowContactInfo = ({ title = '', classNames = null, handleShowHide = f => f }) =>
    <div className={classNames}>
        <button className="btn-default show-more" onClick={handleShowHide}>
            {title}
        </button>
    </div>;

export default ShowContactInfo;