using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseVocabulary.Menus
{
    class MainMenu : Menu
    {
        public MainMenu(params string[] subMenuNames)
            : base("Main Menu", subMenuNames)
        {
        }
    }
}
