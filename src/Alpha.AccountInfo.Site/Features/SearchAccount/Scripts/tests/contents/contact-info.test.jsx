import React from 'react';
import ReactDOM from 'react-dom';
import { shallow } from "enzyme";
import renderer from 'react-test-renderer';
import ContactInfo from 'ContactInfo';
import {ContactInfoRow} from 'ContactInfo';
import { Resources } from '../helpers/resources-sample';
import { Super } from '../helpers/actor-is-super-sample';

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

    test('always render 3 `ContactInfoRow`', () => {
        const wrapper = shallow(<ContactInfo {...props} />)
        expect(wrapper.find(ContactInfoRow).length).toBe(3);
    });
});