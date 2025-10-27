using System.Windows.Input;
using Library.TheraHealth.Models;
using Library.TheraHealth.Services;

namespace Maui.TheraHealthOS.ViewModels
{
    public class PatientViewModel
    {
        public PatientViewModel()
        {
            Model = new Patient();
            SetUpCommands();
        }

        public PatientViewModel(Patient? model)
        {
            Model = model;
            SetUpCommands();
        }
        private void SetUpCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as PatientViewModel));

        }
         private void DoDelete()
        {
            if (Model?.Id > 0)
            {
                PatientServiceProxy.Current.DeletePatient(Model.Id);
                Shell.Current.GoToAsync("//MainPage");
            }
        }

        private void DoEdit(PatientViewModel? pvm)
        {
            if (pvm == null)
            {
                return;
            }
            var selectedPatientId = pvm?.Model?.Id ?? 0;
            Shell.Current.GoToAsync($"//Patient?patientId={selectedPatientId}");
        }

        public Patient? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }
    }
}