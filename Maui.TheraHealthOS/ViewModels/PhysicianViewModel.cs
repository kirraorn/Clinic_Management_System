using System.Windows.Input;
using Library.TheraHealth.Models;
using Library.TheraHealth.Services;
using Library.TheraHealth.DTO;

namespace Maui.TheraHealthOS.ViewModels
{
    public class PhysicianViewModel
    {
        public PhysicianViewModel()
        {
            Model = new PhysicianDTO();
            SetUpCommands();
        }

        public PhysicianViewModel(PhysicianDTO? model)
        {
            Model = model;
            SetUpCommands();
        }
        private void SetUpCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as PhysicianViewModel));

        }
         private void DoDelete()
        {
            if (Model?.Id > 0)
            {
                PhysicianServiceProxy.Current.DeletePhysician(Model.Id);
                Shell.Current.GoToAsync("//MainPage");
            }
        }

        private void DoEdit(PhysicianViewModel? pvm)
        {
            if (pvm == null)
            {
                return;
            }
            var selectedPhysicianId = pvm?.Model?.Id ?? 0;
            Shell.Current.GoToAsync($"//Physician?physicianId={selectedPhysicianId}");
        }

        public PhysicianDTO? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }
    }
}
