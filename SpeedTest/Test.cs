using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest
{
    internal class Test
    {
        bool timer;
        private int Letters = 0;
        string username;
        ConsoleKey key;
        public void Start()
        {
            do
            {
                Console.WriteLine("Введите ваше имя для таблицы рекордов");
                username = Console.ReadLine();
                Console.Clear();
            } while (username == "");

            char[] text = GetText();
            foreach (char letter in text)
            {
                Console.Write(letter);
            }
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Нажмите Enter, чтобы начать тест");

            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(true).Key;
            }

            int HorPos = 0;
            int VerPos = 0;
            int LetterNum = 0;

            new Thread(Timer).Start();
            Console.SetCursorPosition(0, 0);
            while (timer != true)
            {
                Console.CursorVisible = true;
                char InputKey = Console.ReadKey(true).KeyChar;
                if (InputKey == text[LetterNum])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(text[LetterNum]);
                    Console.ResetColor();
                    LetterNum++;
                    Letters++;
                    
                    try
                    {
                        Console.SetCursorPosition(HorPos, VerPos);
                        HorPos++;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        HorPos = 1;
                        VerPos++;
                        Console.SetCursorPosition(HorPos, VerPos);
                    }
                }
            }
            Console.CursorVisible = false;
            Record(username, Letters, Letters/60);

            Console.Clear();
            Console.WriteLine("Ваши результаты");
            Console.WriteLine(username);
            Console.WriteLine(Letters);
            Console.WriteLine(Letters / 60);

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишк, чтобы вернуться в меню");
            Console.ReadKey();
        }
        private void Timer()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (60 - stopwatch.ElapsedMilliseconds / 1000 >= 0)
            {
                Console.CursorVisible = false;

                Console.SetCursorPosition(0, 10);
                Console.WriteLine("Осталось: " + (60 - (stopwatch.ElapsedMilliseconds) / 1000));
                Thread.Sleep(1000);
            }

            stopwatch.Stop();
            stopwatch.Reset();

            timer = true;
        }
        private char[] GetText()
        {
            string text = "Сильно дует норд-вест нагоняя кучевые облака залепляя лазурь неба грязной ватой. По мутно-серому морю перекатываются волны оперённые пеною похожие на бесчисленные стада белых кроликов. Шумит прибой далеко выбрасываясь на песчаный берег взмыливая воду точно сплетая фантастические кружева. Издали выплывая направляются к гавани рыбачьи лодки спеша засветло укрыться от поднимающейся непогоды. Крейсер нёсся по светлой шёлковой равнине моря легко и плавно оставляя за собою длинное серое облако дыма. Казалось его зовёт манит светло-голубая даль а он бурля воду во всю мочь стремится туда в сияющую даль. Мачты вытянувшись точно часовые матросы у флагов резали синеву неба.";
            char[] chars = text.ToCharArray();

            return chars;
        }
        public List<User> GetUsers()
        {
            List<User> users;
            string table = File.ReadAllText("C:\\mpt\\С#\\SpeedTest\\Table.json");
            return JsonConvert.DeserializeObject<List<User>>(table);
        }
        public void Record(string name = "Имя", int SymbolsPerMinute = 0, int SymbolsPerSecond = 0)
        {

            User user = new User();
            user.name = name;
            user.SymbolsPerMinute = SymbolsPerMinute;
            user.SymbolsPerSecond = SymbolsPerSecond;

            List<User> users = GetUsers();
            if (users != null)
            {
                users.Add(user);
                string json = JsonConvert.SerializeObject(users);
                File.WriteAllText("C:\\mpt\\С#\\SpeedTest\\Table.json", json);
            }
            else
            {
                string json = JsonConvert.SerializeObject(user);
                File.WriteAllText("C:\\mpt\\С#\\SpeedTest\\Table.json", json);
            }
        }
    }
}
