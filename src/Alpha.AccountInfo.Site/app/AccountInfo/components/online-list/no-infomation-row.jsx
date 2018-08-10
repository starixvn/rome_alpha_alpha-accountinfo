
const NoInformationRow = ({ resources, classNames = null, colspan = null }) =>
    <tr><td className={classNames} colSpan={colspan}>{resources.ThisUserIsNotOnline}</td></tr>

export default NoInformationRow;