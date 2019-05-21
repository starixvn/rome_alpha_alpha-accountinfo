import React from 'react';
import ReactDOM from 'react-dom';
import '../../../../../__mocks__/global.mock';
import { mount } from 'enzyme';
import sinon from 'sinon';
import renderer from 'react-test-renderer';
import UnsuspendViewList from 'UnsuspendViewList';
import UnsuspendSingle from 'UnsuspendSingle';
import * as unsuspendHelper from 'UnsuspendHelper';
import { BodyRowContent, FooterContent } from 'UnsuspendSingleComponent';
import { resources, responseUnsuspendSingleData } from '../unspend-downlines-resources.mock';


describe('UnsuspendSingle', () => {
    let props, wrapper;

    beforeEach(() => {
        props = {
            viewModel: responseUnsuspendSingleData,
            resources,
            redirectViewListDetail: jest.fn()
        };

        wrapper = undefined;
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

    test('#bootboxConfirm called when clicked submit', () => {
        buildWrapper();
        wrapper.find('.btn-submit').simulate('click');
        expect(window.top.bootboxConfirm).toHaveBeenCalled();
    });

    // test('#doUpdate called when Click OK on confirm popup', () => {
    //     const spy = sinon.spy(UnsuspendSingle.prototype, 'doUpdate');
    //     window.top.bootboxConfirm = jest.fn(() => true);
    // //     const confirmStub = sinon.stub(global, 'bootboxConfirm');
    // // confirmStub.returns(true);
    //     // buildWrapper();
    //     const wrapper= shallow(<UnsuspendSingle {...props} />).dive();

    //     wrapper.find('.btn-submit').simulate('click');
    //     expect(spy.calledOnce).toBeTruthy();
    // });
});