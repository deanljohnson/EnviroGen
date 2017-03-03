using System;

namespace EnviroGenDisplay
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EditorNodeAttribute : Attribute
    {
        private string m_Category;

        public Type ViewType { get; }
        public string QualifiedName { get; private set; }
        public string Name { get; }
        public string Category
        {
            get { return m_Category; }
            set
            {
                m_Category = value;

                if (m_Category == string.Empty)
                {
                    QualifiedName = Name;
                }
                else
                {
                    QualifiedName = m_Category + "." + Name;
                }
            }
        }

        public EditorNodeAttribute(string name, Type viewType)
        {
            Name = name;
            QualifiedName = name;
            ViewType = viewType;
        }
    }
}
