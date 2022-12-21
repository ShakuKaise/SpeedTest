using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest
{
    internal class Menu
    {
        static public void MainMenu()
        {
            int MaxVerPos = 2;
            int MinVerPos = 1;
            int VerPos = 1;
            ConsoleKeyInfo key;

            do
            {
                WriteMenu();
                WriteCursor(VerPos);
                key = Console.ReadKey(true);

                VerPos = UpdateCursorPos(VerPos, MaxVerPos, MinVerPos, key.Key);
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        if (VerPos == 1)
                        {
                            Console.Clear();
                            new Test().Start();
                        }
                        else if (VerPos == 2)
                        {
                            Console.Clear();
                            Table.GetUsers();
                        }
                        break;

                }
                Console.Clear();
            } while (key.Key != ConsoleKey.F9);
        }
        static private int UpdateCursorPos(int VerPos, int MaxVerPos, int MinVerPos, ConsoleKey key) //Изменение позиции курсора
        {
            switch (key)
            {
                case (ConsoleKey.W):
                    VerPos--;
                    if (VerPos < MinVerPos)
                    {
                        VerPos = MinVerPos;
                    }
                    break;
                case (ConsoleKey.S):
                    VerPos++;
                    if (VerPos > MaxVerPos)
                    {
                        VerPos = MaxVerPos;
                    };
                    break;
            }
            return VerPos;
        }
        static private void WriteCursor(int VerPos) //создание курсора в позиции
        {
            Console.SetCursorPosition(0, VerPos);
            Console.WriteLine("-->");
        }
        static private void WriteMenu()
        {
            Console.WriteLine();
            Console.WriteLine("       Начать тест на скоропечатание");
            Console.WriteLine("       Посмотреть таблицу рекордов");
            Console.WriteLine();
            Console.WriteLine("Чтобы выйти из программы необходимо нажать F9");
        }
    }
}
