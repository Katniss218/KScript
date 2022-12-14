using KScript.Language.InputLanguage.Infrastructure;
using KScript.Language.InputLanguage.Infrastructure.CST;
using KScript.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KScript.Language.OutputLanguage.CodeGenerator
{
    /// <summary>
    /// This class is used to generate a sequence of opcodes and their arguments for the KScript runtime to execute.
    /// </summary>
    public class CodeGen
    {
        // first you feed the compiler the script tiles (text files).
        // The compiler runs them through the lexer and the parser and produces a CST.

        // The code generator then uses the CST to produce a list of opcodes and their arguments, and save them into a file.


        //-=-=-=-=-=-=-=-

        // Thought points:

        // We don't have to assign integer values to the opcodes. They can be abstracted away in the runtime. The output file can be more akin to an assembler file, and the runtime then uses that.


        // I can use postfix notation to generate the instructions from the CST BinaryExpression.
        //        IF IF IF the stack machine pops the top operands and pushes the result on top of the stack.

        /*
        public List<(OpCode opCode, StackElement[] operand)> GenerateInstructions( SyntaxNode node )
        {
            // switch type of node, call specific method.
        }

        public List<(OpCode opCode, StackElement[] operand)> GenerateInstructions( BinaryExpression node )
        {
            var instructions = GenerateInstructions( node.Left ); // push '3'
            instructions.AddRange( GenerateInstructions( node.Right )); // push '5'
            // add the operand instruction. // mul => result is 15.
            //instructions.AddRange()
        }

        public (OpCode opCode, StackElement[] operand) GenerateInstructions( Literal node )
        {
            return (OpCode.PUSH_CONST, new StackElement[] { node.Value });
        }

        */

    }
}