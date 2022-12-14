using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KScript.Runtime
{
    public class Script
    {
        // Uses arbitrary stack locations (stored as offset from the stack frame base pointer, which ensures that the variables inside functions always have the same locations)
        // This method can be more performant, which is important, as well as enable the compiler to perform more advanced optimization techniques, because it can use the entire stack frame.

        /// <summary>
        /// Represents the memory assigned to this script. This part is used to store variables and objects.
        /// </summary>
        public StackElement[] Stack { get; private set; }

        /// <summary>
        /// Points to the top of the stack.
        /// </summary>
        /// <remarks>
        /// Points to the index above the top-most element. Also equal to the the number of elements in the stack (its height).
        /// </remarks>
        public int StackPointer { get; private set; }

        /// <summary>
        /// Points to the base of the current stack frame (which should contain the pointer to the stack frame below it, and so on).
        /// </summary>
        public int CurrentStackFramePointer { get; private set; }

        /// <summary>
        /// A list used to store the return values of the most recently returned function.
        /// </summary>
        public Stack<StackElement> ReturnStack { get; set; }


        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=


        /// <summary>
        /// Instructions
        /// </summary>
        public (OpCode opCode, StackElement[] data)[] Op { get; private set; }

        /// <summary>
        /// Instruction pointer
        /// </summary>
        public int Current { get; private set; }


        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=


        /// <summary>
        /// The number of operations that have been performed in the lifetime of this script.
        /// </summary>
        public long OperationCounter { get; private set; }

        /// <summary>
        /// The maximum number of operations allowed in the lifetime of this script.
        /// </summary>
        public long? MaxOperations { get; private set; }



        public Script( int stackSize, (OpCode opCode, StackElement[] data)[] op )
        {
            this.Stack = new StackElement[stackSize];
            this.StackPointer = 0;
            this.CurrentStackFramePointer = 0;

            this.Op = op;

            this.ReturnStack = new Stack<StackElement>();
        }



        /// <summary>
        /// Pushes 'a' onto the stack, increments the stack pointer.
        /// </summary>
        public void Push( StackElement a )
        {
            this.Stack[StackPointer++] = a;
        }

        /// <summary>
        /// Pops the top-most variable from the stack, decrements the stack pointer.
        /// </summary>
        public void Pop()
        {
            // Do not reset the value, no need.
            this.StackPointer--;
        }

        /// <summary>
        /// The object that this script is attached to. I.e. a unit, a building, or whatever else.
        /// </summary>
        /// <remarks>
        /// Will be used for external calls and bindings against fields, and more.
        /// </remarks>
        //public object Owner { get; set; }

        /// <summary>
        /// Runs a script
        /// </summary>
        public void Run()
        {
            while( Current < Op.Length )
            {
                var (opCode, operands) = Op[Current];

                OperationCounter++;

                if( MaxOperations != null && OperationCounter > MaxOperations.Value )
                {
                    throw new Exception( "Exceeded the maximum operation count." );
                }

                switch( opCode )
                {
                    // Addition

                    case OpCode.ADD_I32:

                        Stack[StackPointer - 2].Int32 += Stack[--StackPointer].Int32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
                        break;
                    /*case OpCode.ADD_I32_CONST:

                        Stack[CurrentStackFramePointer + operands[0].Ptr].Int32 += operands[1].Int32;
                        break;*/

                    // Subtraction

                    case OpCode.SUBTRACT_I32:

                        Stack[StackPointer - 2].Int32 -= Stack[--StackPointer].Int32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
                        break;
                    /* case OpCode.SUBTRACT_I32_CONST:

                         Stack[CurrentStackFramePointer + operands[0].Ptr].Int32 -= operands[1].Int32;
                         break;*/

                    // Multiplication

                    case OpCode.MULTIPLY_I32:

                        Stack[StackPointer - 2].Int32 *= Stack[--StackPointer].Int32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
                        break;
                    /*case OpCode.MULTIPLY_I32_CONST:

                        Stack[CurrentStackFramePointer + operands[0].Ptr].Int32 *= operands[1].Int32;
                        break;*/

                    // Division

                    case OpCode.DIVIDE_I32:

                        Stack[StackPointer - 2].Int32 /= Stack[--StackPointer].Int32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
                        break;
                    /*case OpCode.DIVIDE_I32_CONST:

                        Stack[CurrentStackFramePointer + operands[0].Ptr].Int32 /= operands[1].Int32;
                        break;*/

                    // Modular division

                    case OpCode.MODULO_I32:

                        Stack[StackPointer - 2].Int32 %= Stack[--StackPointer].Int32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
                        break;
                    /*case OpCode.MODULO_I32_CONST:

                        Stack[CurrentStackFramePointer + operands[0].Ptr].Int32 %= operands[1].Int32;
                        break;*/

                    // Addition

                    case OpCode.ADD_F32:

                        Stack[StackPointer - 2].Float32 += Stack[--StackPointer].Float32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
                        break;
                    /*case OpCode.ADD_F32_CONST:

                        Stack[CurrentStackFramePointer + operands[0].Ptr].Float32 += operands[1].Float32;
                        break;*/

                    // Subtraction

                    case OpCode.SUBTRACT_F32:

                        Stack[StackPointer - 2].Float32 -= Stack[--StackPointer].Float32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
                        break;
                    /*case OpCode.SUBTRACT_F32_CONST:

                        Stack[CurrentStackFramePointer + operands[0].Ptr].Float32 -= operands[1].Float32;
                        break;*/

                    // Multiplication

                    case OpCode.MULTIPLY_F32:

                        Stack[StackPointer - 2].Float32 *= Stack[--StackPointer].Float32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
                        break;
                    /*case OpCode.MULTIPLY_F32_CONST:

                        Stack[CurrentStackFramePointer + operands[0].Ptr].Float32 *= operands[1].Float32;
                        break;*/

                    // Division

                    case OpCode.DIVIDE_F32:

                        Stack[StackPointer - 2].Float32 /= Stack[--StackPointer].Float32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
                        break;
                    /*case OpCode.DIVIDE_F32_CONST:

                        Stack[CurrentStackFramePointer + operands[0].Ptr].Float32 /= operands[1].Float32;
                        break;*/

                    // Modular division

                    case OpCode.MODULO_F32:

                        Stack[StackPointer - 2].Float32 %= Stack[--StackPointer].Float32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
                        break;
                    /*case OpCode.MODULO_F32_CONST:

                        Stack[CurrentStackFramePointer + operands[0].Ptr].Float32 %= operands[1].Float32;
                        break;*/

                    // Load

                    /*case OpCode.SET:

                        Stack[CurrentStackFramePointer + operands[0].Ptr] = Stack[CurrentStackFramePointer + operands[1].Ptr];
                        break;
                    case OpCode.SET_CONST:

                        Stack[CurrentStackFramePointer + operands[0].Ptr] = operands[1];
                        break;*/

                    case OpCode.PUSH:

                        Stack[StackPointer++] = Stack[operands[0].Ptr];
                        break;
                    case OpCode.PUSH_CONST:

                        Stack[StackPointer++] = operands[0];
                        break;

                    // Control flow statements

                    case OpCode.GOTO_IF_ZERO_I32:
                        if( Stack[StackPointer - 1].Int32 == 0 )
                        {
                            Current = operands[0].Ptr;
                            continue; // don't auto-increment the current
                        }
                        break;

                    case OpCode.GOTO_IF_ZERO_F32:
                        if( Stack[StackPointer - 1].Float32 == 0 )
                        {
                            Current = operands[0].Ptr;
                            continue; // don't auto-increment the current
                        }
                        break;

                    case OpCode.GOTO:

                        Current = operands[0].Ptr;
                        continue; // don't auto-increment the current

                    // stack frame / function calls

                    case OpCode.PUSH_STACK_FRAME:

                        this.Stack[this.StackPointer] = this.CurrentStackFramePointer;
                        this.CurrentStackFramePointer = this.StackPointer++;
                        break;

                    case OpCode.POP_STACK_FRAME:

                        // Doing it 'in reverse' allows to skip a temporary variable.
                        this.StackPointer = this.CurrentStackFramePointer; // pop the frame
                        this.CurrentStackFramePointer = this.Stack[this.CurrentStackFramePointer].Ptr; // restore the previous frame's frame pointer.
                        break;

                    case OpCode.PUSH_RET:

                        ReturnStack.Push( Stack[CurrentStackFramePointer + operands[0].Ptr] );
                        break;

                    case OpCode.POP_RET:

                        Stack[CurrentStackFramePointer + operands[0].Ptr] = ReturnStack.Pop();
                        break;

                    case OpCode.EXIT:

                        return;

                }

                Current++;
            }
        }

        /*
        
        stack_pointer
        stack
        {
            stack_frames
            [
                {
                    caller_frame_base_pointer
                    locals, parameters, etc* - dynamic size
                }
            ]
        }
        return_stack* - dynamic size

        */
    }
}