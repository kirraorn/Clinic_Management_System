using Library.TheraHealth.Models;
using Library.TheraHealth.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Maui.TheraHealthOS.ViewModels
{
    public class AppointmentViewModel : INotifyPropertyChanged
    {
        private PatientServiceProxy _patientSvc;
        private PhysicianServiceProxy _physicianSvc;
        private AppointmentServiceProxy _appointmentSvc;

        public List<Patient?> Patients => _patientSvc.Patients;
        public List<Physician?> Physicians => _physicianSvc.Physicians;

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

        public Patient? SelectedPatient
        {
            get => Model?.Patient;
            set
            {
                if (Model == null) Model = new Appointment();
                Model.Patient = value;
                Model.PatientId = value?.Id ?? 0;
                NotifyPropertyChanged();
            }
        }

        public Physician? SelectedPhysician
        {
            get => Model?.Physician;
            set
            {
                if (Model == null) Model = new Appointment();
                Model.Physician = value;
                Model.PhysicianId = value?.Id ?? 0;
                NotifyPropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get => Model?.StartDate ?? DateTime.Today;
            set
            {
                if (Model == null) Model = new Appointment();
                Model.StartDate = value;
                ValidateAppointment();
                NotifyPropertyChanged();
            }
        }

        public TimeSpan StartTime
        {
            get => Model?.StartTime ?? TimeSpan.Zero;
            set
            {
                if (Model == null) Model = new Appointment();
                Model.StartTime = value;
                ValidateAppointment();
                NotifyPropertyChanged();
            }
        }

        public TimeSpan EndTime
        {
            get => Model?.EndTime ?? TimeSpan.Zero;
            set
            {
                if (Model == null) Model = new Appointment();
                Model.EndTime = value;
                ValidateAppointment();
                NotifyPropertyChanged();
            }
        }

        public string? Notes
        {
            get => Model?.Notes;
            set
            {
                if (Model == null) Model = new Appointment();
                Model.Notes = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime MinStartDate
        {
            get
            {
                return DateTime.Today;
            }
        }
        public bool CanSave => string.IsNullOrEmpty(ErrorMessage);
        private bool _isErrorMessageVisible = false;
        public bool IsErrorMessageVisible
        {
            get => _isErrorMessageVisible;
            set
            {
                if (_isErrorMessageVisible != value)
                {
                    _isErrorMessageVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool IsAppointmentTimeValid(DateTime date, TimeSpan time)
        {

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            var startHour = new TimeSpan(8, 0, 0);  
            var endHour = new TimeSpan(17, 0, 0);   

            if (time < startHour || time >= endHour)
            {
                return false;
            }

            return true;
        }

        private void ValidateAppointment()
        {
            var date = StartDate.Date;
            var startTime = StartTime;
            var endTime = EndTime;

            if (endTime <= startTime)
            {
                ErrorMessage = "The appointment end time must be after the start time.";
            }       

            if (IsAppointmentTimeValid(date, startTime))
            {
                ErrorMessage = string.Empty;
                IsErrorMessageVisible = false; 

            }
            else
            {
                ErrorMessage = "Appointments are only available Monday through Friday, from 8:00 AM to 5:00 PM local time.";
                IsErrorMessageVisible = true;
            }
            NotifyPropertyChanged(nameof(CanSave));
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler? PropertyChanged;

		public void Refresh()
		{
			NotifyPropertyChanged(nameof(Patients));
			NotifyPropertyChanged(nameof(Physicians));
		}

        // check to see if better way 
        public void SetModel(Appointment? model)
        {
            Model = model ?? new Appointment();
            NotifyPropertyChanged(nameof(Model));
            NotifyPropertyChanged(nameof(SelectedPatient));
            NotifyPropertyChanged(nameof(SelectedPhysician));
            NotifyPropertyChanged(nameof(StartDate));
            NotifyPropertyChanged(nameof(StartTime));
            NotifyPropertyChanged(nameof(EndTime));
            NotifyPropertyChanged(nameof(Notes));
            NotifyPropertyChanged(nameof(CanSave));
            NotifyPropertyChanged(nameof(IsErrorMessageVisible));
            NotifyPropertyChanged(nameof(ErrorMessage));
        }

    }
}