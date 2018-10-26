import React from 'react';
import ReactDOM from 'react-dom';
import { mount } from "enzyme";
import CellContainNumberFormat from 'CellContainNumberFormat';
import MemberCreditMaxRow from 'MemberCreditMaxRow';
import { Resources } from '../../helpers/resources-sample';
import { Agent } from '../../helpers/actor-is-agent-sample ';
import { Tooltip } from 'react-tippy';

describe('MemberCreditMaxRow', () => {
    let props;
    beforeEach(() => {
        props = {
            balanceInfo: Agent.ViewMemberInfo,
            resources: Resources
        };
    });

    it('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<MemberCreditMaxRow {...props} />, div);
    });

    it("renders a `CellContainNumberFormat`", () => {
        const memberCreditMaxRow = mount(<MemberCreditMaxRow {...props} />);
        expect(memberCreditMaxRow.find(CellContainNumberFormat).length).toBe(1);
    });

    it("renders a `Tooltip` if ShowMemberMaxCreditLimit is true", () => {
        props.balanceInfo.ShowMemberMaxCreditLimit = true;
        const memberCreditMaxRow = mount(<MemberCreditMaxRow {...props} />);
        expect(memberCreditMaxRow.find(Tooltip).length).toBe(1);
    });

    it("does not renders a `Tooltip` if ShowMemberMaxCreditLimit is false", () => {
        props.balanceInfo.ShowMemberMaxCreditLimit = false;
        const memberCreditMaxRow = mount(<MemberCreditMaxRow {...props} />);
        expect(memberCreditMaxRow.find(Tooltip).length).toBe(0);
    });
});