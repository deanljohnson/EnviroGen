using System.ComponentModel;
using EnviroGen;
using EnviroGen.Nodes;
using EnviroGenDisplay;
using EnviroGenDisplay.ViewModels;

namespace EnviroGenMinecraftMapMaker
{
    [EditorNode("Minecraft Map Exporter", typeof(MinecraftMapExporterView), Category = "Exporters")]
    public class MinecraftMapExporterNodeViewModel : NodeViewModel<ModifierNode<MinecraftMapExporter>>
    {
        private readonly BackgroundWorker m_ModifyWorker = new BackgroundWorker();

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

        public string LevelName
        {
            get { return Node.Modifier.Name; }
            set
            {
                if (Node.Modifier.Name != value)
                {
                    Node.Modifier.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Normalize
        {
            get { return Node.Modifier.Normalize; }
            set {
                if (Node.Modifier.Normalize != value)
                {
                    Node.Modifier.Normalize = value;
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
            Node = new ModifierNode<MinecraftMapExporter>
            {
                Modifier = new MinecraftMapExporter()
            };

            m_ModifyWorker.DoWork += CallBaseModify;
        }

        private void CallBaseModify(object sender, DoWorkEventArgs e)
        {
            var environment = (Environment)e.Argument;
            base.Modify(environment);
        }

        public override void Modify(Environment environment)
        {
            if (!m_ModifyWorker.IsBusy)
                m_ModifyWorker.RunWorkerAsync(environment);
        }
    }
}