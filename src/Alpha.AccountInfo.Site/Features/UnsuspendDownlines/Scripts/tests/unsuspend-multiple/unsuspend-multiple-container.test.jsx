import React from 'react';
import ReactDOM from 'react-dom';
import '../../../../../__mocks__/global.mock';
import { mount } from 'enzyme';
import renderer from 'react-test-renderer';
import UnsuspendMultipleContainer from 'UnsuspendMultipleContainer';
import {renderUnsuspendMultipe} from 'UnsuspendMultipleContainer';
import UnsuspendViewList from 'UnsuspendViewList';
import UnsuspendMultiple from 'UnsuspendMultiple';
import { resources, responseUnsuspendMultipleData } from '../unspend-downlines-resources.mock';

describe('ReactDOM.render', () => {
    const originalRender = ReactDOM.render;
    const originalGetElement = global.document.getElementById;

    beforeEach(() => {
        global.document.getElementById = () => true;
        ReactDOM.render = jest.fn();
    });

    afterAll(() => {
        global.document.getElementById = originalGetElement;
        ReactDOM.render = originalRender;
    });

    it('should call ReactDOM.render', () => {
        renderUnsuspendMultipe(responseUnsuspendMultipleData, resources);
        expect(ReactDOM.render).toHaveBeenCalled();
    });
});

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

    test('Snapshot', () => {
        const component = renderer.create(<UnsuspendMultipleContainer {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    test('Should render #UnsuspendViewList when call redirectViewListDetail', () => {
        buildWrapper();
        wrapper.instance().redirectViewListDetail(true, 2);
        expect(wrapper.state().showViewList).toBeTruthy();
        expect(wrapper.find(UnsuspendViewList).length).toBe(1);
    });

    test('Should render #UnsuspendMultiple when call redirectMainPage', () => {
        buildWrapper();
        wrapper.instance().redirectMainPage();
        expect(wrapper.state().showViewList).toBeFalsy();
        expect(wrapper.find(UnsuspendMultiple).length).toBe(1);
    });

    test('Should change suspended status when call #handleChange', () => {
        buildWrapper();
        wrapper.instance().handleChange(0, false);
        expect(wrapper.state().viewModel[0].Suspended).not.toBe(wrapper.state().originalViewModel[0].Suspended);
    });

    test('Should reset value when call #handleReset', () => {
        buildWrapper();
        wrapper.instance().handleChange(0, false);
        expect(wrapper.state().viewModel).not.toBe(wrapper.state().originalViewModel);

        wrapper.instance().handleReset();
        expect(wrapper.state().viewModel).toBe(wrapper.state().originalViewModel);
    });

    test.each([[true], [false]])(
        'Change suspended status of all row when click toggle all',
        (suspended) => {
            buildWrapper();
            wrapper.instance().toggleSuspendAll(suspended);
            expect(wrapper.state().viewModel[0].Suspended).toBe(suspended);
            expect(wrapper.state().viewModel[1].Suspended).toBe(suspended);
        }
    );
});
