using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Library.TheraHealth.Models;
using Library.TheraHealth.Services;
using Library.TheraHealth.DTO;

namespace Maui.TheraHealthOS.ViewModels
{
public class MainViewModel: INotifyPropertyChanged
	{
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler? PropertyChanged;

		public void Refresh()
		{
			
			NotifyPropertyChanged(nameof(Patients));
			NotifyPropertyChanged(nameof(Physicians));
			NotifyPropertyChanged(nameof(Appointments));
		}
		public string? Query { get; set; }

// APPOINTMENTS ---------------------------------------------------------
		public ObservableCollection<AppointmentViewModel?> Appointments
		{
			get
			{
				var thing = AppointmentServiceProxy.Current.Appointments;
				return new ObservableCollection<AppointmentViewModel?>
				(AppointmentServiceProxy.Current.Appointments/*.
				Where(
					a => (a?.Patient?.Name?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
					|| (a?.Physician?.Name?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
				)*/
				.Select(a => new AppointmentViewModel(a))
				);
			}
		}
		public AppointmentViewModel? SelectedAppointment { get; set; }

		public void DeleteAppointment()
		{
			if (SelectedAppointment== null)
				return;
			AppointmentServiceProxy.Current.DeleteAppointment(SelectedAppointment?.Model?.Id ?? 0);
			NotifyPropertyChanged(nameof(Appointments));
		}
		
// PATIENTS ---------------------------------------------------------
		public ObservableCollection<PatientViewModel?> Patients
		{
			get
			{
				return new ObservableCollection<PatientViewModel?>
					(PatientServiceProxy.
					Current.
					Patients
					.Where(
                        p => (p?.Name?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                    )
					.Select(p => new PatientViewModel(p))
					);
			}
		}
		public PatientViewModel? SelectedPatient { get; set; }

		public void DeletePatient()
		{
			if (SelectedPatient == null)
				return;
			PatientServiceProxy.Current.DeletePatient(SelectedPatient?.Model?.Id ?? 0);
			NotifyPropertyChanged(nameof(Patients));
		}

// PHYSICIANS ---------------------------------------------------------
		public ObservableCollection<PhysicianViewModel?> Physicians
		{
			get
			{
				return new ObservableCollection<PhysicianViewModel?>(
					PhysicianServiceProxy.
					Current.
					Physicians.
					Where(
                        p => (p?.Name?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                    )
					.Select(p => new PhysicianViewModel(p))
				);
			}
		}
		public PhysicianViewModel? SelectedPhysician { get; set; }
		
		public void DeletePhysician()
		{
			if (SelectedPhysician == null)
				return;
			PhysicianServiceProxy.Current.DeletePhysician(SelectedPhysician?.Model?.Id ?? 0);
			NotifyPropertyChanged(nameof(Physicians));
        }
	
	} 

}
