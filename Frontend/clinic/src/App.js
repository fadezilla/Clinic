import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Header from './components/Header';
import Footer from './components/Footer';
import BookAppointment from './pages/BookAppointment';
import SearchDoctor from './pages/SearchDoctor';
import Swagger from './pages/Swagger';
import { Box } from '@mui/material';

const App = () => {
  return (
    <Router>
      <Box sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
        <Header />
        <Box component="main" sx={{ flex: 1 }}>
          <Routes>
            <Route path="/" element={<BookAppointment />} />
            <Route path="/book" element={<BookAppointment />} />
            <Route path="/search" element={<SearchDoctor />} />
            <Route path="/doc" element={<Swagger />} />
          </Routes>
        </Box>
        <Footer />
      </Box>
    </Router>
  );
};

export default App;