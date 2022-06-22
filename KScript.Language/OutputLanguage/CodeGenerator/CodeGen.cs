using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KScript.Language.OutputLanguage.CodeGenerator
{
    public class CodeGen
    {
        // first you feed the compiler the script tiles (text files).

        // The compiler runs them through the lexer and the parser and produces a CST.

        // The code generator then uses the CST to produce a list of opcodes and their arguments, and save them into a file.


        //-=-=-=-=-=-=-=-

        // Thought points:

        // We don't have to assign integer values to the opcodes. They can be abstracted away in the runtime. The output file can be more akin to an assembler file, and the runtime then uses that.

    }
}
