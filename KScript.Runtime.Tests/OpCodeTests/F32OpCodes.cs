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
                    (OpCode.ADD_F32, new StackElement[] { 0, 1 })
                } );

            script.Push( 0.0f );
            script.Push( 5.0f );

            script.Run();

            Assert.IsTrue( script.Stack[0].Float32 == 5.0f );
        }

        [TestMethod]
        public void AddConst()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.ADD_F32_CONST, new StackElement[] { 0, 5.0f })
                } );

            script.Push( 0.0f );

            script.Run();

            Assert.IsTrue( script.Stack[0].Float32 == 5.0f );
        }

        [TestMethod]
        public void Subtract()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.SUBTRACT_F32, new StackElement[] { 0, 1 })
                } );

            script.Push( 10.0f );
            script.Push( 5.0f );

            script.Run();

            Assert.IsTrue( script.Stack[0].Float32 == 5.0f );
        }

        [TestMethod]
        public void SubtractConst()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.SUBTRACT_F32_CONST, new StackElement[] { 0, 5.0f })
                } );

            script.Push( 10.0f );

            script.Run();

            Assert.IsTrue( script.Stack[0].Float32 == 5.0f );
        }

        [TestMethod]
        public void Multiply()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.MULTIPLY_F32, new StackElement[] { 0, 1 })
                } );

            script.Push( 5.0f );
            script.Push( 5.0f );

            script.Run();

            Assert.IsTrue( script.Stack[0].Float32 == 25.0f );
        }

        [TestMethod]
        public void MultiplyConst()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.MULTIPLY_F32_CONST, new StackElement[] { 0, 5.0f })
                } );

            script.Push( 5.0f );

            script.Run();

            Assert.IsTrue( script.Stack[0].Float32 == 25.0f );
        }

        [TestMethod]
        public void Divide()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.DIVIDE_F32, new StackElement[] { 0, 1 })
                } );

            script.Push( 10.0f );
            script.Push( 2.0f );

            script.Run();

            Assert.IsTrue( script.Stack[0].Float32 == 5.0f );
        }

        [TestMethod]
        public void DivideConst()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.DIVIDE_F32_CONST, new StackElement[] { 0, 2.0f })
                } );

            script.Push( 10.0f );

            script.Run();

            Assert.IsTrue( script.Stack[0].Float32 == 5.0f );
        }

        [TestMethod]
        public void Modulo()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.MODULO_F32, new StackElement[] { 0, 1 })
                } );

            script.Push( 25.0f );
            script.Push( 10.0f );

            script.Run();

            Assert.IsTrue( script.Stack[0].Float32 == 5.0f );
        }

        [TestMethod]
        public void ModuloConst()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.MODULO_F32_CONST, new StackElement[] { 0, 10.0f })
                } );

            script.Push( 25.0f );

            script.Run();

            Assert.IsTrue( script.Stack[0].Float32 == 5.0f );
        }
    }
}