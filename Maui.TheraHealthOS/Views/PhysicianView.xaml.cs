using Library.TheraHealth.Models;
using Library.TheraHealth.Services;

namespace Maui.TheraHealthOS.Views;

[QueryProperty(nameof(PhysicianId), "physicianId")]
public partial class PhysicianView : ContentPage
{
    public int PhysicianId { get; set; }
    public PhysicianView()
    {
        InitializeComponent();
        BindingContext = new Physician();
    }

    private void CancelPhysicianClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void OkPhysicianClicked(object sender, EventArgs e)
    {
        PhysicianServiceProxy.Current.AddOrUpdate(BindingContext as Physician);
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (PhysicianId == 0)
        {
            BindingContext = new Physician();
        }
        else
        {
            BindingContext = new Physician(PhysicianId);
        }
    }
}