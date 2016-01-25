using System.Windows;
using System.Windows.Controls;

namespace EnviroGenDisplay.Views
{
    /// <summary>
    /// Interaction logic for ContextInfoProvider.xaml
    /// </summary>
    public partial class ContextInfoProvider : UserControl
    {
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
        }
    }
}
