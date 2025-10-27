using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using Library.TheraHealth.Models;

namespace Library.TheraHealth.Services;

public class PatientServiceProxy
{
    private List<Patient?> patients;
    private PatientServiceProxy()
    {
        patients = new List<Patient?>();
    }
    private static PatientServiceProxy? instance;
    private static object instanceLock = new object();
    public static PatientServiceProxy Current
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new PatientServiceProxy();
                }
            }
            return instance;
        }
    }
    public List<Patient?> Patients
    {
        get { return patients; }
    }

    public Patient? AddOrUpdate(Patient? patient)
    {
        if (patient == null) return null;

        if (patient.Id <= 0)
        {
            var maxId = -1;
            if (patients.Any())
            {
                maxId = patients.Select(p => p?.Id ?? 0).Max();
            }
            else
            {
                maxId = 0;
            }
            patient.Id = ++maxId;
            patients.Add(patient);
        }
        else
        {
            var PatientToEdit = patients.FirstOrDefault(p => p?.Id == patient.Id);
            if (PatientToEdit != null)
            {
                var index = patients.IndexOf(PatientToEdit);
                patients.RemoveAt(index);
                patients.Insert(index, patient);
            }
        }
        return patient;

    }

    public Patient? DeletePatient(int id)
    {
        var patientToDelete = patients
        .Where(p => p != null)
        .FirstOrDefault(p => (p?.Id ?? -1) == id);
        patients.Remove(patientToDelete);
        return patientToDelete;
    }

    public Patient? GetPatientById(int id)
    {
        return Patients.FirstOrDefault(p => p.Id == id);
    }
}
