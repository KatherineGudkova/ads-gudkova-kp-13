using System;
using System.Drawing;
using static System.Console;


class Program
{
    static void Main() 
    {
        Write("n = ");
        int n = Convert.ToInt32(ReadLine());

        int[]arr = new int[n+1];

        Random rnd = new Random();

        for (int i = 0; i < n; i++)
        {
            arr[i] = rnd.Next(0, 1100);
        }
       
        for (int i = 0; i < n; i++)
        {
            Write(arr[i] + "; ");
        }

        Write("\nЗа незростанням за кількістю цифр у числі:\n");

        for (int j = 0; j < n; j++)
        {
            for (int i = 0; i < n-1-j; i++)
            {
                int temp;
                if (arr[i].ToString().Length < arr[i + 1].ToString().Length)
                {
                    temp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = temp;
                }
            }
        }

        BackgroundColor = ConsoleColor.Yellow;
        for (int i = 0; i < n; i++)
        {
            Write(arr[i] + "; ");
            if (arr[i].ToString().Length > arr[i + 1].ToString().Length)
            {
                if (BackgroundColor == ConsoleColor.Yellow)
                    BackgroundColor = ConsoleColor.Green;
                else
                    BackgroundColor = ConsoleColor.Yellow;
            }
        }
        ResetColor();

        Write("\nЗа неспаданням за значенням числа серед чисел, що мають однакову кількість цифр:\n");
        
            for (int j = 0; j < n - 1; j++)
            {
                if (arr[j].ToString().Length >= arr[j + 1].ToString().Length)
                {
                    for (int i = 0; i < n - j - 1; i++)
                    {
                        int temp;
                        if (arr[i].ToString().Length == arr[i + 1].ToString().Length)
                        {
                            if (arr[i] > arr[i + 1])
                            {
                                temp = arr[i];
                                arr[i] = arr[i + 1];
                                arr[i + 1] = temp;
                            }
                        }

                    }
                }
            }
       

        BackgroundColor = ConsoleColor.Yellow;
        for (int i = 0; i < n; i++)
        {
            Write(arr[i] + "; ");
            if (arr[i].ToString().Length > arr[i + 1].ToString().Length)
            {
                if (BackgroundColor == ConsoleColor.Yellow)
                    BackgroundColor = ConsoleColor.Green;
                else
                    BackgroundColor = ConsoleColor.Yellow;
            }
        }
        ResetColor();
    }
}
