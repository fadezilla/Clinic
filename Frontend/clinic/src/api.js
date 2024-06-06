import axios from 'axios';

export const getDoctors = async (searchQuery) => {
  try {
      const response = await axios.get(`/api/doctor/search?query=${searchQuery}`);
      return response.data;
  } catch (error) {
      console.error("Error fetching doctors", error);
      throw error;
  }
};

export const bookAppointment = async (appointmentData) => {
  try {
    const response = await axios.post('/api/Appointments', appointmentData);
    return response.data;
  } catch (error) {
    console.error("Error booking appointment", error);
    throw error;
  }
};
