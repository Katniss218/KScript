using KScript.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KScript.Tests.OpCodeTests
{
    [TestClass]
    public class ControlOpCodes
    {
        [TestMethod]
        public void Goto()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.GOTO, new StackElement[] { 2 }),
                    (OpCode.ADD_I32, new StackElement[] { 1, 2 }),
                    (OpCode.ADD_I32, new StackElement[] { 1, 2 })
                } );

            script.Push( 0 );
            script.Push( 0 );
            script.Push( 5 );

            script.Run();

            Assert.IsTrue( script.Stack[1].Int32 == 5 );
        }

        [TestMethod]
        public void GotoIfZeroI32()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.GOTO_IF_ZERO_I32, new StackElement[] { 0, 2 }),
                    (OpCode.ADD_I32, new StackElement[] { 1, 2 }),
                    (OpCode.ADD_I32, new StackElement[] { 1, 2 })
                } );

            script.Push( 0 );
            script.Push( 0 );
            script.Push( 5 );

            script.Run();

            Assert.IsTrue( script.Stack[1].Int32 == 5 ); // skip first addition
        }

        [TestMethod]
        public void GotoIfZeroF32()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.GOTO_IF_ZERO_F32, new StackElement[] { 0, 2 }),
                    (OpCode.ADD_F32, new StackElement[] { 1, 2 }),
                    (OpCode.ADD_F32, new StackElement[] { 1, 2 })
                } );

            script.Push( 0.0f );
            script.Push( 0.0f );
            script.Push( 5.0f );

            script.Run();

            Assert.IsTrue( script.Stack[1].Float32 == 5 ); // skip first addition
        }

        [TestMethod]
        public void PushRet()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.PUSH_RET, new StackElement[] { 0 })
                } );

            script.Push( 15 );

            script.Run();

            Assert.IsTrue( script.ReturnStack.Pop().Int32 == 15 );
        }

        [TestMethod]
        public void PopRet()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.POP_RET, new StackElement[] { 0 })
                } );
            script.ReturnStack.Push( 15 );

            script.Run();

            Assert.IsTrue( script.Stack[0].Int32 == 15 );
        }

        [TestMethod]
        public void Loop()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.GOTO_IF_ZERO_I32, new StackElement[] { 2, 3 }),
                    (OpCode.SUBTRACT_I32, new StackElement[] { 2, 1 }),
                    (OpCode.GOTO, new StackElement[] { 0 }),
                    (OpCode.EXIT, new StackElement[] {})
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
                    (OpCode.GOTO_IF_ZERO_I32, new StackElement[] { 3, 4 }),
                    (OpCode.SUBTRACT_I32, new StackElement[] { 3, 1 }),
                    (OpCode.ADD_I32, new StackElement[] { 2, 1 }), // 40 ms vs 52 with this one line added.
                    (OpCode.GOTO, new StackElement[] { 0 }),
                    (OpCode.EXIT, new StackElement[] {})
                } );

            script.Push( 0 );
            script.Push( 5 );
            script.Push( 5 );
            script.Push( 1000000 );

            script.Run();

            Assert.IsTrue( true );
        }

        [TestMethod]
        public void PushStackFrame()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.PUSH_STACK_FRAME, new StackElement[]{ }),
                    (OpCode.PUSH_CONST, new StackElement[]{ 5 })
                } );

            script.Push( 1 );
            script.Push( 1 );

            script.Run();

            Assert.IsTrue( script.Stack[3].Int32 == 5 ); // 0-th index of the 2nd frame.
            Assert.IsTrue( script.StackPointer == 4 ); // 2 initial vars + stack frame pointer + pushed const, next pointed to by stack pointer
        }

        [TestMethod]
        public void PopStackFrame()
        {
            Script script = new Script( 10, new[]
                {
                    (OpCode.PUSH_STACK_FRAME, new StackElement[]{ }),
                    (OpCode.PUSH_CONST, new StackElement[]{ 5 }),
                    (OpCode.POP_STACK_FRAME, new StackElement[]{ })
                } );

            script.Push( 1 );
            script.Push( 1 );

            script.Run();

            Assert.IsTrue( script.StackPointer == 2 ); // 2 initial vars, next pointed to by stack pointer
        }
    }
}