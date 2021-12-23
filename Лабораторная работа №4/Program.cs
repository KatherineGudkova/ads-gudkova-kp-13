using System;
using System.Collections;
using System.Collections.Generic;

namespace ЛР4_АСД
{
    public class Node<T>
    {
        public Node(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
        public Node<T> Next { get; set; }
    }



    namespace SimpleAlgorithmsApp
    {
        public class CircularLinkedList<T> : IEnumerable<T>  
        {
             
            Node<T> tail; 
            int count;  
            int current ;
            // head = tail.Next;

            public void Add(T data)
            {
                Node<T> node = new Node<T>(data);
                if (tail.Next == null)
                {
                    tail.Next = node;
                    tail = node;
                    tail.Next = tail.Next;
                }
                else
                {
                    node.Next = tail.Next;
                    tail.Next = node;
                    tail = node;
                }
                count++;
            }

            public void main()
            {
                Node<T> current = tail.Next;
                Node<T> max;
                int nodeData;
                Node<T> newNode = nodeData;

                while (current != tail)
                {
                    if(max() > current())
                    {
                        max = current;
                        current = current.Next;
                    }
                }

                for (int i = 0; i < max() - 2; i ++)
                    current = current.Next;

                newNode.Next = current.Next;
                current.Next = newNode;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)this).GetEnumerator();
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                Node<T> current = tail.Next;
                do
                {
                    if (current != null)
                    {
                        yield return current.Data;
                        current = current.Next;
                    }
                }
                while (current != tail.Next);
            }
        }

    }

}

