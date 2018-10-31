import React from 'react';
import { mount, shallow } from "enzyme";
import sinon from 'sinon';
import Searching from 'Searching';
import Autosuggest from 'react-autosuggest';
import {accountInfoAPI} from 'APIClient';

describe('Searching', () => {
    let props;
    let searchingMount, handleSearchClickSpy;
    global.accountInfoSiteAppPath= "/accountInfo-site";

    const buildSearching = () => {
        if (!searchingMount) {
            searchingMount = mount(<Searching {...props} />);
        }
        return searchingMount;
    }

    beforeEach(() => {
        props = {
            username: "AA",
            userNamePlaceHolder: '',
            fetchCustomerInfoFromServer: jest.fn()
        };
        
        searchingMount = undefined;

         handleSearchClickSpy = sinon.spy(Searching.prototype, 'handleSearchClick');
    });

    afterEach(() => {
        Searching.prototype.handleSearchClick.restore();
    });

    test('renders without crashing', () => {
        expect(shallow(<Searching  {...props} />).find('.icon-search').exists()).toBe(true);
    });

    test('always render a `Autosuggest`', () => {
        const wrapper = shallow(<Searching  {...props} />);
        expect(wrapper.find(Autosuggest).length).toBe(1);
    });

    describe("handleSearchClick()", () => {
        test('called when clicked', () => {
            const wrapper = shallow(<Searching  {...props} />);
    
            wrapper.find('.icon-search').simulate('click');
            expect(handleSearchClickSpy.calledOnce).toBeTruthy();
        });

        test.each([['Enter', 1],  ['AFF', 0]])(
            "correct called handleSearchClick() when key press",
            (key, expectedCallTimes) => {
                buildSearching();
                searchingMount.find('.txt-search').simulate('keypress', { key: key });
                expect(handleSearchClickSpy.callCount).toBe(expectedCallTimes);
            }
        );
    });
    
    test.each([['acguus', 1],  ['', 0]])(
        "correct called fetchCustomerInfoFromServer() when click search",
        (value, expectedCallTimes) => {
            const wrapper = shallow(<Searching  {...props} />);
            wrapper.setState({ username: value });

            wrapper.find('.icon-search').simulate('click');
            expect(props.fetchCustomerInfoFromServer).toBeCalledTimes(expectedCallTimes);
        }
    );

    test('handleSearchKeyPress() called when key press', () => {
        const spy = sinon.spy(Searching.prototype, 'handleSearchKeyPress');

        buildSearching();
        searchingMount.find('.txt-search').simulate('keypress', { key: 'Enter' });
        expect(spy.calledOnce).toBeTruthy();
    });

    describe("state.username", () => {
        it('change when componentWillReceiveProps() receive new props', () => {
            props.username = "new username";
            buildSearching();
            expect(searchingMount.state('username')).toEqual("new username");
        });
    
        it('change called when key press', () => {
            buildSearching();
            searchingMount.find('.txt-search').simulate('change', { target: { value: 'acguus' } });
            expect(searchingMount.state('username')).toEqual("acguus");
        });
    });

    test('onSuggestionsFetchRequested() called when fectch requested', () => {
        var spy = jest.spyOn(accountInfoAPI, 'getAutoUsernameSuggestion');
        buildSearching();
        searchingMount.find('.txt-search').simulate('change', { target: { value: 'acguus' } });
        expect(spy).toBeCalledTimes(1);
    });
});