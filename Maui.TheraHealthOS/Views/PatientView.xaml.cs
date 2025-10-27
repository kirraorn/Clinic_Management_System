using Library.TheraHealth.Models;
using Library.TheraHealth.Services;

namespace Maui.TheraHealthOS.Views;

[QueryProperty(nameof(PatientId), "patientId")]
public partial class PatientView : ContentPage
{
    public int PatientId { get; set; }
    public PatientView()
    {
        InitializeComponent();
        BindingContext = new Patient();
    }

    private void CancelPatientClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void OkPatientClicked(object sender, EventArgs e)
    {
        PatientServiceProxy.Current.AddOrUpdate(BindingContext as Patient);


        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (PatientId == 0)
        {
            BindingContext = new Patient();
        }
        else
        {
            BindingContext = new Patient(PatientId);
        }

    }
}