using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EnviroGenDisplay
{
    public class ContextProvider : ContentControl
    {
        public static Action<ContextProvider> SetContextInfo { get; set; }
        public static Action<ContextProvider> RemoveContextInfo { get; set; }

        public static readonly DependencyProperty ContextInfoProperty
            = DependencyProperty.Register("ContextInfo",
                typeof(string),
                typeof(ContextProvider));

        public string ContextInfo
        {
            get { return (string)GetValue(ContextInfoProperty); }
            set { SetValue(ContextInfoProperty, value); }
        }

        static ContextProvider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContextProvider), new FrameworkPropertyMetadata(typeof(ContextProvider)));
        }

        public ContextProvider()
        {
            //Setting to NaN does in code what Width="Auto" does in xaml
            Width = Double.NaN;
            Height = Double.NaN;

            MouseEnter += OnMouseEnter;
            MouseLeave += OnMouseLeave;
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            SetContextInfo?.Invoke(this);
        }

        private void OnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            RemoveContextInfo?.Invoke(this);
        }
    }
}
