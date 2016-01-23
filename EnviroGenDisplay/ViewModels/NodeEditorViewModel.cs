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
                return new GenerationOptionsViewModel(Environment);
            }

            return null;
        }
    }
}
