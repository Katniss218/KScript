using KScript.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KScript.Tests
{
    [TestClass]
    public class OpCodeTests
    {
        [TestMethod]
        public void Add()
        {
            Script script = new Script()
            {
                Variables = new dynamic[]
                {
                    0,
                    5
                },
                Op = new (OpCode opCode, dynamic[] data)[]
                {
                    (OpCode.ADD, new dynamic[] { 0, 1 })
                }
            };

            script.Run();

            Assert.IsTrue( script.Variables[1] == 5 );
        }

        [TestMethod]
        public void Subtract()
        {
            Script script = new Script()
            {
                Variables = new dynamic[]
                {
                    10,
                    5
                },
                Op = new (OpCode opCode, dynamic[] data)[]
                {
                    (OpCode.SUBTRACT, new dynamic[] { 0, 1 })
                }
            };

            script.Run();

            Assert.IsTrue( script.Variables[1] == 5 );
        }

        [TestMethod]
        public void Goto()
        {
            Script script = new Script()
            {
                Variables = new dynamic[]
                {
                    0,
                    0,
                    5
                },
                Op = new (OpCode opCode, dynamic[] data)[]
                {
                    (OpCode.GOTO, new dynamic[] { 2 }),
                    (OpCode.ADD, new dynamic[] { 1, 2 }),
                    (OpCode.ADD, new dynamic[] { 1, 2 })
                }
            };

            script.Run();
            
            Assert.IsTrue( script.Variables[1] == 5 );
        }
        
        [TestMethod]
        public void GotoIfZero()
        {
            Script script = new Script()
            {
                Variables = new dynamic[]
                {
                    0,
                    0,
                    5
                },
                Op = new (OpCode opCode, dynamic[] data)[]
                {
                    (OpCode.GOTO_IF_ZERO, new dynamic[] { 0, 2 }),
                    (OpCode.ADD, new dynamic[] { 1, 2 }),
                    (OpCode.ADD, new dynamic[] { 1, 2 })
                }
            };

            script.Run();
            
            Assert.IsTrue( script.Variables[1] == 5 );
        }

        [TestMethod]
        public void Loop()
        {
            Script script = new Script()
            {
                Variables = new dynamic[]
                {
                    0,
                    5,
                    10
                },
                Op = new (OpCode opCode, dynamic[] data)[]
                {
                    (OpCode.GOTO_IF_ZERO, new dynamic[] { 2, 3 }),
                    (OpCode.SUBTRACT, new dynamic[] { 2, 1 }),
                    (OpCode.GOTO, new dynamic[] { 0 }),
                    (OpCode.EXIT, new dynamic[] {})
                }
            };

            script.Run();

            Assert.IsTrue( script.OperationCounter == 8 );
        }

        [TestMethod]
        public void LongLoop()
        {
            // 16 ms in 60fps 

            Script script = new Script()
            {
                Variables = new dynamic[]
                {
                    0,
                    5,
                    5,
                    1000000
                },
                Op = new (OpCode opCode, dynamic[] data)[]
                {
                    (OpCode.GOTO_IF_ZERO, new dynamic[] { 3, 4 }),
                    (OpCode.SUBTRACT, new dynamic[] { 3, 1 }),
                    (OpCode.ADD, new dynamic[] { 2, 1 }), // 40 ms vs 52 with this one line added
                    (OpCode.GOTO, new dynamic[] { 0 }),
                    (OpCode.EXIT, new dynamic[] {})
                }
            };

            script.Run();

            Assert.IsTrue( true );
        }
    }
}
