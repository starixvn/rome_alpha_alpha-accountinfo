import React from 'react';
import ReactDOM from 'react-dom';
import renderer from 'react-test-renderer';
import { shallow } from 'enzyme';
import { Master } from '../../helpers/actor-is-master-sample';
import { Resources } from '../../helpers/resources-sample';
import SearchAccountInfo from 'SearchAccountInfo';
import CustomAnchorTag from 'CustomAnchorTag';
import { Tooltip } from 'react-tippy';
import * as _ from 'lodash';
import * as accountInfoHelper from 'AccountInfoHelper';

describe('SearchAccountInfo', () => {
    let props;
    beforeEach(() => {
        props = {
            customerInfo: _.cloneDeep(Master.ViewAgentInfo),
            resources: Resources,
            mobileView: false
        };

        accountInfoHelper.buildTooltipProps = jest.fn();
    });

    it('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<SearchAccountInfo {...props} />, div);
    });

    //// it('snapshot', () => {
    ////     const component = renderer.create(<SearchAccountInfo {...props} />);
    ////     expect(component.toJSON()).toMatchSnapshot();
    //// });

    test.each([
        [true, 0, true, true, 'closed-wrongPwdOthers', 'This account was closed by failed login or account expired.'],
        [true, 0, true, false, 'closed-wrongPwdOthers', 'This account was closed by failed login or account expired.'],
        [true, 0, false, false, 'closed-wrongPwdOthers', 'This account was closed by failed login or account expired.'],
        [true, 1, false, false, 'closed-byAdmin', 'This account was closed by admin.'],
        [true, 2, false, false, 'closed-byUpline', 'This account was closed by upline.'],
        [true, 3, false, false, 'closed-wrongOTPSC', 'This account was closed due to failed OTP/Security Code attempts.'],
        [false, 0, true, false, 'suspended', 'This account was suspended.'],
        [false, 0, false, false, 'disabled', 'This account was disabled.'],
        [false, 0, false, true, 'enabled', '']])(
        'return correct status css and tooltip',
        (closed, closedReason, suspended, enabled, expectedCss, expectedTooltip) => {
            props.customerInfo.Closed = closed;
            props.customerInfo.ClosedReason = closedReason;
            props.customerInfo.Suspended = suspended;
            props.customerInfo.Enabled = enabled;

            const component = shallow(<SearchAccountInfo {...props} />);
            expect(component.state('Css')).toEqual(expectedCss);
            expect(component.state('Tootltip')).toEqual(expectedTooltip);
        }
    );

    test.each([
        [true, 0, true, true, 1],
        [true, 0, true, false, 1],
        [true, 0, false, false, 1],
        [true, 1, false, false, 1],
        [true, 2, false, false, 1],
        [true, 3, false, false, 1],
        [false, 0, true, false, 1],
        [false, 0, false, false, 1],
        [false, 0, false, true, 1]])(
        'return correct #Tooltip call times',
        (closed, closedReason, suspended, enabled, expectedCallTimes) => {
            props.customerInfo.Closed = closed;
            props.customerInfo.ClosedReason = closedReason;
            props.customerInfo.Suspended = suspended;
            props.customerInfo.Enabled = enabled;

            const component = shallow(<SearchAccountInfo {...props} />);

            expect(component.find(Tooltip).length).toBe(expectedCallTimes);
        }
    );

    describe('componentWillReceiveProps()', () => {
        test('change Css and Tooltip when customerInfo change ', () => {
            props.customerInfo = Master.ViewAgentInfo;
            const component = shallow(<SearchAccountInfo {...props} />);
            
            let customerInfo = Master.ViewMemberInfo;
            customerInfo.Closed = false;
            customerInfo.Suspended = true;
            component.setProps({ customerInfo });

            expect(component.state('Css')).toEqual('suspended');
            expect(component.state('Tootltip')).toEqual('This account was suspended.');
        });

        test('unchange Css and Tooltip when customerInfo unchange ', () => {
            let customerInfo = Master.ViewMemberInfo;
            customerInfo.Closed = false;
            customerInfo.Suspended = true;

            props.customerInfo = customerInfo;
            const component = shallow(<SearchAccountInfo {...props} />);
            component.setProps({ customerInfo });

            expect(component.state('Css')).toEqual('suspended');
            expect(component.state('Tootltip')).toEqual('This account was suspended.');
        });
    });

    test.each([[true, 1], [false, 0]])(
        'return correct #CustomAnchorTag  call times base on ViewLog Permission',
        (canViewLog, expectedCallTimes) => {
            props.customerInfo.CanViewLog = canViewLog;
            const component = shallow(<SearchAccountInfo {...props} />);

            expect(component.find(CustomAnchorTag).length).toBe(expectedCallTimes);
        }
    );
});