import React from 'react';
import ReactDOM from 'react-dom';
import renderer from 'react-test-renderer';
import UplineInfo from 'UplineInfo';
import { shallow } from 'enzyme';

describe('UplineInfo', () => {
    let props;
    beforeEach(() => {
        props = {
            username: '',
            fetchCustomerInfoFromServer: jest.fn()
        };
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<UplineInfo {...props} />, div);
    });

    test('snapshot', () => {
        const component = renderer.create(<UplineInfo {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    test('fetchCustomerInfoFromServer called when clicked', () => {
        const component = shallow(<UplineInfo {...props} />);
        component.find('#upline-info').simulate('click');
        expect(props.fetchCustomerInfoFromServer).toBeCalledTimes(1);
    });
});