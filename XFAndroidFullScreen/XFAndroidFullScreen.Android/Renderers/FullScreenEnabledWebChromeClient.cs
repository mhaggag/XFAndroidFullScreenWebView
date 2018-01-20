namespace XFAndroidFullScreen.Droid.Renderers
{
    using System;
    using Android.Views;
    using Xamarin.Forms.Platform.Android;

    /// <summary>
    /// A <see cref="FormsWebChromeClient"/> that implements full-screen callbacks and raises corresponding
    /// .NET events.
    /// </summary>
    public class FullScreenEnabledWebChromeClient : FormsWebChromeClient
    {
        /// <summary>
        /// Triggered when the content requests full-screen.
        /// </summary>
        public event EventHandler<EnterFullScreenRequestedEventArgs> EnterFullscreenRequested;

        /// <summary>
        /// Triggered when the content requests exiting full-screen.
        /// </summary>
        public event EventHandler ExitFullscreenRequested;

        /// <inheritdoc />
        public override void OnHideCustomView()
        {
            ExitFullscreenRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc />
        public override void OnShowCustomView(View view, ICustomViewCallback callback)
        {
            EnterFullscreenRequested?.Invoke(this, new EnterFullScreenRequestedEventArgs(view));
        }
    }
}