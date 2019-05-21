import React from 'react';
import ReactDOM from 'react-dom';
import '../../../../../__mocks__/global.mock';
import { mount } from 'enzyme';
import renderer from 'react-test-renderer';
import UnsuspendMultipleContainer from 'UnsuspendMultipleContainer';
import UnsuspendViewList from 'UnsuspendViewList';
import UnsuspendMultiple from 'UnsuspendMultiple';
import { resources, responseUnsuspendMultipleData } from '../unspend-downlines-resources.mock';
import * as _ from 'lodash';

describe('UnsuspendSingleContainer', () => {
    let props, wrapper;

    beforeEach(() => {
        props = {
            viewModel: responseUnsuspendMultipleData.Data,
            searchAccountRoleId: responseUnsuspendMultipleData.SearchAccountRoleId,
            resources
        };

        wrapper = undefined;
    });

    const buildWrapper = () => {
        if (!wrapper) {
            wrapper = mount(<UnsuspendMultipleContainer {...props} />);
        }

        return wrapper;
    }

    test('Renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<UnsuspendMultipleContainer {...props} />, div);
    });

    // test('Snapshot', () => {
    //     const component = renderer.create(<UnsuspendMultipleContainer {...props} />);
    //     expect(component.toJSON()).toMatchSnapshot();
    // });

    // test('Should render #UnsuspendViewList when call redirectViewListDetail', () => {
    //     buildWrapper();
    //     wrapper.instance().redirectViewListDetail(true, 2);
    //     expect(wrapper.state().showViewList).toBeTruthy();
    //     expect(wrapper.find(UnsuspendViewList).length).toBe(1);
    // });

    // test('Should render #UnsuspendMultiple when call redirectMainPage', () => {
    //     buildWrapper();
    //     wrapper.instance().redirectMainPage();
    //     expect(wrapper.state().showViewList).toBeFalsy();
    //     expect(wrapper.find(UnsuspendMultiple).length).toBe(1);
    // });
});