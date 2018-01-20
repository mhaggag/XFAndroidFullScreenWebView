[assembly: Xamarin.Forms.ExportRenderer(
    typeof(XFAndroidFullScreen.Controls.FullScreenEnabledWebView),
    typeof(XFAndroidFullScreen.Droid.Renderers.FullScreenEnabledWebViewRenderer))]
namespace XFAndroidFullScreen.Droid.Renderers
{
    using System;
    using Android.Content;
    using Controls;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    /// <summary>
    /// An Android renderer for <see cref="FullScreenEnabledWebView"/>.
    /// </summary>
    public class FullScreenEnabledWebViewRenderer : WebViewRenderer
    {
        private FullScreenEnabledWebView _webView;

        /// <summary>
        /// Initializes a new instance of the <see cref="FullScreenEnabledWebViewRenderer"/> class.
        /// </summary>
        /// <param name="context">An Android context.</param>
        public FullScreenEnabledWebViewRenderer(Context context) : base(context)
        {
        }

        /// <inheritdoc/>
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            _webView = (FullScreenEnabledWebView)e.NewElement;
        }

        /// <summary>
        /// Creates a <see cref="FormsWebChromeClient"/> that implements the necessary callbacks to support
        /// full-screen operation.
        /// </summary>
        /// <returns>A <see cref="FullScreenEnabledWebChromeClient"/>.</returns>
        protected override FormsWebChromeClient GetFormsWebChromeClient()
        {
            var client = new FullScreenEnabledWebChromeClient();
            client.EnterFullscreenRequested += OnEnterFullscreenRequested;
            client.ExitFullscreenRequested += OnExitFullscreenRequested;
            return client;
        }

        /// <summary>
        /// Executes the full-screen command on the <see cref="FullScreenEnabledWebView"/> if available. The
        /// Xamarin view to display in full-screen is sent as a command parameter.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="eventArgs">The event arguments.</param>
        private void OnEnterFullscreenRequested(
            object sender,
            EnterFullScreenRequestedEventArgs eventArgs)
        {
            if (_webView.EnterFullScreenCommand != null && _webView.EnterFullScreenCommand.CanExecute(null))
            {
                _webView.EnterFullScreenCommand.Execute(eventArgs.View.ToView());
            }
        }

        /// <summary>
        /// Executes the exit full-screen command on th e <see cref="FullScreenEnabledWebView"/> if available.
        /// The command is passed no parameters.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="eventArgs">The event arguments.</param>
        private void OnExitFullscreenRequested(object sender, EventArgs eventArgs)
        {
            if (_webView.ExitFullScreenCommand != null && _webView.ExitFullScreenCommand.CanExecute(null))
            {
                _webView.ExitFullScreenCommand.Execute(null);
            }
        }
    }
}