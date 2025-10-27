using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Library.TheraHealth.Models;

namespace Library.TheraHealth.Services;

public class AppointmentServiceProxy
{
    private List<Appointment?> appointments;
    private AppointmentServiceProxy()
    {
         appointments = new List<Appointment?>();
    }
    private static AppointmentServiceProxy? instance;
    private static object instanceLock = new object();
    public static AppointmentServiceProxy Current
    {
        get
        {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AppointmentServiceProxy();
                    }
                }

            return instance;
        }
    }
    public List<Appointment?> Appointments
    {
        get { return appointments; }
    }

    public Appointment? AddOrUpdate(Appointment? appointment)
    {
        if (appointment == null) return null;

        if (appointment.Id <= 0)
        {
            var maxId = -1;
            if (appointments.Any())
            {
                maxId = appointments.Select(a => a?.Id ?? 0).Max();
            }
            else
            {
                maxId = 0;
            }
            appointment.Id = ++maxId;
            appointments.Add(appointment);
        }
        else
        {
            var appointmentToEdit = appointments.FirstOrDefault(a => (a?.Id ?? 0) == appointment.Id);
            if (appointmentToEdit != null)
            {
                var index = appointments.IndexOf(appointmentToEdit);
                appointments.RemoveAt(index);
                appointments.Insert(index, appointment);
            }
        }
        return appointment;
    }

    public Appointment? DeleteAppointment(int id)
    {
        var appointmentToDelete = appointments
            .Where(a => a != null)
            .FirstOrDefault(a => (a?.Id ?? -1) == id);
        appointments.Remove(appointmentToDelete);

        return appointmentToDelete;
    }
}

