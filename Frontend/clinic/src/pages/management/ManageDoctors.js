import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Container, Typography, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Button, Box, Dialog, DialogActions, DialogContent, DialogTitle, TextField, Select, MenuItem, FormControl, InputLabel } from '@mui/material';

const ManageDoctors = () => {
  const [doctors, setDoctors] = useState([]);
  const [specialities, setSpecialities] = useState([]);
  const [clinics, setClinics] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [open, setOpen] = useState(false);
  const [newDoctor, setNewDoctor] = useState({
    firstName: '',
    lastName: '',
    specialityId: '',
    clinicId: ''
  });

  useEffect(() => {
    fetchDoctors();
    fetchSpecialities();
    fetchClinics();
  }, []);

  const fetchDoctors = async () => {
    try {
      const response = await axios.get('/api/doctor');
      setDoctors(response.data);
      setLoading(false);
    } catch (error) {
      setError('Failed to load doctors');
      setLoading(false);
    }
  };

  const fetchSpecialities = async () => {
    try {
      const response = await axios.get('/api/speciality');
      setSpecialities(response.data);
    } catch (error) {
      setError('Failed to load specialities');
    }
  };

  const fetchClinics = async () => {
    try {
      const response = await axios.get('/api/clinic');
      setClinics(response.data);
    } catch (error) {
      setError('Failed to load clinics');
    }
  };

  const handleDelete = async (id) => {
    try {
      await axios.delete(`/api/doctor/${id}`);
      setDoctors(doctors.filter(doctor => doctor.id !== id));
    } catch (error) {
      setError('Failed to delete doctor');
    }
  };

  const handleOpen = () => setOpen(true);
  const handleClose = () => {
    setOpen(false);
    setNewDoctor({
      firstName: '',
      lastName: '',
      specialityId: '',
      clinicId: ''
    });
  };

  const handleAddDoctor = async () => {
    if (!newDoctor.firstName || !newDoctor.lastName || !newDoctor.specialityId || !newDoctor.clinicId) {
      setError("All fields are required to add a doctor.");
      return;
    }

    try {
      const response = await axios.post('/api/doctor', newDoctor);
      setDoctors([...doctors, response.data]);
      handleClose();
      fetchDoctors();
    } catch (error) {
      setError('Failed to add doctor. Please ensure no duplicate entries.');
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setNewDoctor((prevData) => ({
      ...prevData,
      [name]: value
    }));
  };

  return (
    <Container>
      <Box display="flex" justifyContent="space-between" alignItems="center" mt={4} mb={2}>
        <Typography variant="h4">Manage Doctors</Typography>
        <Button variant="contained" color="primary" onClick={handleOpen}>
          Add New Doctor
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
                <TableCell>First Name</TableCell>
                <TableCell>Last Name</TableCell>
                <TableCell>Speciality</TableCell>
                <TableCell>Clinic</TableCell>
                <TableCell>Actions</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {doctors.map((doctor) => (
                <TableRow key={doctor.id}>
                  <TableCell>{doctor.id}</TableCell>
                  <TableCell>{doctor.firstName}</TableCell>
                  <TableCell>{doctor.lastName}</TableCell>
                  <TableCell>{doctor.specialityName}</TableCell>
                  <TableCell>{doctor.clinicName}</TableCell>
                  <TableCell>
                    <Button
                      variant="contained"
                      color="secondary"
                      size="small"
                      onClick={() => handleDelete(doctor.id)}
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

      {/* Add New Doctor Dialog */}
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>Add New Doctor</DialogTitle>
        <DialogContent>
          <TextField
            margin="dense"
            label="First Name"
            fullWidth
            name="firstName"
            value={newDoctor.firstName}
            onChange={handleChange}
            required
          />
          <TextField
            margin="dense"
            label="Last Name"
            fullWidth
            name="lastName"
            value={newDoctor.lastName}
            onChange={handleChange}
            required
          />
          <FormControl fullWidth margin="dense" required>
            <InputLabel>Speciality</InputLabel>
            <Select
              name="specialityId"
              value={newDoctor.specialityId}
              onChange={handleChange}
              label="Speciality"
            >
              <MenuItem value="">
                <em>Select Speciality</em>
              </MenuItem>
              {specialities.map((speciality) => (
                <MenuItem key={speciality.id} value={speciality.id}>
                  {speciality.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
          <FormControl fullWidth margin="dense" required>
            <InputLabel>Clinic</InputLabel>
            <Select
              name="clinicId"
              value={newDoctor.clinicId}
              onChange={handleChange}
              label="Clinic"
            >
              <MenuItem value="">
                <em>Select Clinic</em>
              </MenuItem>
              {clinics.map((clinic) => (
                <MenuItem key={clinic.id} value={clinic.id}>
                  {clinic.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose} color="secondary">
            Cancel
          </Button>
          <Button onClick={handleAddDoctor} color="primary">
            Add Doctor
          </Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
};

export default ManageDoctors;