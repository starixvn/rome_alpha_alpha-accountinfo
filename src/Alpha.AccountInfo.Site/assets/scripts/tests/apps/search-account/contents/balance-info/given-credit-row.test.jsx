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

    describe("handleOpenGivenCreditPopup()" , () => {
        it('called when clicked', () => {
            const spy = sinon.spy(GivenCreditRow.prototype, 'handleOpenGivenCreditPopup');
            const wrapper = shallow(<GivenCreditRow {...props}/>)
    
            wrapper.find('#given-credit-popup').simulate('click');
            expect(spy.calledOnce).toBeTruthy();
        });

        describe("accountInfoHelper.mobileScale()", () => {
            accountInfoHelper.resizePopupOnMobile = jest.fn();

            it('not call if `mobileView` is false', () => {
                props.mobileView = false;
    
                const wrapper = shallow(<GivenCreditRow {...props} />);
                wrapper.find('#given-credit-popup').simulate('click');
                expect(accountInfoHelper.resizePopupOnMobile).toBeCalledTimes(0);
            });
    
            it('call if `mobileView` is true', () => {
                props.mobileView = true;
    
                const wrapper = shallow(<GivenCreditRow {...props} />);
                wrapper.find('#given-credit-popup').simulate('click');
                expect(accountInfoHelper.resizePopupOnMobile).toBeCalledTimes(1);
            });
        });

        describe("fetchCustomerInfoFromServer() ", () => {
            let clock;
            beforeEach(function() {
                clock = sinon.useFakeTimers();
              });

              afterEach(function() {
                clock.restore();
              });

              it('call if close popup and updated given credit successfully', () => {
                const wrapper = shallow(<GivenCreditRow {...props}/>);
                wrapper.find('#given-credit-popup').simulate('click');

                global.top.closedPopup = true;
                global.top.realoadLeftPanel = true;
                clock.tick(1000);
               
                expect(props.fetchCustomerInfoFromServer).toBeCalledTimes(1);
            });

            it('not call if close popup and does not do update/update fail', () => {
                const wrapper = shallow(<GivenCreditRow {...props}/>);

                wrapper.find('#given-credit-popup').simulate('click');
                global.top.closedPopup = true;
                global.top.realoadLeftPanel = false;
                clock.tick(1000);
               
                expect(props.fetchCustomerInfoFromServer).toBeCalledTimes(0);
            });

            it('not call if does not close popup', () => {
                const wrapper = shallow(<GivenCreditRow {...props}/>);
                wrapper.find('#given-credit-popup').simulate('click');
                global.top.closedPopup = false;
                clock.tick(1000);

                expect(props.fetchCustomerInfoFromServer).toBeCalledTimes(0);
            });
        });
    });
});