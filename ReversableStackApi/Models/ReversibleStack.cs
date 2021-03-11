using System;
using System.Text;

namespace ReversableStackApi.Models
{
    public interface IReversibleStack<T>
    {
        /// <summary>
        /// adds object to stack
        /// </summary>
        /// <param name="input"></param>
        void Push(T input);

        /// <summary>
        /// removes top of stack and returns it
        /// </summary>
        /// <returns></returns>
        T Pop();

        /// <summary>
        /// returns top of stack without removing it
        /// </summary>
        /// <returns></returns>
        T Peak();

        /// <summary>
        /// Reverts the stack(first item becomes last and etc
        /// </summary>
        void Revert();


    }
    public class Node<T>
    {
        T item;
        Node<T> _next;
        Node<T> _prev;

        public Node(T item)
        {

            this.item = item;
        }

        public T Item
        {
            get => item;
            set => item = value;
        }

        public Node<T> Next
        {
            get => _next;
            set => _next = value;
        }

        public Node<T> Prev
        {
            get => _prev;
            set => _prev = value;
        }
    }
    public class ReversibleStack<T> : IReversibleStack<T>
    {
        private bool _isReversed;
        private Node<T> _head;
        private Node<T> _tail;
        private object locker;

        public ReversibleStack()
        {
            locker = new object();
        }


        private Node<T> Front => _isReversed ? _tail : _head;

        public void Push(T input)
        {
            lock (locker)
            {
                var node = new Node<T> (input);
                if (Front == null) // stack is empty
                {
                    _head = node;
                    _tail = node;
                }
                else if (_isReversed) // list is reversed, push at the right side
                {
                    _tail.Next = node;
                    node.Prev = _tail;
                    _tail = node;
                    _tail.Next = null;
                }
                else // push at left side
                {
                    _head.Prev = node;
                    node.Next = _head;
                    _head = node;
                    _head.Prev = null;
                } 
            }
           
        }

        public T Pop()
        {
            lock (locker)
            {
                T res;
                if (_head == null)
                    return default;

                if (_head == _tail) // popping the last item
                {
                    res = _head.Item;
                    _head = null;
                    _tail = null;


                }
                else if (_isReversed)
                {
                    res = _tail.Item;
                    _tail = _tail.Prev;
                    if (_tail != null)
                        _tail.Next = null; // "old" tail will be collected by GC
                }
                else
                {
                    res = _head.Item;
                    _head = _head.Next;
                    if (_head != null)
                        _head.Prev = null; // collected by GC
                }


                return res; 
            }
           
        }

        public T Peak()
        {
            lock (locker)
            {
                if (_head == null)
                    return default;
                return Front.Item;
            }
           
        }



        public void Revert()
        {
            lock (locker)
            {
                 _isReversed = !_isReversed;
            }
           
        }
    }

  
}