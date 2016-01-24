﻿using System;
using System.ComponentModel;
using System.Windows.Input;
using EnviroGen.Nodes;

namespace EnviroGenDisplay.ViewModels
{
    class TerrainGeneratorNodeViewModel : NodeViewModel<TerrainGeneratorNode>
    {
        private BackgroundWorker m_ModifyWorker { get; }

        public int SizeX
        {
            get { return Node.SizeX; }
            set
            {
                if (Node.SizeX != value)
                {
                    Node.SizeX = value;
                    OnPropertyChanged();
                }
            }
        }

        public int SizeY
        {
            get { return Node.SizeY; }
            set
            {
                if (Node.SizeY != value)
                {
                    Node.SizeY = value;
                    OnPropertyChanged();
                }
            }
        }

        public int OctaveCount
        {
            get { return Node.OctaveCount; }
            set
            {
                if (Node.OctaveCount != value)
                {
                    Node.OctaveCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Seed
        {
            get { return Node.Seed; }
            set
            {
                if (Node.Seed != value)
                {
                    Node.Seed = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Gain
        {
            get { return Node.Gain; }
            set
            {
                if (Math.Abs(Node.Gain - value) > float.Epsilon)
                {
                    Node.Gain = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Frequency
        {
            get { return Node.Frequency; }
            set
            {
                if (Math.Abs(Node.Frequency - value) > float.Epsilon)
                {
                    Node.Frequency = value;
                    OnPropertyChanged();
                }
            }
        }

        public IEnvironment Map { get; set; }

        public ICommand GenerateCommand { get; set; }

        public TerrainGeneratorNodeViewModel(IEnvironment map)
        {
            Map = map;
            Node = new TerrainGeneratorNode();
            GenerateCommand = new RelayCommand(Generate);
            m_ModifyWorker = new BackgroundWorker();
            m_ModifyWorker.DoWork += Modify;
            m_ModifyWorker.RunWorkerCompleted += ModifyComplete;
        }

        private void Generate(object m = null)
        {
            if (Seed == -1)
                Seed = new Random().Next(10000);

            if (!m_ModifyWorker.IsBusy)
                m_ModifyWorker.RunWorkerAsync();
        }

        private void Modify(object sender, DoWorkEventArgs e)
        {
            lock (Map.Environment)
            {
                Node.Modify(Map.Environment);
            }
        }

        private void ModifyComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            Map.Update();
        }
    }
}
