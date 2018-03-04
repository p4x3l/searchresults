import React, { Component } from 'react';
import { connect } from 'react-redux';
import './AppContent.css';
import { searchAllProviders } from '../../actions/searchActions';

class AppContent extends Component {
  constructor(props) {
    super(props);
    this.state = {
        searchTerms: '',
    };
  }

  handleSubmit = () => {
    this.props.searchAllProviders();
  }

  render() {
    console.log(this.props.searchResults);
    return (
      <div className="content">
        <div className="form-group">
          <label htmlFor="inputSearchTerms">SearchTerms</label>
          <input type="text" className="form-control" id="inputSearchTerms" placeholder="Enter searchterms" />
        </div>
        <button className="btn btn-primary" onClick={this.handleSubmit}>Search</button>
      </div>
    );
  }
}

const mapStateToProps = state => (
  {
    searchResults: state.searchReducer.searchResults,
    loading: state.searchReducer.loading,
    error: state.searchReducer.error,
  }
);

const mapDispatchToProps = dispatch => (
  {
    searchAllProviders: () => {
      dispatch(searchAllProviders());
    },
  }
);

export default connect(mapStateToProps, mapDispatchToProps)(AppContent);
