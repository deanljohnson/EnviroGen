﻿using System.Collections;
using System.Collections.Generic;

namespace EnviroGen.Nodes
{
    public class NodeGraph : ICollection<INode>
    {
        private List<INode> m_Nodes { get; } = new List<INode>();

        public RootNode RootNode { get; set; }

        public NodeGraph()
        {

        }

        public bool HasRootToTerminalPath()
        {
            INode currentNode = RootNode;

            var foundTerminalOnGraph = false;

            while (true)
            {
                if (currentNode == null) break;

                if (currentNode.Output is TerminalNode)
                {
                    foundTerminalOnGraph = true;
                    break;
                }

                currentNode = currentNode.Output;
            }

            return foundTerminalOnGraph;
        }

        public IEnumerator<INode> GetEnumerator()
        {
            return m_Nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) m_Nodes).GetEnumerator();
        }

        public void Add(INode item)
        {
            m_Nodes.Add(item);
        }

        public void Clear()
        {
            m_Nodes.Clear();
            RootNode = null;
        }

        public bool Contains(INode item)
        {
            return m_Nodes.Contains(item);
        }

        public void CopyTo(INode[] array, int arrayIndex)
        {
            m_Nodes.CopyTo(array, arrayIndex);
        }

        public bool Remove(INode item)
        {
            return m_Nodes.Remove(item);
        }

        public int Count => m_Nodes.Count;

        public bool IsReadOnly => false;
    }
}