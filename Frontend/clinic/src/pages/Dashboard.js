import React from 'react';
import { Box, Typography, Button, Container } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const Dashboard = () => {
  const navigate = useNavigate();

  return (
    <Container maxWidth="md">
      <Box sx={{ textAlign: 'center', marginTop: 8 }}>
        <Typography variant="h4" gutterBottom>
          Admin Dashboard
        </Typography>
        <Box>
          <Button onClick={() => navigate('/manage/appointments')} variant="contained" sx={{ m: 2 }}>
            Manage Appointments
          </Button>
          <Button onClick={() => navigate('/manage/doctors')} variant="contained" sx={{ m: 2 }}>
            Manage Doctors
          </Button>
          <Button onClick={() => navigate('/manage/specialities')} variant="contained" sx={{ m: 2 }}>
            Manage Specialities
          </Button>
          <Button onClick={() => navigate('/manage/clinics')} variant="contained" sx={{ m: 2 }}>
            Manage Clinics
          </Button>
        </Box>
      </Box>
    </Container>
  );
};

export default Dashboard;
