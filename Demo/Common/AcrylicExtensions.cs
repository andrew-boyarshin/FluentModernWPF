using System;
using System.Windows;
using System.Windows.Interop;
using SourceChord.FluentWPF.Utility;

namespace CodeWanda.UI.Common
{
    public static class AcrylicExtensions
    {
        // Using a DependencyProperty as the backing store for Enabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AcrylicEnabledProperty =
            DependencyProperty.RegisterAttached("AcrylicEnabled", typeof(bool), typeof(AcrylicExtensions),
                new PropertyMetadata(false, OnAcrylicEnabledChanged));

        /// <summary>Helper for getting <see cref="AcrylicEnabledProperty"/> from <paramref name="window"/>.</summary>
        /// <param name="window"><see cref="Window"/> to read <see cref="AcrylicEnabledProperty"/> from.</param>
        /// <returns>AcrylicEnabled property value.</returns>
        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static bool GetAcrylicEnabled(Window window)
        {
            if (window == null) throw new ArgumentNullException(nameof(window));
            return (bool) window.GetValue(AcrylicEnabledProperty);
        }

        /// <summary>Helper for setting <see cref="AcrylicEnabledProperty"/> on <paramref name="window"/>.</summary>
        /// <param name="window"><see cref="Window"/> to set <see cref="AcrylicEnabledProperty"/> on.</param>
        /// <param name="value">AcrylicEnabled property value.</param>
        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static void SetAcrylicEnabled(Window window, bool value)
        {
            if (window == null) throw new ArgumentNullException(nameof(window));
            window.SetValue(AcrylicEnabledProperty, value);
        }

        private static void OnAcrylicEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Window win))
                return;

            var value = (bool) e.NewValue;
            if (value)
            {
                win.Loaded += (_, __) => EnableBlur(win);
                if (win.IsLoaded) EnableBlur(win);
            }
        }

        internal static void EnableBlur(Window win)
        {
            var windowHelper = new WindowInteropHelper(win);

            AcrylicHelper.EnableBlur(windowHelper.Handle);

            // WPF WindowChrome bug with SizeToContent workaround
            void OnContentRendered(object sender, EventArgs e)
            {
                if (win.SizeToContent != SizeToContent.Manual)
                {
                    win.InvalidateMeasure();
                }

                win.ContentRendered -= OnContentRendered;
            }

            win.ContentRendered += OnContentRendered;
        }
    }
}