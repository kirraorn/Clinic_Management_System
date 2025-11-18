using Library.TheraHealth.Services;
using Library.TheraHealth.DTO;
namespace Library.TheraHealth.Models;

public class Physician
{

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LicenseNumber { get; set; }
    public DateTime GraduationDate { get; set; }
    public string? Specializations { get; set; }
    public List<Appointment>? Appointments { get; set; }

    public string LegacyData1 { get; set; }
    public string LegacyData2 { get; set; }
    public string LegacyData3 { get; set; }
    public string LegacyData4 { get; set; }
    public string LegacyData5 { get; set; }
    public string LegacyData6 { get; set; }

    public Physician()
    {
        LegacyData1 = string.Empty;
        LegacyData2 = string.Empty;
        LegacyData3 = string.Empty;
        LegacyData4 = string.Empty;
        LegacyData5 = string.Empty;
        LegacyData6 = string.Empty;
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

        LegacyData1 = string.Empty;
        LegacyData2 = string.Empty;
        LegacyData3 = string.Empty;
        LegacyData4 = string.Empty;
        LegacyData5 = string.Empty;
        LegacyData6 = string.Empty;
    }

    public Physician(PhysicianDTO physicianDTO)
    {
        Id = physicianDTO.Id;
        Name = physicianDTO.Name;
        LicenseNumber = physicianDTO.LicenseNumber;
        GraduationDate = physicianDTO.GraduationDate;
        Specializations = physicianDTO.Specializations;
        Appointments = physicianDTO.Appointments;
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
