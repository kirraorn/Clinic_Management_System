using System;
using System.ComponentModel;
using Library.TheraHealth.Models;

namespace Library.TheraHealth.Services;

public class PhysicianServiceProxy
{
    private List<Physician?> physicians;
    private PhysicianServiceProxy()
    {
        physicians = new List<Physician?>();
    }
    private static PhysicianServiceProxy? instance;
    private static object instanceLock = new object();
    public static PhysicianServiceProxy Current
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new PhysicianServiceProxy();
                }
            }
            return instance;
        }
    }
    public List<Physician?> Physicians
    {
        get { return physicians; }
    }

    public Physician? AddOrUpdate(Physician? physician)
    {
        if (physician == null) return null;

        if (physician.Id <= 0)
        {
            var maxId = -1;
            if (physicians.Any())
            {
                maxId = physicians.Select(p => p?.Id ?? 0).Max();
            }
            else
            {
                maxId = 0;
            }
            physician.Id = ++maxId;
            physicians.Add(physician);
        }
        else
        {
            var physicianToEdit = physicians.FirstOrDefault(p => (p?.Id ?? 0) == physician.Id);
            if (physicianToEdit != null)
            {
                var index = physicians.IndexOf(physicianToEdit);
                physicians.RemoveAt(index);
                physicians.Insert(index, physician);
            }

        }
        return physician;
    }

    public Physician? DeletePhysician(int id)
    {
        var physicianToDelete = physicians
            .Where(p => p != null)
            .FirstOrDefault(p => (p?.Id ?? -1) == id);

        physicians.Remove(physicianToDelete);

        return physicianToDelete;
    }

    public Physician? GetPhysicianId(int id)
    {
        return Physicians.FirstOrDefault(p => p.Id == id);
    }

}



