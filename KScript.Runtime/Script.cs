using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KScript.Runtime
{
    public sealed class Script
    {
        // Uses arbitrary stack locations (stored as offset from the stack frame base pointer, which ensures that the variables inside functions always have the same locations)
        // This method can be more performant, which is important, as well as enable the compiler to perform more advanced optimization techniques, because it can use the entire stack frame.
        /*  Architecture:
        
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

        /// <summary>
        /// Represents the memory assigned to this script. This is used to store variables and objects.
        /// </summary>
        public StackElement[] Stack { get; private set; }

        /// <summary>
        /// Points to the index above the top-most element. Also equal to the the number of elements in the stack (its height).
        /// </summary>
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
        /// This is used to store the instructions to execute.
        /// </summary>
        public (OpCode opCode, StackElement[] data)[] Program { get; private set; }

        /// <summary>
        /// The instruction pointer.
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

            this.Program = op;

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
        public object Owner { get; set; }

        /// <summary>
        /// Runs a script
        /// </summary>
        public void Run()
        {
            while( Current < Program.Length )
            {
                (OpCode opCode, StackElement[] operands) = Program[Current];

                OperationCounter++;

                if( MaxOperations.HasValue && OperationCounter > MaxOperations.Value )
                {
                    throw new Exception( "Exceeded the maximum operation count." );
                }

                switch( opCode )
                {
                    // Int32 arithmetic.

                    case OpCode.ADD_I32:

                        this.AddInt32();
                        break;

                    case OpCode.SUBTRACT_I32:

                        this.SubtractInt32();
                        break;

                    case OpCode.MULTIPLY_I32:

                        this.MultiplyInt32();
                        break;

                    case OpCode.DIVIDE_I32:

                        this.DivideInt32();
                        break;

                    case OpCode.MODULO_I32:

                        this.ModuloInt32();
                        break;

                    // Float32 arithmetic.

                    case OpCode.ADD_F32:

                        this.AddFloat32();
                        break;

                    case OpCode.SUBTRACT_F32:

                        this.SubtractFloat32();
                        break;

                    case OpCode.MULTIPLY_F32:

                        this.MultiplyFloat32();
                        break;

                    case OpCode.DIVIDE_F32:

                        this.DivideFloat32();
                        break;

                    case OpCode.MODULO_F32:

                        this.ModuloFloat32();
                        break;

                    case OpCode.PUSH:

                        Stack[StackPointer++] = Stack[operands[0].Ptr];
                        break;
                    case OpCode.PUSH_CONST:

                        Stack[StackPointer++] = operands[0];
                        break;

                    // Control flow

                    // don't auto-increment the program counter if the condition is met.

                    case OpCode.GOTO_IF_ZERO_I32:
                        if( Stack[StackPointer - 1].Int32 == 0 )
                        {
                            Current = operands[0].Ptr;
                            continue;
                        }
                        break;

                    case OpCode.GOTO_IF_ZERO_F32:
                        if( Stack[StackPointer - 1].Float32 == 0 )
                        {
                            Current = operands[0].Ptr;
                            continue;
                        }
                        break;

                    case OpCode.GOTO:
                        Current = operands[0].Ptr;
                        continue;

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

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        void AddInt32()
        {
            this.Stack[this.StackPointer - 2].Int32 += this.Stack[--this.StackPointer].Int32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        void SubtractInt32()
        {
            this.Stack[this.StackPointer - 2].Int32 -= this.Stack[--this.StackPointer].Int32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        void MultiplyInt32()
        {
            this.Stack[this.StackPointer - 2].Int32 *= this.Stack[--this.StackPointer].Int32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        void DivideInt32()
        {
            this.Stack[this.StackPointer - 2].Int32 /= this.Stack[--this.StackPointer].Int32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        void ModuloInt32()
        {
            this.Stack[this.StackPointer - 2].Int32 %= this.Stack[--this.StackPointer].Int32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
        }


        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        void AddFloat32()
        {
            this.Stack[this.StackPointer - 2].Float32 += this.Stack[--this.StackPointer].Float32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        void SubtractFloat32()
        {
            this.Stack[this.StackPointer - 2].Float32 -= this.Stack[--this.StackPointer].Float32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        void MultiplyFloat32()
        {
            this.Stack[this.StackPointer - 2].Float32 *= this.Stack[--this.StackPointer].Float32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        void DivideFloat32()
        {
            this.Stack[this.StackPointer - 2].Float32 /= this.Stack[--this.StackPointer].Float32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        void ModuloFloat32()
        {
            this.Stack[this.StackPointer - 2].Float32 %= this.Stack[--this.StackPointer].Float32; // no need to set the top value to 0 since it's gonna get overwritten anyway.
        }
    }
}