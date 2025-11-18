using Library.TheraHealth.Models;
using Api.TheraHealth.Database;
using Library.TheraHealth.DTO;

namespace Api.TheraHealthOS.Enterprise;

public class PhysicianEC
{
    public IEnumerable<PhysicianDTO> GetPhysicians()
    {
        return Filebase.Current.Physicians
                .Select(b => new PhysicianDTO(b))
                .OrderByDescending(b => b.Id)
                .Take(100);
    }
    public PhysicianDTO? GetById(int id)
    {
        var physician =  Filebase.Current.Physicians.FirstOrDefault(p => p.Id == id);
        return new PhysicianDTO(physician);
    }

    public PhysicianDTO? Delete(int id)
    {
        var toRemove = Filebase.Current.Physicians.FirstOrDefault(p => p.Id == id);
        if (toRemove != null)
        {
            Filebase.Current.Delete(toRemove.Id.ToString());
        }
        return new PhysicianDTO(toRemove);
    }

    public PhysicianDTO? AddOrUpdate(PhysicianDTO? physicianDTO)
    {
         if (physicianDTO == null)
            {
                return null;
            }
            var physician = new Physician(physicianDTO);
            physicianDTO = new PhysicianDTO(Filebase.Current.AddOrUpdate(physician));
            return physicianDTO;
    }

    public IEnumerable<PhysicianDTO?> Search(string query)
    {
        return Filebase.Current.Physicians
                .Where(p => (p?.Name?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false))
                .Select(p => new PhysicianDTO(p));
    }
}
