import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Container, Typography, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Button, Box, Dialog, DialogActions, DialogContent, DialogTitle, TextField } from '@mui/material';

const ManageClinics = () => {
  const [clinics, setClinics] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [open, setOpen] = useState(false);
  const [newClinic, setNewClinic] = useState({
    name: '',
    address: '',
    phoneNumber: ''
  });

  useEffect(() => {
    fetchClinics();
  }, []);

  const fetchClinics = async () => {
    try {
      const response = await axios.get('/api/clinic');
      const clinicData = response.data.map(clinic => ({
        ...clinic,
        doctors: clinic.doctors || [],       
        appointments: clinic.appointments || [] 
      }));
      
      setClinics(clinicData);
      setLoading(false);
    } catch (error) {
      setError('Failed to load clinics');
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    try {
      await axios.delete(`/api/clinic/${id}`);
      setClinics(clinics.filter(clinic => clinic.id !== id));
    } catch (error) {
      setError('Failed to delete clinic. If it has doctors or appointments, please delete them first.');
    }
  };

  const handleOpen = () => setOpen(true);
  const handleClose = () => {
    setOpen(false);
    setNewClinic({ name: '', address: '', phoneNumber: '' });
  };

  const handleAddClinic = async () => {
    try {
      const response = await axios.post('/api/clinic', newClinic);
      setClinics([...clinics, { ...response.data, doctors: [], appointments: [] }]);
      handleClose();
    } catch (error) {
      setError('Failed to add clinic. Please ensure no duplicate entries for address or phone number.');
    }
  };

  return (
    <Container>
      <Box display="flex" justifyContent="space-between" alignItems="center" mt={4} mb={2}>
        <Typography variant="h4">Manage Clinics</Typography>
        <Button variant="contained" color="primary" onClick={handleOpen}>
          Add New Clinic
        </Button>
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
                <TableCell>Name</TableCell>
                <TableCell>Address</TableCell>
                <TableCell>Phone Number</TableCell>
                <TableCell>Number of Doctors</TableCell>
                <TableCell>Number of Appointments</TableCell>
                <TableCell>Actions</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {clinics.map((clinic) => (
                <TableRow key={clinic.id}>
                  <TableCell>{clinic.id}</TableCell>
                  <TableCell>{clinic.name}</TableCell>
                  <TableCell>{clinic.address}</TableCell>
                  <TableCell>{clinic.phoneNumber}</TableCell>
                  <TableCell>{clinic.doctors.length}</TableCell>
                  <TableCell>{clinic.appointments.length}</TableCell>
                  <TableCell>
                    <Button
                      variant="contained"
                      color="secondary"
                      size="small"
                      onClick={() => handleDelete(clinic.id)}
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

      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>Add New Clinic</DialogTitle>
        <DialogContent>
          <TextField
            margin="dense"
            label="Clinic Name"
            fullWidth
            value={newClinic.name}
            onChange={(e) => setNewClinic({ ...newClinic, name: e.target.value })}
          />
          <TextField
            margin="dense"
            label="Address"
            fullWidth
            value={newClinic.address}
            onChange={(e) => setNewClinic({ ...newClinic, address: e.target.value })}
          />
          <TextField
            margin="dense"
            label="Phone Number"
            fullWidth
            value={newClinic.phoneNumber}
            onChange={(e) => setNewClinic({ ...newClinic, phoneNumber: e.target.value })}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose} color="secondary">
            Cancel
          </Button>
          <Button onClick={handleAddClinic} color="primary">
            Add Clinic
          </Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
};

export default ManageClinics;