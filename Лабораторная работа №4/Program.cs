using System;
using static System.Console;
using static System.Convert;

    namespace SimpleAlgorithmsApp
    {

    public class SLNode
    {
        Node tail;
        int count = 0;
        public class Node
        {
            public Node(int data)
            {
                Data = data;
            }
            public int Data { get; set; }
            public Node Next { get; set; }
        }
        public SLNode(int data)
        {
            tail = new Node(data);
            tail.Next = tail;

        }

        //head = tail.Next;

        public void AddFirst(int data)
        {
            Node node = new Node(data);
            node.Next = tail.Next;
            tail.Next = node;

            count++;
        }

        public void DeleteFirst()
        {
            tail.Next = tail.Next.Next;
            count--;
        }

        public void AddLast(int data)
        {
            Node node = new Node(data);
            if (tail.Next == null)
            {
                tail.Next = node;
                tail = node;
            }
            else
            {
                node.Next = tail.Next;
                tail.Next = node;
                tail = node;
            }
            count++;
        }
        public void DeleteLast()
        {
            Node node = tail.Next;

            while (node.Next != tail)
                node = node.Next;

            node.Next = tail.Next;
            tail = node;
            count--;
        }
        public void Print()
        {
            Node current = tail.Next;
            while(current!=tail)
            {
                Write(current.Data + " ");
                current = current.Next;
            }
            WriteLine(tail.Data);
        }

        public int Max()
        {
            Node current = tail.Next;
            int max = current.Data;
            int result = 0;
            int n = 0;

            while (current != tail)
            {
                if (max < current.Data)
                {
                    max = current.Data;
                    result = n; 
                }
                n++;
                current = current.Next;
            }
            if (max < tail.Data)
                return n;

            return result;
        }

        public void AddAtPosition(int data, int pos)
        {
            pos = pos % count;
            int n = 0;
            Node current = tail.Next;

            while (n != pos)
            { 
                current = current.Next;
                n++;
            }

            Node newNode = new Node(data);
            newNode.Next = current.Next;
            current.Next = newNode;

            count++;
        }

        public void DeleteAtPosition(int pos)
        {
            pos = pos % count;
            if (pos == count-1)
                this.DeleteLast();
            else
            {
                int n = 0;
                Node current = tail.Next;

                while (n != pos - 1)
                {
                    current = current.Next;
                    n++;
                }

                current.Next = current.Next.Next;

                count--;
            }
        }
    }
    class Program
    {
        static void Main()
        {
            Random rnd = new Random();

            SLNode List = new SLNode(rnd.Next(0, 101));

            WriteLine("Додавання новиого вузла після вузла з максимальним значенням:");

            int n = rnd.Next(1, 11);
            for (int i = 0; i < n; i++)
            {
                List.AddLast(rnd.Next(0, 101));
            }
            List.Print();
            List.AddAtPosition(rnd.Next(0, 101),List.Max());
            List.Print();

            WriteLine("Введіть число для:\n1.Додавання нового вузла у голову списку." +
                "\n2.Додавання нового вузла у хвіст списку." +
                "\n3.Додавання нового вузла на визначену позицію." +
                "\n4.Видалення голови списку." +
                "\n5.Видалення хвоста списку." +
                "\n6.Видалення вузла з визначеної позиції." +
                "\n7.Виведення вмісту списку.");

            Write("\nВведіть число:");
            int chislo = ToInt32(ReadLine());
            if (chislo > 7)
                Write("Помилка. Введіть число повторно:");
            
            if (chislo == 1)
                List.AddFirst(rnd.Next(0, 101));
            else if (chislo == 2)
                List.AddLast(rnd.Next(0, 101));
            else if (chislo == 3)
            {
                Write("Введіть номер позиції вузла:");
                int m = ToInt32(ReadLine());
                List.AddAtPosition(rnd.Next(0, 101), m - 1);
            }
            else if (chislo == 4)
                List.DeleteFirst();
            else if (chislo == 5)
                List.DeleteLast();
            else if (chislo == 6)
            {
                Write("Введіть номер позиції вузла:");
                int m = ToInt32(ReadLine());
                List.DeleteAtPosition(m - 1);
            }
            List.Print();

            
        }
    }
}