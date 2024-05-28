import React from 'react';
import { AppBar, Toolbar, Typography, Button, Container } from '@mui/material';
import HealingIcon from '@mui/icons-material/Healing';
import { Link } from 'react-router-dom';

const Header = () => {
  return (
    <AppBar style={{backgroundColor: "MediumTurquoise", position: "static"}}>
      <Container>
        <Toolbar style={{color: "black"}}>
        <HealingIcon style={{paddingRight: "5px", scale: "1.2"}}/>
          <Typography variant="h6" style={{ flexGrow: 1}}>
           Clinic Booking System
          </Typography>
          <Button color="inherit" component={Link} to="/">
            Home
          </Button>
          <Button color="inherit" component={Link} to="/book">
            Book Appointment
          </Button>
          <Button color="inherit" component={Link} to="/search">
            Search Doctor
          </Button>
        </Toolbar>
      </Container>
    </AppBar>
  );
};

export default Header;
