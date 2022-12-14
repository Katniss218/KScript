
namespace KScript.Runtime
{
    public enum OpCode
    {
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        //
        //          ARGUMENT TYPES:
        //
        // [ptr] - Pointer
        //          All pointers are an offset from the stack frame pointer.
        // [any] - Variable (any type)
        // [int32] - Variable (32 bit integer)
        // [float32] - Variable (32 bit floating point)
        //
        // It's up to the compiler to make sure the operands passed are correct for a given opcode.
        //
        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // Control flow

        /// <summary>
        /// _ -- exits the program
        /// </summary>
        EXIT = 0,

        // External call - it needs the name and operands compatible with reflection to call the given method.
        EXTERN_CALL,

        /// <summary>
        ///  _ [ptr] -- moves the current operation to a
        /// </summary>
        GOTO,

        /// <summary>
        /// _ [ptr] -- Moves the current operation to the position stored at stack element {0}, represented as a pointer, if the value at the top of the stack is zero.
        /// </summary>
        GOTO_IF_ZERO_I32,

        /// <summary>
        /// _ [ptr] -- Moves the current operation to the position stored at stack element {0}, represented as a pointer, if the value at the top of the stack is zero.
        /// </summary>
        GOTO_IF_ZERO_F32,

        /// <summary>
        /// _ [ptr] -- Pushes the value of the stack element {0} to the return stack.
        /// </summary>
        PUSH_RET,
        /// <summary>
        /// _ [ptr] -- Pops the top of the return stack and stores it in the stack element {0}.
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
        /// _ -- Pops the 2 top values on the stack and adds them together as integers.
        /// </summary>
        ADD_I32,
        /// <summary>
        /// _ [int32] -- adds b to a, result is stored in a
        /// </summary>
        //ADD_I32_CONST,

        /// <summary>
        /// _ [ptr] [ptr] -- subtracts b from a, result is stored in a
        /// </summary>
        SUBTRACT_I32,
        /// <summary>
        /// _ [ptr] [int32] -- subtracts b from a, result is stored in a
        /// </summary>
        //SUBTRACT_I32_CONST,
        
        /// <summary>
        /// _ [ptr] [ptr] -- multiplies a and b, result is stored in a
        /// </summary>
        MULTIPLY_I32,
        /// <summary>
        /// _ [ptr] [int32] -- multiplies a and b, result is stored in a
        /// </summary>
        //MULTIPLY_I32_CONST,
        
        /// <summary>
        /// _ [ptr] [ptr] -- divides a and b, result is stored in a
        /// </summary>
        DIVIDE_I32,
        /// <summary>
        /// _ [ptr] [int32] -- divides a and b, result is stored in a
        /// </summary>
        //DIVIDE_I32_CONST,
        
        /// <summary>
        /// _ [ptr] [ptr] -- performs a modulo division a mod b, result is stored in a
        /// </summary>
        MODULO_I32,
        /// <summary>
        /// _ [ptr] [int32] -- performs a modulo division a mod b, result is stored in a
        /// </summary>
        //MODULO_I32_CONST,

        /// <summary>
        /// _ [ptr] [ptr] -- adds b to a, result is stored in a
        /// </summary>
        ADD_F32,
        /// <summary>
        /// _ [ptr] [float32] -- adds b to a, result is stored in a
        /// </summary>
        //ADD_F32_CONST,

        /// <summary>
        /// _ [ptr] [ptr] -- subtracts b from a, result is stored in a
        /// </summary>
        SUBTRACT_F32,
        /// <summary>
        /// _ [ptr] [float32] -- subtracts b from a, result is stored in a
        /// </summary>
        //SUBTRACT_F32_CONST,

        /// <summary>
        /// _ [ptr] [ptr] -- multiplies a and b, result is stored in a
        /// </summary>
        MULTIPLY_F32,
        /// <summary>
        /// _ [ptr] [float32] -- multiplies a and b, result is stored in a
        /// </summary>
        //MULTIPLY_F32_CONST,

        /// <summary>
        /// _ [ptr] [ptr] -- divides a and b, result is stored in a
        /// </summary>
        DIVIDE_F32,
        /// <summary>
        /// _ [ptr] [float32] -- divides a and b, result is stored in a
        /// </summary>
        //DIVIDE_F32_CONST,

        /// <summary>
        /// _ [ptr] [ptr] -- performs a modulo division a mod b, result is stored in a
        /// </summary>
        MODULO_F32,
        /// <summary>
        /// _ [ptr] [float32] -- performs a modulo division a mod b, result is stored in a
        /// </summary>
        //MODULO_F32_CONST,

        /// <summary>
        /// _ [ptr] [ptr] -- copies the value at b to a, result is stored in a
        /// </summary>
        //SET,
        /// <summary>
        /// _ [ptr] [any] -- copies b to a, result is stored in a
        /// </summary>
        //SET_CONST,

        /// <summary>
        /// _ [ptr] -- Pushes the value of the stack element {0} to the top of the stack, increments the stack pointer
        /// </summary>
        PUSH,
        /// <summary>
        /// _ [any] -- pushes a on top of the stack, increments the stack pointer
        /// </summary>
        PUSH_CONST

        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        //
    }
}