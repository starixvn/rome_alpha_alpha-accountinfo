import React from 'react';
import ReactDOM from 'react-dom';
import '../../../../../__mocks__/global.mock';
import { mount } from 'enzyme';
import sinon from 'sinon';
import renderer from 'react-test-renderer';
import UnsuspendViewList from 'UnsuspendViewList';
import UnsuspendMultiple from 'UnsuspendMultiple';
import * as unsuspendHelper from 'UnsuspendHelper';
import { BodyRowContent, FooterContent } from 'UnsuspendSingleComponent';
import { resources, responseUnsuspendMultipleData } from '../unspend-downlines-resources.mock';
import * as _ from 'lodash';

describe('UnsuspendMultiple', () => {
    let props, wrapper;

    beforeEach(() => {
        props = {
            viewModel: responseUnsuspendMultipleData.Data,
            originalViewModel: _.cloneDeep(responseUnsuspendMultipleData.Data),
            searchAccountRoleId: responseUnsuspendMultipleData.SearchAccountRoleId,
            resources,
            handleReset: jest.fn(),
            handleChange: jest.fn(),
            toggleSuspendAll: jest.fn(),
            redirectViewListDetail: jest.fn()
        };

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

    test('#handleSubmit called when clicked submit', () => {
        const spy = sinon.spy(UnsuspendMultiple.prototype, 'handleSubmit');
        buildWrapper();
        
        wrapper.find('.btn-unsuspend-all').simulate('click');
        expect(wrapper.state().viewModel).not.toBe(wrapper.state().originalViewModel);

        wrapper.find('.btn-submit').simulate('click');
        expect(spy.calledOnce).toBeTruthy();
    });
});