using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyStackService.Models;

namespace ReversibleStackTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestNormal()
        {
            var input = "A";
            var input2 = "B";
            var input3 = "C";

            var revStack = new ReversibleStack();
            revStack.Push(input);
            revStack.Push(input2);
            revStack.Push(input3);

            var size = revStack.Count;



            Assert.AreEqual(input3, revStack.Peak());
            Assert.AreEqual(input3, revStack.Pop());
            Assert.AreEqual(input2,revStack.Peak());
            Assert.AreEqual(input2, revStack.Pop());
            Assert.AreEqual(input, revStack.Peak());
            Assert.AreEqual(input, revStack.Pop());

        }
        [TestMethod]
        public void TestReverse()
        {
            var input = "A";
            var input2 = "B";
            var input3 = "C";

            IReversibleStack revStack = new ReversibleStack();
            revStack.Push(input);
            revStack.Push(input2);
            revStack.Push(input3);
            revStack.Revert();

            Assert.AreEqual(input, revStack.Peak());
            Assert.AreEqual(input, revStack.Pop());
            Assert.AreEqual(input2, revStack.Peak());
            Assert.AreEqual(input2, revStack.Pop());
            Assert.AreEqual(input3, revStack.Peak());
            Assert.AreEqual(input3, revStack.Pop());


        }
        [TestMethod]
        public void RandomTest()
        {
            var randomString = new Random().Next(1000000,99999999).ToString();



            var stack = new Stack<string>();
            var queue  = new Queue<string>();
            var revStack = new ReversibleStack();
            var revStackFlipped = new ReversibleStack();
            


            for (var i = 0; i < randomString.Length; i++)
            {
                string x = randomString[i].ToString();
                stack.Push(x);
                queue.Enqueue(x);
                revStackFlipped.Push(x);
                revStack.Push(x);


            }
            revStackFlipped.Revert();
            for (var i = 0; i < randomString.Length; i++)
            {
                Assert.AreEqual(stack.Pop(), revStack.Pop());
                Assert.AreEqual(queue.Dequeue(), revStackFlipped.Pop());

            }

        }

        [TestMethod]
        public void MidReverseTest()
        {
            var input = "A";
            var input2 = "B";
            var input3 = "C";
            var input4 = "D";
            var revStack = new ReversibleStack();

            revStack.Push(input);
            revStack.Push(input2);
            revStack.Push(input3);
            revStack.Push(input4);

            Assert.AreEqual(revStack.Pop(), input4);
            revStack.Revert();
            Assert.AreEqual(revStack.Pop(), input);
            revStack.Revert();
            Assert.AreEqual(revStack.Pop(), input3);
            revStack.Revert();
            Assert.AreEqual(revStack.Pop(), input2);
            




        }


    }
}

