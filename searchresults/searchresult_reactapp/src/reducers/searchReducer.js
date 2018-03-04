import {
  UPDATE_SEARCH_RESULTS,
  UPDATE_SEARCH_RESULTS_SUCCESS,
  UPDATE_SEARCH_RESULTS_FAILED,
} from '../constants/searchTypes';

const initialState = {
  searchResults: [],
  loading: false,
  error: null,
};

export default (state = initialState, action) => {
  switch (action.type) {
  case UPDATE_SEARCH_RESULTS:
    return Object.assign(
      {},
      state,
      {
        searchResults: [],
        error: '',
        loading: true,
      },
    );
  case UPDATE_SEARCH_RESULTS_SUCCESS:
    return Object.assign(
      {},
      state,
      {
        searchResults: action.payload,
        loading: false,
      },
    );
  case UPDATE_SEARCH_RESULTS_FAILED:
    return Object.assign(
      {},
      state,
      {
        error: 'Failed to search',
        loading: false,
      },
    );
  default: // need this for default case
    return state 
  }
};
