import React, { useState } from 'react';
import { getDoctors } from '../api';

function SearchPage() {
  const [searchQuery, setSearchQuery] = useState('');
  const [doctors, setDoctors] = useState([]);
  const [error, setError] = useState('');

  const handleSearch = async (e) => {
    e.preventDefault();
    try {
      const response = await getDoctors(searchQuery);
      setDoctors(response);
      setError('');
    } catch (error) {
      setError('Doctor not found.');
      setDoctors([]);
    }
  };

  return (
    <div>
      <h1>Search for a Doctor</h1>
      <form onSubmit={handleSearch}>
        <input type="text" value={searchQuery} onChange={(e) => setSearchQuery(e.target.value)} placeholder="Doctor's Name" required />
        <button type="submit">Search</button>
      </form>
      {error && <p>{error}</p>}
      <ul>
        {doctors.map((doctor) => (
          <li key={doctor.id}>
            {doctor.fullName} - {doctor.clinicName} - {doctor.specialityName}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default SearchPage;
