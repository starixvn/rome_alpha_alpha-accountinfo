import React from 'react';
import ReactDOM from 'react-dom';
import { shallow } from "enzyme";
import renderer from 'react-test-renderer';
import LoginInfo from 'LoginInfo';
import { LoginInfoRow, LoginInfoWithLinkRow } from 'LoginInfo';
import { Resources } from '../helpers/resources-sample';
import { Super } from '../helpers/actor-is-super-sample';

describe('LoginInfo', () => {
    let props;
    beforeEach(() => {
        props = {
            loginInfo: Super.ViewMasterInfo.LoginInfo,
            resources: Resources,
            handleOpenIPInfo: jest.fn()
        };
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<LoginInfo {...props} />, div);
    });

    test('snapshot', () => {
        const component = renderer.create(<LoginInfo {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    test('render 3 #LoginInfoRow and 1 LoginInfoWithLinkRow when loginInfo  is not null', () => {
        const wrapper = shallow(<LoginInfo {...props} />)
        expect(wrapper.find(LoginInfoRow).length).toBe(3);
        expect(wrapper.find(LoginInfoWithLinkRow).length).toBe(1);
    });

    test('render #noinfo when customerloginInfo Info is null', () => {
        props.loginInfo = null;
        const wrapper = shallow(<LoginInfo {...props} />)
        expect(wrapper.find('.no-information').length).toBe(1);
    });
});

describe('LoginInfoRow', () => {
    let props;
    beforeEach(() => {
        props = {
            title: 'date',
            value : '1-1-2019'
        };
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<LoginInfoRow {...props} />, div);
    });

    test('snapshot', () => {
        const component = renderer.create(<LoginInfoRow {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });
});

describe('LoginInfoWithLinkRow ', () => {
    let props;
    beforeEach(() => {
        props = {
            title: 'date',
            value : '1-1-2019',
            handleOpenIPInfo: jest.fn()
        };
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<LoginInfoWithLinkRow {...props} />, div);
    });

    test('snapshot', () => {
        const component = renderer.create(<LoginInfoWithLinkRow {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });
});