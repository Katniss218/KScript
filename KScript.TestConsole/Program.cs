using KScript.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KScript.TestConsole
{
    class Program
    {
        static void Main( string[] args )
        {
            Stopwatch sw = new Stopwatch();

            // 16 ms in 60fps 

            Script script = new Script( 4, new[]
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

            sw.Start();

            script.Run();

            sw.Stop();
            Console.WriteLine( $"goto script funky: {sw.ElapsedMilliseconds}" );


            dynamic[] vars = new dynamic[4]
            {
                0, 0, 0, 0
            };

            sw.Restart();
            for( int i = 0; i < 30000000; i++ )
            {
                vars[i % 4] += 1;
            }
            sw.Stop();

            Console.WriteLine( $"dynamic array: {sw.ElapsedMilliseconds}" );

            int[] vars2 = new int[4]
            {
                0, 0, 0, 0
            };

            sw.Restart();
            for( int i = 0; i < 30000000; i++ )
            {
                vars2[i % 4] += 1;
            }
            sw.Stop();

            Console.WriteLine( $"int array: {sw.ElapsedMilliseconds}" );

            object[] vars3 = new object[4]
            {
                0, 0, 0, 0
            };

            sw.Restart();
            for( int i = 0; i < 30000000; i++ )
            {
                int val = (int)vars3[i % 4];
                val += 1;
                vars3[i % 4] = val;
            }
            sw.Stop();

            Console.WriteLine( $"object array: {sw.ElapsedMilliseconds}" );

            StackElement[] vars4 = new StackElement[4]
            {
                0, 0, 0, 0
            };

            sw.Restart();
            for( int i = 0; i < 30000000; i++ )
            {
                vars4[i % 4].Int32 += 1;
            }
            sw.Stop();

            Console.WriteLine( $"struct array (int): {sw.ElapsedMilliseconds}" );
            
            vars4 = new StackElement[4]
            {
                0, 0, 0, 0
            };

            sw.Restart();
            for( int i = 0; i < 30000000; i++ )
            {
                vars4[i % 4].Int32 += 1;
            }
            sw.Stop();

            Console.WriteLine( $"struct array (float): {sw.ElapsedMilliseconds}" );

            Console.ReadKey();
        }
    }
}
