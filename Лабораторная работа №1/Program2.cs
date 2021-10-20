using System;
using static System.Console;
class Program
{
    static void Main()
    {
        int d, m, y;
        int res = 31;

        Write("Day: ");
        d = Convert.ToInt32(ReadLine());

        Write("Month: ");
        m = Convert.ToInt32(ReadLine());

        Write("Year: ");
        y = Convert.ToInt32(ReadLine());

        if ((m == 1 || m == 3 || m == 5 || m == 7 || m == 8 || m == 10 || m == 12) && d <= 31)
            WriteLine("Old date: {0}.{1}.{2}", d, m, y);
        else if ((m == 4 || m == 6 || m == 9 || m == 11) && d <= 30)
            WriteLine("Old date: {0}.{1}.{2}", d, m, y);
        else if (m == 2 && d <= 28 && y % 4 != 0)
            WriteLine("Old date: {0}.{1}.{2}", d, m, y);
        else if (m == 2 && d == 29 && y % 4 == 0)
            WriteLine("Old date: {0}.{1}.{2}", d, m, y);
        else
            WriteLine("Incorrect data entered");

        if (y >= 1582 && y < 1700)
        {
            d += 10;
            if (d > 31 && (m == 3 || m == 5 || m == 7 || m == 8 || m == 10))
            {
                m += 1;
                d -= 31;
            }
            else if (d > 30 && (m == 4 || m == 6 || m == 9 || m == 11))
            {
                m += 1;
                d -= 30;
            }
            else if (d > 29 && m == 2 && y % 4 == 0)
            {
                m += 1;
                d -= 29;
            }
            else if (d > 28 && m == 2 && y % 4 != 0)
            {
                m += 1;
                d -= 28;
            }
            else if (d > 31 && m == 12)
            {
                d -= 31;
                m = 1;
                y += 1;
            }
        }

        else if (y >= 1700 && y < 1800)
        {
            d += 11;
            if (d > 31 && (m == 3 || m == 5 || m == 7 || m == 8 || m == 10))
            {
                m += 1;
                d -= 31;
            }
            else if (d > 30 && (m == 4 || m == 6 || m == 9 || m == 11))
            {
                m += 1;
                d -= 30;
            }
            else if (d < 29 && m == 2 && y % 4 == 0)
            {
                m += 1;
                d -= 29;
            }
            else if (d > 28 && m == 2 && y % 4 != 0)
            {
                m += 1;
                d -= 28;
            }
            else if (d > 31 && m == 12)
            {
                d -= 31;
                m = 1;
                y += 1;
            }
        }

        else if (y >= 1800 && y < 1900)
        {
            d += 12;
            if (d > 31 && (m == 3 || m == 5 || m == 7 || m == 8 || m == 10))
            {
                m += 1;
                d -= 31;
            }
            else if (d > 30 && (m == 4 || m == 6 || m == 9 || m == 11))
            {
                m += 1;
                d -= 30;
            }
            else if (d < 29 && m == 2 && y % 4 == 0)
            {
                m += 1;
                d -= 29;
            }
            else if (d > 28 && m == 2 && y % 4 != 0)
            {
                m += 1;
                d -= 28;
            }
            else if (d > 31 && m == 12)
            {
                d -= 31;
                m = 1;
                y += 1;
            }
        }

        else if (y >= 1900 && y < 2100)
        {
            d += 13;
            if (d > 31 && (m == 3 || m == 5 || m == 7 || m == 8 || m == 10))
            {
                m += 1;
                d -= 31;
            }
            else if (d > 30 && (m == 4 || m == 6 || m == 9 || m == 11))
            {
                m += 1;
                d -= 30;
            }
            else if (d < 29 && m == 2 && y % 4 == 0)
            {
                m += 1;
                d -= 29;
            }
            else if (d > 28 && m == 2 && y % 4 != 0)
            {
                m += 1;
                d -= 28;
            }
            else if (d > 31 && m == 12)
            {
                d -= 31;
                m = 1;
                y += 1;
            }
        }
        WriteLine("New date: {0}.{1}.{2}", d, m, y);

        for (int i = 1; i < m-1; i++) 
        {
            if (i == 1 || i == 3 || i == 5 || i == 7 || i == 8 || i == 10 || i == 12)
                res += 31;
            else if (i == 4 || i == 6 || i == 9 || i == 11)
                res += 30;
            else if (i == 2 && y % 4 == 0)
                res += 29;
            else if (i == 2 && y % 4 != 0)
                res += 28;
        }
        res += d;
        WriteLine("Days from the beginning of the year:" + res);
        ReadLine();
    }
}