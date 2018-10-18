import * as React from 'react';

const UplineInfo = ({ username, handleSearchFromUserLevel }) => {
    return (
        <span
            className="enabled"
            onClick={handleSearchFromUserLevel}
            data-username={username}
        >
            {username}
        </span>
    );
}

export default UplineInfo; 