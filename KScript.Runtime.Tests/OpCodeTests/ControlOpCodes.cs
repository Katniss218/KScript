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
                (OpCode.PUSH_CONST, new StackElement[] { 1 }),
                (OpCode.PUSH_CONST, new StackElement[] { 1 })
            } );

            script.Run();

            Assert.IsTrue( script.StackPointer == 1 );
        }

        [TestMethod]
        public void GotoIfZeroI32()
        {
            Script script = new Script( 10, new[]
            {
                (OpCode.GOTO_IF_ZERO_I32, new StackElement[] { 2 }),
                (OpCode.PUSH_CONST, new StackElement[] { 1 }),
                (OpCode.PUSH_CONST, new StackElement[] { 1 })
            } );

            script.Push( 0 );

            script.Run();

            Assert.IsTrue( script.StackPointer == 2 );
        }

        [TestMethod]
        public void GotoIfZeroF32()
        {
            Script script = new Script( 10, new[]
            {
                (OpCode.GOTO_IF_ZERO_F32, new StackElement[] { 2 }),
                (OpCode.PUSH_CONST, new StackElement[] { 1 }),
                (OpCode.PUSH_CONST, new StackElement[] { 1 })
            } );

            script.Push( 0.0f );

            script.Run();

            Assert.IsTrue( script.StackPointer == 2 );
        }

        [TestMethod]
        public void Loop()
        {
            const int LOOP_COUNT = 10;

            Script script = new Script( 10, new[]
            {
                (OpCode.GOTO_IF_ZERO_F32, new StackElement[] { 4 }),
                (OpCode.PUSH_CONST, new StackElement[] { 1 }),
                (OpCode.SUBTRACT_I32, new StackElement[] { }),
                (OpCode.GOTO, new StackElement[] { 0 }),
                (OpCode.EXIT, new StackElement[] { }),
            } );

            script.Push( LOOP_COUNT );

            script.Run();

            Assert.IsTrue( script.OperationCounter == LOOP_COUNT * 4 + 2 );
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
        public void PushStackFrame()
        {
            Script script = new Script( 10, new[]
            {
                (OpCode.PUSH_STACK_FRAME, new StackElement[] { }),
                (OpCode.PUSH_CONST, new StackElement[] { 5 })
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
                (OpCode.PUSH_STACK_FRAME, new StackElement[] { }),
                (OpCode.PUSH_CONST, new StackElement[] { 5 }),
                (OpCode.POP_STACK_FRAME, new StackElement[] { })
            } );

            script.Run();

            Assert.IsTrue( script.StackPointer == 0 ); // 2 initial vars, next pointed to by stack pointer
        }
    }
}