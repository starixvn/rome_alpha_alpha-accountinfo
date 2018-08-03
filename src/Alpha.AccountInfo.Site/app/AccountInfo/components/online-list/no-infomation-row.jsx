
const NoInformationRow = ({ classNames = null, colspan = null }) =>
    <tr><td className={classNames} colSpan={colspan}>This user is not online</td></tr>

export default NoInformationRow;