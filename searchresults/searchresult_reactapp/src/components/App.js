import React, { Component } from 'react';
import './App.css';

import AppContentContainer from '../container/AppContentContainer';
import AppHeader from './AppHeader/AppHeader';

class App extends Component {
  componentWillMount() {
    document.body.style.backgroundColor = "#A8A8A8";
  }

  render() {
    return (
      <div className="app">
        <AppHeader headerText="Search Results"></AppHeader>
        <AppContentContainer />
      </div>
    );
  }
}

export default App;
