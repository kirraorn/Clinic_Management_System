using Library.TheraHealth.Models;
using Library.TheraHealth.Services;
using Maui.TheraHealthOS.ViewModels;
using System.Linq;

namespace Maui.TheraHealthOS.Views;

[QueryProperty(nameof(AppointmentId), "appointmentId")]
public partial class AppointmentView : ContentPage
{
    public int AppointmentId { get; set; }
    public AppointmentView()
    {
        InitializeComponent();
        BindingContext = new AppointmentViewModel();
    }
    private void CancelAppointmentClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    
    private void OkAppointmentClicked(object sender, EventArgs e)
    {
        var vm = BindingContext as AppointmentViewModel;
        if (vm != null)
        {
            AppointmentServiceProxy.Current.AddOrUpdate(vm.Model);
        }
        Shell.Current.GoToAsync("//MainPage");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        var vm = BindingContext as AppointmentViewModel;
        if (vm == null)
        {
            vm = new AppointmentViewModel();
            BindingContext = vm;
        }

        if (AppointmentId == 0)
        {
            vm.SetModel(new Appointment());
        }
        else
        {
            vm.SetModel(new Appointment(AppointmentId));
        }

        vm.Refresh();
    }     

}