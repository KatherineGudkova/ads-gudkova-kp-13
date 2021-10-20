using System;
using static System.Console;
class Program
{
    static void Main()
    {
        double x, y, z;
        double ch, zn;
        double a, b;

        Write("x = ");
        x = Convert.ToDouble(ReadLine());

        Write("y = ");
        y = Convert.ToDouble(ReadLine());

        Write("z = ");
        z = Convert.ToDouble(ReadLine());

        ch = y - Math.Sqrt(Math.Abs(Math.Pow(x, 3)));
        zn = Math.Sqrt(Math.Pow(x, 3) + 5 * Math.Pow(y, -z) + Math.Pow(z, 2));

        if (zn == 0)
            WriteLine("Error.");
        else
        {
            a = ch / zn;
            WriteLine("a =" + a);

            b = Math.Sin(Math.Pow(a, -x)) + y;
            WriteLine("b =" + b);
        }
        ReadLine();
    }
}