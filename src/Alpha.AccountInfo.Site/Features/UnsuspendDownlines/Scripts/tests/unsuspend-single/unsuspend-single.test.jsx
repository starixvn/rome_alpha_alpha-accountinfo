import React from 'react';
import ReactDOM from 'react-dom';
import '../../../../../__mocks__/global.mock';
import { mount } from 'enzyme';
import sinon from 'sinon';
import renderer from 'react-test-renderer';
import UnsuspendSingle from 'UnsuspendSingle';
import * as unsuspendHelper from 'UnsuspendHelper';
import { BodyRowContent, FooterContent } from 'UnsuspendSingleComponent';
import { resources, responseUnsuspendSingleData } from '../unspend-downlines-resources.mock';
import * as _ from 'lodash';
import mockAxios from 'jest-mock-axios';

describe('UnsuspendSingle', () => {
    let props, wrapper, clock;

    beforeEach(() => {
        props = {
            viewModel: _.cloneDeep(responseUnsuspendSingleData),
            resources,
            redirectViewListDetail: jest.fn()
        };

        clock = sinon.useFakeTimers();
    });

    afterEach(() => {
        mockAxios.reset();
        clock.restore();
        props = null;
        wrapper = undefined;
        window.top.reloadCustomerList = null;
        window.top.realoadLeftPanel = null;
        window.top.closedPopup = null;
    });

    const buildWrapper = () => {
        if (!wrapper) {
            wrapper = mount(<UnsuspendSingle {...props} />);
        }

        return wrapper;
    }

    test('Renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<UnsuspendSingle {...props} />, div);
    });

    test('Snapshot', () => {
        const component = renderer.create(<UnsuspendSingle {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    test('renders #FooterContent component', () => {
        buildWrapper();
        expect(wrapper.find(FooterContent).length).toBe(1);
    });

    test.each([[1, 0, 1], [0, 1, 1], [0, 0, 0]])(
        "Should render correct #BodyRowContent components",
        (affectedDownlinesTotal, unchangedDownlinesTotal, expectedBodyRows) => {
            props.AffectedDownlinesTotal = affectedDownlinesTotal;
            props.UnchangedDownlinesTotal = unchangedDownlinesTotal;

            buildWrapper();
            expect(wrapper.find(BodyRowContent).length).toBeGreaterThanOrEqual(expectedBodyRows);
        }
    );

    test('renders #BodyRowContent component', () => {
        buildWrapper();
        expect(wrapper.find(BodyRowContent).length).toBeGreaterThanOrEqual(1);
    });

    test('#viewInfomation called when clicked viewInfomation icon', () => {
        unsuspendHelper.viewInfomation = jest.fn();
        buildWrapper();

        wrapper.find('.icon-information-outline').simulate('click');
        expect(unsuspendHelper.viewInfomation).toHaveBeenCalled();
    });

    test('#handleSubmit called when clicked submit', () => {
        const spy = sinon.spy(UnsuspendSingle.prototype, 'handleSubmit');
        buildWrapper();
        
        wrapper.find('.btn-submit').simulate('click');
        expect(spy.calledOnce).toBeTruthy();
    });

    test.each([
        [1, 0, "Are you sure you want to change Suspend status? All affected downlines will be changed Suspend status as well."], 
        [0, 1, "Are you sure you want to change Suspend status? All affected downlines will be changed Suspend status as well."], 
        [0, 0, "Are you sure you want to change Suspend status for this customer?"]])(
        "Should render correct confirm message base on affected downlines",
        (affectedDownlinesTotal, unchangedDownlinesTotal, expectedMessage) => {
            props.viewModel.AffectedDownlinesTotal = affectedDownlinesTotal;
            props.viewModel.UnchangedDownlinesTotal = unchangedDownlinesTotal;

            buildWrapper();
            const mockedEvent = { target: {} }
            const spy = jest.spyOn(wrapper.instance(), 'doUpdate');
            wrapper.instance().handleSubmit(mockedEvent);
            expect(window.top.bootboxConfirm).toHaveBeenCalledWith(expectedMessage, mockedEvent.target, spy);
        }
    );

    test('Call server with specific url and params', () => {
        buildWrapper();
        wrapper.instance().doUpdate();

        expect(mockAxios.post).toHaveBeenCalledWith(
            '/site-accountinfo/UnsuspendDownlines/UnsuspendSingle', 
            { "SearchAccountId":  36204210},
            {"headers": {"X-Requested-With": "XMLHttpRequest", "__RequestVerificationToken": ""}
        });
    });

    test('Render and does not call callback when update failed', () => {
        buildWrapper();
        wrapper.instance().doUpdate();
        mockAxios.mockResponse({ data: {
            "Code": 10,
            "Message": "Update Failed.",
            "IsSuccess": false
          }
        });

        expect(wrapper.instance().state.hideMessages).toBeFalsy();
        expect(wrapper.find('.msg-error').length).toBe(1);
        expect(wrapper.find('.msg-error').text()).toBe("Update Failed.");
        expect(window.top.main.DelayReloadPage).toHaveBeenCalledTimes(0);

        clock.tick(3000);
        expect(window.top.ageWnd.close).toHaveBeenCalledTimes(0);
    });

    test('#window.top.main.DelayReloadPage does not call when window.top.reloadCustomerList is false', () => {
        window.top.reloadCustomerList = false;
        buildWrapper();
        wrapper.instance().doUpdate();
        mockAxios.mockResponse({ data: {
            "Code": 0,
            "Message": "",
            "IsSuccess": true
          }
        });
        expect(window.top.main.DelayReloadPage).toHaveBeenCalledTimes(0);
    });

    test('Render and call callback when update successfully', () => {
        buildWrapper();
        wrapper.instance().doUpdate();
        window.top.reloadCustomerList = true;
        mockAxios.mockResponse({ data: {
            "Code": 0,
            "Message": "",
            "IsSuccess": true
          }
        });

        expect(wrapper.instance().state.hideMessages).toBeFalsy();
        expect(wrapper.find('.msg-success').length).toBe(1);
        expect(wrapper.find('.msg-success').text()).toBe("Successfully updated!");
        expect(window.top.main.DelayReloadPage).toHaveBeenCalled();
        
        clock.tick(3000);
        expect(window.top.ageWnd.close).toHaveBeenCalled();
        expect(window.top.realoadLeftPanel).toBe(true);
        expect(window.top.closedPopup).toBe(true);
    });
});