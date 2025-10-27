using Library.TheraHealth.Services;
namespace Library.TheraHealth.Models;

public class Physician
{

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LicenseNumber { get; set; }
    public DateTime GraduationDate { get; set; }
    public string? Specializations { get; set; }
    public List<Appointment>? Appointments { get; set; }

    public Physician()
    {
    }

    public Physician(int id)
    {
        var physicianCopy = PhysicianServiceProxy.Current.Physicians.FirstOrDefault(p => (p?.Id ?? 0) == id);
        if (physicianCopy != null)
        {
            Id = physicianCopy.Id;
            Name = physicianCopy.Name;
            LicenseNumber = physicianCopy.LicenseNumber;
            GraduationDate = physicianCopy.GraduationDate;
            Specializations = physicianCopy.Specializations;
            Appointments = physicianCopy.Appointments;
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
