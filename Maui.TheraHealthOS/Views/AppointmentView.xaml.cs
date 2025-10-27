using Library.TheraHealth.Models;
using Library.TheraHealth.Services;
using Maui.TheraHealthOS.ViewModels;

namespace Maui.TheraHealthOS.Views;

[QueryProperty(nameof(AppointmentId), "appointmentId")]
public partial class AppointmentView : ContentPage
{
    public int AppointmentId { get; set; }
    public AppointmentView()
    {
        InitializeComponent();
        BindingContext = new AppointmentViewModel(new Appointment());
    }
    private void CancelAppointmentClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    
    private async void OkAppointmentClicked(object sender, EventArgs e)
    {
        if (BindingContext is AppointmentViewModel vm)
        {
            var appt = vm.Model;
            if (appt != null)
            {
                if (appt.StartTime == null)
                    appt.StartTime = DateTime.Now;

                AppointmentServiceProxy.Current.AddOrUpdate(appt);
            }
        }
        else
        {
            AppointmentServiceProxy.Current.AddOrUpdate(BindingContext as Appointment);
        }

        await Shell.Current.GoToAsync("//MainPage");
        if (Shell.Current.CurrentPage is Maui.TheraHealthOS.MainPage mp)
        {
            (mp.BindingContext as Maui.TheraHealthOS.ViewModels.MainViewModel)?.Refresh();
        }
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (AppointmentId == 0)
        {
            BindingContext = new AppointmentViewModel(new Appointment());
        }
        else
        {
            BindingContext = new AppointmentViewModel(new Appointment(AppointmentId));
        }

    }
}

