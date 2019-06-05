import React from 'react';
import { mount, shallow } from 'enzyme';
import { Super } from '../../helpers/actor-is-super-sample';
import { Master } from '../../helpers/actor-is-master-sample';
import { Agent } from '../../helpers/actor-is-agent-sample';
import { Resources } from '../../helpers/resources-sample';
import GivenCreditRow from 'GivenCreditRow';
import MemberCreditMaxRow from 'MemberCreditMaxRow';
import BalanceInfo from 'BalanceInfo';

describe('BalanceInfo', () => {
    let props, balanceInfoMount, balanceInfoShallow;
    const buildBalaneInfo = cusomerinfo => {
        props.customerInfo = cusomerinfo;
        balanceInfoMount = mount(<BalanceInfo {...props} />);
        balanceInfoShallow = shallow(<BalanceInfo {...props} />);
    };

    beforeEach(() => {
        props = {
            customerInfo: undefined,
            resources: Resources,
            mobileView: false,
            fetchCustomerInfoFromServer: jest.fn()
        };
        balanceInfoMount = null;
        balanceInfoShallow = null;
    });

    test.each([
        [Super.ViewMasterInfo, 1],
        [Super.ViewAgentInfo,1],
        [Super.ViewMemberInfo, 0],
        [Master.ViewAgentInfo, 1],
        [Master.ViewMemberInfo, 0],
        [Agent.ViewMemberInfo, 0]])(
        'renders a Total Cash Row',
        (customerInfo, expectedCallTimes) => {
            buildBalaneInfo(customerInfo);
            expect(balanceInfoShallow.find('#total-cash').length).toBe(expectedCallTimes);
        }
    );
    
    test.each([
        [Super.ViewMasterInfo, 1],
        [Super.ViewAgentInfo,1],
        [Super.ViewMemberInfo, 0],
        [Master.ViewAgentInfo, 1],
        [Master.ViewMemberInfo, 0],
        [Agent.ViewMemberInfo, 0]])(
        'renders a Yesterday Cash Row',
        (customerInfo, expectedCallTimes) => {
            buildBalaneInfo(customerInfo);
            expect(balanceInfoShallow.find('#yesterday-cash').length).toBe(expectedCallTimes);
        }
    );

    test.each([
        Super.ViewMasterInfo,
        Super.ViewAgentInfo,
        Super.ViewMemberInfo,
        Master.ViewAgentInfo,
        Master.ViewMemberInfo,
        Agent.ViewMemberInfo])(
        'always renders a Total Balance Row',
        customerInfo => {
            buildBalaneInfo(customerInfo);
            expect(balanceInfoShallow.find('#total-balance').length).toBe(1);
        }
    );

    test.each([
        Super.ViewMasterInfo,
        Super.ViewAgentInfo,
        Super.ViewMemberInfo,
        Master.ViewAgentInfo,
        Master.ViewMemberInfo,
        Agent.ViewMemberInfo])(
        'always renders a Yesterday Balance Row',
        customerInfo => {
            buildBalaneInfo(customerInfo);
            expect(balanceInfoShallow.find('#yesterday-balance').length).toBe(1);
        }
    );

    test.each([
        Super.ViewMasterInfo,
        Super.ViewAgentInfo,
        Super.ViewMemberInfo,
        Master.ViewAgentInfo,
        Master.ViewMemberInfo,
        Agent.ViewMemberInfo])(
        'always renders a Report Link Row',
        customerInfo => {
            buildBalaneInfo(customerInfo);
            expect(balanceInfoShallow.find('#report-link').length).toBe(1);
        }
    );

    test.each([
        [Super.ViewMasterInfo, 1],
        [Super.ViewAgentInfo,1],
        [Super.ViewMemberInfo, 0],
        [Master.ViewAgentInfo, 1],
        [Master.ViewMemberInfo, 0],
        [Agent.ViewMemberInfo, 0]])(
        'renders a Used Credit Row',
        (customerInfo, expectedCallTimes) => {
            buildBalaneInfo(customerInfo);
            expect(balanceInfoShallow.find('#used-credit').length).toBe(expectedCallTimes);
        }
    );

    test.each([
        Super.ViewMasterInfo,
        Super.ViewAgentInfo,
        Super.ViewMemberInfo,
        Master.ViewAgentInfo,
        Master.ViewMemberInfo,
        Agent.ViewMemberInfo])(
        'always renders a `GivenCreditRow`',
        customerInfo => {
            buildBalaneInfo(customerInfo);
            expect(balanceInfoMount.find(GivenCreditRow).length).toBe(1);
        }
    );

    test.each([
        [Super.ViewMasterInfo, 0],
        [Super.ViewAgentInfo,1],
        [Super.ViewMemberInfo, 0],
        [Master.ViewAgentInfo, 1],
        [Master.ViewMemberInfo, 0],
        [Agent.ViewMemberInfo, 0]])(
         'renders a `MemberCreditMaxRow`',
        (customerInfo, expectedCallTimes) => {
            buildBalaneInfo(customerInfo);
            expect(balanceInfoMount.find(MemberCreditMaxRow).length).toBe(expectedCallTimes);
        }
    );

    test.each([
        [Super.ViewMasterInfo, 0],
        [Super.ViewAgentInfo, 0],
        [Super.ViewMemberInfo, 1],
        [Master.ViewAgentInfo, 0],
        [Master.ViewMemberInfo, 1],
        [Agent.ViewMemberInfo, 1]])(
        'correct renders a Net Position',
        (customerInfo, expectedCallTimes) => {
            buildBalaneInfo(customerInfo);
            expect(balanceInfoShallow.find('#net-position').length).toBe(expectedCallTimes);
        }
    );

    test.each([
        [Super.ViewMasterInfo, 0],
        [Super.ViewAgentInfo, 0],
        [Super.ViewMemberInfo, 1],
        [Master.ViewAgentInfo, 0],
        [Master.ViewMemberInfo, 1],
        [Agent.ViewMemberInfo, 1]])(
        'correct renders a Bet Credit',
        (customerInfo, expectedCallTimes) => {
            buildBalaneInfo(customerInfo);
            expect(balanceInfoShallow.find('#bet-credit').length).toBe(expectedCallTimes);
        }
    );

    test('render #noinfo when customerInfo is null', () => {
        buildBalaneInfo(null);
        expect(balanceInfoShallow.find('.no-information').length).toBe(1);
    });
});