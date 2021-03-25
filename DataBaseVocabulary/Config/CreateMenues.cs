using DataBaseVocabulary.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseVocabulary.Config
{
    static class CreateMenues
    {
        public static Dictionary<string, Menu> Create()
        {
            Dictionary<string, Menu> menus = new Dictionary<string, Menu>();
            Menu mainMenu = new MainMenu("New Game", "Score", "Add New Words", "Remove Words", "Update Words", "Exit");
            menus.Add(mainMenu.Name, mainMenu);

            Menu addMenu = new AddMenu("English Word", "Bulgarian Word", "Back");
            menus.Add(addMenu.Name, addMenu);

            return menus;
        }
    }
}
