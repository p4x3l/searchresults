import {
    UPDATE_SEARCH_RESULTS,
    UPDATE_SEARCH_RESULTS_SUCCESS,
    UPDATE_SEARCH_RESULTS_FAILED,
} from '../constants/searchTypes';

import * as searchService from '../services/searchServiceMock';

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

export const searchAllProviders = () => (
    (dispatch) => {
        dispatch(fetchSearchResults());

        searchService.searchAllProviders()
            .then((response) => {
                dispatch(storeSearchResults(response));
            })
            .catch((error) => {
                dispatch(handleSearchResultError());
            });
    }
);