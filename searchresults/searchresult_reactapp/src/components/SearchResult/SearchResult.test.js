import React from 'react';
import ReactDOM from 'react-dom';
import SearchResult from './SearchResult';

it('renders without crashing', () => {
  const data = {
    "searchType": "google",
    "searchResults": [
      {
      "searchTerm": "string",
      "searchCount": 9390000
      }
    ]
  };
  const div = document.createElement('div');
  ReactDOM.render(<SearchResult searchData={data} />, div);
  ReactDOM.unmountComponentAtNode(div);
});