using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KScript.Runtime
{
    public enum OpCode
    {
        EXTERN_CALL,
        /// <summary>
        ///  _ [int] -- moves the current operation to ^
        /// </summary>
        GOTO,
        /// <summary>
        /// _ [num] [int] -- moves the current operation to ^ if the variable at a is zero
        /// </summary>
        GOTO_IF_ZERO,
        /// <summary>
        /// _ -- exits the program
        /// </summary>
        EXIT,
        /// <summary>
        /// _ [num] [num] -- adds b to a, result is stored in a
        /// </summary>
        ADD,
        /// <summary>
        /// _ [num] [num] -- subtracts b from a, result is stored in a
        /// </summary>
        SUBTRACT,
        MULTIPLY,
        DIVIDE
    }
}
