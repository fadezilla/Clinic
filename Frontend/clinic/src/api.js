import axios from 'axios';

const API_BASE_URL = '/api';

export const getDoctors = async (searchQuery) => {
  try {
      const response = await axios.get(`${API_BASE_URL}/doctor/search?query=${searchQuery}`);
      return response.data;
  } catch (error) {
      console.error("Error fetching doctors", error);
      throw error;
  }
};

export const bookAppointment = async (appointmentData) => {
  try {
    const response = await axios.post(`${API_BASE_URL}/Appointments`, appointmentData);
    return response.data;
  } catch (error) {
    console.error("Error booking appointment", error);
    throw error;
  }
};
