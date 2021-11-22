using System;
using static System.Console;

namespace Гудкова_КП_13_ЛР2_В2
{
    class Program
    {
        static void Main()
        {
            int N, M;

            Write("Введіть N кратне 2: ");
            N = Convert.ToInt32(ReadLine());

            if (N % 2 != 0)
            {
                Write("Помилка під час введення даних!Введіть нове значення N = ");
                N = Convert.ToInt32(ReadLine());
            }

            Write("Введіть M: ");
            M = Convert.ToInt32(ReadLine());

            int[,] arr = new int[N, M];

            Random rnd = new Random();

            for (int i = 0; i < N; i++)
            {
                arr[i, 0] = rnd.Next(1, 101);
                Write(arr[i, 0].ToString() + "\t");

                for (int j = 1; j < M; j++)
                {
                    arr[i, j] = rnd.Next(1, 101);
                    Write(arr[i, j].ToString() + "\t");
                }
                WriteLine();
            }
            int x, y, a;
            int max = arr[N - 1,0], min = arr[N - 1, 0];
            int d = N / 2 + M - 1; // Кількість діагоналей у нижній половині масиву

            Write("\nПослідовність обходу: ");

            for (int D = d; D > d / 2; D--) 
            {
                if (D % 2 == 0) 
                {
                    x = N - 1;
                    y = d - D;
                    while (y >= 0)
                    {
                        a = arr[x, y];
                        Write("{0} [{1},{2}];  ", a, x, y);
                        x--;
                        y--;
                    }
                }
                else if (D % 2 != 0)
                {
                    x = D - 1;
                    y = 0;

                    while (x <= N - 1)
                    {
                        a = arr[x, y];
                        Write("{0} [{1},{2}];  ", a, x, y);
                        x++;
                        y++;
                    }
                }
            }

            for (int D = d/2; D > 0; D--)
            {
                if (D % 2 == 0)
                {
                    x = D + N/2 - 1;
                    y = M - 1;

                    while (x >= N / 2)
                    {
                        a = arr[x, y];
                        Write("{0} [{1},{2}];  ", a, x, y);
                        x--;
                        y--;
                    }
                }
                else if (D % 2 != 0)
                {
                    x = N/2;
                    y = M - D;

                    while (y <= M - 1)
                    {
                        a = arr[x, y];
                        Write("{0} [{1},{2}];  ", a, x, y);
                        x++;
                        y++;
                    }
                }
            }

            for (y = M - 1; y>=0; y--)
            {
                if (M % 2 == 0) 
                {
                    if (y % 2 != 0)
                    {
                        x = N / 2 - 1;
                        while (x >= 0) 
                        {
                            a = arr[x, y];
                            Write("{0} [{1},{2}];  ", a, x, y);
                            x--;
                        }
                    }
                    else if (y % 2 == 0)
                    {
                        x = 0;
                        while (x <= N / 2 - 1)
                        {
                            a = arr[x, y];
                            Write("{0} [{1},{2}];  ", a, x, y);
                            x++;
                        }
                    }
                }
                else if (M % 2 != 0)
                {
                    if (y % 2 == 0)
                    {
                        x = N / 2 - 1;
                        while (x >= 0)
                        {
                            a = arr[x, y];
                            Write("{0} [{1},{2}];  ", a, x, y);
                            x--;
                        }
                    }
                    else if (y % 2 != 0)
                    {
                        x = 0;
                        while (x <= N / 2 - 1)
                        {
                            a = arr[x, y];
                            Write("{0} [{1},{2}];  ", a, x, y);
                            x++;
                        }
                    }
                }
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (arr[i, j] > max)
                        max = arr[i, j];
                    if (arr[i, j] < min)
                        min = arr[i, j];
                }
            }
            WriteLine("\n\nМаксимальний елемент: " + max);
            WriteLine("\nМінімальний елемент: " + min);
        }
    }
}