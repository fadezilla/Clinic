import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Header from './components/Header';
import Footer from './components/Footer';
import BookAppointment from './pages/BookAppointment';
import SearchDoctor from './pages/SearchDoctor';
import HomePage from './pages/HomePage';

const App = () => {
  return (
    <Router>
      <Header />
      <main>
        <Routes>
          <Route path='/' element={<HomePage />} />
          <Route path='/book' element={<BookAppointment />} />
          <Route path='/search' element={<SearchDoctor />} />
        </Routes>
      </main>
      <Footer />
    </Router>
  );
};

export default App;