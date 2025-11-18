// BUGS:
// 2: when editing appointments, the physician dropdown needs to be reclicked or else it will be deleted

using Maui.TheraHealthOS.ViewModels;

namespace Maui.TheraHealthOS;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainViewModel();

	}

// Refresh on navigation ---------------
	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(BindingContext as MainViewModel)?.Refresh();
	}
// Add buttons ----------------
	private void AddAppointmentClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Appointment?appointmentId=0");
	}

	private void AddPatientClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Patient?patientId=0");
	}

	private void AddPhysicianClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Physician?physicianId=0");
	}

	// Delete buttons -----------------

	private void DeleteAppointmentClicked(object sender, EventArgs e)
	{
		(BindingContext as MainViewModel)?.DeleteAppointment();
	}
	private void DeletePatientClicked(object sender, EventArgs e)
	{
		(BindingContext as MainViewModel)?.DeletePatient();
	}
	private void DeletePhysicianClicked(object sender, EventArgs e)
	{
		(BindingContext as MainViewModel)?.DeletePhysician();
	}

	// Edit buttons -----------------

	private void EditAppointmentClicked(object sender, EventArgs e)
	{
		var selectedId = (BindingContext as MainViewModel)?.SelectedAppointment?.Model?.Id ?? 0;
		Shell.Current.GoToAsync($"//Appointment?appointmentId={selectedId}");
	}
	private void EditPatientClicked(object sender, EventArgs e)
	{
		var selectedId = (BindingContext as MainViewModel)?.SelectedPatient?.Model?.Id ?? 0;
		Shell.Current.GoToAsync($"//Patient?patientId={selectedId}");
	}
	private void EditPhysicianClicked(object sender, EventArgs e)
	{
		var selectedId = (BindingContext as MainViewModel)?.SelectedPhysician?.Model?.Id ?? 0;
		Shell.Current.GoToAsync($"//Physician?physicianId={selectedId}");
	}

	// Inline edit to refresh the list after deletion

	private void InlineEditAppointmentClicked(object sender, EventArgs e)
	{
		(BindingContext as MainViewModel)?.Refresh();
	}
	private void InlineEditPatientClicked(object sender, EventArgs e)
	{
		(BindingContext as MainViewModel)?.Refresh();
	}

	private void InlineEditPhysicianClicked(object sender, EventArgs e)
	{
		(BindingContext as MainViewModel)?.Refresh();
	}

	/*search buttons -----------------
	private void SearchAppointmentClicked(object sender, EventArgs e)
	{
		(BindingContext as MainViewModel)?.Refresh();
	}
	private void SearchPatientClicked(object sender, EventArgs e)
	{
		(BindingContext as MainViewModel)?.Refresh();
	}
		private void SearchPhysicianClicked(object sender, EventArgs e)
	{
		(BindingContext as MainViewModel)?.Refresh();
	}*/
}

