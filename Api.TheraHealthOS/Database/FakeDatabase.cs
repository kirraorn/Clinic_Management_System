using Library.TheraHealth.Models;
namespace Api.TheraHealthOS.Nonesence;

static class FakeDatabase
{
    public static List<Physician> Physicians = new List<Physician>
    {
            new Physician
            {
                Id = 1,
                Name = "John Doe",
                LicenseNumber = "123456",
                GraduationDate = new DateTime(2010, 5, 20),
                Specializations = "Heart Disease, Hypertension"
            },

            new Physician
            {
                Id = 2,
                Name = "Jane Smith",
                LicenseNumber = "654321",
                GraduationDate = new DateTime(2012, 8, 15),
                Specializations = "Diabetes, Endocrinology"
            },

            new Physician
            {
                Id = 3,
                Name = "Emily Johnson",
                LicenseNumber = "789012",
                GraduationDate = new DateTime(2015, 3, 10),
                Specializations = "Pediatrics, General Medicine"
            }

    };
}
