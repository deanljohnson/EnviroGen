using EnviroGen.Nodes;
using EnviroGenDisplay;
using EnviroGenDisplay.ViewModels;

namespace EnviroGenMinecraftMapMaker
{
    [EditorNode("Minecraft Map Exporter", typeof(MinecraftMapExporterView), Category = "Exporters")]
    public class MinecraftMapExporterNodeViewModel : NodeViewModel<ModifierNode<MinecraftMapExporter>>
    {

        public string Path {
            get { return Node.Modifier.Path; }
            set
            {
                if (Node.Modifier.Path != value)
                {
                    Node.Modifier.Path = value;
                    OnPropertyChanged();
                }
            }
        }

        static MinecraftMapExporterNodeViewModel()
        {
            Name = "Minecraft Map Exporter";
        }

        public MinecraftMapExporterNodeViewModel()
            : base("Exporting to Minecraft Map File")
        {
            Node = new ModifierNode<MinecraftMapExporter>();
        }
    }
}