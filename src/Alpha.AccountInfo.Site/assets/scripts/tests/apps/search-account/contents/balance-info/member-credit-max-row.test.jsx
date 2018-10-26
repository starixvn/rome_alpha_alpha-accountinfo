import React from 'react';
import ReactDOM from 'react-dom';
import { mount } from "enzyme";
import CellContainNumberFormat from 'CellContainNumberFormat';
import MemberCreditMaxRow from 'MemberCreditMaxRow';
import { Resources } from '../../helpers/resources-sample';
import { AgentLevelInfo } from '../../helpers/agent-level-info-sample';
import { Tooltip } from 'react-tippy';

describe('MemberCreditMaxRow', () => {
    let props;
    beforeEach(() => {
        props = {
            balanceInfo: AgentLevelInfo.BalanceInfo,
            resources: Resources
        };
    });

    it('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<MemberCreditMaxRow {...props} />, div);
    });

    it("renders a `CellContainNumberFormat`", () => {
        props.balanceInfo.ShowMemberMaxCreditLimit = true;
        const memberCreditMaxRow = mount(<MemberCreditMaxRow {...props} />);
        expect(memberCreditMaxRow.find(CellContainNumberFormat).length).toBe(1);
    });

    it("renders a `Tooltip` if ShowMemberMaxCreditLimit", () => {
        props.balanceInfo.ShowMemberMaxCreditLimit = false;
        const memberCreditMaxRow = mount(<MemberCreditMaxRow {...props} />);
        expect(memberCreditMaxRow.find(Tooltip).length).toBe(0);
    });
});