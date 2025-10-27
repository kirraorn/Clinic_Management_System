
using Library.TheraHealth.Services;
namespace Library.TheraHealth.Models;

public class Appointment
{
    public int Id { get; set; }
    public Appointment() { }
    public int PatientId { get; set; }
    public int PhysicianId { get; set; }
    public Patient? Patient { get; set; }
    public Physician? Physician { get; set; }
    public DateTime? StartDate { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string? Notes { get; set; }
    public Appointment(int id)
    {
        var appointmentCopy = AppointmentServiceProxy.Current.Appointments.FirstOrDefault(a => (a?.Id ?? 0) == id);
        if (appointmentCopy != null)
        {
            Id = appointmentCopy.Id;
            PatientId = appointmentCopy.PatientId;
            PhysicianId = appointmentCopy.PhysicianId;
            StartDate = appointmentCopy.StartDate;
            StartTime = appointmentCopy.StartTime;
            EndTime = appointmentCopy.EndTime;
            Notes = appointmentCopy.Notes;

            Patient = PatientServiceProxy.Current.GetPatientById(PatientId); // added these two lines
            Physician = PhysicianServiceProxy.Current.GetPhysicianId(PhysicianId);
        }
    }
    public string Display
    {
        get
        {
            return ToString();
        }
    }
    public override string ToString()
    {
        if (Patient == null || Physician == null)
        {
            return $"{StartTime}: {PatientId} with {PhysicianId}";
        }
        return $"{StartTime}: {Patient.Name} with {Physician.Name}";
    }
}