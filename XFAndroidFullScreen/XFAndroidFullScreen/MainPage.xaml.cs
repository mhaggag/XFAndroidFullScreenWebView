namespace XFAndroidFullScreen
{
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// The main (and only) page in the application. Loads YouTube in a WebView to test full-screen support.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Bindable property for <see cref="IsLoading" />.
        /// </summary>
        public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
            nameof(IsLoading),
            typeof(bool),
            typeof(MainPage),
            defaultValue: true);

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class. The page uses itself as the
        /// binding context for simplicity. In a bigger project, you'd typically use a view model.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the web viewer is loading.
        /// </summary>
        public bool IsLoading
        {
            get => (bool)GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }

        private void OnWebViewNavigated(object sender, EventArgs eventArgs)
        {
            IsLoading = false;
        }
    }
}