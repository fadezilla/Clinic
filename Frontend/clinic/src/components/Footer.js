import React from 'react';
import { Container, Typography, Box } from '@mui/material';

const Footer = () => {
  return (
    <Box sx={{ py: 2, px: 2, mt: 'mt', display: 'flex', justifyContent: 'center', backgroundColor: (theme) => theme.palette.grey[200], }} component="footer" >
      <Container maxWidth="sm">
        <Typography variant="body1" textAlign={'center'}>
          &copy; {new Date().getFullYear()} Clinic Booking System
        </Typography>
      </Container>
    </Box>
  );
};

export default Footer;
