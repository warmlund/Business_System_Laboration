using System.Windows;


namespace Business_System_Laboration_4
{
    class WindowEvents
    {

        #region dependencyproperty för closing
        public static bool GetEnableClosing(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableClosingProperty);
        }

        public static void SetEnableClosing(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableClosingProperty, value);
        }

        public static readonly DependencyProperty EnableClosingProperty = DependencyProperty.RegisterAttached(nameof(EnableClosingProperty), typeof(bool), typeof(WindowEvents), new PropertyMetadata(false, EnableClosingChanged));

        private static void EnableClosingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                window.Loaded += (s, e) =>
                {
                    if (window.DataContext is ViewModelBase viewModel)
                    {

                        window.Closing += (s, e) =>
                {
                    viewModel.Cart.ReturnItemsToStock();
                    viewModel.ProdHandler.SaveProducts();

                };
                    }
                };
            }
        }

        #endregion

        #region dependencyProperty för loading
        public static bool GetEnableLoading(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableLoadingProperty);
        }

        public static void SetEnableLoading(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableLoadingProperty, value);
        }

        public static readonly DependencyProperty EnableLoadingProperty = DependencyProperty.RegisterAttached(nameof(EnableLoadingProperty), typeof(bool), typeof(WindowEvents), new PropertyMetadata(false, EnableLoadingChanged));

        private static void EnableLoadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                window.Loaded += (s, e) =>
                {
                    if (window.DataContext is ViewModelBase viewModel)
                    {
                        viewModel.ProdHandler.LoadProducts(viewModel);
                    }
                };
            }
        }

        #endregion
    }
}
