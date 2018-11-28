import React from 'react';
import { mount, shallow } from "enzyme";
import sinon from 'sinon';
import Searching from 'Searching';
import { getSuggestionValue, renderSuggestion } from 'Searching';
import Autosuggest from 'react-autosuggest';
import { accountInfoAPI } from 'APIClient';
import mockAxios from 'jest-mock-axios';

describe('Searching', () => {
    let props;
    let searchingMount, handleSearchClickSpy;
    global.accountInfoSiteAppPath = "/accountInfo-site";

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

        mockAxios.reset();
        jest.restoreAllMocks();
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

        test.each([['Enter', 1], ['AFF', 0]])(
            "correct called handleSearchClick() when key press",
            (key, expectedCallTimes) => {
                buildSearching();
                searchingMount.find('.txt-search').simulate('keypress', { key: key });
                expect(handleSearchClickSpy.callCount).toBe(expectedCallTimes);
            }
        );
    });

    test.each([['acguus', 1], ['', 0]])(
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
        test('change when componentWillReceiveProps() receive new username props', () => {
            buildSearching();
            let username = "new username";
            searchingMount.setProps({ username });
            expect(searchingMount.state('username')).toEqual("new username");
        });

        test('unchange when componentWillReceiveProps() does not receive new username props', () => {
            const spy = sinon.spy(Searching.prototype, 'setState');
            buildSearching();
            let username = "AA";
            searchingMount.setProps({ username });
            expect(spy.calledOnce).toBeFalsy();
        });

        test('change called when key press', () => {
            buildSearching();
            searchingMount.find('.txt-search').simulate('change', { target: { value: 'acguus' } });
            expect(searchingMount.state('username')).toEqual("acguus");
        });
    });

    describe("onSuggestionsFetchRequested() ", () => {
        test('called when fectch requested', () => {
            var spy = jest.spyOn(accountInfoAPI, 'getAutoUsernameSuggestion');
            buildSearching();
            searchingMount.find('.txt-search').simulate('change', { target: { value: 'acguus' } });
            expect(spy).toBeCalledTimes(1);
        });

        // test('set suggestions states called when input change', () => {
        //     const resp = {
        //         "data": {
        //             "Result": [
        //                 {
        //                     "UserName": "SUPERTEST120",
        //                     "FirstName": "---",
        //                     "LastName": "---"
        //                 }
        //             ]
        //         }
        //     };
    
        //     buildSearching();
        //     searchingMount.find('.txt-search').simulate('change', { target: { value: 'su' } });
    
        //     mockAxios.mockResponse(resp);
        //     expect(searchingMount.state('suggestions')).toEqual(resp.data.Result);
        // });
    });

    // test('set suggestions states called when input change0000', () => {
    //     const resp = {
    //         "data": {
    //             "Result": [
    //                 {
    //                     "UserName": "SUPERTEST120",
    //                     "FirstName": "---",
    //                     "LastName": "---"
    //                 }
    //             ]
    //         }
    //     };

    //     buildSearching();
    //     searchingMount.find('.txt-search').simulate('change', { target: { value: 'su' } });

    //     mockAxios.mockResponse(resp);
    //     expect(searchingMount.state('suggestions')).toEqual(resp.data.Result);
    //     expect(searchingMount.find('#SUPERTEST120-suggestion').length).toBe(0);
    //   //  searchingMount.find('#SUPERTEST120-suggestion').simulate('click');
    // });

});