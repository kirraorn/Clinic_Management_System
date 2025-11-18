using System;
using Library.TheraHealth.Models;
using Library.TheraHealth.Services;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("Welcome to TheraHealth!");
            List<Patient?> patients = PatientServiceProxy.Current.Patients;
            List<Physician?> physicians = PhysicianServiceProxy.Current.Physicians;
            List<Appointment?> appointments = AppointmentServiceProxy.Current.Appointments;
            bool cont = true;
            do
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Add Patient");
                Console.WriteLine("2. Add Physician");
                Console.WriteLine("3. Schedule Appointment");
                Console.WriteLine("4. View Patients");
                Console.WriteLine("5. View Physicians");
                Console.WriteLine("6. View Appointments");
                Console.WriteLine("7. Exit");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        // Add Patient
                        Patient newPatient = new Patient();
                        Console.Write("Enter Name: ");
                        newPatient.Name = Console.ReadLine();
                        Console.Write("Enter Address: ");
                        newPatient.Address = Console.ReadLine();
                        Console.Write("Enter Birth Date (yyyy-mm-dd): ");
                        newPatient.BirthDate = DateTime.Parse(Console.ReadLine() ?? "");
                        Console.Write("Enter Race: ");
                        newPatient.Race = Console.ReadLine();
                        Console.Write("Enter Gender: ");
                        newPatient.Gender = Console.ReadLine();
                        PatientServiceProxy.Current.AddOrUpdate(newPatient);
                        break;

                    case "2":
                        // Add Physician
                        Physician newPhysician = new Physician();
                        Console.Write("Enter Name: ");
                        newPhysician.Name = Console.ReadLine();
                        Console.Write("Enter License Number: ");
                        newPhysician.LicenseNumber = Console.ReadLine();
                        Console.Write("Enter Graduation Date (yyyy-mm-dd): ");
                        newPhysician.GraduationDate = DateTime.Parse(Console.ReadLine() ?? "");
                        PhysicianServiceProxy.Current.AddOrUpdate(newPhysician);
                        break;

                    case "3":
                        // Schedule Appointment
                        Appointment newAppointment = new Appointment();

                        Console.Write("Enter Start Time (yyyy-mm-dd hh:mm): ");
                        newAppointment.StartTime = DateTime.Parse(Console.ReadLine() ?? "");
                        Console.Write("Enter End Time (yyyy-mm-dd hh:mm): ");
                        newAppointment.EndTime = DateTime.Parse(Console.ReadLine() ?? "");
                        

                        Console.Write("Enter Patient Name: ");
                        string? patientName = Console.ReadLine();
                        newAppointment.Patient = patients.FirstOrDefault(p => p.Name == patientName);
                        Console.Write("Enter Physician Name: ");
                        string? physicianName = Console.ReadLine();
                        newAppointment.Physician = physicians.FirstOrDefault(p => p.Name == physicianName);

                        bool conflict = appointments.Any(existing =>
                            existing.Physician?.Name == newAppointment.Physician?.Name &&
                            (newAppointment.StartTime < existing.EndTime) && (newAppointment.EndTime > existing.StartTime));
                        if (conflict)
                        {
                            Console.WriteLine("Appointment conflicts with an existing appointment.");
                        }
                        else
                        {
                            AppointmentServiceProxy.Current.AddOrUpdate(newAppointment);
                        }
                        break;

                    case "4":
                        // View Patients
                        Console.WriteLine("Patients:");
                        foreach (var patient in patients)
                        {
                            Console.WriteLine($"- {patient.Name}");
                        }
                        break;

                    case "5":
                        // View Physicians
                        Console.WriteLine("Physicians:");
                        foreach (var physician in physicians)
                        {
                            Console.WriteLine($"- {physician.Name}");
                        }
                        break;

                    case "6":
                        // View Appointments
                        Console.WriteLine("Appointments:");
                        foreach (var appointment in appointments)
                        {
                            Console.WriteLine($"- {appointment.StartTime} to {appointment.EndTime}: {appointment.Patient?.Name} with {appointment.Physician?.Name}");
                        }
                        break;

                    case "7":
                        cont = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            } while (cont);*/
        }
    }
}

