using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EnviroGenDisplay.Views
{
    /// <summary>
    /// Interaction logic for ContextInfoProvider.xaml
    /// </summary>
    public partial class ContextInfoProvider : UserControl
    {
        public static Action<ContextInfoProvider> SetContextInfo { get; set; }
        public static Action<ContextInfoProvider> RemoveContextInfo { get; set; }

        public static readonly DependencyProperty ContextInfoProperty 
            = DependencyProperty.Register("ContextInfo",
                typeof(string), 
                typeof(ContextInfoProvider));

        public string ContextInfo
        {
            get { return (string)GetValue(ContextInfoProperty); }
            set { SetValue(ContextInfoProperty, value);}
        }

        public ContextInfoProvider()
        {
            InitializeComponent();

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
