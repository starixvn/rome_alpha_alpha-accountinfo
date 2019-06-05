import React from 'react';
import { mount, shallow } from 'enzyme';
import '../../../../../__mocks__/global.mock';
import sinon from 'sinon';
import Searching from 'Searching';
import { renderSuggestion } from 'Searching';
import Autosuggest from 'react-autosuggest';
import { accountInfoAPI } from 'AccountInfoAPI';
import mockAxios from 'jest-mock-axios';

describe('Searching', () => {
    let props;
    let searchingMount, handleSearchClickSpy;

    const buildSearching = () => {
        if (!searchingMount) {
            searchingMount = mount(<Searching {...props} />);
        }
        return searchingMount;
    };

    beforeEach(() => {
        props = {
            username: 'AA',
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
        mockAxios.reset();
    });

    test('renders without crashing', () => {
        expect(shallow(<Searching  {...props} />).find('.icon-search').exists()).toBe(true);
    });

    test('always render a `Autosuggest`', () => {
        const wrapper = shallow(<Searching  {...props} />);
        expect(wrapper.find(Autosuggest).length).toBe(1);
    });

    describe('handleSearchClick()', () => {
        test('called when clicked', () => {
            const wrapper = shallow(<Searching  {...props} />);
            const event = Object.assign(jest.fn(), {preventDefault: () => {}});

            wrapper.find('.icon-search').simulate('click', event);
            expect(handleSearchClickSpy.calledOnce).toBeTruthy();
        });

        test.each([['Enter', 1], ['AFF', 0]])(
            'correct called handleSearchClick() when key press',
            (key, expectedCallTimes) => {
                buildSearching();
                searchingMount.find('.txt-search').simulate('keypress', { key: key });
                expect(handleSearchClickSpy.callCount).toBe(expectedCallTimes);
            }
        );
    });

    test.each([['acguus', 1], ['', 0]])(
        'correct called fetchCustomerInfoFromServer() when click search',
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

    describe('state.username', () => {
        test('change when componentWillReceiveProps() receive new username props', () => {
            buildSearching();
            const username = 'new username';
            searchingMount.setProps({ username });
            expect(searchingMount.state('username')).toEqual('new username');
        });

        test('unchange when componentWillReceiveProps() does not receive new username props', () => {
            const spy = sinon.spy(Searching.prototype, 'setState');
            buildSearching();
            const username = 'AA';
            searchingMount.setProps({ username });
            expect(spy.calledOnce).toBeFalsy();
        });

        test('change called when key press', () => {
            buildSearching();
            searchingMount.find('.txt-search').simulate('change', { target: { value: 'acguus' } });
            expect(searchingMount.state('username')).toEqual('acguus');
        });
    });

    describe('onSuggestionsFetchRequested() ', () => {
        test('called when fectch requested', () => {
            const spy = jest.spyOn(accountInfoAPI, 'getAutoUsernameSuggestion');
            buildSearching();
            searchingMount.find('.txt-search').simulate('change', { target: { value: 'acguus' } });
            expect(spy).toBeCalledTimes(1);
        });

        test('Call server with specific url and params', () => {
            buildSearching();
            searchingMount.instance().onSuggestionsFetchRequested({ value: 'test' });
            
            expect(mockAxios.get).toHaveBeenCalledWith(
                '/site-accountinfo/SearchAccount/GetUsernamesSuggestion',
                {'params': { 'SuggestionQuery':  'test'}});
        });

        test('set suggestions states called when input change', () => {
            buildSearching();
            searchingMount.instance().onSuggestionsFetchRequested({ value: 'test' });

            const resp = { data: [
                {
                    'UserName': 'SUPERTEST120',
                    'FirstName': '---',
                    'LastName': '---'
                }
            ]};

            mockAxios.mockResponse(resp);
            expect(searchingMount.state('suggestions')).toEqual(resp.data);
        });
    });

    test('#fetchCustomerInfoFromServer is called when onSuggestionSelected is called', () => {
        buildSearching();
        const event = Object.assign(jest.fn(), {preventDefault: () => {}});
        searchingMount.instance().onSuggestionSelected(event, { suggestionValue: 'test' });

        expect(props.fetchCustomerInfoFromServer).toHaveBeenCalled();
    });

    test('#renderSuggestion() render correct element Id.', () => {
        const suggestion = renderSuggestion({
            'UserName': 'SUPERTEST120',
            'FirstName': '---',
            'LastName': '---',
            'UserName2': null
        });

        const newNode = shallow(suggestion);

        expect(newNode.html()).toContain(`id=\"SUPERTEST120-suggestion\"`);
    });

    test.each([['nickname', 'SUPERTEST120 (nickname)'], ['', 'SUPERTEST120'], [null, 'SUPERTEST120']])(
        '#renderSuggestion() render correct element content.',
        (username2, expectedValue) => {
            const suggestion = renderSuggestion({
                'UserName': 'SUPERTEST120',
                'FirstName': '---',
                'LastName': '---',
                'UserName2': username2
            });

            const newNode = shallow(suggestion);
            expect(newNode.html()).toContain(expectedValue);
        }
    );
});