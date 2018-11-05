import React from 'react';
import ReactDOM from 'react-dom';
import { mount, shallow } from "enzyme";
import sinon from 'sinon';
import renderer from 'react-test-renderer';
import ConfirmStatus from 'ConfirmStatus';
import { Resources } from '../../helpers/resources-sample';
import * as accountInfoHelper from 'AccountInfoHelper';
import $ from 'jquery';
import { accountInfoAPI } from 'APIClient';
import mockAxios from '../../../../__mocks__/axios';

describe('ConfirmStatus', () => {
    let props;
    global.$ = global.jQuery = $;
    global.$.blockUI =jest.fn();
    global.$.unblockUI =jest.fn();
    global.__RequestVerificationToken = '';

    beforeEach(() => {
        props = {
            status:{
                "ConfirmMessage": "Are you sure you want to change Close status for this customer?",
                "Disabled": false,
                "Checked": false,
                "Url": "/site-accountinfo/(S(nycmx2rjfmd5hmyywrpmvhzc))/SearchAccount/UpdateCustomerStatus",
                "JsonParams": "{\"SearchAccountId\":\"1382145\",\"SyncedCasino3rd\":\"True\",\"StatusName\":\"Closed\",\"StatusValue\":\"True\"}",
                "Text": "Closed",
                "DisplayOrder": 2,
                "Visible": true
              },
            resources: Resources,
            mobileView: false,
            fetchCustomerInfoFromServer: jest.fn()
        };
    });

    afterEach(() => {
        mockAxios.reset();
    });

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
        const wrapper = mount(<ConfirmStatus {...props} />)

        wrapper.find('#confirm-Closed').simulate('click');
        expect(spy.calledOnce).toBeTruthy();
    });

    test.each([[true, false, 0], [true, true, 0], [false, true, 1], [false, false, 0]])(
        "correct call accountInfoHelper.resizeBootboxOnMobile()",
        (disabled, mobileView, expectedCallTimes) => {
            props.status.Disabled = disabled;
            props.mobileView = mobileView;
            global.top.bootboxConfirm = jest.fn();
            accountInfoHelper.resizeBootboxOnMobile = jest.fn();

            const wrapper = mount(<ConfirmStatus {...props} />);
            wrapper.find('#confirm-Closed').simulate('click');
            expect(accountInfoHelper.resizeBootboxOnMobile).toBeCalledTimes(expectedCallTimes);
        } 
    );

    test.each([[false, 1],[true, 0]])(
        "correct call bootboxConfirm",
        (disabled, expectedCallTimes) => {
            props.status.Disabled = disabled;
            global.top.bootboxConfirm = jest.fn();
            const wrapper = mount(<ConfirmStatus {...props} />);
            wrapper.find('#confirm-Closed').simulate('click');
            expect(global.top.bootboxConfirm).toBeCalledTimes(expectedCallTimes);
        }
    );

    test('updateCustomerStatus() called when clicked', () => {
        props.status.Disabled = false;
        const spy = jest.spyOn(ConfirmStatus.prototype, 'updateCustomerStatus');
        global.top.bootboxConfirm = (message, target, spy ) => spy();
        const wrapper = mount(<ConfirmStatus {...props} />)

        wrapper.find('#confirm-Closed').simulate('click');
        expect(spy).toHaveBeenCalledTimes(1);
    });

    //test('updateCustomerStatus() called when clicked', () => {
    //    let catchFn = jest.fn(), thenFn = jest.fn();
    //    accountInfoAPI.updateCustomerStatus('',{})
    //        .then(thenFn)
    //        .catch(catchFn);

    //    let responseObj = { data: {IsSuccess: true} };
    //    mockAxios.mockResponse(responseObj);

    //    props.status.Disabled = false;
    //    const spy = sinon.spy(ConfirmStatus.prototype, 'updateCustomerStatus');
    //    global.top.bootboxConfirm = (message, target, spy ) => spy();
    //    const wrapper = mount(<ConfirmStatus {...props} />)

    //    wrapper.find('#confirm-Closed').simulate('click');
    //    expect(props.fetchCustomerInfoFromServer).toBeCalledTimes(1);
    //});
});