import React from 'react';

const DownlineListPage = ({ usernames, handleSearchFromUserLevel  }) =>
    <div className="swiper-slide" >
        {usernames.map((row, index) => {
            return <div data-username={row.UserName} className="two-col" onClick={handleSearchFromUserLevel} key={index}>
                <a href="#">{row.UserName}</a>
            </div>
        })}
    </div>

export default DownlineListPage;