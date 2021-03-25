using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseVocabulary.Menus
{
    class AddMenu : Menu
    {
        public AddMenu(params string[] subMenuNames)
            : base("Add New Words", subMenuNames)
        {
        }
    }
}
