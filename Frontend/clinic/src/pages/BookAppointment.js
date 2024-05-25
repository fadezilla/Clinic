import React, { useState, useEffect } from 'react';

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
        duration: 60,
        category: '',
    });

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

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
        setFormData({ ...formData, [name]: value });
    };

    const formatDate = (date) => {
        const d = new Date(date);
        const day = String(d.getDate()).padStart(2, '0');
        const month = String(d.getMonth() + 1).padStart(2, '0');
        const year = d.getFullYear();
        const hours = String(d.getHours()).padStart(2, '0');
        const minutes = String(d.getMinutes()).padStart(2, '0');
        return `${day}.${month}.${year} ${hours}:${minutes}`;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);

        const selectedDoctor = doctors.find(doc => doc.id === parseInt(formData.doctorId, 10));
        const clinicId = selectedDoctor.clinicId;

        const bookingData = {
            category: formData.category,
            date: formatDate(formData.date),
            socialSecurityNumber: parseInt(formData.socialSecurityNumber, 10),
            clinicId: clinicId,
            patient: {
                firstName: formData.firstName,
                lastName: formData.lastName,
                birthdate: formData.birthDate,
                email: formData.email
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
                duration: 60,
                category: '',
            });
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <h1>Book Appointment</h1>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    name="firstName"
                    placeholder="First Name"
                    value={formData.firstName}
                    onChange={handleChange}
                    required
                />
                <input
                    type="text"
                    name="lastName"
                    placeholder="Last Name"
                    value={formData.lastName}
                    onChange={handleChange}
                    required
                />
                <input
                    type="date"
                    name="birthDate"
                    placeholder="Birthdate"
                    value={formData.birthDate}
                    onChange={handleChange}
                    required
                />
                <input
                    type="email"
                    name="email"
                    placeholder="Email"
                    value={formData.email}
                    onChange={handleChange}
                    required
                />
                <input
                    type="text"
                    name="socialSecurityNumber"
                    placeholder="Social Security Number"
                    value={formData.socialSecurityNumber}
                    onChange={handleChange}
                    required
                />
                <input
                    type="text"
                    name="category"
                    placeholder="Reason for appointment"
                    value={formData.category}
                    onChange={handleChange}
                    required
                />
                <select
                    name="doctorId"
                    value={formData.doctorId}
                    onChange={handleChange}
                    required
                >
                    <option value="">Select Doctor</option>
                    {doctors.map((doctor) => (
                        <option key={doctor.id} value={doctor.id}>
                            {doctor.firstName} {doctor.lastName}
                        </option>
                    ))}
                </select>
                <input
                    type="datetime-local"
                    name="date"
                    value={formData.date}
                    onChange={handleChange}
                    required
                />
                <input
                    type="number"
                    name="duration"
                    placeholder="Duration (minutes)"
                    value={formData.duration}
                    onChange={handleChange}
                    required
                />
                <button type="submit" disabled={loading}>
                    {loading ? 'Booking...' : 'Book Appointment'}
                </button>
            </form>
        </div>
    );
};

export default BookAppointment;
