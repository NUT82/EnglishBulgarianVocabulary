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
            Menu mainMenu = new MainMenu("New Game", "Score", "Add New Words", "Remove Words", "Show Me All Words", "Exit");
            menus.Add(mainMenu.Name, mainMenu);

            Menu addMenu = new AddMenu("English Word", "Bulgarian Word", "Back");
            menus.Add(addMenu.Name, addMenu);

            Menu removeMenu = new RemoveMenu("English Word", "Bulgarian Word", "Back");
            menus.Add(removeMenu.Name, removeMenu);

            Menu showMeAllWordsMenu = new ShowMeAllWordsMenu("English Word", "Bulgarian Word", "Back");
            menus.Add(showMeAllWordsMenu.Name, showMeAllWordsMenu);

            return menus;
        }
    }
}
