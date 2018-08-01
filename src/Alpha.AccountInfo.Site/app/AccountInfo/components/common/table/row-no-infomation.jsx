
const RowNoInformation = ({ classNames = null, title, colspan = null }) =>
    <tr><td className={classNames} colSpan={colspan}>{title}</td></tr>

export default RowNoInformation;