import React, { Component } from 'react';
import { PulseLoader } from 'halogenium';

import './AppContent.css';

import SearchResult from '../SearchResult/SearchResult';

class AppContent extends Component {
  constructor(props) {
    super(props);
    this.state = {
        searchTerms: '',
    };
  }

  handleInputChange = (event) => {
    this.setState({searchTerms: event.target.value});
  }

  handleSubmit = () => {
    this.props.searchAllProviders(this.state.searchTerms);
  }

  renderSearchResult = () => {
    return this.props.searchResults.map(searchResult => 
      <SearchResult searchData={searchResult} />
    );
  };

  render() {
    return (
      <div className="content">
        <div className="section">
          <div className="section-header">Search Field</div>
          <div className="form-group">
            <label htmlFor="inputSearchTerms">Enter Searchterms</label>
            <input type="text" className="form-control" id="inputSearchTerms" placeholder="Enter searchterms" value={this.state.searchTerms} onChange={this.handleInputChange} />
          </div>
          <button className="btn btn-primary" onClick={this.handleSubmit}>Search</button>
        </div>
        {
          this.props.loading ? (
            <PulseLoader color="#26A65B" size="16px" margin="4px"/>
          ) : null
        }
        {
          this.props.error ? (
            <div>{this.props.error}</div>
          ) : null
        }
        {
          (this.props.searchResults || []).length > 0 ? (
            <div className="section">
              <div className="section-header">Results from search</div>
              {this.renderSearchResult()}
            </div>
          ) : null
        }
      </div>
    );
  }
}

export default AppContent;
