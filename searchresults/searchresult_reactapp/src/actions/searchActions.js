import {
    UPDATE_SEARCH_RESULTS,
    UPDATE_SEARCH_RESULTS_SUCCESS,
    UPDATE_SEARCH_RESULTS_FAILED,
} from '../constants/searchTypes';

import * as searchService from '../services/searchService';
// import * as searchService from '../services/searchServiceMock';

export const fetchSearchResults = () => (
    {
        type: UPDATE_SEARCH_RESULTS,
    }
);

export const storeSearchResults = payload => (
    {
        type: UPDATE_SEARCH_RESULTS_SUCCESS,
        payload,
    }
);

export const handleSearchResultError = () => (
    {
        type: UPDATE_SEARCH_RESULTS_FAILED,
    }
);

export const searchAllProviders = (searchTerms) => (
    (dispatch) => {
        dispatch(fetchSearchResults());

        searchService.searchAllProviders(searchTerms)
            .then((response) => {
                dispatch(storeSearchResults(response));
            })
            .catch((error) => {
                dispatch(handleSearchResultError());
            });
    }
);