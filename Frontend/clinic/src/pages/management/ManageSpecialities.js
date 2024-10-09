import React, { useEffect, useState } from 'react';
import axios from 'axios';
import {
  Container,
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Button,
  Box,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  TextField,
} from '@mui/material';

const ManageSpecialities = () => {
  const [specialities, setSpecialities] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [open, setOpen] = useState(false);
  const [newSpeciality, setNewSpeciality] = useState('');

  useEffect(() => {
    fetchSpecialities();
  }, []);

  const fetchSpecialities = async () => {
    try {
      const response = await axios.get('/api/speciality');
      setSpecialities(response.data);
      setLoading(false);
    } catch (error) {
      setError('Failed to load specialities');
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    try {
      await axios.delete(`/api/speciality/${id}`);
      setSpecialities(specialities.filter((speciality) => speciality.id !== id));
    } catch (error) {
      setError('Failed to delete speciality');
    }
  };

  const handleOpen = () => setOpen(true);
  const handleClose = () => {
    setOpen(false);
    setNewSpeciality('');
  };

  const handleAddSpeciality = async () => {
    if (!newSpeciality) {
      setError('Speciality name is required.');
      return;
    }

    try {
      await axios.post('/api/speciality', { name: newSpeciality });
      handleClose();
      fetchSpecialities(); // Refresh the list after adding a new speciality
    } catch (error) {
      setError('Failed to add speciality. Please ensure no duplicate entries.');
    }
  };

  return (
    <Container>
      <Box display="flex" justifyContent="space-between" alignItems="center" mt={4} mb={2}>
        <Typography variant="h4">Manage Specialities</Typography>
        <Button variant="contained" color="primary" onClick={handleOpen}>
          Add New Speciality
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
                <TableCell>Number of Doctors</TableCell>
                <TableCell>Actions</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {specialities.map((speciality) => (
                <TableRow key={speciality.id}>
                  <TableCell>{speciality.id}</TableCell>
                  <TableCell>{speciality.name}</TableCell>
                  <TableCell>{speciality.doctors ? speciality.doctors.length : 0}</TableCell>
                  <TableCell>
                    <Button
                      variant="contained"
                      color="secondary"
                      size="small"
                      onClick={() => handleDelete(speciality.id)}
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

      {/* Add New Speciality Dialog */}
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>Add New Speciality</DialogTitle>
        <DialogContent>
          <TextField
            margin="dense"
            label="Speciality Name"
            fullWidth
            value={newSpeciality}
            onChange={(e) => setNewSpeciality(e.target.value)}
            required
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose} color="secondary">
            Cancel
          </Button>
          <Button onClick={handleAddSpeciality} color="primary">
            Add Speciality
          </Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
};

export default ManageSpecialities;