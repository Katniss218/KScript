using KScript.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KScript.Tests.OpCodeTests
{
    [TestClass]
    public class I32OpCodes
    {
        [TestMethod]
        public void Add()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.ADD_I32, new StackElement[] {})
                } );

            script.Push( 0 );
            script.Push( 5 );

            script.Run();

            Assert.IsTrue( script.StackPointer == 1 && script.Stack[0].Int32 == 5 );
        }

        [TestMethod]
        public void Subtract()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.SUBTRACT_I32, new StackElement[] {})
                } );

            script.Push( 10 );
            script.Push( 5 );

            script.Run();

            Assert.IsTrue( script.StackPointer == 1 && script.Stack[0].Int32 == 5 );
        }

        [TestMethod]
        public void Multiply()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.MULTIPLY_I32, new StackElement[] {})
                } );

            script.Push( 5 );
            script.Push( 5 );

            script.Run();

            Assert.IsTrue( script.StackPointer == 1 && script.Stack[0].Int32 == 25 );
        }

        [TestMethod]
        public void Divide()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.DIVIDE_I32, new StackElement[] {})
                } );

            script.Push( 10 );
            script.Push( 2 );

            script.Run();

            Assert.IsTrue( script.StackPointer == 1 && script.Stack[0].Int32 == 5 );
        }

        [TestMethod]
        public void Modulo()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.MODULO_I32, new StackElement[] {})
                } );

            script.Push( 25 );
            script.Push( 10 );

            script.Run();

            Assert.IsTrue( script.StackPointer == 1 && script.Stack[0].Int32 == 5 );
        }
    }
}