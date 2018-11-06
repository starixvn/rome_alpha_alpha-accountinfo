import React from 'react';
import ReactDOM from 'react-dom';
import renderer from 'react-test-renderer';
import UplineInfo from 'UplineInfo';

describe('UplineInfo', () => {
    let props;
    beforeEach(() => {
        props = {
            username: '',
            fetchCustomerInfoFromServer: jest.fn()
        };
    });

    it('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<UplineInfo {...props} />, div);
    });

    it('snapshot', () => {
        const component = renderer.create(<UplineInfo {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });
});