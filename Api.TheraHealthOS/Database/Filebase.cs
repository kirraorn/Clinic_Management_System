using Library.TheraHealth.Models;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Api.TheraHealth.Database
{
    public class Filebase
    {
        private readonly string _root;
        private readonly string _physicianRoot;
        private static Filebase _instance;

        public static Filebase Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Filebase();
                }
                return _instance;
            }
        }

        private Filebase()
        {
            
            _root = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "ApiData");
            _physicianRoot = Path.Combine(_root, "Physicians");

           
            Directory.CreateDirectory(_physicianRoot);
        }

        public int LastBlogKey
        {
            get
            {
                if (Physicians.Any())
                {
                    return Physicians.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        public Physician AddOrUpdate(Physician physician)
        {
            if (physician.Id <= 0)
            {
                physician.Id = LastBlogKey + 1;
            }

            string path = Path.Combine(_physicianRoot, $"{physician.Id}.json");

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.WriteAllText(path, JsonConvert.SerializeObject(physician));

            return physician;
        }

        public List<Physician> Physicians
        {
            get
            {
                if (!Directory.Exists(_physicianRoot)) return new List<Physician>();

                var physicians = new List<Physician>();
                var rootDir = new DirectoryInfo(_physicianRoot);

                foreach (var file in rootDir.GetFiles("*.json"))
                {
                    var content = File.ReadAllText(file.FullName);
                    var physician = JsonConvert.DeserializeObject<Physician>(content);
                    if (physician != null)
                    {
                        physicians.Add(physician);
                    }
                }

                return physicians;
            }
        }

        public bool Delete(string id)
        {
            string path = Path.Combine(_physicianRoot, $"{id}.json");
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
    }
}

