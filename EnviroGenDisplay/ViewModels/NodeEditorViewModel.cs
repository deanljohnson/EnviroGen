using EnviroGenDisplay.ViewModels.Modifiers;

namespace EnviroGenDisplay.ViewModels
{
    public class NodeEditorViewModel : ViewModelBase
    {
        public IEnvironment Environment { get; set; }

        public NodeEditorViewModel(IEnvironment environment)
        {
            Environment = environment;
        }

        public ViewModelBase GetNodeViewModel(string nodeName)
        {
            //TODO: Fix these hard coded mappings.... this is not neat by any means
            if (nodeName == "Simplex Noise")
            {
                return new TerrainGeneratorNodeViewModel(Environment);
            }
            if (nodeName == "Add")
            {
                return new AddModifierNodeViewModel();
            }
            if (nodeName == "Clamp")
            {
                return new ClampModifierNodeViewModel();
            }
            if (nodeName == "Exponent")
            {
                return new ExponentModifierNodeViewModel();
            }
            if (nodeName == "Invert")
            {
                return new InvertModifierNodeViewModel();
            }
            if (nodeName == "Normalize")
            {
                return new NormalizeModifierNodeViewModel();
            }
            if (nodeName == "Ridged")
            {
                return new RidgedModifierNodeViewModel();
            }
            if (nodeName == "Scale")
            {
                return new ScaleModifierNodeViewModel();
            }

            return null;
        }
    }
}
