using CommunityToolkit.Mvvm.ComponentModel;

namespace FullScreenIntentNotification;
public class BasePage<TViewModel> : ContentPage where TViewModel : ObservableObject
{
    public BasePage(in TViewModel vm)
    {
        BindingContext = vm;
    }
}
