using System.Windows.Controls;
using System.Windows.Media;
using EnviroGenDisplay.ViewModels;
using EnviroGenDisplay.ViewModels.Modifiers;

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

            AddChildModifier(new AddModifierViewModel(environment));
            AddChildModifier(new ClampModifierViewModel(environment));
            AddChildModifier(new ExponentModifierViewModel(environment));
            AddChildModifier(new InvertModifierViewModel(environment));
            AddChildModifier(new NormalizeModifierViewModel(environment));
            AddChildModifier(new RidgedModifierViewModel(environment));
            AddChildModifier(new ScaleModifierViewModel(environment));
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
