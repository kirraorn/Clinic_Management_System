using Library.TheraHealth.Models;
using Library.TheraHealth.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maui.TheraHealthOS.ViewModels
{
    public class AppointmentViewModel
    {
        private PatientServiceProxy _patientSvc;
        private PhysicianServiceProxy _physicianSvc;
        private AppointmentServiceProxy _appointmentSvc;

        private List<Appointment?> appointments;
        public List<Appointment?> Appointments
        {
            get
            {
                return appointments;
            }
        }

        public AppointmentViewModel()
        {
            _patientSvc = PatientServiceProxy.Current;
            _physicianSvc = PhysicianServiceProxy.Current;
            _appointmentSvc = AppointmentServiceProxy.Current;

            appointments = _appointmentSvc.Appointments;

            foreach (var app in appointments)
            {
                app.Patient = _patientSvc.GetPatientById(app.PatientId);
                app.Physician = _physicianSvc.GetPhysicianId(app.PhysicianId);
            }

            Model = new Appointment();
            SetUpCommands();
        }

        public AppointmentViewModel(Appointment? model)
        {
             _patientSvc = PatientServiceProxy.Current;
            _physicianSvc = PhysicianServiceProxy.Current;
            _appointmentSvc = AppointmentServiceProxy.Current;

            appointments = _appointmentSvc.Appointments;

            foreach (var app in appointments)
            {
                app.Patient = _patientSvc.GetPatientById(app.PatientId);
                app.Physician = _physicianSvc.GetPhysicianId(app.PhysicianId);
            }
            Model = model;
            SetUpCommands();
        }
        private void SetUpCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((a) => DoEdit(a as AppointmentViewModel));

        }
         private void DoDelete()
        {
            if (Model?.Id > 0)
            {
                AppointmentServiceProxy.Current.DeleteAppointment(Model.Id);
                Shell.Current.GoToAsync("//MainPage");
            }
        }

        private void DoEdit(AppointmentViewModel? avm)
        {
            if (avm == null)
            {
                return;
            }
            var selectedAppointmentId = avm?.Model?.Id ?? 0;
            Shell.Current.GoToAsync($"//Appointment?appointmentId={selectedAppointmentId}");
        }

        public Appointment? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }
       
        public DateTime MinStartDate
        {
            get
            {
                return DateTime.Today;
            }
        }
    }
}