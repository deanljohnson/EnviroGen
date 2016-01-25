using EnviroGenDisplay.ViewModels.Continents;
using EnviroGenDisplay.ViewModels.Erosion;
using EnviroGenDisplay.ViewModels.Modifiers;

namespace EnviroGenDisplay.ViewModels
{
    public class NodeEditorViewModel : ViewModelBase
    {
        public IDisplayedEnvironment Environment { get; set; }

        public NodeEditorViewModel(IDisplayedEnvironment environment)
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
            if (nodeName == "Hydraulic")
            {
                return new HydraulicErosionNodeViewModel();
            }
            if (nodeName == "Improved Thermal")
            {
                return new ImprovedThermalErosionNodeViewModel();
            }
            if (nodeName == "Thermal")
            {
                return new ThermalErosionNodeViewModel();
            }
            if (nodeName == "Square")
            {
                return new SquareContinentNodeViewModel();
            }
            if (nodeName == "Simple Colorizer")
            {
                return new ColorizerNodeViewModel();
            }

            return null;
        }
    }
}
