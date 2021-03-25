using DataBaseVocabulary.Data;
using DataBaseVocabulary.Menus;
using DataBaseVocabulary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBaseVocabulary
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Config.Database.CreateDB();
            var menues = Config.CreateMenues.Create();

            Stack<Menu> menu = new Stack<Menu>();
            menu.Push(menues["Main Menu"]);

            PrintMenu(menu.Peek());

            string input;
            while ((input = ReadMenuInput(menu.Peek())) != "Exit")
            {
                if (input == null)
                {
                    Console.WriteLine("Invalid choise!!!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    PrintMenu(menu.Peek());
                    continue;
                }

                if (input == "Back")
                {
                    menu.Pop();
                    PrintMenu(menu.Peek());
                    continue;
                }

                if (menu.Count == 1)
                {
                    PrintSubMenu(menues, menu, input);
                    continue;
                }

                switch (input)
                {
                    case "English Word":
                    case "Bulgarian Word":
                        AddNewWordToDB(input);
                        break;
                }
            }
            
        }

        private static void AddNewWordToDB(string input)
        {

            VocabolaryDBContext context = new VocabolaryDBContext();
            Console.WriteLine("Enter English word:");
            string word = Console.ReadLine();
            Console.WriteLine("Enter word in Bulgarian:");
            string translateWord = Console.ReadLine();

            context.Add(new BulgarianWordEnglishWord
            {
                EnglishWord = new EnglishWord { Word = word, Points = 1 } ,
                BulgarianWord = new BulgarianWord { Word = translateWord, Points = 1},
            });

            context.SaveChanges();
        }

        private static void PrintSubMenu(Dictionary<string, Menu> menues, Stack<Menu> menu, string input)
        {
            menu.Push(menues[input]);
            PrintMenu(menu.Peek());
        }

        private static string ReadMenuInput(Menu menu)
        {
            Console.Write("Your choise: ");

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (index >= 1 && index <= menu.SubMenuesCount)
                {
                    return menu.SubMenuesNames.ElementAt(index - 1);
                }
            }

            return null;
        }

        private static void PrintMenu(Menu menu)
        {
            Console.Clear();
            Console.WriteLine(menu);
        }
    }
}
