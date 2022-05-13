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
            //RuntimeStuff.Function();
            CompilerStuff.Function();

            Console.WriteLine( "Press any key to exit..." );
            Console.ReadKey();
        }
    }
}
