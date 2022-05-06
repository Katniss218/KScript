using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KScript.Runtime
{
    public enum OpCode
    {
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        //
        // [int] - Pointer
        //          All pointers are an offset from the stack frame pointer.
        // [num] - Variable (int/long/float/double/etc)
        //
        // It's up to the compiler to make sure these operands are correct.
        //
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // Control flow

        /// <summary>
        /// _ -- exits the program
        /// </summary>
        EXIT = 0,

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
        /// _ [int] -- pushes a to the return stack
        /// </summary>
        PUSH_RET,
        /// <summary>
        /// _ [int] -- pops the top of the return stack and loads it into a
        /// </summary>
        POP_RET,

        // TODO - The frame base pointers could be an additional native int array in the Script class. It should improve performance by quite a lot with highly recursive algorithms.
        /// <summary>
        /// _ -- pushes a new stack frame onto the stack. -> Pushes the caller frame base pointer onto the stack, increments the stack pointer
        /// </summary>
        PUSH_STACK_FRAME,
        /// <summary>
        /// _ -- pops the stack frame from the stack. -> Restores the stack frame pointer to the previous stack frame pointer, resets the stack pointer
        /// </summary>
        POP_STACK_FRAME,

        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // Arithmetic

        /// <summary>
        /// _ [int] [int] -- adds b to a, result is stored in a
        /// </summary>
        ADD,
        /// <summary>
        /// _ [int] [num] -- adds b to a, result is stored in a
        /// </summary>
        ADD_CONST,

        /// <summary>
        /// _ [int] [int] -- subtracts b from a, result is stored in a
        /// </summary>
        SUBTRACT,
        /// <summary>
        /// _ [int] [num] -- subtracts b from a, result is stored in a
        /// </summary>
        SUBTRACT_CONST,

        /// <summary>
        /// _ [int] [int] -- multiplies a and b, result is stored in a
        /// </summary>
        MULTIPLY,
        /// <summary>
        /// _ [int] [num] -- multiplies a and b, result is stored in a
        /// </summary>
        MULTIPLY_CONST,

        /// <summary>
        /// _ [int] [int] -- divides a and b, result is stored in a
        /// </summary>
        DIVIDE,
        /// <summary>
        /// _ [int] [num] -- divides a and b, result is stored in a
        /// </summary>
        DIVIDE_CONST,

        /// <summary>
        /// _ [int] [int] -- performs a modulo division a mod b, result is stored in a
        /// </summary>
        MODULO,
        /// <summary>
        /// _ [int] [num] -- performs a modulo division a mod b, result is stored in a
        /// </summary>
        MODULO_CONST,

        /// <summary>
        /// _ [int] [int] -- copies the value at b to a, result is stored in a
        /// </summary>
        SET,
        /// <summary>
        /// _ [int] [num] -- copies b to a, result is stored in a
        /// </summary>
        SET_CONST,
            
        /// <summary>
        /// _ [int] -- pushes the value at a on top of the stack, increments the stack pointer
        /// </summary>
        PUSH,
        /// <summary>
        /// _ [num] -- pushes a on top of the stack, increments the stack pointer
        /// </summary>
        PUSH_CONST

        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        //
    }
}
