using System.Windows.Controls;
using System.Windows.Media;
using EnviroGenDisplay.ViewModels;

namespace EnviroGenDisplay.Views.Modifiers
{
    /// <summary>
    /// Interaction logic for ModifierView.xaml
    /// </summary>
    public partial class ModifierView : UserControl
    {
        private Brush m_BackgroundBrushOne { get; } = new SolidColorBrush(Color.FromRgb(240, 240, 240));
        private Brush m_BackgroundBrushTwo { get; } = new SolidColorBrush(Color.FromRgb(200, 200, 200));

        public ModifierView(IEnvironment environment)
        {
            InitializeComponent();

            //AddChildModifier(new AddModifierViewModel(environment));
            //AddChildModifier(new ClampModifierNodeViewModel(environment));
            //AddChildModifier(new ExponentModifierNodeViewModel(environment));
            //AddChildModifier(new InvertModifierNodeViewModel(environment));
            //AddChildModifier(new NormalizeModifierNodeViewModel(environment));
            //AddChildModifier(new RidgedModifierNodeViewModel(environment));
            //AddChildModifier(new ScaleModifierNodeViewModel(environment));
        }

        private void AddChildModifier(ViewModelBase vm)
        {
            var brush = (Modifiers.Children.Count % 2 == 0) 
                        ? m_BackgroundBrushOne 
                        : m_BackgroundBrushTwo;

            Modifiers.Children.Add(new Border
            {
                Child = new ContentControl {Content = vm},
                Background = brush
            });
        }
    }
}
