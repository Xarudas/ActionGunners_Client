using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MeatInc.ActionGunnersSharedLegacy
{

    public class Buffer<T>
    {
        public int Count => _elements.Count;

        private Queue<T> _elements;
        private int _bufferSize;
        private int _counter;
        private int _correctionTollerance;

        public Buffer(int bufferSize, int correctionTollerance)
        {
            _bufferSize = bufferSize;
            _correctionTollerance = correctionTollerance;
            _elements = new Queue<T>();
        }


        public void Add(T element)
        {
            _elements.Enqueue(element);
        }

        public T[] Get()
        {
            int size = _elements.Count - 1;

            if (size == _bufferSize)
            {
                _counter = 0;
            }

            if (size > _bufferSize)
            {
                if (_counter < 0)
                {
                    _counter = 0;
                }
                _counter++;
                if (_counter > _correctionTollerance)
                {
                    int amount = _elements.Count - _bufferSize;
                    T[] temp = new T[amount];
                    for (int i = 0; i < amount; i++)
                    {
                        temp[i] = _elements.Dequeue();
                    }

                    return temp;
                }
            }

            if (size < _bufferSize)
            {
                if (_counter > 0)
                {
                    _counter = 0;
                }
                _counter--;
                if (-_counter > _correctionTollerance)
                {
                    return new T[0];
                }
            }

            if (_elements.Any())
            {
                return new T[] { _elements.Dequeue() };
            }
            return new T[0];
        }

    }
}
