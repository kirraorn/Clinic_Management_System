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
using Library.TheraHealth.DTO;

namespace Maui.TheraHealthOS.ViewModels
{
    public class AppointmentViewModel : INotifyPropertyChanged
    {
        private PatientServiceProxy _patientSvc;
        private PhysicianServiceProxy _physicianSvc;
        private AppointmentServiceProxy _appointmentSvc;

        public List<Patient?> Patients => _patientSvc.Patients;
        public List<PhysicianDTO> Physicians => _physicianSvc.Physicians;

        private List<Appointment?> appointments;
        public List<Appointment?> Appointments
        {
            get
            {
                return appointments;
            }
        }

// CONSTRUCTORS ---------------------------------------------------
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
            ValidateAppointment();
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
            ValidateAppointment();
        }
// COMMANDS ---------------------------------------------------
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

// SETTERS/GETTERS ---------------------------------------------------
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

        public PhysicianDTO? SelectedPhysician
        {
            get => Model?.Physician;
            set
            {
                if (Model == null) Model = new Appointment();
                Model.PhysicianId = value?.Id ?? 0;
                Model.Physician = value;
            
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

// UPDATING MODEL ---------------------------------------------------
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

// VALIDATION ---------------------------------------------------
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

        private void ValidateAppointment()
        {
            var date = StartDate.Date;
            var startTime = StartTime;
            var endTime = EndTime;

      
            var startHour = new TimeSpan(8, 0, 0);  
            var endHour = new TimeSpan(17, 0, 0);  

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                ErrorMessage = "Appointments are only available Monday through Friday";
                IsErrorMessageVisible = true;

            }
            else if (startTime < startHour || startTime > endHour)
            {
                ErrorMessage = "The start time must be between 8:00 AM and 5:00 PM.";
                IsErrorMessageVisible = true;
            }
            else if (endTime < startHour || endTime > endHour)
            {
                ErrorMessage = "The end time must be between 8:00 AM and 5:00 PM.";
                IsErrorMessageVisible = true;
            }
            else if (startTime > endTime || startTime == endTime)
            {
                ErrorMessage = "The start time must be before the end time.";
                IsErrorMessageVisible = true;
            }
            else if (SelectedPhysician == null)
            {
                ErrorMessage = "Please select a physician.";
                IsErrorMessageVisible = true;
            }
            else if (SelectedPatient == null)
            {
                ErrorMessage = "Please select a patient.";
                IsErrorMessageVisible = true;
            } 
            else if (StartDate == null)
            {
                ErrorMessage = "Please select a start date.";
                IsErrorMessageVisible = true;
            }
            if (StartDate < DateTime.Today || (StartDate == DateTime.Today && StartTime < DateTime.Now.TimeOfDay))
            {
                ErrorMessage = "Start date/time cannot be in the past.";
                IsErrorMessageVisible = true;
            }
            else
            {
               ErrorMessage = string.Empty;
               IsErrorMessageVisible = false;
            }
           
            // If no other validation error, check physician double-booking 
            {
                try
                {
                    if (Model != null && Model.PhysicianId > 0 && (Model.StartDate.HasValue || true) && (Model.StartTime.HasValue || true) && (Model.EndTime.HasValue || true))
                    {
                        var newStart = StartDate.Date + StartTime;
                        var newEnd = StartDate.Date + EndTime;

                        if (newEnd > newStart)
                        {
                            var conflict = AppointmentServiceProxy
                                .Current
                                .Appointments
                                .Where(a => a != null)
                                .Where(a => (a?.PhysicianId ?? -1) == Model.PhysicianId)
                                .Where(a => (a?.Id ?? 0) != (Model.Id)) // exclude self when editing
                                .Where(a => a?.StartDate.HasValue == true && a?.StartTime.HasValue == true && a?.EndTime.HasValue == true)
                                .Any(a =>
                                {
                                    var existingStart = a!.StartDate!.Value.Date + a.StartTime!.Value;
                                    var existingEnd = a.StartDate!.Value.Date + a.EndTime!.Value;
                                    if (existingStart.Date != newStart.Date) return false; // only same date
                                    return (newStart < existingEnd) && (existingStart < newEnd);
                                });

                            if (conflict)
                            {
                                ErrorMessage = "This physician already has an overlapping appointment. Pick a different time.";
                                IsErrorMessageVisible = true;
                            }
                        }
                    }
                }
                catch
                {}
            }

            NotifyPropertyChanged(nameof(CanSave));
        }


    }
}