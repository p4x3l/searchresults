import React, { Component } from 'react';
import './App.css';

import AppContent from './AppContent/AppContent';

class App extends Component {
  render() {
    return (
      <div className="app">
        <AppContent></AppContent>
      </div>
    );
  }
}

export default App;
