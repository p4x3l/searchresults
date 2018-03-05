import React from 'react';

import './SearchResult.css';

const SearchResult = ({ searchData }) => (
  <div className="search-result" key={searchData.searchType}>
    <div className="search-result-header">
      {searchData.searchType}
    </div>
    <ul className="list-group">
      {
        searchData.searchResults.map(searchResult => (
          <li key={searchResult.searchTerm} className="list-group-item">
            <div className="row">
              <div className="col-12 col-md-6">
                <label>SearchTerm:</label> {searchResult.searchTerm}
              </div>
              <div className="col-12 col-md-6"><label>SearchCount:</label> {searchResult.searchCount}</div>
            </div>
          </li>
        ))
      }
    </ul>
  </div>
);

export default SearchResult;