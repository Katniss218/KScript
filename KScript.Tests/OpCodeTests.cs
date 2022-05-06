using KScript.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KScript.Tests
{
    [TestClass]
    public class OpCodeTests
    {
        [TestMethod]
        public void PushStackFrame()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.PUSH_STACK_FRAME, new dynamic[]{ }),
                    (OpCode.PUSH_CONST, new dynamic[]{ 5 })
                } );

            script.Push( 1 );
            script.Push( 1 );

            script.Run();

            Assert.IsTrue( script.Stack[3] == 5 ); // 0-th index of the 2nd frame.
            Assert.IsTrue( script.StackPointer == 4 ); // 2 initial vars + stack frame pointer + pushed const, next pointed to by stack pointer
        }

        [TestMethod]
        public void PopStackFrame()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.PUSH_STACK_FRAME, new dynamic[]{ }),
                    (OpCode.PUSH_CONST, new dynamic[]{ 5 }),
                    (OpCode.POP_STACK_FRAME, new dynamic[]{ })
                } );

            script.Push( 1 );
            script.Push( 1 );

            script.Run();

            Assert.IsTrue( script.StackPointer == 2 ); // 2 initial vars, next pointed to by stack pointer
        }

        [TestMethod]
        public void Add()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.ADD, new dynamic[] { 0, 1 })
                } );

            script.Push( 0 );
            script.Push( 5 );

            script.Run();

            Assert.IsTrue( script.Stack[0] == 5 );
        }

        [TestMethod]
        public void AddConst()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.ADD_CONST, new dynamic[] { 0, 5 })
                } );

            script.Push( 0 );

            script.Run();

            Assert.IsTrue( script.Stack[0] == 5 );
        }

        [TestMethod]
        public void Subtract()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.SUBTRACT, new dynamic[] { 0, 1 })
                } );

            script.Push( 10 );
            script.Push( 5 );

            script.Run();

            Assert.IsTrue( script.Stack[0] == 5 );
        }

        [TestMethod]
        public void SubtractConst()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.SUBTRACT_CONST, new dynamic[] { 0, 5 })
                } );

            script.Push( 10 );

            script.Run();

            Assert.IsTrue( script.Stack[0] == 5 );
        }

        [TestMethod]
        public void Multiply()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.MULTIPLY, new dynamic[] { 0, 1 })
                } );

            script.Push( 5 );
            script.Push( 5 );

            script.Run();

            Assert.IsTrue( script.Stack[0] == 25 );
        }

        [TestMethod]
        public void MultiplyConst()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.MULTIPLY_CONST, new dynamic[] { 0, 5 })
                } );

            script.Push( 5 );

            script.Run();

            Assert.IsTrue( script.Stack[0] == 25 );
        }

        [TestMethod]
        public void Divide()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.DIVIDE, new dynamic[] { 0, 1 })
                } );

            script.Push( 10 );
            script.Push( 2 );

            script.Run();

            Assert.IsTrue( script.Stack[0] == 5 );
        }

        [TestMethod]
        public void DivideConst()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.DIVIDE_CONST, new dynamic[] { 0, 2 })
                } );

            script.Push( 10 );

            script.Run();

            Assert.IsTrue( script.Stack[0] == 5 );
        }

        [TestMethod]
        public void Goto()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.GOTO, new dynamic[] { 2 }),
                    (OpCode.ADD, new dynamic[] { 1, 2 }),
                    (OpCode.ADD, new dynamic[] { 1, 2 })
                } );

            script.Push( 0 );
            script.Push( 0 );
            script.Push( 5 );

            script.Run();

            Assert.IsTrue( script.Stack[1] == 5 );
        }

        [TestMethod]
        public void GotoIfZero()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.GOTO_IF_ZERO, new dynamic[] { 0, 2 }),
                    (OpCode.ADD, new dynamic[] { 1, 2 }),
                    (OpCode.ADD, new dynamic[] { 1, 2 })
                } );

            script.Push( 0 );
            script.Push( 0 );
            script.Push( 5 );

            script.Run();

            Assert.IsTrue( script.Stack[1] == 5 );
        }

        [TestMethod]
        public void PushRet()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.PUSH_RET, new dynamic[] { 0 })
                } );

            script.Push( 15 );

            script.Run();

            Assert.IsTrue( script.ReturnStack.Pop() == 15 );
        }

        [TestMethod]
        public void PopRet()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.POP_RET, new dynamic[] { 0 })
                } );
            script.ReturnStack.Push( 15 );

            script.Run();

            Assert.IsTrue( script.Stack[0] == 15 );
        }

        [TestMethod]
        public void Loop()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.GOTO_IF_ZERO, new dynamic[] { 2, 3 }),
                    (OpCode.SUBTRACT, new dynamic[] { 2, 1 }),
                    (OpCode.GOTO, new dynamic[] { 0 }),
                    (OpCode.EXIT, new dynamic[] {})
                } );

            script.Push( 0 );
            script.Push( 5 );
            script.Push( 10 );

            script.Run();

            Assert.IsTrue( script.OperationCounter == 8 );
        }

        [TestMethod]
        public void LongLoop()
        {
            // There is 16 miliseconds in 1/60 of a second (60 fps).

            Script script = new Script( 10, new[]
                {
                    (OpCode.GOTO_IF_ZERO, new dynamic[] { 3, 4 }),
                    (OpCode.SUBTRACT, new dynamic[] { 3, 1 }),
                    (OpCode.ADD, new dynamic[] { 2, 1 }), // 40 ms vs 52 with this one line added.
                    (OpCode.GOTO, new dynamic[] { 0 }),
                    (OpCode.EXIT, new dynamic[] {})
                } );

            script.Push( 0 );
            script.Push( 5 );
            script.Push( 5 );
            script.Push( 1000000 );

            script.Run();

            Assert.IsTrue( true );
        }
    }
}
