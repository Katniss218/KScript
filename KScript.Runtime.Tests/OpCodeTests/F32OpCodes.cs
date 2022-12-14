using KScript.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KScript.Tests.OpCodeTests
{
    [TestClass]
    public class F32OpCodes
    {
        [TestMethod]
        public void Add()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.ADD_F32, new StackElement[] {})
                } );

            script.Push( 0.0f );
            script.Push( 5.0f );

            script.Run();

            Assert.IsTrue( script.StackPointer == 1 && script.Stack[0].Float32 == 5.0f );
        }

        [TestMethod]
        public void Subtract()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.SUBTRACT_F32, new StackElement[] {})
                } );

            script.Push( 10.0f );
            script.Push( 5.0f );

            script.Run();

            Assert.IsTrue( script.StackPointer == 1 && script.Stack[0].Float32 == 5.0f );
        }

        [TestMethod]
        public void Multiply()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.MULTIPLY_F32, new StackElement[] {})
                } );

            script.Push( 5.0f );
            script.Push( 5.0f );

            script.Run();

            Assert.IsTrue( script.StackPointer == 1 && script.Stack[0].Float32 == 25.0f );
        }

        [TestMethod]
        public void Divide()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.DIVIDE_F32, new StackElement[] {})
                } );

            script.Push( 10.0f );
            script.Push( 2.0f );

            script.Run();

            Assert.IsTrue( script.StackPointer == 1 && script.Stack[0].Float32 == 5.0f );
        }

        [TestMethod]
        public void Modulo()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.MODULO_F32, new StackElement[] {})
                } );

            script.Push( 25.0f );
            script.Push( 10.0f );

            script.Run();

            Assert.IsTrue( script.StackPointer == 1 && script.Stack[0].Float32 == 5.0f );
        }
    }
}