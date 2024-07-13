using Microsoft.AspNetCore.Components.WebView;

namespace PomtoApp.Views;

public partial class NewAccountPage : ContentPage
{
    public NewAccountPage()
    {
        InitializeComponent();
        blazorWebView.UrlLoading += (sender, urlLoadingEventArgs) =>
        {
            if (urlLoadingEventArgs.Url.Host != "0.0.0.0")
            {
                urlLoadingEventArgs.UrlLoadingStrategy =
                    UrlLoadingStrategy.OpenInWebView;
            }
        };
    }

    private void blazorWebView_UrlLoading(object sender, Microsoft.AspNetCore.Components.WebView.UrlLoadingEventArgs e)
    {
        if (e.Url.Host != "0.0.0.0")
        {
            e.UrlLoadingStrategy =
                UrlLoadingStrategy.OpenInWebView;
        }
    }
}