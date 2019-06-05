import React from 'react';
import ReactDOM from 'react-dom';
import '../../../../../__mocks__/global.mock';
import { mount } from 'enzyme';
import sinon from 'sinon';
import renderer from 'react-test-renderer';
import UnsuspendMultiple from 'UnsuspendMultiple';
import { RowContent } from 'UnsuspendMultipleComponent';
import * as unsuspendHelper from 'UnsuspendHelper';
import { resources, responseUnsuspendMultipleData } from '../unspend-downlines-resources.mock';
import * as _ from 'lodash';
import mockAxios from 'jest-mock-axios';

describe('UnsuspendMultiple', () => {
    let props, wrapper, clock;

    beforeEach(() => {
        props = {
            viewModel: _.cloneDeep(responseUnsuspendMultipleData.Data),
            originalViewModel: _.cloneDeep(responseUnsuspendMultipleData.Data),
            searchAccountRoleId: responseUnsuspendMultipleData.SearchAccountRoleId,
            resources,
            handleReset: jest.fn(),
            handleChange: jest.fn(),
            toggleSuspendAll: jest.fn(),
            redirectViewListDetail: jest.fn()
        };

        clock = sinon.useFakeTimers();
    });

    afterEach(() => {
        mockAxios.reset();
        clock.restore();
        props = null;
        wrapper = undefined;
    });

    const buildWrapper = () => {
        if (!wrapper) {
            wrapper = mount(<UnsuspendMultiple {...props} />);
        }

        return wrapper;
    }

    test('Renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<UnsuspendMultiple {...props} />, div);
    });

    test('Snapshot', () => {
        const component = renderer.create(<UnsuspendMultiple {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    test.each([['.btn-suspend-all'], ['.btn-unsuspend-all']])(
        'Change call #toggleSuspendAll when click toggle suspended/unsuspended all',
        (buttonClass) => {
            buildWrapper();
            const event = Object.assign(jest.fn(), {preventDefault: () => {}});
            wrapper.find(buttonClass).simulate('click', event);
            expect(props.toggleSuspendAll).toBeCalledTimes(1);
        }
    );

    test('#viewInfomation called when clicked viewInfomation icon', () => {
        unsuspendHelper.viewInfomation = jest.fn();
        buildWrapper();

        wrapper.find('.icon-information-outline').simulate('click');
        expect(unsuspendHelper.viewInfomation).toHaveBeenCalled();
    });
    
    test('Header should render 2 rows', () => {
        buildWrapper();
        expect(wrapper.find('thead').find('tr').length).toBe(2);
    });

    test.each([[3, 4], [2, 4], [1, 2]])(
        'Should render correct header columns',
        (searchAccountRoleId, expectedColumns) => {
            props.searchAccountRoleId = searchAccountRoleId;
            buildWrapper();
            expect(wrapper.find('thead').find('tr').at(0).find('th').length).toBe(expectedColumns);
        }
    );

    test.each([
        [3, 'Affected Downlines <br/> (Agent/Member)', 'Remain Unchanged Downlines <br/> (Agent/Member)'], 
        [2, 'Affected Downlines <br/> (Member)', 'Remain Unchanged Downlines <br/> (Member)']])(
        'Should render correct header text base on level',
        (searchAccountRoleId, expectedAffectedText, expectedRemainUnchangedText) => {
            props.searchAccountRoleId = searchAccountRoleId;
            buildWrapper();
            expect(wrapper.find('thead').find('tr').at(0).find('th').at(2).text()).toBe(expectedAffectedText);
            expect(wrapper.find('thead').find('tr').at(0).find('th').at(3).text()).toBe(expectedRemainUnchangedText);
        }
    );

    test('Should render #RowContent when has data', () => {
        buildWrapper();
        expect(wrapper.find(RowContent).length).toBeGreaterThanOrEqual(1);
    });

    test('#handleReset called when clicked reset button', () => {
        buildWrapper();
        wrapper.find('.btn-reset').simulate('click');
        expect(props.handleReset).toHaveBeenCalled();
    });

    test('Should render correct #Disabled submit button base on whether data change', () => {
        buildWrapper();
        expect(wrapper.find('.btn-submit').prop('disabled')).toBeTruthy();

        props.viewModel[0].Suspended = !props.viewModel[0].Suspended;
        expect(wrapper.find('.btn-submit').prop('disabled')).toBeTruthy();
    });

    test('#handleSubmit called when clicked submit', () => {
        const spy = sinon.spy(UnsuspendMultiple.prototype, 'handleSubmit');
        buildWrapper();
        
        props.viewModel[0].Suspended = !props.viewModel[0].Suspended;
        wrapper.find('.btn-submit').simulate('click');
        expect(spy.calledOnce).toBeTruthy();
    });

    test('#doUpdate called when clicked ok button on bootbox confirm', () => {
        const spy = sinon.spy(UnsuspendMultiple.prototype, 'doUpdate');
        window.top.bootboxConfirm = function(message, handleElement, acceptCallback, cancelCallback, popUpOptions)
        {
            acceptCallback();
        }

        buildWrapper();
        
        props.viewModel[0].Suspended = !props.viewModel[0].Suspended;
        wrapper.find('.btn-submit').simulate('click');
        expect(spy.calledOnce).toBeTruthy();
    });

     test('Call server with specific url and params', () => {
        buildWrapper();

        props.viewModel[0].Suspended = !props.viewModel[0].Suspended;
        wrapper.instance().doUpdate();

        expect(mockAxios.post).toHaveBeenCalledWith(
            '/site-accountinfo/UnsuspendDownlines/UnsuspendMultiple', 
            { 'UnsuspendList':  [{
                'SearchAccountId': 36204210,
                'Username': 'davidtest13May01',
                'Suspended': false
            }]},
            {'headers': {'X-Requested-With': 'XMLHttpRequest', '__RequestVerificationToken': ''}
        });
    });

    test('Render and does not call callback when update failed', () => {
        buildWrapper();

        props.viewModel[0].Suspended = !props.viewModel[0].Suspended;
        wrapper.instance().doUpdate();
        mockAxios.mockResponse({ data:  {
            'Results': [
              {
                'Code': 10,
                'Message': '',
                'IsSuccess': false
              }
            ]
          }
        });

       expect(wrapper.find('.msg-error').length).toBe(1);
       expect(window.top.main.DelayReloadPage).toHaveBeenCalledTimes(0);

       clock.tick(3000);
       expect(window.top.ageWnd.close).toHaveBeenCalledTimes(0);
    });

    test('Render and call callback when update successfully', () => {
        buildWrapper();

        props.viewModel[0].Suspended = !props.viewModel[0].Suspended;
        wrapper.instance().doUpdate();
        mockAxios.mockResponse({ data:  {
            'Results': [
              {
                'Code': 0,
                'Message': '',
                'IsSuccess': true
              }
            ]
          }
        });

        expect(wrapper.instance().state.hideMessages).toBeFalsy();
        expect(wrapper.find('.msg-success').length).toBe(1);

        expect(window.top.main.DelayReloadPage).toHaveBeenCalled();
        
        clock.tick(3000);
        expect(window.top.ageWnd.close).toHaveBeenCalled();
    });
});