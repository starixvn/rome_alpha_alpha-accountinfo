import React from 'react';
import ReactDOM from 'react-dom';
import { shallow } from "enzyme";
import renderer from 'react-test-renderer';
import OnlineList from 'OnlineList';
import { OnlineListRow } from 'OnlineList';
import { Resources } from '../helpers/resources-sample';

describe('OnlineList', () => {
    let props;
    beforeEach(() => {
        props = {
            onlineListData: [],
            resources: Resources,
            handleOpenIPInfo: jest.fn()
        };
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<OnlineList {...props} />, div);
    });

    test('snapshot', () => {
        const component = renderer.create(<OnlineList {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    test('renders `OnlineListRow` if it has data', () => {
        props.onlineListData=[{
            IP: '192.168.1.1',
            FormattedLoginTime: '31 Oct 2018 12:28 PM'
        }];

        const wrapper = shallow(<OnlineList {...props} />)
        expect(wrapper.find(OnlineListRow).length).toBe(1);
        expect(wrapper.find('.no-information').length).toBe(0);
    });

    test('renders `OnlineListNoInfoRow` if it has no data', () => {
        props.onlineListData=[];

        const wrapper = shallow(<OnlineList {...props} />)
        expect(wrapper.find(OnlineListRow).length).toBe(0);
        expect(wrapper.find('.no-information').length).toBe(1);
    });
});

describe('OnlineListRow', () => {
    let props;
    beforeEach(() => {
        props = {
            onlineList: {
                IP: '192.168.1.1',
                index: 0,
                FormattedLoginTime: '31 Oct 2018 12:28 PM'
            },
            resources: Resources,
            handleOpenIPInfo: jest.fn()
        };
    });

    test('renders without crashing', () => {
        const div = document.createElement('tbody');
        ReactDOM.render(<OnlineListRow {...props} />, div);
    });

    test('snapshot', () => {
        const component = renderer.create(<OnlineListRow {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });
});