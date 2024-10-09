import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Box, Button, TextField, Typography, Container } from '@mui/material';

const Login = ({ setIsAuthenticated }) => {
  const [credentials, setCredentials] = useState({ username: '', password: '' });
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setCredentials({ ...credentials, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const hardcodedUsername = 'admin';
    const hardcodedPassword = 'admin';

    if (credentials.username === hardcodedUsername && credentials.password === hardcodedPassword) {
      setIsAuthenticated(true);
      navigate('/manage-appointments');
    } else {
      setError('Invalid username or password, only admins can login');
    }
  };

  return (
    <Container maxWidth="xs">
      <Box
        sx={{
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
          marginTop: 8,
        }}
      >
        <Typography component="h1" variant="h5">
          Admin Login
        </Typography>
        {error && <Typography color="error">{error}</Typography>}
        <Box component="form" onSubmit={handleSubmit} sx={{ mt: 1 }}>
          <TextField
            name="username"
            label="Username"
            value={credentials.username}
            onChange={handleChange}
            required
            fullWidth
            margin="normal"
          />
          <TextField
            name="password"
            label="Password"
            type="password"
            value={credentials.password}
            onChange={handleChange}
            required
            fullWidth
            margin="normal"
          />
          <Button type="submit" fullWidth variant="contained" sx={{ mt: 3, mb: 2 }}>
            Login
          </Button>
        </Box>
      </Box>
    </Container>
  );
};

export default Login;