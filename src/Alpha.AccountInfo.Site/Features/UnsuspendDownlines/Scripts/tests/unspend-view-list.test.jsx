import React from 'react';
import ReactDOM from 'react-dom';
import '../../../../__mocks__/global.mock';
import { mount } from 'enzyme';
import renderer from 'react-test-renderer';
import UnsuspendViewList from 'UnsuspendViewList';
import mockAxios from 'jest-mock-axios';
import  {resources, responseViewListData} from './unspend-downlines-resources.mock';

describe('UnsuspendViewList', () => {
    let props, wrapper;

    beforeEach(() => {
        props = {
            isAffected: true,
            downlineRoleId: 2,
            resources,
            searchAccountId: 36204210,
            searchAccountName: 'davidtest13May01',
            redirectMainPage: jest.fn()
        };
    });

    afterEach(() => {
        mockAxios.reset();
        wrapper = null;
    });

    const buildWrapper = totalRows => {
        if (!wrapper) {
            responseViewListData.Data.TotalRows = totalRows;
            wrapper = mount(<UnsuspendViewList {...props} />);
            wrapper.instance().componentWillMount();
            mockAxios.mockResponse({ data: responseViewListData });
        }

        return wrapper;
    };

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<UnsuspendViewList {...props} />, div);
    });

    test('snapshot', () => {
        const component = renderer.create(<UnsuspendViewList {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    test('fetch viewlist data on #componentWillMount', () => {
       buildWrapper();

        expect(wrapper.state().activePage).toBe(1);
        expect(wrapper.state().pageLength).toBe(50);

        expect(mockAxios.get).toHaveBeenCalledWith(
            '/site-accountinfo/UnsuspendDownlines/FetchCustomerList',
            { 'params': {
                'DownlineRoleId': 2,
                'IsAffected': true,
                'PageIndex': 0,
                'PageSize': 50,
                'SearchAccountId': 36204210,
                'SearchAccountName': 'davidtest13May01'
            }});

        expect(wrapper.state().viewModel).toBe(responseViewListData);
      });

      test.each([
        [0, 1],
        [49, 1],
        [60, 2]])(
        'always renders #BackButton callTimes',
        (totalRows, expectedCallTimes) => {
            buildWrapper(totalRows);
            expect(wrapper.find('.btn-cancel').length).toBe(expectedCallTimes);
        }
    );

      test('redirectMainPage called when clicked back button', () => {
        buildWrapper(30);

        const event = Object.assign(jest.fn(), {preventDefault: () => {}});
        wrapper.find('.btn-cancel').simulate('click', event);
        expect(props.redirectMainPage).toHaveBeenCalled();
    });

    test.each([
        [false, 'List of downlines whose status are unchanged after davidtest13May01 is unsuspended'],
        [true, 'List of downlines whose status are changed after davidtest13May01 is unsuspended']
    ])(
        'always renders correct view note',
        (isAffected, expectedViewNote) => {
            props.isAffected = isAffected;
            buildWrapper(60);
            expect(wrapper.find('.note').text()).toBe(expectedViewNote);
        }
    );
});