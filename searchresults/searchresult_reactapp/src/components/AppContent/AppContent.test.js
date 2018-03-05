import React from 'react';
import ReactDOM from 'react-dom';
import AppContent from './AppContent';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(<AppContent />, div);
  ReactDOM.unmountComponentAtNode(div);
});
