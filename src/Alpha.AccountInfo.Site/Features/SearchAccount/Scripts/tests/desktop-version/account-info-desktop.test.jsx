import React from 'react';
import ReactDOM from 'react-dom';
import { mount, shallow } from 'enzyme';
import sinon from 'sinon';
import '../../../../../__mocks__/global.mock';
import AccountInfoDesktop from 'AccountInfoDesktop';
import CustomizedAccordionItem from 'CustomizedAccordionItem';
import { AccountInfoProps } from '../helpers/account-info-props-sample';

describe('AccountInfoDesktop', () => {
    let props;
    const contactInfoId = '#contactInfo';

    beforeEach(() => {
        props = AccountInfoProps;
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<AccountInfoDesktop {...props} />, div);
    });

    test('renders at least 6 `CustomizedAccordionItem`', () => {
        const wrapper = shallow(<AccountInfoDesktop {...props} />);
        expect(wrapper.find(CustomizedAccordionItem).length).toBeGreaterThanOrEqual(6);
    });

    test('always expanded balance info when init', () => {
        const wrapper = shallow(<AccountInfoDesktop {...props} />);
        expect(wrapper.state().expandedItems.balanceInfo).toBeTruthy();
        expect(wrapper.state().expandedItems.contactInfo).toBeFalsy();
        expect(wrapper.state().expandedItems.downlineList).toBeFalsy();
        expect(wrapper.state().expandedItems.status).toBeFalsy();
        expect(wrapper.state().expandedItems.settings).toBeFalsy();
        expect(wrapper.state().expandedItems.loginInfo).toBeFalsy();
        expect(wrapper.state().expandedItems.onlineList).toBeFalsy();
    });

    test('handleCollapExpandBlock() called when clicked on `CustomizedAccordionItem`', () => {
        const spy = sinon.spy(AccountInfoDesktop.prototype, 'handleCollapExpandBlock');
        const wrapper = mount(<AccountInfoDesktop {...props} />);

        wrapper.find(contactInfoId).simulate('click');
        expect(spy.calledOnce).toBeTruthy();
    });

    test('always expanded balance info when `redirectToBalanceInfoPage` props is true', () => {
        const wrapper = mount(<AccountInfoDesktop {...props} />);
        wrapper.find(contactInfoId).simulate('click');
        wrapper.setProps({redirectToBalanceInfoPage: true});

        expect(wrapper.state().expandedItems.balanceInfo).toBeTruthy();
        expect(wrapper.state().expandedItems.contactInfo).toBeFalsy();
        expect(wrapper.state().expandedItems.downlineList).toBeFalsy();
        expect(wrapper.state().expandedItems.status).toBeFalsy();
        expect(wrapper.state().expandedItems.settings).toBeFalsy();
        expect(wrapper.state().expandedItems.loginInfo).toBeFalsy();
        expect(wrapper.state().expandedItems.onlineList).toBeFalsy();
    });

    test('always keep current state when `redirectToBalanceInfoPage` props is false', () => {
        const wrapper = mount(<AccountInfoDesktop {...props} />);
        wrapper.find(contactInfoId).simulate('click');
        wrapper.setProps({redirectToBalanceInfoPage: false});

        expect(wrapper.state().expandedItems.balanceInfo).toBeFalsy();
        expect(wrapper.state().expandedItems.contactInfo).toBeTruthy();
        expect(wrapper.state().expandedItems.downlineList).toBeFalsy();
        expect(wrapper.state().expandedItems.status).toBeFalsy();
        expect(wrapper.state().expandedItems.settings).toBeFalsy();
        expect(wrapper.state().expandedItems.loginInfo).toBeFalsy();
        expect(wrapper.state().expandedItems.onlineList).toBeFalsy();
    });

    test('call #fetchDataFromServer if define', () => {
        const wrapper = mount(<AccountInfoDesktop {...props} />);
        wrapper.instance().handleCollapExpandBlock('aa', props.fetchDataFromServer);
        expect(props.fetchDataFromServer).toHaveBeenCalled();
    });
});

