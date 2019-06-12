import React from 'react';
import ReactDOM from 'react-dom';
import '../../../../../__mocks__/global.mock';
import { mount } from 'enzyme';
import renderer from 'react-test-renderer';
import UnsuspendSingleContainer, { renderUnsuspendSingle } from 'UnsuspendSingleContainer';
import UnsuspendViewList from 'UnsuspendViewList';
import UnsuspendSingle from 'UnsuspendSingle';
import { resources, responseUnsuspendSingleData } from '../unspend-downlines-resources.mock';

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
        renderUnsuspendSingle(responseUnsuspendSingleData, resources);
        expect(ReactDOM.render).toHaveBeenCalled();
    });
});

describe('UnsuspendSingleContainer', () => {
    let props, wrapper;

    beforeEach(() => {
        props = {
            viewModel: responseUnsuspendSingleData,
            resources
        };

        wrapper = null;
    });

    const buildWrapper = () => {
        if (!wrapper) {
            wrapper = mount(<UnsuspendSingleContainer {...props} />);
        }

        return wrapper;
    };

    test('Renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<UnsuspendSingleContainer {...props} />, div);
    });

    test('Snapshot', () => {
        const component = renderer.create(<UnsuspendSingleContainer {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    test('Should render #UnsuspendViewList when call redirectViewListDetail', () => {
        buildWrapper();
        wrapper.instance().redirectViewListDetail(true, 2);
        expect(wrapper.state().showViewList).toBeTruthy();
        expect(wrapper.find(UnsuspendViewList).length).toBe(1);
    });

    test('Should render #UnsuspendSingle when call redirectMainPage', () => {
        buildWrapper();
        wrapper.instance().redirectMainPage();
        expect(wrapper.state().showViewList).toBeFalsy();
        expect(wrapper.find(UnsuspendSingle).length).toBe(1);
    });
});