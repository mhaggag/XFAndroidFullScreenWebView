namespace XFAndroidFullScreen.Controls
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    /// <summary>
    /// A web view that supports full-screen display.
    /// </summary>
    public class FullScreenEnabledWebView : WebView
    {
        /// <summary>
        /// Bindable property for <see cref="EnterFullScreenCommand"/>.
        /// </summary>
        public static readonly BindableProperty EnterFullScreenCommandProperty =
            BindableProperty.Create(
                nameof(EnterFullScreenCommand),
                typeof(ICommand),
                typeof(FullScreenEnabledWebView),
                defaultValue: new Command(async (view) => await DefaultEnterAsync((View)view)));

        /// <summary>
        /// Bindable property for <see cref="ExitFullScreenCommand"/>.
        /// </summary>
        public static readonly BindableProperty ExitFullScreenCommandProperty =
            BindableProperty.Create(
                nameof(ExitFullScreenCommand),
                typeof(ICommand),
                typeof(FullScreenEnabledWebView),
                defaultValue: new Command(async (view) => await DefaultExitAsync()));

        /// <summary>
        /// Gets or sets the command executed when the web view content requests entering full-screen.
        /// The command is passed a <see cref="View"/> containing the content to display.
        /// The default command displays the content as a modal page.
        /// </summary>
        public ICommand EnterFullScreenCommand
        {
            get => (ICommand)GetValue(EnterFullScreenCommandProperty);
            set => SetValue(EnterFullScreenCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the command executed when the web view content requests exiting full-screen.
        /// The command is passed no parameters.
        /// The default command pops a modal page off the navigation stack.
        /// </summary>
        public ICommand ExitFullScreenCommand
        {
            get => (ICommand)GetValue(ExitFullScreenCommandProperty);
            set => SetValue(ExitFullScreenCommandProperty, value);
        }

        private static async Task DefaultEnterAsync(View view)
        {
            var page = new ContentPage
            {
                Content = view
            };

            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        private static async Task DefaultExitAsync()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
