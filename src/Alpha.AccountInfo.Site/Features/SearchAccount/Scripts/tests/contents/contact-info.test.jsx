import React from 'react';
import ReactDOM from 'react-dom';
import { shallow } from 'enzyme';
import renderer from 'react-test-renderer';
import ContactInfo from 'ContactInfo';
import {ContactInfoRow} from 'ContactInfo';
import { Resources } from '../helpers/resources-sample';
import { Super } from '../helpers/actor-is-super-sample';
import { Tooltip } from 'react-tippy';

describe('ContactInfo', () => {
    let props;
    beforeEach(() => {
        props = {
            customerInfo: Super.ViewMasterInfo,
            resources: Resources
        };
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<ContactInfo {...props} />, div);
    });

    test('snapshot', () => {
        const component = renderer.create(<ContactInfo {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    test('render 3 #ContactInfoRow when customerInfo is not null', () => {
        const wrapper = shallow(<ContactInfo {...props} />);
        expect(wrapper.find(ContactInfoRow).length).toBe(3);
    });

    test('render #noinfo when customerInfo is null', () => {
        props.customerInfo = null;
        const wrapper = shallow(<ContactInfo {...props} />);
        expect(wrapper.find('.no-information').length).toBe(1);
    });
});

describe('ContactInfoRow ', () => {
    let props;
    beforeEach(() => {
        props = {
            title: 'Username',
            value : 'usertest'
        };
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<ContactInfoRow {...props} />, div);
    });

    test('snapshot', () => {
        const component = renderer.create(<ContactInfoRow {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    test.each([['usertest', 0], ['usertestusertest01010101011000', 1]])(
        'render correct #Tooltip',
        (value, expectedTooltip) => {
            props.value = value;
            const wrapper = shallow(<ContactInfoRow {...props} />);
            expect(wrapper.find(Tooltip).length).toBe(expectedTooltip);
        }
    );
});