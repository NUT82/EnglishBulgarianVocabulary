using DataBaseVocabulary.Data;
using DataBaseVocabulary.Menus;
using DataBaseVocabulary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

                if (menues.ContainsKey(input))
                {
                    PrintSubMenu(menues, menu, input);
                    continue;
                }

                switch (input)
                {
                    case "English Word":
                    case "Bulgarian Word":
                        switch (menu.Peek().Name)
                        {
                            case "Add New Words":
                                AddNewWordToDB(input);
                                break;
                            case "Remove Words":
                                RemoveWordsFromDb(input);
                                break;
                            case "Show Me All Words":
                                ShowMeAllWords(input);
                                break;
                        }
                        break;
                    case "New Game":
                        NewGame();
                        break;
                    case "Score":
                        ShowScore();
                        break;
                }

                PrintMenu(menu.Peek());
            }
        }

        private static void ShowScore()
        {
            VocabolaryDBContext context = new VocabolaryDBContext();
            var games = context.Games
                .Select(g => new
                {
                    GuesWords = g.GuesBulgarianWords + g.GuesEnglishWords,
                    g.UserName,
                    g.Points
                })
                .Take(10)
                .OrderByDescending(g => g.Points)
                .ThenByDescending(g => g.GuesWords);

            Console.Clear();
            int count = 1;
            foreach (var game in games)
            {
                Console.WriteLine($"{count++}. User {game.UserName} - guuess words {game.GuesWords} - TOTAL POINTS - {game.Points}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void NewGame()
        {
            
            string input = "Bulgarian";
            Random random = new Random();
            if (random.Next(1,100) < 50)
            {
                input = "English";
            }
            Dictionary<string, List<String>> result = GetAllWords(input);
            if (result.Count == 0)
            {
                Console.WriteLine($"There is no words in {input} dictionary, plese add some first! ");
                return;
            }
            string[] keys = result.Keys.ToArray();

            Console.Clear();
            Console.WriteLine("Please enter your name:");
            string userName = Console.ReadLine();
            int points = 0;
            int guessCount = 0;
            int accelerator = 1;
            do
            {
                Console.Clear();
                string randomWord = keys[random.Next(keys.Length)];
                string answer;
                Console.WriteLine($"Translate {input} Word {randomWord}");
                if (input == "English")
                {
                    answer = GetInputForBulgarianWord();
                }
                else
                {
                    answer = GetInputForEnglishWord();
                }

                if (result[randomWord].Contains(answer))
                {
                    guessCount++;
                    points += accelerator;
                    Console.WriteLine($"YOU WIN {accelerator} points! CONGRATULATIONS!!! :)");
                    accelerator *= 2;
                }
                else
                {
                    Console.WriteLine("YOU LOOSE!!! STUDY MORE!!! :(");
                    accelerator = 1;
                }

                Console.WriteLine($"You have {points} total points! Press any key to continue, or ESC to End Game!");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);

            if (points > 0)
            {
                AddNewScore(userName, guessCount, points, input);
            }
        }

        private static void AddNewScore(string userName, int guessCount, int points, string input)
        {
            int guessBulgarianWords = 0;
            int guessEnglishWords = 0;
            VocabolaryDBContext context = new VocabolaryDBContext();
            if (input == "Bulgarian")
            {
                guessBulgarianWords = guessCount;
            }
            else
            {
                guessEnglishWords = guessCount;
            }

            context.Games.Add(new Game
            {
                GuesBulgarianWords = guessBulgarianWords,
                GuesEnglishWords = guessEnglishWords,
                Points = points,
                UserName = userName,
            });
            context.SaveChanges();
        }

        private static void ShowMeAllWords(string input)
        {
            Dictionary<string, List<String>> result = GetAllWords(input);

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Key} - {string.Join(", ", item.Value)}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static Dictionary<string, List<string>> GetAllWords(string input)
        {
            VocabolaryDBContext context = new VocabolaryDBContext();
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

            var words = context.BulgarianWordsEnglishWords
                    .Select(w => new
                    {
                        EnglishWord = w.EnglishWord.Word,
                        BulgarianWord = w.BulgarianWord.Word
                    }).ToList();

            if (input.Contains("English"))
            {
                foreach (var word in words)
                {
                    if (!result.ContainsKey(word.EnglishWord))
                    {
                        result.Add(word.EnglishWord, new List<string>());
                    }

                    result[word.EnglishWord].Add(word.BulgarianWord);
                }
            }
            else
            {
                foreach (var word in words)
                {
                    if (!result.ContainsKey(word.BulgarianWord))
                    {
                        result.Add(word.BulgarianWord, new List<string>());
                    }

                    result[word.BulgarianWord].Add(word.EnglishWord);
                }
            }

            return result;
        }

        private static void RemoveWordsFromDb(string input)
        {
            VocabolaryDBContext context = new VocabolaryDBContext();

            if (input.Contains("English"))
            {
                string inputEnglishWord = GetInputForEnglishWord();
                var englishWordToDelete = context.EnglishWords.FirstOrDefault(w => w.Word == inputEnglishWord);
                if (englishWordToDelete is null)
                {
                    Console.WriteLine($"Word {inputEnglishWord} dosn't exist!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                context.Remove(englishWordToDelete);
            }
            else
            {
                string inputbBulgarianWord = GetInputForBulgarianWord();
                var bulgarianWordToDelete = context.BulgarianWords.FirstOrDefault(w => w.Word == inputbBulgarianWord);
                if (bulgarianWordToDelete is null)
                {
                    Console.WriteLine($"Word {inputbBulgarianWord} dosn't exist!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                context.Remove(bulgarianWordToDelete);
            }
            context.SaveChanges();
        }

        private static void AddNewWordToDB(string input)
        {
            string inputEnglishWord;
            string inputbBulgarianWord;
            VocabolaryDBContext context = new VocabolaryDBContext();

            if (input.Contains("English"))
            {
                inputEnglishWord = GetInputForEnglishWord();
                inputbBulgarianWord = GetInputForBulgarianWord();
            }
            else
            {
                inputbBulgarianWord = GetInputForBulgarianWord();
                inputEnglishWord = GetInputForEnglishWord();
            }

            int? idEnglishWord = null;
            int? idBulgarianWord = null;
            try
            {
                idEnglishWord = context.EnglishWords.FirstOrDefault(w => w.Word == inputEnglishWord).EnglishWordId;
                idBulgarianWord = context.BulgarianWords.FirstOrDefault(w => w.Word == inputbBulgarianWord).BulgarianWordId;
            }
            catch (NullReferenceException ex)
            {
                //one or both words dosnt contain i DB
            }

            if (context.BulgarianWordsEnglishWords.Find(idEnglishWord, idBulgarianWord) != null)
            {
                Console.WriteLine("Words already exists!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            EnglishWord englishWord;
            BulgarianWord bulgarianWord;
            if (idEnglishWord is null)
            {
                englishWord = new EnglishWord { Word = inputEnglishWord, Points = 1 };
            }
            else
            {
                englishWord = context.EnglishWords.Find(idEnglishWord);
            }

            if (idBulgarianWord is null)
            {
                bulgarianWord = new BulgarianWord { Word = inputbBulgarianWord, Points = 1 };
            }
            else
            {
                bulgarianWord = context.BulgarianWords.Find(idBulgarianWord);
            }

            context.Add(new BulgarianWordEnglishWord
            {
                EnglishWord = englishWord,
                BulgarianWord = bulgarianWord,
            });

            context.SaveChanges();
            //var englishWords = context.BulgarianWords.Where(w => w.Word == bulgarianWord).Select(w => w.EnglishWords).ToHashSet();
            //var bulgarianWords = context.EnglishWords.Where(w => w.Word == englishWord).Select(w => w.BulgarianWords).ToHashSet();
        }

        private static string GetInputForBulgarianWord()
        {
            string word;
            while (true)
            {
                Console.WriteLine("Enter word in Bulgarian:");
                word = Console.ReadLine();

                if (!Regex.IsMatch(word, @"[A-Za-z]"))
                {
                    break;
                }

                Console.WriteLine("Word must conatain only Bulgarian letters!");
            }
            return word.Trim().ToLower();
        }

        private static string GetInputForEnglishWord()
        {
            string word;
            while (true)
            {
                Console.WriteLine("Enter English word:");
                word = Console.ReadLine();
                if (!Regex.IsMatch(word, @"[А-Яа-я]"))
                {
                    break;
                }

                Console.WriteLine("Word must conatain only English letters!");
            }
            return word.Trim().ToLower();
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
