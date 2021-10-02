using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MeatInc.ActionGunnersShared.Utility
{
    public class Buffer<T>
    {
        public int Count => elements.Count;

        private Queue<T> elements;
        
        private int counter;

        private readonly int bufferSize;
        private readonly int correctionTollerance;

        public Buffer(int bufferSize, int correctionTollerance)
        {
            this.bufferSize = bufferSize;
            this.correctionTollerance = correctionTollerance;
            elements = new Queue<T>();
        }

        public void Add(T element)
        {
            elements.Enqueue(element);
        }

        public T[] Get()
        {
            int size = elements.Count - 1;

            if (size == bufferSize)
            {
                counter = 0;
            }

            if (size > bufferSize)
            {
                if (counter < 0)
                {
                    counter = 0;
                }
                counter++;
                if (counter > correctionTollerance)
                {
                    int amount = elements.Count - bufferSize;
                    T[] temp = new T[amount];
                    for (int i = 0; i < amount; i++)
                    {
                        temp[i] = elements.Dequeue();
                    }

                    return temp;
                }
            }

            if (size < bufferSize)
            {
                if (counter > 0)
                {
                    counter = 0;
                }
                counter--;
                if (-counter > correctionTollerance)
                {
                    return new T[0];
                }
            }

            if (elements.Any())
            {
                return new T[] { elements.Dequeue() };
            }
            return new T[0];
        }
    }
}
