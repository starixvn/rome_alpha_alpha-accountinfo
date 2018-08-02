
const NoInformationRow = ({ classNames = null, colspan = null }) =>
    <tr><td className={classNames} colSpan={colspan}>No information is available</td></tr>

export default NoInformationRow;