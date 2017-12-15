using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Media;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;


namespace PocketcastsUWP
{

    public sealed partial class MainPage : Page
    {
        string status = string.Empty;
        string playPauseCommand = "angular.element(document).injector().get('mediaPlayer').playPause()";
        string forwardCommand = "angular.element(document).injector().get('mediaPlayer').jumpForward()";
        string backCommand = "angular.element(document).injector().get('mediaPlayer').jumpBack()";
        string statusCommand = "angular.element(document).injector().get('mediaPlayer').playing.toString()";

        string getEpisodeCommand = "angular.element(document).injector().get('mediaPlayer').episode.title.toString()";
        string getPodcastCommand = "angular.element(document).injector().get('mediaPlayer').podcast.title.toString()";
        string getArtCommand = "angular.element(document).injector().get('mediaPlayer').podcast.thumbnail_url.toString()";

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
            switch (args.Button)
            {
                case SystemMediaTransportControlsButton.Play:
                case SystemMediaTransportControlsButton.Pause:
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        try
                        {
                            await webEntryPoint.InvokeScriptAsync("eval", new string[] { playPauseCommand });
                        }
                        catch (Exception ex)
                        {
                            //Throws exception on first press if nothing is playing, not sure why
                        }
                        updateMediaOverlay();
                    });
                    break;
                case SystemMediaTransportControlsButton.Next:
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        try {
                            await webEntryPoint.InvokeScriptAsync("eval", new string[] { forwardCommand });
                        }
                        catch (Exception ex)
                        {
                            //Throws exception on first press if nothing is playing, not sure why
                        }
                    });
                    break;
                case SystemMediaTransportControlsButton.Previous:
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        try {
                            await webEntryPoint.InvokeScriptAsync("eval", new string[] { backCommand });
                        }
                        catch (Exception ex)
                        {
                            //Throws exception on first press if nothing is playing, not sure why
                        }
                    });
                    injectNotification();
                    break;

            }
        }

        async private void updateMediaOverlay()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                SystemMediaTransportControlsDisplayUpdater mediaOverlay = systemControls.DisplayUpdater;
                mediaOverlay.Type = Windows.Media.MediaPlaybackType.Music;

                String episode, podcast, artURI;
                
                try
                {
                    episode = await webEntryPoint.InvokeScriptAsync("eval", new string[] { getEpisodeCommand });
                    podcast = await webEntryPoint.InvokeScriptAsync("eval", new string[] { getPodcastCommand });
                    artURI = await webEntryPoint.InvokeScriptAsync("eval", new string[] { getArtCommand });                    
                }
                catch(Exception ex)
                {
                    episode = "";
                    podcast = "";
                    artURI = "ms-appx:///Assets/StoreLogo.scale-400.png";
                }

                mediaOverlay.MusicProperties.Artist = podcast;
                mediaOverlay.MusicProperties.Title = episode;
                mediaOverlay.Thumbnail = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromUri(new Uri(artURI));

                mediaOverlay.Update();

                status = await webEntryPoint.InvokeScriptAsync("eval", new string[] { statusCommand });
                SystemMediaTransportControls.GetForCurrentView().PlaybackStatus = status.Equals("true") ? MediaPlaybackStatus.Playing : MediaPlaybackStatus.Paused;
            });

        }

        private void webEntryPoint_ScriptNotify(object sender, NotifyEventArgs e)
        {
            String eventString = e.Value;
            Debug.WriteLine(eventString);
            updateMediaOverlay();
        }

        async private void injectNotification()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                string playPauseNotifyFunc = "var btns = document.getElementsByClassName('episode_button'); " +
                                             "for (var i=0; i < btns.length; i++) { " +
                                             "btns[i].addEventListener('click', function() { " +
                                             "window.external.notify(\"PlayPauseEvent\"); })} " +
                                             "document.getElementsByClassName('play_pause_button')[0].addEventListener('click', function() { window.external.notify(\"PlayPauseEvent\"); })";

                await Task.Delay(750); //Superrrr Ghetto, but can't figure out how to fire event on rendering complete ('NavigationCompleted" fires to soon and also multiple times for some reason)
                await webEntryPoint.InvokeScriptAsync("eval", new string[] { playPauseNotifyFunc });                
            });
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
            injectNotification();
            updateMediaOverlay();
        }
    }
}
