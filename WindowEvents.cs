using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Business_System_Laboration_4
{
    class WindowEvents
    {
        #region dependencyproperty för closing av huvudfönstret
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

        #region dependencyproperty för closing av dialogfönster
        public static bool GetEnableDialogClosing(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableDialogClosingProperty);
        }

        public static void SetEnableDialogClosing(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableDialogClosingProperty, value);
        }

        public static readonly DependencyProperty EnableDialogClosingProperty = DependencyProperty.RegisterAttached(nameof(EnableDialogClosingProperty), typeof(bool), typeof(WindowEvents), new PropertyMetadata(false, EnableDialogClosingChanged));

        private static void EnableDialogClosingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                window.Loaded += (s, e) =>
                {
                    if (window.DataContext is ICloseWindow vm)
                    {
                        vm.Close += () =>
                        {
                            window.Close();
                        };
                    }
                };
            }
        }

        #endregion

        #region dependencyProperty för loading av huvudfönstret
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
                        viewModel.ProdHandler.LoadProducts();
                    }
                };
            }
        }

        #endregion

        #region dependencyProperties för textboxes
        public static bool GetEnableOnlyNumbericsChanged(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableOnlyNumericsProperty);
        }

        public static void SetEnableOnlyNumericsChanged(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableOnlyNumericsProperty, value);
        }

        public static readonly DependencyProperty EnableOnlyNumericsProperty = DependencyProperty.RegisterAttached(nameof(EnableOnlyNumericsProperty), typeof(bool), typeof(WindowEvents), new PropertyMetadata(false, EnableOnlyNumericsChanged));

        private static void EnableOnlyNumericsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {

                if ((bool)e.NewValue)
                {
                    textBox.PreviewTextInput += NumericInputPreviewTextInput;
                    DataObject.AddPastingHandler(textBox, NumericInputPastingHandler);
                }
                else
                {
                    textBox.PreviewTextInput -= NumericInputPreviewTextInput;
                    DataObject.RemovePastingHandler(textBox, NumericInputPastingHandler);
                }
            }
        }

        public static bool GetEnableOnlyIntegersChanged(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableOnlyIntegersProperty);
        }

        public static void SetEnableOnlyIntegersChanged(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableOnlyIntegersProperty, value);
        }

        public static readonly DependencyProperty EnableOnlyIntegersProperty = DependencyProperty.RegisterAttached(nameof(EnableOnlyIntegersProperty), typeof(bool), typeof(WindowEvents), new PropertyMetadata(false, EnableOnlyIntegersChanged));

        private static void EnableOnlyIntegersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {

                if ((bool)e.NewValue)
                {
                    textBox.PreviewTextInput += IntegerInputPreviewTextInput;
                    DataObject.AddPastingHandler(textBox, IntegerInputPastingHandler);
                }
                else
                {
                    textBox.PreviewTextInput -= IntegerInputPreviewTextInput;
                    DataObject.RemovePastingHandler(textBox, IntegerInputPastingHandler);
                }
            }
        }

        #endregion



        private static void NumericInputPreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            foreach (var ch in e.Text)
            {
                if (!char.IsDigit(ch) && ch != ',')
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private static void NumericInputPastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));
                if (!IsValidInput(pastedText))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private static bool IsValidInput(string input)
        {

            foreach (var ch in input)
            {
                if (!char.IsDigit(ch) && ch != ',')
                {
                    return false;
                }
            }
            return true;
        }

        private static void IntegerInputPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var ch in e.Text)
            {
                if (!char.IsDigit(ch))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private static void IntegerInputPastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));
                if (!IsValidIntegerInput(pastedText))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private static bool IsValidIntegerInput(string input)
        {

            foreach (var ch in input)
            {
                if (!char.IsDigit(ch))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
