using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseVocabulary.Menus
{
    abstract class Menu
    {
        private readonly List<string> subMenuesNames;

        public Menu(string name, params string[] subMenuNames)
        {
            Name = name;
            subMenuesNames = new List<string>(subMenuNames);
        }

        public string Name { get; private set; }

        public int SubMenuesCount => SubMenuesNames.Count;

        public IReadOnlyCollection<string> SubMenuesNames => subMenuesNames.AsReadOnly();

        public override string ToString()
        {
            int counter = 1;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Name.ToUpper()}");
            foreach (var item in SubMenuesNames)
            {
                sb.AppendLine($"{counter++}. {item}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
