using System;
using static System.Console;

namespace Лабораторна6
{
    class Program
    {
        public static void Main(String[] args)
        {
            Deque q = new Deque();
            string command = "Enter", example = "asdffdsa";
            string Input;
            bool result = false;

            while (command != "End")
            {
                WriteLine("\nГоловне меню");
                WriteLine("Введiть одне з наступних ключових слiв:");
                WriteLine("Example - вивести контрольний приклад");
                WriteLine("Enter - виконати перетворення рядка");
                WriteLine("End - завершити виконання програми");
                command = ReadLine();

                if (command == "Enter")
                {
                    do
                    {
                        q = new Deque();
                        WriteLine("\nВведiть рядок: ");
                        Input = ReadLine();

                        while (Input.Length % 2 != 0 || Input.Length == 0)
                        {
                            WriteLine("\nЧисло символiв має бути кратне двом. Введiть iнший рядок: ");
                            Input = ReadLine();
                        }

                        StringToDeque(q, Input);
                        result = q.isPalindrome();

                        if (!result)
                        {
                            WriteLine("\nДаний рядок не палiндром. \nВведiть iнший рядок: ");
                        }
                    }
                    while (!result);
                    printResult(q);
                }

                else if (command == "Example")
                {
                    WriteLine("Рядок-приклад: " + example);

                    StringToDeque(q, example);

                    if (q.isPalindrome())
                        printResult(q);
                    else
                    {
                        WriteLine("\nДаний рядок - не палiндром");
                    }
                }

                else if (command == "End")
                    break;
                else
                {
                    WriteLine("\nПомилка при введеннi даних");
                }
            }
        }
        static public void printResult(Deque q)
        {
            WriteLine("\nДаний рядок - палiндром.");
            q.printHalfDeque();
            q.printTransposition();
            WriteLine();
        }
        static public void StringToDeque(Deque q, string input)
        {
            for (int i = 0; i < input.Length; i++)
                q.insertTail(input[i]);
            q.printDeque();
        }
    }
}