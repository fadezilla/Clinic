import React, { useState, useEffect } from 'react';
import {Container, Typography, TextField, MenuItem, Select, FormControl, InputLabel, Button, Grid, Box, CircularProgress, } from '@mui/material';

const BookAppointment = () => {
    const [doctors, setDoctors] = useState([]);
    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        birthDate: '',
        email: '',
        socialSecurityNumber: '',
        doctorId: '',
        date: '',
        time: '',
        duration: '',
        category: '',
    });

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');
    const [availableTimes, setAvailableTimes] = useState([]);

    useEffect(() => {
        const fetchDoctors = async () => {
            try {
                const response = await fetch('https://localhost:7255/api/doctor');
                const data = await response.json();
                console.log(data);
                setDoctors(data);
            } catch (err) {
                setError('Failed to load doctors');
            }
        };
        fetchDoctors();
    }, []);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => {
            const newFormData = { ...prevData, [name]: value };
            
            if ((name === 'date' && newFormData.doctorId) || (name === 'doctorId' && newFormData.date)) {
                fetchAvailableTimes(newFormData.date, newFormData.doctorId);
            }
            
            return newFormData;
        });
    };

    const fetchAvailableTimes = async (selectedDate, doctorId) => {
        const selectedDoctor = doctors.find(doc => doc.id === parseInt(doctorId, 10));
        const clinicId = selectedDoctor ? selectedDoctor.clinicId : null;
        
        if (!selectedDate || !clinicId) return;

        try {
            const formattedDate = formatDate(selectedDate);
            const response = await fetch(`https://localhost:7255/api/appointments/availableTimes?date=${formattedDate}&clinicId=${clinicId}`);
            if (!response.ok) {
                throw new Error('Failed to load available times');
            }
            const data = await response.json();
            setAvailableTimes(data);
        } catch (err) {
            setError('Failed to load available times');
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);

        const selectedDoctor = doctors.find(doc => doc.id === parseInt(formData.doctorId, 10));
        const clinicId = selectedDoctor.clinicId;

        const bookingData = {
            category: formData.category,
            date: `${formatDate(formData.date)} ${formData.time}`,
            socialSecurityNumber: parseInt(formData.socialSecurityNumber, 10),
            duration: parseInt(formData.duration, 10),
            clinicId: clinicId,
            patient: {
                firstName: formData.firstName,
                lastName: formData.lastName,
                birthdate: formData.birthDate,
                email: formData.email,
            }
        };

        try {
            const response = await fetch('https://localhost:7255/api/appointments', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(bookingData),
            });

            if (!response.ok) {
                throw new Error('Failed to book appointment');
            }

            alert('Appointment booked successfully.');
            setFormData({
                firstName: '',
                lastName: '',
                birthDate: '',
                email: '',
                socialSecurityNumber: '',
                doctorId: '',
                date: '',
                time: '',
                duration: '',
                category: '',
            });
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    const getMinBookingDate = () => {
        const now = new Date();
        now.setHours(now.getHours() + 1);
        now.setMinutes(0, 0, 0);
        return now.toISOString().split('T')[0];
    };

   const timeOptions = () => {
    const times = [];
    for (let i = 7; i < 20; i++) {
        const hour = String(i).padStart(2, '0');
        times.push(`${hour}:00`);
        times.push(`${hour}:30`);
    }
    return times;
   }
   
    const formatDate = (dateString) => {
        const date = new Date(dateString);
        const day = String(date.getDate()).padStart(2, '0');
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const year = date.getFullYear();
        return `${day}.${month}.${year}`;
    };

    const isTimeAvailable = (time) => {
    return availableTimes.includes(time);
    };

    return (
        <Container component="main" maxWidth="sm">
          <Box
            sx={{
              display: 'flex',
              flexDirection: 'column',
              alignItems: 'center',
              marginTop: 4,
            }}
          >
            <Typography component="h1" variant="h5">
              Book Appointment
            </Typography>
            {error && <Typography color="error">{error}</Typography>}
            <Box component="form" onSubmit={handleSubmit} sx={{ mt: 1 }}>
              <Grid container spacing={2}>
                <Grid item xs={12} sm={6}>
                  <TextField
                    name="firstName"
                    label="First Name"
                    value={formData.firstName}
                    onChange={handleChange}
                    required
                    fullWidth
                  />
                </Grid>
                <Grid item xs={12} sm={6}>
                  <TextField
                    name="lastName"
                    label="Last Name"
                    value={formData.lastName}
                    onChange={handleChange}
                    required
                    fullWidth
                  />
                </Grid>
                <Grid item xs={12}>
                  <TextField
                    name="birthDate"
                    label="Birthdate"
                    type="date"
                    value={formData.birthDate}
                    onChange={handleChange}
                    required
                    fullWidth
                    InputLabelProps={{
                      shrink: true,
                    }}
                  />
                </Grid>
                <Grid item xs={12}>
                  <TextField
                    name="email"
                    label="Email"
                    type="email"
                    value={formData.email}
                    onChange={handleChange}
                    required
                    fullWidth
                  />
                </Grid>
                <Grid item xs={12}>
                  <TextField
                    name="socialSecurityNumber"
                    label="Social Security Number"
                    value={formData.socialSecurityNumber}
                    onChange={handleChange}
                    required
                    fullWidth
                  />
                </Grid>
                <Grid item xs={12}>
                  <TextField
                    name="category"
                    label="Reason for Appointment"
                    value={formData.category}
                    onChange={handleChange}
                    required
                    fullWidth
                  />
                </Grid>
                <Grid item xs={12}>
                  <FormControl fullWidth required>
                    <InputLabel>Doctor</InputLabel>
                    <Select
                      name="doctorId"
                      value={formData.doctorId}
                      onChange={handleChange}
                      label="Doctor"
                    >
                      <MenuItem value="">
                        <em>Select Doctor</em>
                      </MenuItem>
                      {doctors.map((doctor) => (
                        <MenuItem key={doctor.id} value={doctor.id}>
                          {doctor.firstName} {doctor.lastName}
                        </MenuItem>
                      ))}
                    </Select>
                  </FormControl>
                </Grid>
                <Grid item xs={12}>
                  <TextField
                    name="date"
                    label="Date"
                    type="date"
                    value={formData.date}
                    onChange={handleChange}
                    min={getMinBookingDate()}
                    required
                    fullWidth
                    InputLabelProps={{
                      shrink: true,
                    }}
                  />
                </Grid>
                <Grid item xs={12}>
                  <FormControl fullWidth required>
                    <InputLabel>Time</InputLabel>
                    <Select
                      name="time"
                      value={formData.time}
                      onChange={handleChange}
                      label="Time"
                    >
                      <MenuItem value="">
                        <em>Select Time</em>
                      </MenuItem>
                      {timeOptions().map((time) => (
                        <MenuItem key={time} value={time} disabled={!isTimeAvailable(time)}>
                          {time}
                        </MenuItem>
                      ))}
                    </Select>
                  </FormControl>
                </Grid>
                <Grid item xs={12}>
                  <TextField
                    name="duration"
                    label="Duration (minutes)"
                    type="number"
                    value={formData.duration}
                    onChange={handleChange}
                    required
                    fullWidth
                  />
                </Grid>
              </Grid>
              <Button
                type="submit"
                fullWidth
                variant="contained"
                color="primary"
                sx={{ mt: 3, mb: 2 }}
                disabled={loading}
              >
                {loading ? <CircularProgress size={24} /> : 'Book Appointment'}
              </Button>
            </Box>
          </Box>
        </Container>
      );
    };
    
    export default BookAppointment;