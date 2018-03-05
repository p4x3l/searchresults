import { connect } from 'react-redux';

import { searchAllProviders } from '../actions/searchActions';

import AppContent from '../components/AppContent/AppContent';

const mapStateToProps = state => (
    {
      searchResults: state.searchReducer.searchResults,
      loading: state.searchReducer.loading,
      error: state.searchReducer.error,
    }
  );
  
  const mapDispatchToProps = dispatch => (
    {
      searchAllProviders: (searchTerms) => {
        dispatch(searchAllProviders(searchTerms));
      },
    }
  );
  
  export default connect(mapStateToProps, mapDispatchToProps)(AppContent);