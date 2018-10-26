import React from 'react';
import ReactDOM from 'react-dom';
import { mount, shallow } from "enzyme";
import { MasterLevelInfo } from '../../helpers/master-level-info-sample';
import { AgentLevelInfo } from '../../helpers/agent-level-info-sample';
import { MemberLevelInfo } from '../../helpers/member-level-info-sample ';
import { Resources } from '../../helpers/resources-sample';
import BalanceInfoRow from 'BalanceInfoRow';
import GivenCreditRow from 'GivenCreditRow';
import MemberCreditMaxRow from 'MemberCreditMaxRow';
import BalanceInfo from 'BalanceInfo';

describe('BalanceInfo', () => {
    let props;
    beforeEach(() => {
        props = {
            customerInfo: MasterLevelInfo,
            resources: Resources,
            mobileView: false,
            fetchCustomerInfoFromServer: jest.fn()
        };
    });

    //const renderSnapshot = () => {
    //    const component = renderer.create(<BalanceInfo {...props} />);
    //    return component.toJSON();
    //}

    it('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<BalanceInfo {...props} />, div);
    });

    describe("Master Level", () => {
        let balanceInfoMount, balanceInfoShallow;
        beforeEach(() => {
            props.customerInfo = MasterLevelInfo;
            balanceInfoMount = mount(<BalanceInfo {...props} />);
            balanceInfoShallow = shallow(<BalanceInfo {...props}/>);
        });
        //it('snapshot Master', () => {
        //    expect(renderSnapshot()).toMatchSnapshot();
        //});

        it("renders 6 `BalanceInfoRow`", () => {
            const balanceInfoRow = mount(<BalanceInfo {...props} />);
            expect(balanceInfoRow.find(BalanceInfoRow).length).toBe(6);
        });

        it("renders a Total Cash Row", () => {
            expect(balanceInfoShallow.find("#total-cash").length).toBe(1);
        });

        it("renders a Yesterday Cash Row", () => {
            expect(balanceInfoShallow.find("#yesterday-cash").length).toBe(1);
        });

        it("renders a Total Balance Row", () => {
            expect(balanceInfoShallow.find("#total-balance").length).toBe(1);
        });

        it("renders a Yesterday Balance Row", () => {
            expect(balanceInfoShallow.find("#yesterday-balance").length).toBe(1);
        });
        
        it("renders a Report Link Row", () => {
            expect(balanceInfoShallow.find("#report-link").length).toBe(1);
        });

        it("renders a Used Credit Row", () => {
            expect(balanceInfoShallow.find("#used-credit").length).toBe(1);
        });

        it("renders a `GivenCreditRow`", () => {
            expect(balanceInfoMount.find(GivenCreditRow).length).toBe(1);
        });
    });

    describe("Agent Level", () => {
        let balanceInfoMount, balanceInfoShallow;
        beforeEach(() => {
            props.customerInfo = AgentLevelInfo;
            balanceInfoMount = mount(<BalanceInfo {...props} />);
            balanceInfoShallow = shallow(<BalanceInfo {...props}/>);
        });

        it("renders 6 `BalanceInfoRow`", () => {
            const balanceInfoRow = mount(<BalanceInfo {...props} />);
            expect(balanceInfoRow.find(BalanceInfoRow).length).toBe(6);
        });

        it("renders a Total Cash Row", () => {
            expect(balanceInfoShallow.find("#total-cash").length).toBe(1);
        });

        it("renders a Yesterday Cash Row", () => {
            expect(balanceInfoShallow.find("#yesterday-cash").length).toBe(1);
        });

        it("renders a Total Balance Row", () => {
            expect(balanceInfoShallow.find("#total-balance").length).toBe(1);
        });

        it("renders a Yesterday Balance Row", () => {
            expect(balanceInfoShallow.find("#yesterday-balance").length).toBe(1);
        });
        
        it("renders a Report Link Row", () => {
            expect(balanceInfoShallow.find("#report-link").length).toBe(1);
        });

        it("renders a Used Credit Row", () => {
            expect(balanceInfoShallow.find("#used-credit").length).toBe(1);
        });

        it("renders a `GivenCreditRow`", () => {
            expect(balanceInfoMount.find(GivenCreditRow).length).toBe(1);
        });

        it("renders a `MemberCreditMaxRow`", () => {
            expect(balanceInfoMount.find(MemberCreditMaxRow).length).toBe(1);
        });
    });

    describe("Member Level", () => {
        let balanceInfoMount, balanceInfoShallow;
        beforeEach(() => {
            props.customerInfo = MemberLevelInfo;
            balanceInfoMount = mount(<BalanceInfo {...props} />);
            balanceInfoShallow = shallow(<BalanceInfo {...props}/>);
        });

        it("renders 5 `BalanceInfoRow`", () => {
            expect(balanceInfoMount.find(BalanceInfoRow).length).toBe(5);
        });

        it("renders a Total Balance Row", () => {
            expect(balanceInfoShallow.find("#total-balance").length).toBe(1);
        });

        it("renders a Yesterday Balance Row", () => {
            expect(balanceInfoShallow.find("#yesterday-balance").length).toBe(1);
        });
        
        it("renders a Report Link Row", () => {
            expect(balanceInfoShallow.find("#report-link").length).toBe(1);
        });

        it("renders a `GivenCreditRow`", () => {
            expect(balanceInfoMount.find(GivenCreditRow).length).toBe(1);
        });

        it("renders a Net Position Row", () => {
            expect(balanceInfoShallow.find("#net-position").length).toBe(1);
        });

        it("renders a Bet Credit Row", () => {
            expect(balanceInfoShallow.find("#bet-credit").length).toBe(1);
        });
    });
});