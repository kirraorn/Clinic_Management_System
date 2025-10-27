using Library.TheraHealth.Services;
namespace Library.TheraHealth.Models;

public class Patient
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Race { get; set; }
    public string? Gender { get; set; }
    public string? MedicalNotes { get; set; }
    //public List<MedicalNote> MedicalNotes { get; set; }
    public Patient()
    {
    }

    public Patient(int id)
    {
        var patientCopy = PatientServiceProxy.Current.Patients.FirstOrDefault(p => (p?.Id ?? 0) == id);
        if (patientCopy != null)
        {
            Id = patientCopy.Id;
            Name = patientCopy.Name;
            Address = patientCopy.Address;
            BirthDate = patientCopy.BirthDate;
            Race = patientCopy.Race;
            Gender = patientCopy.Gender;
            MedicalNotes = patientCopy.MedicalNotes;

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
        return $"{Id}: {Name}";
    }

}
