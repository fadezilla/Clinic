import React, { useEffect, useState } from 'react';
import { Container, Typography, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Button, Box } from '@mui/material';

const ManageAppointments = () => {
  const [appointments, setAppointments] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    fetchAppointments();
  }, []);

  const fetchAppointments = async () => {
    try {
      const response = await fetch('/api/appointments');
      const data = await response.json();
      setAppointments(data);
      setLoading(false);
    } catch (error) {
      setError('Failed to load appointments');
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    try {
      await fetch(`/api/appointments/${id}`, { method: 'DELETE' });
      setAppointments(appointments.filter((appointment) => appointment.id !== id));
    } catch (error) {
      setError('Failed to delete appointment');
    }
  };

  return (
    <Container>
      <Box display="flex" justifyContent="space-between" alignItems="center" mt={4} mb={2}>
        <Typography variant="h4">Manage Appointments</Typography>
      </Box>
      {error && <Typography color="error">{error}</Typography>}
      {loading ? (
        <Typography>Loading...</Typography>
      ) : (
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>Id</TableCell>
                <TableCell>Category</TableCell>
                <TableCell>Date</TableCell>
                <TableCell>Patient</TableCell>
                <TableCell>Clinic</TableCell>
                <TableCell>Duration</TableCell>
                <TableCell>Actions</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {appointments.map((appointment) => (
                <TableRow key={appointment.id}>
                  <TableCell>{appointment.id}</TableCell>
                  <TableCell>{appointment.category}</TableCell>
                  <TableCell>{appointment.date}</TableCell>
                  <TableCell>{appointment.patient}</TableCell>
                  <TableCell>{appointment.clinic}</TableCell>
                  <TableCell>{appointment.duration} min</TableCell>
                  <TableCell>
                    <Button
                      variant="contained"
                      color="secondary"
                      size="small"
                      onClick={() => handleDelete(appointment.id)}
                    >
                      Delete
                    </Button>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
    </Container>
  );
};

export default ManageAppointments;