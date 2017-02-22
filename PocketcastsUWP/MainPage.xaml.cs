using System;
using System.Diagnostics;
using Windows.Media;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;


namespace PocketcastsUWP
{

    public sealed partial class MainPage : Page
    {

        SystemMediaTransportControls systemControls;

        public MainPage()
        {
            this.InitializeComponent();

            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = Windows.UI.Color.FromArgb(255, 244, 67, 54);
            titleBar.ForegroundColor = Windows.UI.Color.FromArgb(255, 255, 255, 255);   
            titleBar.ButtonBackgroundColor = Windows.UI.Color.FromArgb(255, 244, 67, 54);
            titleBar.ButtonForegroundColor = Windows.UI.Color.FromArgb(255, 255, 255, 255);

        }

        async void SystemControls_ButtonPressed(SystemMediaTransportControls sender, SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            string status = string.Empty;
            string playCommand = "angular.element(document).injector().get('mediaPlayer').playPause()";
            string statusCommand = "angular.element(document).injector().get('mediaPlayer').playing";
            string forwardCommand = "angular.element(document).injector().get('mediaPlayer').jumpForward()";
            string backCommand = "angular.element(document).injector().get('mediaPlayer').jumpBack()";

            switch (args.Button)
            {
                case SystemMediaTransportControlsButton.Play:
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        await webEntryPoint.InvokeScriptAsync("eval", new string[] { playCommand });
                        status = await webEntryPoint.InvokeScriptAsync("eval", new string[] { statusCommand });
                    });
                    Debug.WriteLine("Status: " + status);
                    break;
                case SystemMediaTransportControlsButton.Next:
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        await webEntryPoint.InvokeScriptAsync("eval", new string[] { forwardCommand });
                    });
                    break;
                case SystemMediaTransportControlsButton.Previous:
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        await webEntryPoint.InvokeScriptAsync("eval", new string[] { backCommand });
                    });
                    break;
            }
        }

        private void webEntryPoint_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            systemControls = SystemMediaTransportControls.GetForCurrentView();
            systemControls.ButtonPressed += SystemControls_ButtonPressed;
            systemControls.IsPlayEnabled = true;
            systemControls.IsPauseEnabled = true;
            systemControls.IsFastForwardEnabled = true;
            systemControls.IsNextEnabled = true;
            systemControls.IsPreviousEnabled = true;
            systemControls.IsRewindEnabled = true;
        }

    }
}
