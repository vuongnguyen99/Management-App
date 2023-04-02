import React from 'react';
import { Navigate, Routes, Route } from 'react-router'
import { LoginPage } from './pages/index';
function App() {
  return (
    <Routes>
      <Route path='/login' element={
        <LoginPage />
      } />
    </Routes>
  );
}

export default App;
