using System;
using System.ComponentModel;
using Library.TheraHealth.DTO;
using Library.TheraHealth.Models;
using Library.TheraHealth.Utilities;
using Newtonsoft.Json;

namespace Library.TheraHealth.Services;

public class PhysicianServiceProxy
{
    private List<PhysicianDTO?> physicians;
    private PhysicianServiceProxy()
    {
        physicians = new List<PhysicianDTO?>();
        var physiciansResponse = new WebRequestHandler().Get("/Physician").Result;
        if (physiciansResponse != null)
        {
            physicians = JsonConvert.DeserializeObject<List<PhysicianDTO?>>(physiciansResponse) ?? new List<PhysicianDTO?>();
        }

    
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
    public List<PhysicianDTO?> Physicians
    {
        get { return physicians; }
    }

    public async Task<PhysicianDTO?> AddOrUpdate(PhysicianDTO? physician)
    {
        if (physician == null) return null;

        var physicianPayload = await new WebRequestHandler().Post("/Physician", physician);
        var physicianFromServer = JsonConvert.DeserializeObject<PhysicianDTO?>(physicianPayload);

       if (physician.Id <= 0)
        {
            physicians.Add(physicianFromServer);
        }
        else
        {
            var physicianToEdit = Physicians.FirstOrDefault(b => (b?.Id ?? 0) == physician.Id);
            if (physicianToEdit != null)
            {
                var index = Physicians.IndexOf(physicianToEdit);
                Physicians.RemoveAt(index);
                physicians.Insert(index, physician);
            }
        }
            return physician;
    }

    public PhysicianDTO? DeletePhysician(int id)
    {
        var response = new WebRequestHandler().Delete($"/Physician/{id}").Result;

        var physicianToDelete = physicians
            .Where(p => p != null)
            .FirstOrDefault(p => (p?.Id ?? -1) == id);

        physicians.Remove(physicianToDelete);

        return physicianToDelete;
    }

    public PhysicianDTO? GetPhysicianId(int id)
    {
        return Physicians.FirstOrDefault(p => (p?.Id ?? -1) == id);
    }

}



