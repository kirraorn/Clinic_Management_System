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
    public event Action? PhysiciansChanged;
    private PhysicianServiceProxy()
    {
        physicians = new List<PhysicianDTO?>();
        // Initial load
        Refresh();
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

        // Update local cache from server response
        if (physicianFromServer != null)
        {
            var existing = physicians.FirstOrDefault(p => (p?.Id ?? 0) == (physicianFromServer?.Id ?? 0));
            if (existing == null)
            {
                physicians.Add(physicianFromServer);
            }
            else
            {
                var index = physicians.IndexOf(existing);
                physicians[index] = physicianFromServer;
            }
            PhysiciansChanged?.Invoke();
        }

        return physicianFromServer;
    }

    public PhysicianDTO? DeletePhysician(int id)
    {
        var response = new WebRequestHandler().Delete($"/Physician/{id}").Result;

        var physicianToDelete = physicians
            .Where(p => p != null)
            .FirstOrDefault(p => (p?.Id ?? -1) == id);

        physicians.Remove(physicianToDelete);

        PhysiciansChanged?.Invoke();

        return physicianToDelete;
    }

    public void Refresh()
    {
        try
        {
            var physiciansResponse = new WebRequestHandler().Get("/Physician").Result;
            if (physiciansResponse != null)
            {
                physicians = JsonConvert.DeserializeObject<List<PhysicianDTO?>>(physiciansResponse) ?? new List<PhysicianDTO?>();
            }
            else
            {
                physicians = new List<PhysicianDTO?>();
            }
            PhysiciansChanged?.Invoke();
        }
        catch
        {
            // ignore for now; keep existing list
        }
    }

    public PhysicianDTO? GetPhysicianId(int id)
    {
        return Physicians.FirstOrDefault(p => (p?.Id ?? -1) == id);
    }

}



