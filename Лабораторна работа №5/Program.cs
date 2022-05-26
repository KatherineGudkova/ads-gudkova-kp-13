using System;
using static System.Console;

namespace ЛР5_АСД
{
    class Program
    {
       
        static void Sorting(int[] s, int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int k = 0; k < array.Length - 1; k++)
                {
                    if (s[k] % 10 > s[k + 1] % 10)
                    {
                        int z = s[k];
                        s[k] = s[k + 1];
                        s[k + 1] = z;
                        int x = array[k];
                        array[k] = array[k + 1];
                        array[k + 1] = x;
                    }
                }
            }
        }
        static int trans(int x)
        {
            string res = "";
            int ost;
            while (x >= 5)
            {
                ost = x % 5;
                res = Convert.ToString(ost) + res;
                x = x / 5;
            }
            res = Convert.ToString(x) + res;
            x = Convert.ToInt32(res);
            return x;
        }

        static void Main(string[] args)
        {
            Random rnd = new Random();

            int N = rnd.Next(5, 21);
            int[] arr = new int[N];
            int[] a = new int[N];
            int[] b = new int[N];
            int[] c = new int[N];
            //string res = "";
            //int ost;
            int  n1 = N / 2, n2 = N * N;

            //WriteLine("Перекладемо числа із системи числення з основою 10,в систему з основою 5.");
            WriteLine("Масив чисел з основою 10:");

            for (int i = 0; i < N; i++)
            {
                arr[i] = rnd.Next(0, 51);
                Write(+arr[i] + "  ");
                a[i] = arr[i];

                a[i]=trans(a[i]);
                b[i] = a[i];
                c[i] = a[i];
            }
            WriteLine();


            n1=trans(n1);
            //WriteLine("n1=" + n1);

            n2=trans(n2);
            //WriteLine("n2=" + n2);

            WriteLine("Масив чисел з основою 5:");

            for (int i = 0; i < N; i++)
            {
                Write(+a[i] + "  ");
            }
            WriteLine();

            int max = 0;

            for (int i = 0; i < N; i++)
            {
                if (max < a[i])
                    max = a[i];
            }
            int lenght = (int)Math.Log10(max) + 1;
            //Writeline(lenght);

            WriteLine("Сортування:");

            for (int l = lenght; l > 0; l--)
            {
                for (int i = 0; i < N; i++)
                        Sorting(b,a);

                for (int i = 0; i < N; i++)
                    Write(a[i]+"  ");

                WriteLine();

                for (int i = 0; i < b.Length; i++)
                    b[i] = b[i] / 10;
            }

            //for (int i = 0; i < N; i++)
            //    Write(a[i] + "  ");
           
            WriteLine();
            WriteLine("Тоді відсортований масив:");

            for (int i = 0; i < N; i++)
            {
                if ((a[i] >= n1) && (a[i] <= n2)) 
                    Write(a[i] + "  ");
            }
            for (int i = N-1; i >= 0; i--)
            {
                if (!((c[i] >= n1) && (c[i] <= n2))) 
                {
                    Write(c[i] + "  ");
                }
            }
        }
    }
}