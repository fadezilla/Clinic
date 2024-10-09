import React from 'react';
import { AppBar, Toolbar, Typography, Button, Container } from '@mui/material';
import HealingIcon from '@mui/icons-material/Healing';
import { Link, useNavigate } from 'react-router-dom';

const Header = ({ isAuthenticated, setIsAuthenticated }) => {
  const navigate = useNavigate();

  const handleLogout = () => {
    setIsAuthenticated(false);
    localStorage.setItem('isAuthenticated', 'false');
    navigate('/');
  };

  return (
    <AppBar style={{ backgroundColor: 'MediumTurquoise', position: 'static' }}>
      <Container>
        <Toolbar style={{ color: 'black' }}>
          <HealingIcon style={{ paddingRight: '5px', scale: '1.2' }} />
          <Typography variant="h6" style={{ flexGrow: 1 }}>
            Clinic Booking System
          </Typography>
          

          {/* Show for non-logged-in users */}
          {!isAuthenticated && (
            <>
              <Button color="inherit" component={Link} to="/book">
                Book Appointment
              </Button>
              <Button color="inherit" component={Link} to="/search">
                Search Doctor
              </Button>
            </>
          )}

          {/* Show management pages only if logged in */}
          {isAuthenticated && (
            <>
              <Button color="inherit" component={Link} to="/manage-appointments">
                Manage Appointments
              </Button>
              <Button color="inherit" component={Link} to="/manage-doctors">
                Manage Doctors
              </Button>
              <Button color="inherit" component={Link} to="/manage-specialities">
                Manage Specialities
              </Button>
              <Button color="inherit" component={Link} to="/manage-clinics">
                Manage Clinics
              </Button>
            </>
          )}

          {/* Login/Logout button */}
          {isAuthenticated ? (
            <Button color="inherit" onClick={handleLogout}>
              Logout
            </Button>
          ) : (
            <Button color="inherit" component={Link} to="/login">
              Login
            </Button>
          )}
        </Toolbar>
      </Container>
    </AppBar>
  );
};

export default Header;