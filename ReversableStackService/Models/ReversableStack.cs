using System;
using System.Text;

namespace MyStackService.Models
{
    public class Node
    {
        string str; 
        Node _next;
        Node _prev;
        

        public Node(string str,Node prev=null, Node next= null)
        {
            Prev = prev;
            Next = next;
            Str = str;

        }

        public string Str
        {
            get => str;
            set => str = value;
        }

        public Node Next
        {
            get => _next;
            set => _next = value;
        }

        public Node Prev
        {
            get => _prev;
            set => _prev = value;
        }
    }
    public class ReversibleStack: IReversibleStack
    {
        private object locker;
        private bool _isReversed;
        private Node _head;
        private Node _tail;

        public ReversibleStack()
        {
            locker = new object();
        }
        private Node Front => _isReversed ? _tail : _head;


        public  int Count
        {
            get
            {
                if (_head == null)
                    return 0;
                else
                {
                    int i = 1;
                    Node tmp = _head;
                    while (tmp.Next != null&& tmp!=_tail) 
                    {
                        i++;
                        tmp = tmp.Next;
                    }
                    return i;
                }
            }
        }

        public string PrintStack()
        {
            if (_head == null)
                return "EMPTY!";
            var strBuilder = new StringBuilder();
            if (!_isReversed)
            {
                Node tmp = _head;
                while (tmp != null) 
                {
                    strBuilder.AppendLine(tmp.Str);
                    tmp = tmp.Next;
                }
            }
            else
            {
                Node tmp = _tail;
                while (tmp != null)
                {
                    strBuilder.AppendLine(tmp.Str);
                    tmp = tmp.Prev;
                }
            }

            return strBuilder.ToString();
        }

        

        public void Push(string input)
        {
            lock (locker)
            {


                var node = new Node(input);
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

        public string Pop()
        {
            lock (locker)
            {

                if (_head == null)
                    return null;

                string res;
                if (_head == _tail) // popping the last item
                {
                    res = _head.Str;
                    _head = null;
                    _tail = null;


                }
                else if (_isReversed)
                {
                    res = _tail.Str;
                    _tail = _tail.Prev;
                    if (_tail != null)
                        _tail.Next = null; // "old" tail will be collected by GC
                }
                else
                {
                    res = _head.Str;
                    _head = _head.Next;
                    if (_head != null)
                        _head.Prev = null; // collected by GC
                }


                return res;
            }
        }

        public string Peak()
        {
            lock (locker)
            {
                if (Front == null)
                    return null;
                return Front.Str;

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

    public interface IReversibleStack
    {
        /// <summary>
        /// adds text to stack
        /// </summary>
        /// <param name="input"></param>
        void Push(string input);

        /// <summary>
        /// removes top of stack and returns it
        /// </summary>
        /// <returns></returns>
        string Pop();

        /// <summary>
        /// returns top of stack without removing it
        /// </summary>
        /// <returns></returns>
        string Peak();

        /// <summary>
        /// Reverts the stack(first item becomes last and etc
        /// </summary>
        void Revert(); 


    }
}
