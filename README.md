[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-24ddc0f5d75046c5622901739e7c5dd533143b0c8e959d652212380cedb1ea36.svg)](https://classroom.github.com/a/veBR0SAn)

![](http://images.restapi.co.za/pvt/Noroff-64.png)
# Noroff
# Back-end Development Year 2
### Exam Project 2

This repository does not have any startup code. Use the 2 folders
- Backend
- Frontend

for your respective applications.


Instruction for the course assignment is in the LMS (Moodle) system of Noroff.
[https://lms.noroff.no](https://lms.noroff.no)

![](http://images.restapi.co.za/pvt/ca_important.png)

You will not be able to make any submissions after the course assignment deadline. Make sure to make all your commit **BEFORE** the deadline to this repository.

![](http://images.restapi.co.za/pvt/help.png)

If you need help with any instructions for the course assignment, contact your teacher on **Microsoft Teams**.

**REMEMBER** Your Moodle LMS submission must have your repository link **AND** your Github username in the text file.

### ENDPOINTS
Backend endpoints:
/api/Appointments
 - GET = Endpoint that returns all appointments, including the Category, Date, Duration, which patient and at what clinic.
 - POST = Endpoint that creates an appointment, Category, date, SocialSecurityNumber, CliniciD, Duration and patient info such as FirstName, LastName, Birthdate and Email must be provided.
 - Get(id) = Endpoint that returns a single appointment using the Id as an identifier.
 - Put = Endpoint used to update an appointment with new information, request body will be the same as the post, but Id must be provided aswell.
 - Delete = Endpoint that deletes a specific Appointment using an Id as an identifier.
 - Get AvailableTimes = Endpoint that uses the Date and clinicId values to search for availaible times during workday and returns all the avaiable full and half hours.

 /api/Clinic
  - Get = Endpoint that returns all Clinics, Including the name, address, PhoneNumber, with Doctors and Appointments.
  - Post = Endpoint that creates a Clinic, Name, Address and PhoenNumber must be provided.
  - Get(id) = Endpoint that returns a single Clinic using the Id as an identifier.
  - Put = Endpoint used to update a Clinic with new information, Request body will be the same as the post, but Id must also be provided.
  - Delete = Endpoint that deletes a specific Clinic using an Id as an identifier. A clinic cannot be deleted if they have any Appointment or doctor attached to it.

  /api/Doctor
  - Get = Endpoint that returns all Doctors, Including FirstName, LastName, SpecialityName, SpecialityId, ClinicName and ClinicId
  - Post = Endpoint that creates a Doctor, FirstName, LastName, SpecialityId and ClinicId must be provided.
  - Get(id) = Endpoint that returns a single Doctor using the Id as an identifier.
  - PUt = Endpoint used to update a Doctor with new information, Request body will be the same as the Post, but Id must also be provided.
  - Delete = Endpoint that deletes a specific Doctor using an Id as an identifier.
  - Get Search = Endpoint that takes a query string to search for a Doctor with matching firstName or lastName, will return any doctors matching the string.

  /api/Patient
  - Get = Endpoint that returns all Patients, Including firstName, lastName, Birthdate, Email and belonging appointments.
  - Post = Endpoint that creates a Patient, firstName, lastName, Birthdate and Email must be provided.
  - Get(id) = endpoint that returns a single Patient using the Id as an identifier.
  - Put = Endpoint used to update a patient with new information, Request body will be the same as the Post, but Id must also be provided.
  - Delete = Endpoint that deletes a specific Patient using id as an identifier. When a patient is being deleted, any appointments belonging to that patient will also be deleted from the database.

  /api/Speciality
  - Get = Endpoint that returns all Speciality, name and belonging doctors will be returned.
  - Post = Endpoint that creates a Speciality, only a name need to be provided.
  - Get(id) = Endpoint that returns a single speciality using id as an identifier.
  - Put = Endpoint to update a Speciality, Id and name must be provided.
  - Delete = Endpoint that deletes a specific Speciality using Id as an identifier.

  Frontend endpoints:

  / and /book endpoints are used to book an appointment, The user will fill out a form with First name, Last name, Birthdate, Email, Social Security Number, Reason for appointment, Select a Doctor, Select a date and Select any available times aswell as choosing the duration for the appointment. Then if the Email already exists in the database, a appointment will be created, if the email does not exists, both a patient and a appointment will be created.
  The avaibable time function will search for any appointments at the clinic that the chosen doctor belongs to and it will check what times are available at that clinic for that specific date. If the user changes the date or doctor at any point, the available times will be updated in real time.

  /search endpoint is used to search for a Doctor. User will type in either the Doctor`s first or last name in the form and press search. if a doctor with a matching name exists in the database, that doctor will be returned. Multiple doctors can be returned, if the doctor does not exist "Doctor not found." will be stated. 

### REFERENCES

Course lessons

Stack overflow - Mostly error searching and looking what other people have done, i.e have not copied anything from stack overflow.

I tried out GitHub Copilot, so some auto fill code was provided but the code provided was already code I was planning on writing such as autofilled error messages or after doing two controllers for my model, some of the methods in the other controllers were autogenerated based on my other controllers and so on. 

If I stumbled upon a bug/error and I could not figure it out by googling or looking through older courses, I would either ask Co-pilot or ChatGpt. While sometimes they do point out common mistakes such as spelling errors, or missing commas, most of the time they did not provide much for error solving, but a few times during this project did they point out some "obvious" flaws that I did not see.