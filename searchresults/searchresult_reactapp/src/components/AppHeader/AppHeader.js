import React from 'react';

import './AppHeader.css';

const Header = ({ headerText }) => (
  <div className="header">
    <div className="header-text">{headerText}</div>
  </div>
)

export default Header;
