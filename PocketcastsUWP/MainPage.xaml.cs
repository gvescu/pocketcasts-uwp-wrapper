using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PocketcastsUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        SystemMediaTransportControls systemControls;

        public MainPage()
        {
            this.InitializeComponent();

        }

        void SystemControls_ButtonPressed(SystemMediaTransportControls sender, SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            switch (args.Button)
            {
                case SystemMediaTransportControlsButton.Play:
                    playPause();
                    break;
                case SystemMediaTransportControlsButton.Pause:
                    playPause();
                    break;
                case SystemMediaTransportControlsButton.Next:
                    skipForward();
                    break;
                case SystemMediaTransportControlsButton.FastForward:
                    skipForward();
                    break;
                case SystemMediaTransportControlsButton.Previous:
                    jumpBack();
                    break;
                case SystemMediaTransportControlsButton.Rewind:
                    jumpBack();
                    break;
                default:
                    break;
            }
        }

        async void playPause() {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                string playCommand = "angular.element(document).injector().get('mediaPlayer').playPause()";
                await webEntryPoint.InvokeScriptAsync("eval", new string[] { playCommand });
            });
        }
        async void skipForward() {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                string skipCommand = "angular.element(document).injector().get('mediaPlayer').jumpForward()";
                await webEntryPoint.InvokeScriptAsync("eval", new string[] { skipCommand });
            });
        }
        async void jumpBack() {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                string jumpCommand = "angular.element(document).injector().get('mediaPlayer').jumpBack()";
                await webEntryPoint.InvokeScriptAsync("eval", new string[] { jumpCommand });
            });
        }

        private void webEntryPoint_LoadCompleted(object sender, NavigationEventArgs e)
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
