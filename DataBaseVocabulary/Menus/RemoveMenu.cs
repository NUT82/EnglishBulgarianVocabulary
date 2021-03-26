using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseVocabulary.Menus
{
    class RemoveMenu : Menu
    {
        public RemoveMenu(params string[] subMenuNames)
            : base("Remove Words", subMenuNames)
        {
        }
    }
}
