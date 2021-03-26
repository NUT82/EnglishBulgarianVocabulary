using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseVocabulary.Menus
{
    class ShowMeAllWordsMenu : Menu
    {
        public ShowMeAllWordsMenu(params string[] subMenuNames)
            : base("Show Me All Words", subMenuNames)
        {
        }
    }
}
