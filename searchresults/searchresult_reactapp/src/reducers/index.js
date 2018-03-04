import { combineReducers } from 'redux';
import searchReducer from './searchReducer';

const searchApp = combineReducers({
  searchReducer,
});

export default searchApp;
