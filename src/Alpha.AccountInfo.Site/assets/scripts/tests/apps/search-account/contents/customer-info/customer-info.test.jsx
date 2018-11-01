import React from 'react';
import ReactDOM from 'react-dom';
import renderer from 'react-test-renderer';
import CustomerInfo from 'CustomerInfo';
import { mount } from "enzyme";
import { Super } from '../../helpers/actor-is-super-sample';
import { Master } from '../../helpers/actor-is-master-sample';
import { Agent } from '../../helpers/actor-is-agent-sample';
import { Resources } from '../../helpers/resources-sample';
import SearchAccountInfo from 'SearchAccountInfo';
import UplineInfo from 'UplineInfo';
import CustomAnchorTag from 'CustomAnchorTag';

describe('CustomerInfo', () => {
    let props;
    let customerInfoMount;

    beforeEach(() => {
        props = {
            customerInfo: Super.ViewMasterInfo,
            resources: Resources,
            mobileView: false,
            handleSearchFromUserLevel: jest.fn()
        };

        customerInfoMount = undefined;
    });

    const buildCustomerInfo = (customerInfo) => {
        props.customerInfo = customerInfo;
        if (!customerInfoMount) {
            customerInfoMount = mount(<CustomerInfo {...props} />);
        }

        return customerInfoMount;
    }

    it('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<CustomerInfo {...props} />, div);
    });

    it('snapshot', () => {
        const component = renderer.create(<CustomerInfo {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    test.each([
        Super.ViewMasterInfo,
        Super.ViewAgentInfo,
        Super.ViewMemberInfo,
        Master.ViewAgentInfo,
        Master.ViewMemberInfo,
        Agent.ViewMemberInfo])(
        "always renders a `SearchAccountInfo`",
        (customerInfo) => {
            buildCustomerInfo(customerInfo);
            expect(customerInfoMount.find(SearchAccountInfo).length).toBe(1);
        }
    );

    test.each([
        [Super.ViewMasterInfo, 0],
        [Super.ViewAgentInfo,1],
        [Super.ViewMemberInfo, 2],
        [Master.ViewAgentInfo, 0],
        [Master.ViewMemberInfo, 1],
        [Agent.ViewMemberInfo, 0]])(
        "always renders `UplineInfo` callTimes",
        (customerInfo, expectedCallTimes) => {
            buildCustomerInfo(customerInfo);
            expect(customerInfoMount.find(UplineInfo).length).toBe(expectedCallTimes);
        }
    );

    test.each([
        Super.ViewMasterInfo,
        Super.ViewAgentInfo,
        Super.ViewMemberInfo,
        Master.ViewAgentInfo,
        Master.ViewMemberInfo,
        Agent.ViewMemberInfo])(
        "always renders 3 `CustomAnchorTag`",
        (customerInfo) => {
            buildCustomerInfo(customerInfo);
            expect(customerInfoMount.find(CustomAnchorTag).length).toBe(3);
        }
    );
});