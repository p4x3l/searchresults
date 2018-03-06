import { searchAllProviders } from '../../services/searchService';

export class appContent {
  constructor() {
    this.searchTerms = '';
    this.searchResults = [];
    this.isLoading = false;
    this.error = null;
  }

  search = () => {
    this.searchResults = [];
    this.isLoading = true;
    this.error = null;
    searchAllProviders(this.searchTerms)
      .then((response) => {
        this.searchResults = response;
      })
      .catch((error) => {
        this.error = 'Failed to search';
      })
      .finally(() => {
        this.isLoading = false;
      });
  }
}
  