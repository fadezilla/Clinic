import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Header from './components/Header';
import Footer from './components/Footer';
import BookAppointment from './pages/BookAppointment';
import SearchDoctor from './pages/SearchDoctor';
import Login from './pages/Login';
import ManageAppointments from './pages/management/ManageAppointments';
import ManageDoctors from './pages/management/ManageDoctors';
import ManageSpecialities from './pages/management/ManageSpecialities';
import ManageClinics from './pages/management/ManageClinics';
import Swagger from './pages/Swagger';
import { Box } from '@mui/material';

const App = () => {

  const [isAuthenticated, setIsAuthenticated] = useState(
    localStorage.getItem('isAuthenticated') === 'true'
  );
  
  const handleAuthentication = (status) => {
    setIsAuthenticated(status);
    localStorage.setItem('isAuthenticated', status);
  };

  return (
    <Router>
      <Box sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
      <Header isAuthenticated={isAuthenticated} setIsAuthenticated={handleAuthentication} />
        <Box component="main" sx={{ flex: 1 }}>
          <Routes>
            <Route path="/" element={<BookAppointment />} />
            <Route path="/book" element={<BookAppointment />} />
            <Route path="/search" element={<SearchDoctor />} />
            <Route path="/doc" element={<Swagger />} />
            <Route path='/login' element={<Login setIsAuthenticated={handleAuthentication} />} />
            <Route path='/manage-appointments' element={isAuthenticated ? <ManageAppointments /> : <Login setIsAuthenticated={handleAuthentication} />} />
            <Route path='/manage-doctors' element={isAuthenticated ? <ManageDoctors /> : <Login setIsAuthenticated={handleAuthentication} />} />
            <Route path='/manage-specialities' element={isAuthenticated ? <ManageSpecialities /> : <Login setIsAuthenticated={handleAuthentication} />} />
            <Route path='/manage-clinics' element={isAuthenticated ? <ManageClinics /> : <Login setIsAuthenticated={handleAuthentication} />} />
            </Routes>
        </Box>
        <Footer />
      </Box>
    </Router>
  );
};

export default App;