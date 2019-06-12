import React from 'react';
import ReactDOM from 'react-dom';
import { mount } from 'enzyme';
import sinon from 'sinon';
import renderer from 'react-test-renderer';
import '../../../../../../__mocks__/global.mock';
import ConfirmStatus from 'ConfirmStatus';
import { Resources } from '../../helpers/resources-sample';
import * as accountInfoHelper from 'AccountInfoHelper';
import mockAxios from 'jest-mock-axios';

describe('ConfirmStatus', () => {
    let props, wrapper, event, clock;

    beforeEach(() => {
        props = {
            status: {
                'ConfirmMessage': 'Are you sure you want to change Close status for this customer?',
                'Disabled': false,
                'Checked': false,
                'Url': '/site-accountinfo/SearchAccount/UpdateCustomerStatus',
                'JsonParams': '{\"SearchAccountId\":\"1382145\",\"SyncedCasino3rd\":\"True\",\"StatusName\":\"Closed\",\"StatusValue\":\"True\"}',
                'Text': 'Closed',
                'DisplayOrder': 2,
                'Visible': true,
                'ReloadCustomerInfo': true,
                'IsPopup': false
            },
            resources: Resources,
            mobileView: false,
            fetchCustomerInfoFromServer: jest.fn(),
            fetchCustomerAllStatusFromServer: jest.fn(),
            username: 'user test'
        };

        clock = sinon.useFakeTimers();
        accountInfoHelper.usernameRegExp = jest.fn();
        accountInfoHelper.formatMessage = jest.fn();
        accountInfoHelper.buildTooltipProps = jest.fn();
        accountInfoHelper.resizePopupOnMobile = jest.fn();
        accountInfoHelper.resizeBootboxOnMobile = jest.fn();
        event = Object.assign(jest.fn(), { preventDefault: () => { } });
    });

    afterEach(() => {
        mockAxios.reset();
        clock.restore();
        props = null;
        wrapper = undefined;
        event = undefined;
    });

    const buildWrapper = () => {
        const div = document.createElement('div');
        const input = document.createElement('input');
        input.name = '__RequestVerificationToken';
        input.value = '';

        global.document.body.appendChild(div);
        global.document.body.appendChild(input);
        if (!wrapper) {
            wrapper = mount(<ConfirmStatus {...props} />, { hydrateIn: div });
        }

        return wrapper;
    };

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<ConfirmStatus {...props} />, div);
    });

    test('snapshot', () => {
        const component = renderer.create(<ConfirmStatus {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });


    test('handleOpenPopup() called when clicked', () => {
        global.top.bootboxConfirm = jest.fn();
        const spy = sinon.spy(ConfirmStatus.prototype, 'handleOpenPopup');
        buildWrapper();
        wrapper.find('#confirm-2').simulate('click');
        expect(spy.calledOnce).toBeTruthy();
    });

    test('#openPopup() is called when #handleOpenPopup() is called and status.IsPopup is true', () => {
        props.status.IsPopup =true;
        const spy = sinon.spy(ConfirmStatus.prototype, 'openPopup');
        buildWrapper();
        wrapper.instance().handleOpenPopup(event);
        expect(spy.calledOnce).toBeTruthy();
    });

    test('#openBootboxConfirm() is called when #handleOpenPopup() is called and status.IsPopup is false', () => {
        props.status.IsPopup = false;
        const spy = sinon.spy(ConfirmStatus.prototype, 'openBootboxConfirm');
        buildWrapper();
        wrapper.instance().handleOpenPopup(event);
        expect(spy.calledOnce).toBeTruthy();
    });

    test.each([[true, false, 0], [true, true, 0], [false, true, 1], [false, false, 0]])(
        'correct call accountInfoHelper.resizeBootboxOnMobile()',
        (disabled, mobileView, expectedCallTimes) => {
            props.status.Disabled = disabled;
            props.mobileView = mobileView;
            global.top.bootboxConfirm = jest.fn();
            accountInfoHelper.resizeBootboxOnMobile = jest.fn();

            buildWrapper();
            wrapper.find('#confirm-2').simulate('click');
            expect(accountInfoHelper.resizeBootboxOnMobile).toBeCalledTimes(expectedCallTimes);
        }
    );

    test.each([[false, 1], [true, 0]])(
        'correct call bootboxConfirm',
        (disabled, expectedCallTimes) => {
            props.status.Disabled = disabled;
            global.top.bootboxConfirm = jest.fn();
            buildWrapper();
            wrapper.find('#confirm-2').simulate('click');
            expect(global.top.bootboxConfirm).toBeCalledTimes(expectedCallTimes);
        }
    );

    test('updateCustomerStatus() called when clicked', () => {
        props.status.Disabled = false;
        const spy = jest.spyOn(ConfirmStatus.prototype, 'updateCustomerStatus');
        global.top.bootboxConfirm = (message, target, spy) => spy();
        buildWrapper();

        wrapper.find('#confirm-2').simulate('click');
        expect(spy).toHaveBeenCalledTimes(1);
    });

    test('Call server with specific url and params', () => {
        props.status.Disabled = false;
        buildWrapper();
        wrapper.instance().updateCustomerStatus(event);

        expect(mockAxios.post).toHaveBeenCalledWith(
            '/site-accountinfo/SearchAccount/UpdateCustomerStatus',
            { 'SearchAccountId': '1382145', 'StatusName': 'Closed', 'StatusValue': 'True', 'SyncedCasino3rd': 'True' },
            {
                'headers': { 'X-Requested-With': 'XMLHttpRequest', '__RequestVerificationToken': '' }
            });
    });

    test('always call #fetchCustomerAllStatusFromServer when update successfully', () => {
        props.status.Disabled = false;
        buildWrapper();
        wrapper.instance().updateCustomerStatus(event);

        mockAxios.mockResponse({
            data: {
                'Code': 0,
                'Message': '',
                'IsSuccess': true
            }
        });

        clock.tick(3000);
        expect(props.fetchCustomerAllStatusFromServer).toHaveBeenCalled();
    });

    test.each([[true, 1], [false, 0]])(
        'call #fetchCustomerInfoFromServer base on status.ReloadCustomerInfo when update successfully',
        (reloadCustomerInfo, expectedCallTimes) => {
            props.status.Disabled = false;
            props.status.ReloadCustomerInfo = reloadCustomerInfo;
            buildWrapper();
            wrapper.instance().updateCustomerStatus(event);

            mockAxios.mockResponse({
                data: {
                    'Code': 0,
                    'Message': '',
                    'IsSuccess': true
                }
            });

            clock.tick(3000);
            expect(props.fetchCustomerInfoFromServer).toHaveBeenCalledTimes(expectedCallTimes);
        }
    );

    test.each([[true, null], [false, undefined]])(
        'call #window.top.bootboxAlert with correct position param when update failed',
        (mobileView, expectedPosition) => {
        props.status.Disabled = false;
        props.mobileView = mobileView;

        buildWrapper();
        wrapper.instance().updateCustomerStatus(event);

        mockAxios.mockResponse({
            data: {
                'Code': 10,
                'Message': '',
                'IsSuccess': false
            }
        });

        clock.tick(3000);
        expect(window.top.bootboxAlert).toHaveBeenCalledWith('', expectedPosition);
    });

    test.each([[true, 1], [false, 0]])(
        'correct call accountInfoHelper.resizePopupOnMobile()',
        (mobileView, expectedCallTimes) => {
            props.mobileView = mobileView;
            global.top.bootboxConfirm = jest.fn();
            accountInfoHelper.resizePopupOnMobile = jest.fn();

            buildWrapper();
            wrapper.instance().openPopup(event);
            expect(accountInfoHelper.resizePopupOnMobile).toBeCalledTimes(expectedCallTimes);
        }
    );

    test.each([[false, true, 0], [false, false, 0], [true, false, 0], [true, true, 1]])(
        'call correct #fetchCustomerInfoFromServer and #fetchCustomerAllStatusFromServer base on window.top.realoadLeftPanel && window.top.closedPopup',
        (closedPopup, realoadLeftPanel, expectedCallTimes) => {
            window.top.ageWnd.open = jest.fn();
            props.status.Disabled = false;
            buildWrapper();

            wrapper.instance().openPopup(event);
            window.top.realoadLeftPanel = realoadLeftPanel;
            window.top.closedPopup = closedPopup;
            clock.tick(1000);
            expect(props.fetchCustomerInfoFromServer).toHaveBeenCalledTimes(expectedCallTimes);
            expect(props.fetchCustomerAllStatusFromServer).toHaveBeenCalledTimes(expectedCallTimes);
        });
});