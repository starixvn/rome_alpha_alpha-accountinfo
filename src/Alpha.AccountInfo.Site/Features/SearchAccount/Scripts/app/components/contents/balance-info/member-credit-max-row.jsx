import React from 'react';
import CellContainNumberFormat from '../../common/cell-contain-number-format';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';

const MemberCreditMaxRow = ({ resources, balanceInfo }) => {
    const tooltipProps = {
        title: balanceInfo.MemberMaxCreditLimit,
        position: "bottom",
        trigger: "mouseenter",
        arrow: "true",
    };

    const content = <CellContainNumberFormat value={balanceInfo.MemberMaxCredit} />;
    const contentWithTooltip = <Tooltip {...tooltipProps}> {content} </Tooltip>;

    return (
        <div className="row-info">
            <div className="col">{resources.MemberMaxCredit}</div>
            <div className="col col-right">
                {
                    balanceInfo.ShowMemberMaxCreditLimit ? contentWithTooltip : content
                }
            </div>
        </div>
    );
}

export default MemberCreditMaxRow;