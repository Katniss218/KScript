
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
        /// _ -- Exits the program.
        /// </summary>
        EXIT = 0,

        // External call - it needs the name and operands compatible with reflection to call the given method.
        EXTERN_CALL,

        /// <summary>
        ///  _ [ptr] -- Moves the program counter to the position specified by the constant.
        /// </summary>
        GOTO,

        /// <summary>
        /// _ [ptr] -- Moves the current operation to the position specified by the constant, if the value at the top of the stack is zero.
        /// </summary>
        GOTO_IF_ZERO_I32,

        /// <summary>
        /// _ [ptr] -- Moves the current operation to the position specified by the constant, if the value at the top of the stack is zero.
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
        /// _ -- pushes a new stack frame onto the stack. -> Pushes the caller frame base pointer onto the stack, increments the stack pointer.
        /// </summary>
        PUSH_STACK_FRAME,
        /// <summary>
        /// _ -- pops the stack frame from the stack. -> Restores the stack frame pointer to the previous stack frame pointer, resets the stack pointer.
        /// </summary>
        POP_STACK_FRAME,

        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // Arithmetic

        /// <summary>
        /// _ -- Pops the 2 top-most values on the stack, performs {0} + {1}, and pushes the result on top of the stack.
        /// </summary>
        ADD_I32,

        /// <summary>
        /// _ -- Pops the 2 top-most values on the stack, performs {0} - {1}, and pushes the result on top of the stack.
        /// </summary>
        SUBTRACT_I32,

        /// <summary>
        /// _ -- Pops the 2 top-most values on the stack, performs {0} * {1}, and pushes the result on top of the stack.
        /// </summary>
        MULTIPLY_I32,

        /// <summary>
        /// _ -- Pops the 2 top-most values on the stack, performs {0} / {1}, and pushes the result on top of the stack.
        /// </summary>
        DIVIDE_I32,

        /// <summary>
        /// _ -- Pops the 2 top-most values on the stack, performs {0} mod {1}, and pushes the result on top of the stack.
        /// </summary>
        MODULO_I32,

        /// <summary>
        /// _ -- Pops the 2 top-most values on the stack, performs {0} + {1}, and pushes the result on top of the stack.
        /// </summary>
        ADD_F32,

        /// <summary>
        /// _ -- Pops the 2 top-most values on the stack, performs {0} - {1}, and pushes the result on top of the stack.
        /// </summary>
        SUBTRACT_F32,

        /// <summary>
        /// _ -- Pops the 2 top-most values on the stack, performs {0} * {1}, and pushes the result on top of the stack.
        /// </summary>
        MULTIPLY_F32,

        /// <summary>
        /// _ -- Pops the 2 top-most values on the stack, performs {0} / {1}, and pushes the result on top of the stack.
        /// </summary>
        DIVIDE_F32,

        /// <summary>
        /// _ -- Pops the 2 top-most values on the stack, performs {0} mod {1}, and pushes the result on top of the stack.
        /// </summary>
        MODULO_F32,

        /// <summary>
        /// _ [ptr] -- Pushes the value of the stack element {0} to the top of the stack, increments the stack pointer
        /// </summary>
        PUSH,
        /// <summary>
        /// _ [int32|float32] -- pushes a constant on top of the stack, increments the stack pointer
        /// </summary>
        PUSH_CONST
    }
}