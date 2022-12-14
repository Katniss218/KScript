using KScript.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KScript.TestConsole
{
    public class RuntimeStuff
    {
        public static void Function()
        {
            Stopwatch sw = new Stopwatch();

            // 16 ms in 60fps - release mode speeds the stack machine up ~10 times.

            Script script = new Script( 10, new[]
            {
                (OpCode.GOTO_IF_ZERO_F32, new StackElement[] { 4 }),
                (OpCode.PUSH_CONST, new StackElement[] { 1 }),
                (OpCode.SUBTRACT_I32, new StackElement[] { }),
                (OpCode.GOTO, new StackElement[] { 0 }),
                (OpCode.EXIT, new StackElement[] { }),
            } );

            script.Push( 10 );

            sw.Start();

            script.Run();

            sw.Stop();
            Console.WriteLine( $"goto script funky: {sw.ElapsedTicks / 10000f} ms" );


        }
    }
}
