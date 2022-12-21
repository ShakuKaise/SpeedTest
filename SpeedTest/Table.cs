using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpeedTest
{
    internal class Table
    {
        static public void GetUsers() 
        {
            List<User> users;
            string table = File.ReadAllText("C:\\mpt\\С#\\SpeedTest\\Table.json");
            users = JsonConvert.DeserializeObject<List<User>>(table);

            Console.WriteLine("Таблица рекордов");
            Console.WriteLine("Имя пользователя Кол-во/минута Кол-во/секунда");
            foreach (User user in users)
            {
                Console.WriteLine($"{user.name} {user.SymbolsPerMinute} {user.SymbolsPerSecond}");
            }
            Console.WriteLine("");
            Console.WriteLine("Чтобы вернуться в главное меню нажмите любую клавишу");
            Console.ReadKey();
        }
    }
}
