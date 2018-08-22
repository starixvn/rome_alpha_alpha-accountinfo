const UserNameViewLog = ({ viewLogTitle, viewLogLink, statusCSS, username}) =>
    <a className="link-viewlog" href={viewLogLink} target="main">
        <span className={statusCSS} data-username={username}>
            {username}
        </span>
        <span className="icon-viewlog" title={viewLogTitle}></span>
    </a>


export default UserNameViewLog;