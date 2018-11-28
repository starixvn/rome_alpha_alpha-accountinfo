import React from 'react';
import ReactDOM from 'react-dom';
import { mount, shallow } from "enzyme";
import sinon from 'sinon';
import renderer from 'react-test-renderer';
import NumberFormat from 'react-number-format';
import GivenCreditRow from 'GivenCreditRow';
import {Resources} from '../../helpers/resources-sample';
import { Master } from '../../helpers/actor-is-master-sample';
import * as accountInfoHelper from 'AccountInfoHelper';

describe('GivenCreditRow', () => {
    let props;
    global.accountInfoSiteAppPath= "/accountInfo-site";
    global.top.ageWnd =  {
        open: jest.fn()
    };

    beforeEach(() => {
        props = {
            customerInfo: Master.ViewAgentInfo,
            resources: Resources,
            mobileView: false,
            fetchCustomerInfoFromServer: jest.fn()
        };
    });

    it('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<GivenCreditRow {...props}/>, div);
    });

    it('snapshot', () => {
        const component = renderer.create(<GivenCreditRow {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    it("renders a `NumberFormat`", () => {
        const givenCreditRow = mount(<GivenCreditRow {...props} />);
        expect(givenCreditRow.find(NumberFormat).length).toBe(1);
    });

    describe("handleOpenGivenCreditPopup()", () => {
        let clock;
        beforeEach(function () {
            clock = sinon.useFakeTimers();
        });

        afterEach(function () {
            clock.restore();
        });

        it('called when clicked', () => {
            const spy = sinon.spy(GivenCreditRow.prototype, 'handleOpenGivenCreditPopup');
            const wrapper = shallow(<GivenCreditRow {...props}/>)
    
            wrapper.find('#given-credit-popup').simulate('click');
            expect(spy.calledOnce).toBeTruthy();
        });

        test.each([[true, 1], [false, 0]])(
                "correct call accountInfoHelper.resizePopupOnMobile()",
            (mobileView, expectedCallTimes) => {
                props.mobileView = mobileView;
                accountInfoHelper.resizePopupOnMobile = jest.fn();

                const wrapper = shallow(<GivenCreditRow {...props} />);
                wrapper.find('#given-credit-popup').simulate('click');
                expect(accountInfoHelper.resizePopupOnMobile).toBeCalledTimes(expectedCallTimes);
                }
        );

        test.each([[true, true, 1], [true, false, 0], [false, false, 0]])(
            "correct call fetchCustomerInfoFromServer()",
            (closedPopup, realoadLeftPanel, expectedCallTimes) => {

                const wrapper = shallow(<GivenCreditRow {...props} />);
                wrapper.find('#given-credit-popup').simulate('click');

                global.top.closedPopup = closedPopup;
                global.top.realoadLeftPanel = realoadLeftPanel;
                clock.tick(1000);

                expect(props.fetchCustomerInfoFromServer).toBeCalledTimes(expectedCallTimes);
            }
        );
    });
});